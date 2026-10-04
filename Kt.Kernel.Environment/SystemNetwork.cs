using System.Net;
using System.Net.NetworkInformation;
using System.Security.Principal;


namespace Kt.Kernel.Environment;


public class KtSystemNetwork {

    /// <summary>
    /// Get the current windows user (that is running the application that is)
    /// </summary>
    /// <returns></returns>
    public static string GetCurrentWindowsUser() {
        WindowsPrincipal mWp = Thread.CurrentPrincipal as WindowsPrincipal;
        //Environment.UserName;
        //Context.User.Identity.Name;
        return mWp.Identity.Name;
    }



    /// <summary>
    /// Get Fully Qualified Domain Name
    /// </summary>
    /// <returns></returns>
    public static string GetFQDN() {
        string domainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
        string hostName = Dns.GetHostName();
        string fqdn = "";
        if (!hostName.Contains(domainName)) {
            fqdn = hostName + "." + domainName;
        } else {
            fqdn = hostName;
        }

        return fqdn;
    }



    /// <summary>
    /// Download the specified file
    /// </summary>
    /// <param name="PathToFile">The file to download</param>
    /// <returns></returns>
    public static string DownloadFile(string PathToFile) {
        return new System.Net.WebClient().DownloadString(PathToFile);
    }


    /// <summary>
    /// Get the list of network interfaces
    /// </summary>
    /// <returns></returns>
    public static string GetNetworkInterfacesDetailed() {
        IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
        string sData = "";
        sData = "<interfaces hostname=\"" + computerProperties.HostName + "\" domain=\"" + computerProperties.DomainName + "\">\n";
        foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces()) {
            IPInterfaceProperties properties = ni.GetIPProperties();

            sData += "<interface name=\"" + ni.Name + "\" type=\"" + ni.NetworkInterfaceType.ToString() + "\">\n";
            sData += "<description>" + ni.Description.ToString() + "</description>\n";
            sData += "<type>" + ni.NetworkInterfaceType.ToString() + "</type>\n";
            sData += "<speed>" + ni.Speed.ToString() + "</speed>\n";
            sData += "<op>" + ni.OperationalStatus.ToString() + "</op>\n";
            foreach (UnicastIPAddressInformation ip in properties.UnicastAddresses) {
                sData += "<ip type=\"" + ip.Address.AddressFamily.ToString() + "\">" + ip.Address.ToString() + "</ip>\n";
            }
            foreach (UnicastIPAddressInformation ip in properties.AnycastAddresses) {
                sData += "<ipanycast type=\"" + ip.Address.AddressFamily.ToString() + "\">" + ip.Address.ToString() + "</ip>\n";
            }
            foreach (IPAddress dns in properties.DhcpServerAddresses) {
                sData += "<dhcp>" + dns.ToString() + "</dhcp>\n";
            }
            foreach (IPAddress dns in properties.WinsServersAddresses) {
                sData += "<wins>" + dns.ToString() + "</wins>\n";
            }
            foreach (IPAddress dns in properties.DnsAddresses) {
                sData += "<dns>" + dns.ToString() + "</dns>\n";
            }
            sData += "<dnsSufix>" + properties.DnsSuffix.ToString() + "</dnsSufix>\n";
            sData += "<dnsEnabled>" + properties.IsDnsEnabled.ToString() + "</dnsEnabled>\n";
            sData += "<rxOnly>" + properties.IsDynamicDnsEnabled.ToString() + "</rxOnly>\n";
            sData += "<multicast>" + ni.IsReceiveOnly.ToString() + "</multicast>\n";
            sData += "<dnsSufix>" + ni.SupportsMulticast.ToString() + "</dnsSufix>\n";
            sData += "<dnsSufix>" + properties.DnsSuffix.ToString() + "</dnsSufix>\n";
            sData += "</interface>\n";
        }
        sData += "</interfaces>\n";
        return sData;
    }

}

