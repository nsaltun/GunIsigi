using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Objects.ScreenObjects
{
    public class MufredatTakipObj
    {
        public long MUFREDAT_GUID { get; set; }
        public string HOCA_AD_SOYAD { get; set; }
        public string TARIH { get; set; }
        public Nullable<short> SINIF { get; set; }
        public string KONU { get; set; }
        public string TAKIP_DURUMU { get; set; }
        public string DERS_ADI { get; set; }
        public long HOCA_GUID { get; set; }
    }
}
