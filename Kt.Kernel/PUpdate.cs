namespace Kt.Kernel;


public static class KtUpdate {
    /// <summary>
    /// Get the lastest version of the software
    /// </summary>
    /// <returns></returns>
    public static string GetUpdateRecord() {
        return "";
        //return Kt.Kernel.Environment.KtSystemNetwork.DownloadFile("https://kt.live/appversions.xml");
        //			WebClient Client = new WebClient();
        //			Client.DownloadFile("http://i.stackoverflow.com/Content/Img/stackoverflow-logo-250.png", @"C:\folder\stackoverflowlogo.png");
    }
}

