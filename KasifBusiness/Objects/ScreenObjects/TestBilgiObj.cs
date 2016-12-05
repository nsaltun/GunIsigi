using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Objects.ScreenObjects
{
    public class TestBilgiObj
    {
        public long TEST_GUID { get; set; }
        public Int32 TEST_NO { get; set; }
        public string HAFTA { get; set; }
        public string TEST_ADI { get; set; }
        public string DERS_ADI { get; set; }
        public long DERS_ID { get; set; }
        public Nullable<short> SINIF { get; set; }
        public string KONU { get; set; }
        public long KONU_ID { get; set; }
    }
}
