using System;
using System.Text;
using System.Security.Cryptography;

namespace ServerLibrary.Utils
{
    public class CryptoUtils
    {
        const string SALT        = @"FUb'hX4Vzyzny";
        const string TOKENCHARS  = "abcdefghijklmnopqrstuvwyxz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        const int    TOKENLENGTH = 32;
        const int    PINLENGTH   = 6;

        public static string GetMD5Hash(string password)
        {
            MD5 md5Hash = MD5.Create();
            return CreateMD5Hash(md5Hash, password + SALT);
        }

        public static string CreateToken()
        {
            Random rand = new Random((int) DateUtils.TimeStamp);
            char[] chars = new char[TOKENLENGTH];
            for (int i = 0; i < TOKENLENGTH; i++)
            {
                chars[i] = TOKENCHARS[rand.Next(TOKENCHARS.Length)];
            }
            return new string(chars);
        }

        // Verify a hash against a string
        public static bool VerifyMD5Hash(string accountPassword, string dbPassword)
        {
            return CheckMD5Hash(accountPassword, dbPassword);
        }

        public static string CreatePINCode()
        {
            Random random = new Random();
            var pincode = random.Next(1, 999999).ToString();
            return pincode.PadLeft(PINLENGTH, '0');        
        }

        private static string CreateMD5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            // Create a new Stringbuilder to collect the bytes and create a string
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            // Return the hexadecimal string
            return sBuilder.ToString();
        }

        private static bool CheckMD5Hash(string accountPassword, string hashPassword)
        {
            // Hash the accountPassword. 
            string hashOfInput = GetMD5Hash(accountPassword);
            // Create a StringComparer an compare the hashes
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            return comparer.Compare(hashOfInput, hashPassword) == 0;
        }
    }
}
