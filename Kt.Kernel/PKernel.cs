namespace Kt.Kernel;


/// <summary>
/// Store the main application data that can be distributed to all the other modules in the application
/// </summary>
[Serializable]
public class KtKernelText {


    /// <summary>
    /// Store the application long name
    /// </summary>
    public static string AppName = "Katrix.AI";


    /// <summary>
    /// Store the application short name
    /// </summary>
    public static string AppShortName = "KT-PLO";


    /// <summary>
    /// Get the company name that manufactured the software
    /// </summary>
    public static string CompanyName { get { return "Katrix.AI"; } }
}

