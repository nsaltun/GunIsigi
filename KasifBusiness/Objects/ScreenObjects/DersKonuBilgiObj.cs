using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Objects.ScreenObjects
{
    public class DersKonuBilgiObj
    {
        public long DERS_GUID { get; set; }
        public long DERS_KONU_GUID { get; set; }
        public string DERS_ADI { get; set; }
        public string KONU { get; set; }
        public Nullable<short> SINIF { get; set; }
        public string TARIH { get; set; }
    }
}
