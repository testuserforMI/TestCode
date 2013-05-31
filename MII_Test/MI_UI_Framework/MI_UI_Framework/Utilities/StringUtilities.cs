using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI_UI_Framework.Utilities
{
    public class StringUtilities
    {
        /// <summary>
        /// Generates a random string of specified length with no special characters.
        /// </summary>
        /// <param name="length">length of the string to generate</param>
        /// <returns>The random string</returns>
        public static string GetRandomPlainString(int length)
        {
            const string legalCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();
            StringBuilder builder = new StringBuilder();
            char ch;

            for (int i = 0; i < length; i++)
            {
                ch = legalCharacters[random.Next(0, legalCharacters.Length)];
                builder.Append(ch);
            }

            return builder.ToString().Replace("/", String.Empty);
        }

        /// <summary>
        /// Generates random string value
        /// </summary>
        /// <param name="length">Zero based size</param>
        /// <returns>The random string.</returns>
        public static string GetRandomString(int length)
        {
            StringBuilder randomStr = new StringBuilder();
            randomStr.Append("!!!" + Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
            while (randomStr.Length < length)
            {
                randomStr.Append(DateTime.Now.Ticks.ToString());
            }

            return randomStr.ToString(0, length).Replace("/", String.Empty);
        }

        /// <summary>
        /// Generates a random string of specified length, suffixed by a set of special Characters
        /// </summary>
        /// <param name="length">length of the string to generate</param>
        /// <returns>random string</returns>
        public static string GetRandomStringWithSpecialChars(int length)
        {
            StringBuilder randomStr = new StringBuilder();
            string guId = Guid.NewGuid().ToString();
            randomStr.Append(String.Format("^$?x{0};~!{0}@#%^{0}&*()+", guId));

            while (randomStr.ToString().Length < length)
            {
                randomStr.Append(Guid.NewGuid().ToString());
            }

            return randomStr.ToString(0, length).Replace("/", String.Empty);
        }

        /// <summary>
        /// In Cases of the Firefox the string which we get from the control may contains invalid spaces (ASCII = 160) but in IE
        /// We will get correct space (ASCII = 32) this will cause the Comparison to fail so use this function to check string A
        /// is the sub string of string A
        /// </summary>
        /// <param name="a">String 'a' </param>
        /// <param name="b">String 'b'</param>
        /// <param name="ignoreSpaces">Whether spaces has to be ignored</param>
        /// <param name="ignoreNewLine">Whether new line character has to be ignored</param>
        /// <returns>Returns true if the string a contains string b</returns>
        public static bool CompareStrings(string a, string b, bool ignoreSpaces, bool ignoreNewLine)
        {
            if (String.IsNullOrEmpty(a) && String.IsNullOrEmpty(b))
            {
                return true;
            }

            ModifyStrings(ref a, ignoreSpaces, ignoreNewLine);
            ModifyStrings(ref b, ignoreSpaces, ignoreNewLine);

            return a.Contains(b);
        }

        /// <summary>
        /// In Cases of the Firefox the string which we get from the control may contains invalid spaces (ASCII = 160) but in IE
        /// We will get correct space (ASCII = 32) this will cause the Comparison to fail so use this function to modify the strings        /// 
        /// </summary>
        /// <param name="a">String which is to be modified </param>
        /// <param name="ignoreSpaces">Whether spaces has to be ignored</param>
        /// <param name="ignoreNewLine">Whether new line character has to be ignored</param>
        public static void ModifyStrings(ref string a, bool ignoreSpaces, bool ignoreNewLine)
        {
            if (String.IsNullOrEmpty(a))
            {
                return;
            }
            string abc = Char.ConvertFromUtf32(160);

            if (ignoreSpaces)
            {
                a = a.Replace(" ", String.Empty);
            }

            if (ignoreNewLine)
            {
                a = a.Replace("\n", String.Empty);
                a = a.Replace("\r", String.Empty);
            }

            a = a.Replace(abc, String.Empty);
        }
    }
}
