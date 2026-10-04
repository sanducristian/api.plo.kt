using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace Kt.Kernel;


[Serializable]
[ComVisibleAttribute(true)]
public class PException2 : Exception, ISerializable {

    /// <summary>
    /// Store the enumeration for the exception
    /// </summary>
    protected KtExceptionEnum mvarEnum = KtExceptionEnum.Unknown;


    /// <summary>
    /// Store the inner QbException
    /// </summary>
    protected PException2 mobjInnerException = null;


    /// <summary>
    /// Store the information for the exception
    /// </summary>
    protected string mvarInfo = "";



    /// <summary>
    /// Store the extra parameters
    /// </summary>
    protected object[] mvarParameters = null;




    public PException2(string MsgText) : base(MsgText) {

    }


    public PException2(string MsgText, Exception innerException)
        : base(MsgText, innerException) {
        if (innerException is PException2)
            mobjInnerException = innerException as PException2;
    }


    public PException2(string MsgText, PException2 InnerException)
        : base(MsgText, InnerException) {
        this.mobjInnerException = InnerException;
    }




    public PException2(KtExceptionEnum ExpId) {
        mvarEnum = ExpId;
    }

    public PException2(KtExceptionEnum ExpId, string Param1) {
        mvarEnum = ExpId;
        mvarInfo = "Parameter [string]: " + Param1;
        mvarParameters = new object[1] { Param1 };
    }

    public PException2(KtExceptionEnum ExpId, uint Param1) {
        mvarEnum = ExpId;
        mvarInfo = "Parameter [uint]: " + Param1.ToString();
        mvarParameters = new object[1] { Param1 };
    }

    public PException2(KtExceptionEnum ExpId, Exception Exp)
        : base("", Exp) {
        if (Exp is PException2)
            mobjInnerException = Exp as PException2;
        mvarEnum = ExpId;
    }
    public PException2(KtExceptionEnum ExpId, Exception Exp, string sParameter)
        : base("", Exp) {
        if (Exp is PException2)
            mobjInnerException = Exp as PException2;
        mvarEnum = ExpId;
        mvarInfo = "Parameter [string]: " + sParameter;
        mvarParameters = new object[1] { sParameter };
    }
    public PException2(KtExceptionEnum ExpId, Exception Exp, UInt64 sParameter)
        : base("", Exp) {
        if (Exp is PException2)
            mobjInnerException = Exp as PException2;
        mvarEnum = ExpId;
        mvarInfo = "Parameter [UInt64]: " + sParameter.ToString();
        mvarParameters = new object[1] { sParameter };
    }
    public PException2(KtExceptionEnum ExpId, Exception Exp, params object[] parameters)
        : base("", Exp) {
        if (Exp is PException2)
            mobjInnerException = Exp as PException2;
        mvarEnum = ExpId;
        mvarParameters = parameters;
    }

    public PException2(KtExceptionEnum ExpId, PException2 Exp)
        : base("", Exp) {
        mobjInnerException = Exp;
        mvarEnum = ExpId;
    }
    public PException2(KtExceptionEnum ExpId, PException2 Exp, string sParameter)
        : base("", Exp) {
        mobjInnerException = Exp;
        mvarEnum = ExpId;
        mvarInfo = "Parameter [string]: " + sParameter;
        mvarParameters = new object[1] { sParameter };
    }
    public PException2(KtExceptionEnum ExpId, PException2 Exp, UInt64 sParameter)
        : base("", Exp) {
        mobjInnerException = Exp;
        mvarEnum = ExpId;
        mvarInfo = "Parameter [UInt64]: " + sParameter.ToString();
        mvarParameters = new object[1] { sParameter };
    }
    public PException2(KtExceptionEnum ExpId, PException2 Exp, params object[] parameters)
        : base("", Exp) {
        mobjInnerException = Exp;
        mvarEnum = ExpId;
        mvarParameters = parameters;
    }

    /// <summary>
    /// Deserialization constructor 
    /// </summary>
    /// <param name="info"><see cref="System.Runtime.Serialization.SerializationInfo"/> for this constructor</param>
    /// <param name="context"><see cref="StreamingContext"/> for this constructor</param>
    protected PException2(SerializationInfo info, StreamingContext context)
        : base(info, context) {
    }



    //
    // Summary:
    //     Gets the System.Exception instance that caused the current exception.
    //
    // Returns:
    //     An instance of Exception that describes the error that caused the current
    //     exception. The InnerException property returns the same value as was passed
    //     into the constructor, or a null reference (Nothing in Visual Basic) if the
    //     inner exception value was not supplied to the constructor. This property
    //     is read-only.
    /// <summary>
    /// Gets the System.Exception instance that caused the current exception.
    /// </summary>
    public new Exception InnerException {
        get {
            if (mobjInnerException != null)
                return mobjInnerException;
            return base.InnerException;
        }
    }





    /// <summary>
    /// Return the text associated with the exception
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        return GetFullText;

        //string sText = "QbException (" + mvarEnum.ToString() + ")\r\n";
        //sText += (mvarInfo.Length != 0) ? this.mvarInfo + "\r\n" : "";
        //sText += (mobjInnerException != null) ? mobjInnerException.ToString() : base.ToString();
        //return sText + "\r\n";

        //return "QbException (" + mvarEnum.ToString() + ")\n" + mvarInfo + "\n" + base.ToString();
    }


    /// <summary>
    /// Get the info text
    /// </summary>
    public virtual string Info { get { return mvarInfo; } }



    /// <summary>
    /// Get the parameters
    /// </summary>
    public virtual object[] Parameters { get { return mvarParameters; } }


    /// <summary>
    /// Get the message for the exception
    /// </summary>
    public override string Message { get { return mvarInfo + ((base.Message.Length == 0) ? "" : "\n" + base.Message); } }


    /// <summary>
    /// Get the type of the exception
    /// </summary>
    public KtExceptionEnum Type { get { return mvarEnum; } }


    /// <summary>
    /// Get a short text for the exception
    /// </summary>
    /// <returns></returns>
    public virtual string NicePrint() {
        string sParams = "";
        if (this.mvarEnum != 0) {
            if (mvarParameters != null) {
                sParams = "\nwith parameter: " + mvarParameters[0].ToString();
            }
            return "Error: " + KtExceptionText.GetText(mvarEnum) + sParams;
        } else {
            return "An unknwon error occured.\n For more details please contact the administrator";
        }
    }



    /// <summary>
    /// Get the full text of the error
    /// </summary>
    public virtual string GetFullText {
        get {
            string evString = "Exception " + mvarEnum.ToString() + " in '" + base.Source + "' at '" + base.TargetSite + "'";
            if (mvarParameters != null) {
                evString += "\r\nParams[" + mvarParameters.Length + "] -> ";
                foreach (var item in mvarParameters) {
                    evString += "\r\n ";
                    evString += item.GetType().ToString();
                    evString += " := ";
                    evString += item.ToString();
                }
            }
            evString += "\r\nData: " + base.Data;
            evString += "\r\nMessage: " + base.ToString();
            if (mvarInfo != "")
                evString += "\r\nInfo: " + mvarInfo;
            evString += "\r\nStack trace: \r\n" + base.StackTrace;
            return evString + "\r\n";
        }
    }
}

