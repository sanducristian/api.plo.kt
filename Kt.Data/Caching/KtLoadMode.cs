namespace Kt.Data.Load;


/// <summary>
/// Specifies the load mode for data in the KT system.
/// </summary>
public enum KtLoadMode {
    /// <summary>
    /// Load data during system startup.
    /// </summary>
    Startup,

    /// <summary>
    /// Load data when a module is entered.
    /// </summary>
    ModuleEntry,

    /// <summary>
    /// Load data on demand.
    /// </summary>
    OnDemand
}