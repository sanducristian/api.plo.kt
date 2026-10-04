using System.Data;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace Kt.Kernel;


/// <summary>
/// The tools class for the application
/// </summary>
public class KtTools {

    /// <summary>
    /// The byte values that will help in the decoding process
    /// </summary>
    public static readonly byte[] byteValues = new byte[8] { 1, 2, 4, 8, 16, 32, 64, 128 };

    public static readonly UInt16[] uint16Mask = new UInt16[16] {
            0x0001, 0x0003, 0x0007, 0x000f,
            0x001f, 0x003f, 0x007f, 0x00ff,
            0x01ff, 0x03ff, 0x07ff, 0x0fff,
            0x1fff, 0x3fff, 0x7fff, 0xffff,
        };
    public static readonly UInt32[] uint32Mask = new UInt32[32] {
            0x00000001, 0x00000003, 0x00000007, 0x0000000f,
            0x0000001f, 0x0000003f, 0x0000007f, 0x000000ff,
            0x000001ff, 0x000003ff, 0x000007ff, 0x00000fff,
            0x00001fff, 0x00003fff, 0x00007fff, 0x0000ffff,
            0x0001ffff, 0x0003ffff, 0x0007ffff, 0x000fffff,
            0x001fffff, 0x003fffff, 0x007fffff, 0x00ffffff,
            0x01ffffff, 0x03ffffff, 0x07ffffff, 0x0fffffff,
            0x1fffffff, 0x3fffffff, 0x7fffffff, 0xffffffff,
        };
    public static readonly UInt64[] uint64Mask = new UInt64[64] {
            0x0000000000000001, 0x0000000000000003, 0x0000000000000007, 0x000000000000000f,
            0x000000000000001f, 0x000000000000003f, 0x000000000000007f, 0x00000000000000ff,
            0x00000000000001ff, 0x00000000000003ff, 0x00000000000007ff, 0x0000000000000fff,
            0x0000000000001fff, 0x0000000000003fff, 0x0000000000007fff, 0x000000000000ffff,
            0x000000000001ffff, 0x000000000003ffff, 0x000000000007ffff, 0x00000000000fffff,
            0x00000000001fffff, 0x00000000003fffff, 0x00000000007fffff, 0x0000000000ffffff,
            0x0000000001ffffff, 0x0000000003ffffff, 0x0000000007ffffff, 0x000000000fffffff,
            0x000000001fffffff, 0x000000003fffffff, 0x000000007fffffff, 0x00000000ffffffff,
            0x00000001ffffffff, 0x00000003ffffffff, 0x00000007ffffffff, 0x0000000fffffffff,
            0x0000001fffffffff, 0x0000003fffffffff, 0x0000007fffffffff, 0x000000ffffffffff,
            0x000001ffffffffff, 0x000003ffffffffff, 0x000007ffffffffff, 0x00000fffffffffff,
            0x00001fffffffffff, 0x00003fffffffffff, 0x00007fffffffffff, 0x0000ffffffffffff,
            0x0001ffffffffffff, 0x0003ffffffffffff, 0x0007ffffffffffff, 0x000fffffffffffff,
            0x001fffffffffffff, 0x003fffffffffffff, 0x007fffffffffffff, 0x00ffffffffffffff,
            0x01ffffffffffffff, 0x03ffffffffffffff, 0x07ffffffffffffff, 0x0fffffffffffffff,
            0x1fffffffffffffff, 0x3fffffffffffffff, 0x7fffffffffffffff, 0xffffffffffffffff,
        };


    public static object CreateObject(string pattern) {
        Type t = Type.GetType(pattern);
        if (t == null) {
            throw new Exception("Type " + pattern + " not found.");
        }
        return Activator.CreateInstance(t);
    }



    /// <summary>
    /// Check if the application is running as a user administrator
    /// </summary>
    /// <returns>True if the application is runned with administrator priviledges, false otherwise</returns>
    public static bool IsUserAdministrator() {
        bool isAdmin;
        try {
            WindowsIdentity user = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(user);
            isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        catch (UnauthorizedAccessException) {
            isAdmin = false;
        }
        catch (Exception) {
            isAdmin = false;
        }
        return isAdmin;
    }

    /// <summary>
    /// Calculate the MD5 hash for the specified input
    /// </summary>
    /// <param name="input">The string for which to compute the MD5</param>
    /// <returns>The MD5 result based on the input string</returns>
    public static string CalculateMD5Hash(string input) {
        MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++) {
            sb.Append(hash[i].ToString("X2"));
        }
        return sb.ToString();
    }


    static string GetMd5Hash(MD5 md5Hash, string input) {
        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
        // Create a new Stringbuilder to collect the bytes and create a string.
        StringBuilder sBuilder = new StringBuilder();

        // Loop through each byte of the hashed data and format each one as a hexadecimal string. 
        for (int i = 0; i < data.Length; i++) {
            sBuilder.Append(data[i].ToString("x2"));
        }
        // Return the hexadecimal string. 
        return sBuilder.ToString();
    }

    // Verify a hash against a string. 
    static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash) {
        // Hash the input. 
        string hashOfInput = GetMd5Hash(md5Hash, input);

        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;

        if (0 == comparer.Compare(hashOfInput, hash)) {
            return true;
        } else {
            return false;
        }
    }


    public static Type GetNullableType(Type t) {
        Type returnType = t;
        if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>))) {
            returnType = Nullable.GetUnderlyingType(t);
        }
        return returnType;
    }


    public static bool IsNullableType(Type type) {
        return (type == typeof(string) ||
                type.IsArray ||
                (type.IsGenericType &&
                    type.GetGenericTypeDefinition().Equals(typeof(Nullable<>))));
    }


    public static Boolean IsNumber(String value) {
        return value.All(Char.IsDigit);
    }


    public static DataTable ListToDataTable<T>(List<T> list) {
        DataTable dt = new DataTable();

        foreach (PropertyInfo info in typeof(T).GetProperties()) {
            dt.Columns.Add(new DataColumn(info.Name, GetNullableType(info.PropertyType)));
        }
        foreach (T t in list) {
            DataRow row = dt.NewRow();
            foreach (PropertyInfo info in typeof(T).GetProperties()) {
                if (!IsNullableType(info.PropertyType))
                    row[info.Name] = info.GetValue(t, null);
                else
                    row[info.Name] = (info.GetValue(t, null) ?? DBNull.Value);
            }
            dt.Rows.Add(row);
        }
        return dt;
    }


    /// <summary>
    /// Reverse the bits inside a byte (ex: 0001 -> 1000)
    /// </summary>
    /// <param name="b">The byte to have the bits reversed</param>
    /// <returns>The byte with the reversed bits</returns>
    public static byte ReverseBits(byte b) {
        int output = b;
        output = ((output & 0xF0) >> 4) | ((output & 0x0F) << 4);
        output = ((output & 0xCC) >> 2) | ((output & 0x33) << 2);
        output = ((output & 0xAA) >> 1) | ((output & 0x55) << 1);
        return (byte)(output & 0xff);
    }


    /// <summary>
    /// Reserve the bits specified by the parameter (used by compression and encoding algorithms)
    /// </summary>
    /// <param name="b">The data to have the bits reversed</param>
    /// <returns>The value with the bits reversed</returns>
    public static byte ReverseBits3(byte b) {
        return (byte)((b * 0x0202020202ul & 0x010884422010ul) % 1023);
    }



    /// <summary>
    /// Reserve the bits specified by the parameter (used by compression and encoding algorithms)
    /// </summary>
    /// <param name="b">The data to have the bits reversed</param>
    /// <returns>The value with the bits reversed</returns>
    public static byte ReverseBits7(byte b) {
        return (byte)(((b * 0x0802u & 0x22110u) | (b * 0x8020u & 0x88440u)) * 0x10101u >> 16);
    }






    /// <summary>
    /// Checks if the byte specified as parameter is a digit or not
    /// </summary>
    /// <param name="b">The value to check if it is a digit</param>
    /// <returns>True if the parameter is a digit, false otherwise</returns>
    public static bool CharIsDigit(byte b) {
        return (b >= '0' && b <= '9');
    }



    /// <summary>
    /// Checks if the byte specified as parameter is a upper char or not
    /// </summary>
    /// <param name="b">The value to check if it is a upper char</param>
    /// <returns>True if the parameter is a upper char, false otherwise</returns>
    public static bool CharIsUpper(byte b) {
        return (b >= 'A' && b <= 'Z');
    }



    /// <summary>
    /// Checks if the byte specified as parameter is a lower char or not
    /// </summary>
    /// <param name="b">The value to check if it is a lower char</param>
    /// <returns>True if the parameter is a lower char, false otherwise</returns>
    public static bool CharIsLower(byte b) {
        return (b >= 'a' && b <= 'z');
    }



    /// <summary>
    /// Convert the specified parameter to a lower charater
    /// </summary>
    /// <param name="b">The character to convert to lower</param>
    /// <returns>The converted parameter</returns>
    public static char CharToLower(byte b) {
        return ((char)b).ToString().ToLower()[0];
    }




    /// <summary>
    /// Convert to string the size based on width and height
    /// </summary>
    /// <param name="height">The height to be converted</param>
    /// <param name="width">The width to be converted</param>
    /// <returns></returns>
    public static string LogStringSize(int width, int height) {
        return "[ Size (" + height.ToString() + " x " + width.ToString() + ")]";
    }



    /// <summary>
    /// Convert to string the overlimit value
    /// </summary>
    /// <param name="value">The value that is over limit</param>
    /// <param name="limit">The limit that had been surpassed</param>
    /// <returns></returns>
    public static string LogStringLimitOver(int value, int limit) {
        return "[ Overlimit (" + value.ToString() + " over " + limit.ToString() + ")]";
    }


    /// <summary>
    /// Convert the specified byte to string
    /// </summary>
    /// <param name="b">The byte to convert to its hexa decimal representation</param>
    /// <returns>The string for the byte</returns>
    public static string LogNumberHex(byte param) {
        return "0x" + param.ToString("x2");
    }


    /// <summary>
    /// Convert the specified byte to string
    /// </summary>
    /// <param name="b">The byte to convert to its hexa decimal representation</param>
    /// <returns>The string for the byte</returns>
    public static string LogNumberHex(char param) {
        return "0x" + Convert.ToByte(param).ToString("x2");
    }



    public static string LogVector(byte[] vector) {
        if (vector == null)
            return "byte[]";
        string sData = "";
        sData = "byte[" + vector.Length.ToString() + "= {";
        foreach (byte b in vector) {
            sData += b.ToString("X2") + ", ";
        }
        sData += "}";
        return sData;
    }



    public static string LogVectorAsString(byte[] vector) {
        if (vector == null)
            return "";
        return System.Text.Encoding.Default.GetString(vector);
    }



    /// <summary>
    /// Log the specified variable by decoding its type and display it
    /// </summary>
    /// <param name="variable"></param>
    /// <returns></returns>
    public static string LogVariable(object variable) {
        return LogVariable(variable, false);
    }


    /// <summary>
    /// Log the specified variable by decoding its type and display it
    /// </summary>
    /// <param name="variable"></param>
    /// <param name="DisplayType"></param>
    /// <returns></returns>
    public static string LogVariable(object? variable, bool DisplayType) {
        Type valType;
        bool bFirst;
        string sData;

        if (variable == null)
            return "{null}";


        if (variable is string sData2) {
            if (DisplayType) {
                return "String[" + sData2.Length + "]: \"" + sData2 + "\"";
            }
            return sData2;
        }

        if (variable is char) {
            char charData = Convert.ToChar(variable);
            if ((charData < 32) && (charData > 0x7f)) {
                sData = charData.ToString();
            } else {
                sData = "0x" + Convert.ToByte(charData).ToString("X2");
            }
            if (DisplayType)
                return "Char[" + sData + "]";

            return sData;
        }


        //if (variable is PropertyData) {
        //    PropertyData property = (PropertyData)variable;
        //    if (property.Value == null) {
        //        return property.Name + "> null";
        //    }
        //    return property.Name + ": " + LogVariable(property.Value, true);
        //}



        valType = variable.GetType();

        if (valType.IsArray) {
            Array vals = variable as Array;
            bFirst = true;
            if (DisplayType)
                sData = valType.Name.Replace("]", "") + vals?.Length + "] = [";
            else
                sData = "[";
            foreach (object val in vals) {
                if (bFirst == false) sData += ", ";
                sData += LogVariable(val, false);
                bFirst = false;
            }
            sData += "]";
            return sData;
        }


        if (valType.IsClass) {
            System.Reflection.PropertyInfo[] mProps = valType.GetProperties();
            sData = "Class { \n";
            foreach (System.Reflection.PropertyInfo mProp in mProps) {
                try {
                    var getMethod = mProp.GetGetMethod();
                    if (getMethod.MemberType == MemberTypes.Method) continue;
                    if (getMethod.MemberType == MemberTypes.Event) continue;
                    if (getMethod.MemberType == MemberTypes.Constructor) continue;
                    if (getMethod.ReturnType.IsArray) {
                        sData += mProp.Name + "[] : ";
                        var arrayObject = getMethod.Invoke(variable, null);
                        foreach (object element in (Array)arrayObject) {
                            foreach (PropertyInfo arrayObjPinfo in element.GetType().GetProperties()) {
                                sData += arrayObjPinfo.Name + ":" + arrayObjPinfo.GetGetMethod().Invoke(element, null).ToString() + "\n";
                            }
                        }
                        sData += "\n";
                    } else if (getMethod.GetParameters().Length > 0) {
                        sData += mProp.Name + ": (Function with parameters)\n";
                    } else {
                        sData += mProp.Name + ": " + LogVariable(mProp.GetValue(variable, null), true) + "\n";
                    }
                }
                catch (Exception ex) {
                    sData += "Cannot get value for:" + mProp.Name + " - " + mProp.ToString() + "\n";
                    System.Diagnostics.Debug.Write(ex.ToString());
                }
            }
            sData += "};";
            return sData;
        }


        if (DisplayType)
            return valType.Name + "[" + variable.ToString() + "]";

        return variable.ToString();
    }




    /// <summary>
    /// Draw the specified text on to the graphics object
    /// </summary>
    /// <param name="Grp">The graphics object where the text will go</param>
    /// <param name="TextValue">The text to be written</param>
    /// <param name="TextFont">The text font to be used</param>
    /// <param name="TextBrush">The brush type to draw the text</param>
    /// <param name="AngleDeg">The angle to draw the text</param>
    /// <param name="x">The Left position of the text to be drawed</param>
    /// <param name="y">The top position of the text to be drawed</param>
    /// <param name="Width">The width of the region used to draw</param>
    /// <param name="Height">The height of the region used to draw</param>
    //public static void DrawText(Graphics Grp, string TextValue, Font TextFont, Brush TextBrush, float AngleDeg, float x, float y) {
    //    SizeF txt = Grp.MeasureString(TextValue, TextFont);
    //    SizeF sz = Grp.VisibleClipBounds.Size;
    //    if (AngleDeg == 0) {
    //        Grp.TranslateTransform(0, 0);
    //        Grp.RotateTransform(0);
    //        Grp.DrawString(TextValue, TextFont, TextBrush, x, y); //  new RectangleF(x, y, Width, Height));
    //        Grp.ResetTransform();
    //    } else if (AngleDeg == 90) {
    //        Grp.TranslateTransform(sz.Width, 0);
    //        Grp.RotateTransform(90);
    //        Grp.DrawString(TextValue, TextFont, TextBrush, x, y);
    //        Grp.ResetTransform();
    //    } else if (AngleDeg == 180) {
    //        Grp.TranslateTransform(sz.Width, sz.Height);
    //        Grp.RotateTransform(180);
    //        Grp.DrawString(TextValue, TextFont, TextBrush, x, y);
    //        Grp.ResetTransform();
    //    } else if (AngleDeg == 270) {
    //        Grp.TranslateTransform(0, sz.Width); // Height);
    //        Grp.RotateTransform(270);
    //        Grp.DrawString(TextValue, TextFont, TextBrush, x, y);
    //        Grp.ResetTransform();
    //    } else {
    //        double angleRad = ((double)(AngleDeg % 360) / 180) * Math.PI;
    //        Grp.TranslateTransform(
    //            (sz.Width + (float)(txt.Height * Math.Sin(angleRad)) - (float)(txt.Width * Math.Cos(angleRad))) / 2,
    //            (sz.Height - (float)(txt.Height * Math.Cos(angleRad)) - (float)(txt.Width * Math.Sin(angleRad))) / 2);
    //        Grp.RotateTransform(AngleDeg);
    //        Grp.DrawString(TextValue, TextFont, TextBrush, x, y);
    //        Grp.ResetTransform();
    //    }
    //}



    //public static void DrawTextTEST(Graphics Grp, string TextValue, Font TextFont, Brush TextBrush, float AngleDeg, float x, float y) {
    //    SizeF txt = Grp.MeasureString(TextValue, TextFont);
    //    SizeF sz = Grp.VisibleClipBounds.Size;
    //    if (AngleDeg == 0) {
    //        Grp.TranslateTransform(x, y);
    //        Grp.RotateTransform(0);
    //        Grp.DrawString(TextValue, TextFont, TextBrush, 0, 0); //  new RectangleF(x, y, Width, Height));
    //        Grp.ResetTransform();
    //    } else if (AngleDeg == 90) {
    //        Grp.TranslateTransform(x, y);
    //        Grp.RotateTransform(90);
    //        Grp.DrawString(TextValue, TextFont, TextBrush, 0, 0);
    //        Grp.ResetTransform();
    //    } else if (AngleDeg == 180) {
    //        Grp.TranslateTransform(x, y);
    //        Grp.RotateTransform(180);
    //        Grp.DrawString(TextValue, TextFont, TextBrush, 0, 0);
    //        Grp.ResetTransform();
    //    } else if (AngleDeg == 270) {
    //        Grp.TranslateTransform(x, y); // Height);
    //        Grp.RotateTransform(270);
    //        Grp.DrawString(TextValue, TextFont, TextBrush, 0, 0);
    //        Grp.ResetTransform();
    //    } else {
    //        double angleRad = ((double)(AngleDeg % 360) / 180) * Math.PI;
    //        Grp.TranslateTransform(
    //            (sz.Width + (float)(txt.Height * Math.Sin(angleRad)) - (float)(txt.Width * Math.Cos(angleRad))) / 2,
    //            (sz.Height - (float)(txt.Height * Math.Cos(angleRad)) - (float)(txt.Width * Math.Sin(angleRad))) / 2);
    //        Grp.RotateTransform(AngleDeg);
    //        Grp.DrawString(TextValue, TextFont, TextBrush, x, y);
    //        Grp.ResetTransform();
    //    }
    //}
}



public static class GenericCopier<T> {    //deep copy a list
    public static T DeepCopy(object objectToCopy) {
        using (MemoryStream memoryStream = new MemoryStream()) {
            byte[] data = JsonSerializer.SerializeToUtf8Bytes(objectToCopy);
            //BinaryFormatter binaryFormatter = new BinaryFormatter();
            //binaryFormatter.Serialize(memoryStream, objectToCopy);

            return (T)JsonSerializer.Deserialize<T>(data)?? throw new InvalidOperationException("Deserialization returned null.");
             
            //memoryStream.Seek(0, SeekOrigin.Begin);
            //return (T)binaryFormatter.Deserialize(memoryStream);
        }
    }
}

