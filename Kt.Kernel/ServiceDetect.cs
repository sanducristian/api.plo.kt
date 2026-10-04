using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Kt.Kernel;


/// <summary>
/// Detect using a UDP port all the services available on the network
/// </summary>
public class ServiceDetect {

    /// <summary>
    /// Store the socket used to connect to the servers
    /// </summary>
    protected Socket mvarSocket = null;


    /// <summary>
    /// Store the address used to send the broadcast to
    /// </summary>
    protected string mvarAddressToSend = "";


    /// <summary>
    /// Store the port used to send the requested data
    /// </summary>
    protected int mvarPortNo = 0;


    /// <summary>
    /// Specify the address to send to (the IP address)
    /// </summary>
    protected IPAddress mvarSendToAddress;


    /// <summary>
    /// Specify the sending end point (the IP & port number)
    /// </summary>
    protected IPEndPoint mvarSendingEndPoint;


    /// <summary>
    /// Specify the receiving end point
    /// </summary>
    protected IPEndPoint mvarRemoteIpEndPoint;




    /// <summary>
    /// Constructor for the PLO Service Detector
    /// </summary>
    /// <param name="AddressToSend">The IP address to send to</param>
    /// <param name="PortNo">The port number to send to</param>
    public ServiceDetect(string AddressToSend, int PortNo) {
        mvarAddressToSend = AddressToSend;
        mvarPortNo = PortNo;

        mvarRemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        mvarSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        mvarSendToAddress = IPAddress.Parse(mvarAddressToSend);
        mvarSendingEndPoint = new IPEndPoint(mvarSendToAddress, mvarPortNo);
    }


    ~ServiceDetect() {
        try {
            if (mvarSocket != null) {
                mvarSocket.Close();
                mvarSocket = null;
            }
        }
        catch (Exception ex) {
            Debug.WriteLine("ServiceDetect::~ServiceDetect-> Exception {0}", ex.ToString());
        }
    }



    /// <summary>
    /// Start the scanning
    /// </summary>
    /// <param name="RxTimeout">The time-out value, in milliseconds. The default value is 0, which indicates an infinite time-out period. Specifying -1 also indicates an infinite time-out period.</param>
    public void StartScan(string TextToSend, int RxTimeout) {
        byte[] txBuffer = Encoding.ASCII.GetBytes(TextToSend);
        byte[] rxBuffer = new byte[128];

        //////////////////////////////////////////////////////
        //
        // Send the packet 
        //
        try {
            mvarSocket.SendTo(txBuffer, mvarSendingEndPoint);
        }
        catch (Exception ex) {
            Debug.WriteLine("ServiceDetect::Scan-> Tx Exception {0}", ex.Message);
            return;             // We can exit because there is no way a server could answar to un unsend request
        }


        //////////////////////////////////////////////////////
        //
        // Wait for packet receive 
        //
        try {
            EndPoint mRemote = (EndPoint)(mvarRemoteIpEndPoint);
            mvarSocket.ReceiveTimeout = RxTimeout;
            mvarSocket.ReceiveFrom(rxBuffer, ref mRemote);
        }
        catch (Exception ex) {
            Debug.WriteLine("ServiceDetect::Scan-> Rx Exception {0}", ex.Message);
        }
    }
}

