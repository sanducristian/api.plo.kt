using Kt.Data;
using Kt.Data.Objects;
using Kt.Data.Repositories;
using MySqlConnector;
using Kt.Data.Authorization.Objects;

// Read-only checks against an explicitly configured test database. No credentials are logged.
static string Required(string key) => Environment.GetEnvironmentVariable(key)
    ?? throw new InvalidOperationException($"Set {key} before running the checks.");
static void Check(bool condition, string message) {
    if (!condition) throw new InvalidOperationException(message);
}
var connection = new MySqlConnectionStringBuilder(Required("KT_TEST_CONNECTION")) { MaximumPoolSize = 1 };
var actorA = ulong.Parse(Required("KT_TEST_ACTOR_A"));
var actorB = ulong.Parse(Required("KT_TEST_ACTOR_B"));
Check(actorA != actorB, "Use distinct ACTIVE USER principals.");
var workspace = ulong.Parse(Required("KT_TEST_WORKSPACE"));
var deniedWorkspace = ulong.Parse(Required("KT_TEST_DENIED_WORKSPACE"));
Check(workspace != deniedWorkspace, "Use distinct allowed and denied workspaces.");
var permission = Required("KT_TEST_PERMISSION");
await using var db = new KtDb(connection.ConnectionString);
var identity = new KtIdentityRepository();
var workspaces = new KtWorkspaceRepository();
foreach (var actor in new[] { actorA, actorB, actorA, actorB }) {
    await using var session = await db.OpenAsync(actor);
    Check((await identity.GetCurrentAsync(session))?.Id == actor, "Pooled actor context mismatch.");
}
await using (var session = await db.OpenAsync(actorA)) {
    Check(await workspaces.HasPermissionAsync(session, workspace, permission), "Expected positive grant missing.");
    Check(!await workspaces.HasPermissionAsync(session, deniedWorkspace, permission), "Cross-workspace grant leak.");
    var objects = new KtObjectRepository(db, new DenyObjects(), KtObjectTable.KnownTables);
    try {
        await objects.GetStaticAsync(session, "sys_currency", 1);
        throw new InvalidOperationException("Deny policy was bypassed.");
    }
    catch (UnauthorizedAccessException) { }
}
// Validate transaction failure/disposal does not strand the sole pool connection.
try {
    await db.InTransactionAsync<int>(actorA, (_, _) => throw new DeliberateFailure());
}
catch (DeliberateFailure) { }
await using (var session = await db.OpenAsync(actorB))
    Check((await identity.GetCurrentAsync(session))?.Id == actorB, "Session recovery after failure failed.");
Console.WriteLine("PASS: pooled actors, permission isolation, deny policy and failed-transaction disposal.");
Console.WriteLine("Write rollback, singleton races and invoice concurrency still require the documented fixture checks.");
sealed class DeliberateFailure : Exception { }
sealed class DenyObjects : IKtObjectAccessPolicy {
    public Task<bool> CanReadAsync(KtDbSession s, string t, ulong id, CancellationToken ct) => Task.FromResult(false);
    public Task<bool> CanCreatePropertyAsync(KtDbSession s, ulong m, ulong r, string t, CancellationToken ct) => Task.FromResult(false);
}
