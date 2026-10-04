using System.Runtime.InteropServices;
using System.Runtime.Serialization;


namespace Kt.Kernel;


[Serializable]
[ComVisibleAttribute(true)]
public class KtExNetHostNotFound : KtException, ISerializable {

    /// <summary>
    /// Store the server where the connection should be made
    /// </summary>
    public string Server { get; protected set; } = "";



    /// <summary>
    /// Initializes a new instance of the <see cref="KtExNetHostNotFound"/> class.
    /// </summary>
    /// <param name="server">The server where the connection should be made.</param>
    public KtExNetHostNotFound(string server) : base(KtExceptionEnum.AuthError) {
        Server = server;
    }

    
    /// <summary>
    /// Initializes a new instance of the <see cref="KtExNetHostNotFound"/> class.
    /// </summary>
    /// <param name="ex">The exception that caused the current exception.</param>
    /// <param name="server">The server where the connection should be made.</param>
    public KtExNetHostNotFound(Exception ex, string server) : base(KtExceptionEnum.AuthError, ex) {
        Server = server;
    }

}

