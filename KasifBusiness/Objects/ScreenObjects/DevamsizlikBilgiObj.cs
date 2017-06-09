using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Objects.ScreenObjects
{
    public class DevamsizlikBilgiObj
    {
        public long DEVAMSIZLIK_GUID { get; set; }
        public string DATE { get; set; }
        public string TIP { get; set; }
        public string KISI_AD_SOYAD { get; set; }
        public Nullable<short> SINIF { get; set; }
        public string DEVAM_DURUMU { get; set; }
        public string SEBEP { get; set; }
        public string BOLGE_ADI { get; set; }

    }
}
