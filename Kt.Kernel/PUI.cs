using System.Drawing;



namespace Kt.Kernel;



/// <summary>
/// Handles most of the userinterface common tasks
/// </summary>
public class PUI {


    /// <summary>
    /// Background colors
    /// </summary>
    public static class ColorsBk {
        public static Color msgException = Color.LightSalmon;
        public static Color msgError = Color.LightSalmon;
        public static Color msgWarning = Color.Gold;
        public static Color msgInformation = Color.Transparent;
        public static Color msgSuccess = Color.MediumSeaGreen;
        public static Color msgStatus = Color.Transparent;
        public static Color msgTrace = Color.Transparent;
    }


    /// <summary>
    /// Foreground colors
    /// </summary>
    public static class ColorsFk {
        public static Color msgException = Color.Black;
        public static Color msgError = Color.Black;
        public static Color msgWarning = Color.Black;
        public static Color msgInformation = Color.Black;
        public static Color msgSuccess = Color.Black;
        public static Color msgStatus = Color.Black;
        public static Color msgTrace = Color.Black;
    }



    //public static void FormatControlForMessage(Control objControl, PMessageType type) {
    //    switch (type) {
    //        case PMessageType.Exception:
    //            objControl.BackColor = PUI.ColorsBk.msgError;
    //            objControl.ForeColor = PUI.ColorsFk.msgError;
    //            break;

    //        case PMessageType.Error:
    //            objControl.BackColor = PUI.ColorsBk.msgException;
    //            objControl.ForeColor = PUI.ColorsFk.msgException;
    //            break;

    //        case PMessageType.Warning:
    //            objControl.BackColor = PUI.ColorsBk.msgWarning;
    //            objControl.ForeColor = PUI.ColorsFk.msgWarning;
    //            break;

    //        case PMessageType.Information:
    //            objControl.BackColor = PUI.ColorsBk.msgInformation;
    //            objControl.ForeColor = PUI.ColorsFk.msgInformation;
    //            break;

    //        case PMessageType.Success:
    //            objControl.BackColor = PUI.ColorsBk.msgSuccess;
    //            objControl.ForeColor = PUI.ColorsFk.msgSuccess;
    //            break;

    //        case PMessageType.Status:
    //            objControl.BackColor = PUI.ColorsBk.msgStatus;
    //            objControl.ForeColor = PUI.ColorsFk.msgStatus;
    //            break;

    //        case PMessageType.Trace:
    //            objControl.BackColor = PUI.ColorsBk.msgTrace;
    //            objControl.ForeColor = PUI.ColorsFk.msgTrace;
    //            break;

    //        case PMessageType.Undefined:
    //            objControl.BackColor = PUI.ColorsBk.msgError;
    //            objControl.ForeColor = PUI.ColorsFk.msgError;
    //            break;
    //    }
    //}
}

