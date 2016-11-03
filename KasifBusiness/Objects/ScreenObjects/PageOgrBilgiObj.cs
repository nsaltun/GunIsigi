using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Objects.ScreenObjects
{
    public class PageOgrBilgiObj
    {
        public long GUID { get; set; }
        public short STATUS { get; set; }
        public Nullable<long> LASTUPDATED { get; set; }
        public Nullable<int> OGR_ID { get; set; }
        public Nullable<int> OGR_NO { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public Nullable<short> CLASS { get; set; }
        public string SUBCLASS { get; set; }
        public string SCHOOL_NAME { get; set; }
        public Nullable<long> BOLGE_ID { get; set; }
        public string BOLGE_ADI { get; set; }
        public string PARENT_NAME { get; set; }
        public string DATE_OF_BIRTH { get; set; }
        public string BIRT_PLACE { get; set; }
        public string PHONE { get; set; }
        public string PARENT_PHONE { get; set; }
        public string OGR_EMAIL { get; set; }
        public string PARENT_EMAIL { get; set; }
        public byte[] OGR_IMG { get; set; }
        public Nullable<long> HOCA_GUID { get; set; }
        public string HOCA_AD_SOYAD { get; set; }
    }
}
