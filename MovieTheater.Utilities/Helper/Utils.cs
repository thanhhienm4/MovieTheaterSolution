using System;

namespace MovieTheater.Common.Helper
{
    public static class Utils
    {
        public static readonly string strAphabet = "abcdefghijklmnopqrstuvwxyz";
        public static readonly string strNumeric = "0123456789";

        public static string RandPassword()
        {
            string res = "";
            Random rnd = new Random();
            for (int i = 0; i < 3; i++)
            {
                res += strAphabet[rnd.Next(strAphabet.Length)];
                res += strAphabet.ToUpper()[rnd.Next(strAphabet.Length)];
                res += strNumeric[rnd.Next(strNumeric.Length)];
            }
            return res;
        }

        
    }
}
