using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Utilities
{
    public class KasifHelper
    {
        public static Int64 GuidFactory()
        {
            Random rnd = new Random();
            return rnd.Next(100000000, 999999999);
        }

        public static Int64 GetCurrentDate()
        {
            return Convert.ToInt64(DateTime.Now.ToString("yyyyMMddssfff"));
        }

        public static byte[] GetSha512HashedData(string clearData)
        {
            try
            {
                SHA512 s512 = SHA512.Create();
                UnicodeEncoding ByteConverter = new UnicodeEncoding();
                byte[] bytes = s512.ComputeHash(ByteConverter.GetBytes(clearData));

                return bytes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

    }
}
