//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KasifBusiness.DB_Operations.EntityObject
{
    using System;
    using System.Collections.Generic;
    
    public partial class HOCA_BILGI
    {
        public long GUID { get; set; }
        public short STATUS { get; set; }
        public Nullable<long> LASTUPDATED { get; set; }
        public Nullable<int> HOCA_ID { get; set; }
        public string HOCA_ADI { get; set; }
        public string HOCA_SOYADI { get; set; }
        public string HOCA_DOGUM_TARIHI { get; set; }
        public string HOCA_TEL { get; set; }
        public string HOCA_EMAIL { get; set; }
        public Nullable<long> HOCA_BOLGE_ID { get; set; }
        public Nullable<short> SINIF { get; set; }
        public string DIGER { get; set; }
    }
}