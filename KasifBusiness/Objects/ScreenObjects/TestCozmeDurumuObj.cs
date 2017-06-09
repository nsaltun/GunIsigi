using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Objects.ScreenObjects
{
    public class TestCozmeDurumuObj
    {
        public long TEST_REL_GUID { get; set; }
        public Int32 TEST_NO { get; set; }
        public string TARIH { get; set; }
        public string TEST_ADI { get; set; }
        public string DERS_ADI { get; set; }
        public Nullable<short> SINIF { get; set; }
        public string KONU { get; set; }
        public string OGR_AD_SOYAD { get; set; }
        public string HOCA_AD_SOYAD { get; set; }
        public string DURUM { get; set; }
        public Int32 DOGRU_SAYISI { get; set; }
        public Int32 YANLIS_SAYISI { get; set; }
        public string MAHALLE { get; set; }
    }
}
