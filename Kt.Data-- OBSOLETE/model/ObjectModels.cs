namespace Kt.Data.Objects;

public sealed record KtObjectIdentity(ulong Id, ulong TableId, string TableName, bool Invalid, ulong OriginalId, ulong MasterId, ulong RoleId, ulong? ActorId, DateTime LastUpdate);
public sealed record KtObjectRecord(string TableName, ulong Id, KtObjectIdentity? Identity, IReadOnlyDictionary<string, object?> Fields);
public sealed record KtObjectPage(IReadOnlyList<KtObjectRecord> Items, ulong LastScannedId);

/// <summary>Must resolve authoritative ownership and action permissions. No implicit master/role inheritance.</summary>
public interface IKtObjectAccessPolicy {
    // Called for metadata as well as payloads. tableName + id is essential for non-global static IDs.
    Task<bool> CanReadAsync(KtDbSession session, string tableName, ulong id, CancellationToken cancellationToken);
    // Must check the owner's scope, property role and chosen data table. Called before allocation.
    Task<bool> CanCreatePropertyAsync(KtDbSession session, ulong masterId, ulong roleId,
        string valueTable, CancellationToken cancellationToken);
}

// These shapes follow physical SQL types. Floating point is preserved, not silently made exact.
public abstract record KtObjectDynamicValue;
public sealed record KtObjectDynamicIntegerValue(long Value) : KtObjectDynamicValue;
public sealed record KtObjectDynamicDoubleValue(double? Value) : KtObjectDynamicValue;
public sealed record KtObjectDynamicTextValue(string? Value) : KtObjectDynamicValue;
public sealed record KtObjectDynamicLocalizedTextValue(ulong LanguageId, string? Value) : KtObjectDynamicValue;
public sealed record KtObjectDynamicAttributeValue(string Name, string Value) : KtObjectDynamicValue;
public sealed record KtObjectDynamicBlobValue(byte[]? Value) : KtObjectDynamicValue;
public sealed record KtObjectDynamicDateValue(DateOnly? Value) : KtObjectDynamicValue;
public sealed record KtObjectDynamicDateTimeValue(DateTime Value) : KtObjectDynamicValue;
public sealed record KtObjectDynamicTimeValue(TimeSpan Value) : KtObjectDynamicValue;
