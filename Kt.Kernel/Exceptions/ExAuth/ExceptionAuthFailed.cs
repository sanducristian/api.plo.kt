using Kt.Kernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace KT.Kernel;


[Serializable]
[ComVisibleAttribute(true)]
public class KtExceptionAuthFailed : KtException, ISerializable {

    /// <summary>
    /// Store the username that failed the authentification
    /// </summary>
    public string Username { get; protected set; } = "";



    /// <summary>
    /// Store the server where the connection should be made
    /// </summary>
    public string Server { get; protected set; } = "";



    public KtExceptionAuthFailed(string username, string server) : base(KtExceptionEnum.AuthError) {
        Username = username;
        Server = server;
    }


    public KtExceptionAuthFailed(Exception ex, string username, string server) : base(KtExceptionEnum.AuthError, ex) {
        Username = username;
        Server = server;
    }
}

