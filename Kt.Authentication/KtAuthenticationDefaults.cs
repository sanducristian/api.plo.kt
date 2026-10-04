namespace Kt.Authentication;

/// <summary>
/// Provides default values for authentication scheme and policy used in the KT authentication system.
/// </summary>
public static class KtAuthenticationDefaults {

    /// <summary>
    /// The default authentication scheme used for KT authentication.
    /// </summary>
    public const string Scheme = "KtSession";


    /// <summary>
    /// The default authorization policy name used for KT authentication.
    /// </summary>
    public const string Policy = "Kt.Authenticated";
}
