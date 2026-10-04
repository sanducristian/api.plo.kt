using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kt.Kernel;


/// <summary>
/// Service descriptor class
/// </summary>
public class ServiceDescriptor {

    // Example of service descriptor
    // 		
    // #descriptor#1#
    // SERVER
    // STORAGESERVER
    // The storageser that will hold several services
    // 192.168.0.2;
    // mysql:3306;files:9390;faxes:9291;


    /// <summary>
    /// Get or set the descriptor name
    /// </summary>
    public string Name { get; set; } = "";



    /// <summary>
    /// Get or set the description
    /// </summary>
    public string Description { get; set; } = "";



    /// <summary>
    /// Get or set the class name
    /// </summary>
    public string Class { get; set; } = "";




    /// <summary>
    /// Get or set the IP Addresses
    /// </summary>
    public string[] Addresses { get; set; } = new string[0];



    /// <summary>
    /// Get or set the services
    /// </summary>
    public string[] Services { get; set; } = new string[0];




    /// <summary>
    /// Pack the class data into the specified packet
    /// </summary>
    /// <returns></returns>
    public byte[] Pack() {
        string sDataOut = "";
        sDataOut += "#descriptor#1#" + "\r\n";
        sDataOut += Class + "\r\n";
        sDataOut += Name + "\r\n";
        sDataOut += Description + "\r\n";
        sDataOut += string.Join(";", Addresses) + "\r\n";
        sDataOut += string.Join(";", Services) + "\r\n";
        return Encoding.ASCII.GetBytes(sDataOut);
    }



    /// <summary>
    /// Unpack the received data in order to extract the service descriptor
    /// </summary>
    /// <param name="ReceivedData">The received data to be extracted</param>
    public void UnPack(string ReceivedData) {
        if (ReceivedData == null)
            throw new KtException(KtExceptionEnum.DataNull);
        if (ReceivedData.Length == 0)
            throw new KtException   (KtExceptionEnum.DataEmpty);
        string[] sData = ReceivedData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

        if (sData.Length == 0)
            throw new KtException(KtExceptionEnum.DataEmpty);
        if (sData.Length != 7)
            throw new KtException(KtExceptionEnum.DataSizeInconsistent);
        if (sData[0] != "#descriptor#1#")
            throw new KtException(KtExceptionEnum.DataInvalidSignature); // Exception("Invalid data packet (invalid signature)");

        Class = sData[1];
        Name = sData[2];
        Description = sData[3];
        Addresses = sData[4].Split(new char[] { ';' });
        Services = sData[5].Split(new char[] { ';' });
    }


    /// <summary>
    /// Try to locate a service and return its descriptor
    /// </summary>
    /// <param name="ServiceName">The service name to look for</param>
    /// <returns>The string containing the service or empty if service was not found</returns>
    public string GetService(string ServiceName) {
        if (Services == null)
            return string.Empty;
        if (Services.Length == 0)
            return string.Empty;
        string[] sName;
        char[] cSep = new char[] { ';' };
        foreach (string service in Services) {
            sName = service.Split(cSep);
            if (sName != null) {
                if (sName.Length > 0) {
                    if (sName[0] == ServiceName) {
                        return sName[1];
                    }
                }
            }
        }

        return String.Empty;
    }
}

