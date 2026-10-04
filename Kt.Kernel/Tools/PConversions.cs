using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Kt.Kernel;


public class PConversions {


    public static readonly Regex IsBinary = new Regex("^[01]{1,32}$", RegexOptions.Compiled);


    public static byte[] StringHexToByteArray2(string input) {
        return Enumerable.Range(0, input.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(input.Substring(x, 2), 16))
                         .ToArray();
    }


    /// <summary>
    /// Convert the specified text to an UInt8. The text can be formated like: "0x" or "h"
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static byte StringHexToUInt8(string input) {
        string data;
        byte val;
        if (input.StartsWith("0x", StringComparison.CurrentCultureIgnoreCase)) {
            data = input.Substring(2);
        } else if (input.EndsWith("h", StringComparison.CurrentCultureIgnoreCase)) {
            data = input.Substring(0, input.Length - 1);
        } else {
            data = input;
        }
        if (byte.TryParse(data, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out val) == false)
            throw new KtException(KtExceptionEnum.TextConvError);
        return val;
    }

    /// <summary>
    /// Convert the specified text to an UInt32. The text can be formated in programming style
    /// nameny 123h = 0x123, 1101b = 13d = dh = 0x0d
    /// </summary>
    /// <param name="text">The text to be converted</param>
    /// <returns></returns>
    public static byte StringToUInt8(string text) {
        UInt64 val = StringToUInt64(text);
        if (val > byte.MaxValue) throw new KtException(KtExceptionEnum.TextConvErrorToLarge);
        return (byte)val;
    }

    /// <summary>
    /// Convert the specified text to an UInt32. The text can be formated in programming style
    /// nameny 123h = 0x123, 1101b = 13d = dh = 0x0d
    /// </summary>
    /// <param name="text">The text to be converted</param>
    /// <returns></returns>
    public static UInt16 StringToUInt16(string text) {
        UInt64 val = StringToUInt64(text);
        if (val > UInt16.MaxValue) throw new KtException(KtExceptionEnum.TextConvErrorToLarge);
        return (UInt16)val;
    }

    /// <summary>
    /// Convert the specified text to an UInt32. The text can be formated in programming style
    /// nameny 123h = 0x123, 1101b = 13d = dh = 0x0d
    /// </summary>
    /// <param name="text">The text to be converted</param>
    /// <returns></returns>
    public static UInt32 StringToUInt32(string text) {
        UInt64 val = StringToUInt64(text);
        if (val > UInt32.MaxValue) throw new KtException(KtExceptionEnum.TextConvErrorToLarge);
        return (UInt32)val;
    }


    /// <summary>
    /// Convert the specified text to an UInt32. The text can be formated in programming style
    /// nameny 123h = 0x123, 1101b = 13d = dh = 0x0d
    /// </summary>
    /// <param name="text">The text to be converted</param>
    /// <returns></returns>
    public static UInt64 StringToUInt64(string text) {
        UInt64 val = 0;
        string data = text.Trim();
        int dataType = 0;

        if (data.Length == 0) return 0;
        if (data.Substring(0, 1) == "-") throw new KtException(KtExceptionEnum.TextConvErrorNegative);


        if (data.EndsWith("h", StringComparison.CurrentCultureIgnoreCase)) {  // The text is hexa
            dataType = 1;
            data = data.Substring(0, data.Length - 1);
        } else if (data.EndsWith("d", StringComparison.CurrentCultureIgnoreCase)) {  // The text is hexa
            dataType = 0;
            data = data.Substring(0, data.Length - 1);
        } else if (data.StartsWith("0x", StringComparison.CurrentCultureIgnoreCase)) {      // The text is hexa
            dataType = 1;
            data = data.Substring(2);
        } else if (data.StartsWith("0b", StringComparison.CurrentCultureIgnoreCase)) {      // The text is binary
            dataType = 2;
            data = data.Substring(2);
        }

        if (data.Length == 0) throw new KtException(KtExceptionEnum.TextConvErrorEmpty);

        switch (dataType) {
            case 0: // If decimal
                if (UInt64.TryParse(data, out val) == false) throw new KtException(KtExceptionEnum.TextConvError);
                break;
            case 1: // If hexadecimal
                if (UInt64.TryParse(data, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out val) == false)
                    throw new KtException(KtExceptionEnum.TextConvError);
                break;
            case 2: // If binary
                if (IsBinary.IsMatch(data) == false)
                    throw new KtException(KtExceptionEnum.TextConvError);
                val = Convert.ToUInt64(data, 2);
                break;
        }
        return val;
    }



    /// <summary>
    /// Convert a string where there are hex numbers into an array of byte
    /// </summary>
    /// <param name="input">The hexa string to be converted</param>
    /// <returns>The byte array representing the converted string</returns>
    public static byte[] StringHexToByteArray(string input) {
        if (input.Length == 0) return new byte[0];
        byte[] result = new byte[input.Length / 2];
        for (int i = 0; i < result.Length; i++) {
            result[i] = Convert.ToByte(input.Substring(2 * i, 2), 16);
        }
        return result;
    }


    /// <summary>
    /// Convert the given byte array into a string 
    /// ex: byte[] ba = { 1, 2, 4, 8, 16, 32 }; -> 010204081020 
    /// </summary>
    /// <param name="input">The byte array to be converted to hex-string</param>
    /// <returns>The string containg the byte array</returns>
    public static string BytesToHexString(byte[] input) {
        if (input == null) return "";
        return string.Concat(input.Select(b => b.ToString("X2")).ToArray());
    }


    #region Conversion - String to ByteArray


    /// <summary>
    /// Convert the string to the byte array 
    /// </summary>
    /// <param name="MyString">The string that will be converted to byte array</param>
    /// <returns>The byte array of the converted string</returns>
    public static byte[] StringToByteArray(string MyString) {
        return Encoding.ASCII.GetBytes(MyString); ;
    }


    /// <summary>
    /// Convert the string to the byte array 
    /// </summary>
    /// <param name="MyString">The string that will be converted to byte array</param>
    /// <returns>The byte array of the converted string</returns>
    public static byte[] StringToByteArrayRaw(string MyString) {
        byte[] bytes = new byte[MyString.Length * sizeof(char)];
        System.Buffer.BlockCopy(MyString.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }


    #endregion

    #region Conversion - ByteArray to String


    /// <summary>
    /// Convert to string the byte array
    /// </summary>
    /// <param name="MyArray">The array to convert to string</param>
    /// <returns></returns>
    public static string ByteArrayToString(byte[] bytes) {
        if (bytes == null)
            return "";
        return System.Text.Encoding.ASCII.GetString(bytes);
    }


    /// <summary>
    /// Convert the byte array to string in a raw fashion without internal build-in encoder
    /// </summary>
    /// <param name="bytes">The byte array to be converted to string</param>
    /// <returns>The converted string based on the byte array</returns>
    public static string ByteArrayRawToString(byte[] bytes) {
        if (bytes == null) return "";
        char[] chars = new char[bytes.Length / sizeof(char)];
        System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
        return new string(chars);
    }


    #endregion

    #region Conversion - Int to Byte[]


    /// <summary>
    /// Convert an integer to an array of 2 bytes. LITTLE-ENDIAN
    /// </summary>
    /// <param name="i">The integer to convert</param>
    /// <returns>The byte array containg the two values</returns>
    public static byte[] IntTo2Bytes(UInt16 i) {
        return new byte[] {
                (byte)(i & 255),
                (byte)((i>>8) & 255)
            };
    }


    /// <summary>
    /// Convert an integer to an array of 4 bytes. LITTLE-ENDIAN
    /// </summary>
    /// <param name="i"></param>
    /// <returns>The byte array containg the two values</returns>
    public static byte[] IntTo4Bytes(UInt32 i) {
        return new byte[] {
                (byte)(i & 255),
                (byte)((i>>8) & 255),
                (byte)((i>>16) & 255),
                (byte)((i>>24) & 255)
            };
    }

    #endregion





}

