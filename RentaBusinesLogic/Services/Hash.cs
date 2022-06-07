using System;
using System.Security.Cryptography;
using System.Text;

namespace DataBaseContext.Services
{
    public static class Hash
    {
        public static byte[] CreateHash(string password)
        {
            string sSourceData;
            byte[] tmpSource;
            sSourceData = password;
            tmpSource = Encoding.ASCII.GetBytes(sSourceData);

            return new MD5CryptoServiceProvider().ComputeHash(tmpSource);
        }

        public static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
    }
}
