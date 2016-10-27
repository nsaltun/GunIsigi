using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
