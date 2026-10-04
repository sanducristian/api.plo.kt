namespace Kt.Data.Authorization.Objects;

/// <summary>
/// Trusted configuration connecting a location metadata role and physical
/// value table to the permissions required to read or create that property.
/// </summary>
public sealed record KtLocationPropertyRule(
    ulong RoleId,
    string ValueTable,
    string ReadPermission,
    string WritePermission
   );