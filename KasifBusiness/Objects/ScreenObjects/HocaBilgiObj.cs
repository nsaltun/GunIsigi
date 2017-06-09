using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Objects.ScreenObjects
{
    public class HocaBilgiObj
    {
        public long HOCA_GUID { get; set; }
        public string HOCA_ADI { get; set; }
        public string HOCA_SOYADI { get; set; }
        public Nullable<short> SINIF { get; set; }
        public string BOLGE_ADI { get; set; }
        public string DOGUM_TARIHI { get; set; }
        public string TEL_NO { get; set; }
        public string EMAIL { get; set; }
        public long HOCA_BOLGE_ID { get; set; }
    }
}
