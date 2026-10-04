namespace Kt.Kernel.Network;

using System;
using System.Net;
using System.Net.NetworkInformation;


/// <summary>
/// Provides network-related information and utilities for the KT system.
/// </summary>
public static class KtSystemNetworkInfo {


    /// <summary>
    /// Get Fully Qualified Domain Name (Cross-platform)
    /// </summary>
    /// <returns></returns>
    public static string GetFQDN() {
        try {
            string hostName = Dns.GetHostName();
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            string domainName = ipProperties.DomainName;

            // If the hostname already contains the domain, or domain is empty, return hostname
            if (string.IsNullOrEmpty(domainName) || hostName.EndsWith("." + domainName, StringComparison.OrdinalIgnoreCase)) {
                // On some Linux distributions, Dns.GetHostName() only returns the short name.
                // We can attempt a full DNS lookup to see if a canonical name exists.
                try {
                    var entry = Dns.GetHostEntry(hostName);
                    if (!string.IsNullOrEmpty(entry.HostName) && entry.HostName.Contains('.')) {
                        return entry.HostName;
                    }
                }
                catch {
                    // Fall back if DNS lookup fails
                }

                return hostName;
            }

            return $"{hostName}.{domainName}";
        }
        catch {
            // Ultimate fallback if network queries throw
            return Environment.MachineName;
        }
    }
}