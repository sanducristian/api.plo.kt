using System.Globalization;

namespace Kt.Kernel;


public class QbLocale {


    /// <summary>
    /// Convert the specified string to a decimal value
    /// </summary>
    /// <param name="sData">The string to be converted</param>
    /// <returns></returns>
    public static double getDecimalValue(ref string sData) {
        string sDecimal = null;
        string sData2 = null;

        sDecimal = getLocaleDecimalChar();
        if (sDecimal == ".") {
            sData2 = sData.Replace(",", ".");
        } else if (sDecimal == ",") {
            sData2 = sData.Replace(".", ",");
        } else {
            sData2 = sData.Replace(",", sDecimal);
            sData2 = sData.Replace(".", sDecimal);
        }
        return Convert.ToDouble(sData2);
    }




    /// <summary>
    /// Get the string with the correct decimal
    /// </summary>
    /// <param name="sData"></param>
    /// <returns></returns>
    public static string getStringWithCorrectDecimal(ref string sData) {
        string functionReturnValue = null;
        string sDecimal = null;

        sDecimal = getLocaleDecimalChar();
        if (sDecimal == ".") {
            functionReturnValue = sData.Replace(",", ".");
        } else if (sDecimal == ",") {
            functionReturnValue = sData.Replace(".", ",");
        } else {
            functionReturnValue = sData.Replace(",", sDecimal);
            functionReturnValue = sData.Replace(".", sDecimal);
        }
        return functionReturnValue;
    }



    public static string getLocaleDecimalChar() {
        return CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
    }



    public static string getLocaleThousandChar() {
        return CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator;
    }



    public static string getUserLocaleInfo(int dwLocaleID, int dwLCType) {
        return CultureInfo.CurrentCulture.DisplayName;
    }
}

