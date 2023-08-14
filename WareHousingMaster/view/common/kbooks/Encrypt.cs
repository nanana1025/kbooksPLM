using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace WareHousingMaster.view.common.kbooks
{
    static class Encrypt
    {
        static public string EncryptionSHA256(string data)
        {
            byte[] array = Encoding.Default.GetBytes(data);
            byte[] hashValue;
            string result = string.Empty;

            using (SHA256 mySHA256 = SHA256.Create())
            {
                hashValue = mySHA256.ComputeHash(array);

                for (int i = 0; i < hashValue.Length; i++)
                    result += hashValue[i].ToString("x2");
            }

            return result;
        }

        static public string HashSHA256(string data)
        {
            byte[] array = Encoding.Default.GetBytes(data);
            byte[] hashValue;
            string result = string.Empty;

            using (SHA256 mySHA256 = SHA256.Create())
            {
                hashValue = mySHA256.ComputeHash(array);
                result = Encoding.Default.GetString(hashValue);
            }

            return result;
        }

        static public string Password(string id, string pswd)
        {
            string data = id + pswd;
            for (int i = 0; i < 5; i++)
            {
                data = EncryptionSHA256(data);
                data = data.Replace(ConvertUtil.ToString(i), ConvertUtil.ToString(9 - i));
            }

            return data;
        }

        public static string Hex2String(string input)
        {
            var builder = new StringBuilder();
            for (int i = 0; i < input.Length; i += 2)
            { //throws an exception if not properly formatted
                string hexdec = input.Substring(i, 2);
                int number = Int32.Parse(hexdec, NumberStyles.HexNumber);
                char charToAdd = (char)number;
                builder.Append(charToAdd);
            }
            return builder.ToString();
        }
    }
}
