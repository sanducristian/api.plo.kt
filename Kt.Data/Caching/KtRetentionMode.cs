namespace Kt.Data.Load;


/// <summary>
/// Specifies the retention mode for cached data in the KT system.
/// </summary>
public enum KtRetentionMode {
    /// <summary>
    /// No retention. Cached data is not retained.
    /// </summary>
    None,
    
    /// <summary>
    /// Expiring retention. Cached data is retained for a limited time.
    /// </summary>
    Expiring,

    /// <summary>
    /// Snapshot retention. Cached data is retained as a snapshot.
    /// </summary>
    Snapshot
}