using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Objects.ScreenObjects
{
    public class UserTableObj
    {
        public long GUID { get; set; }
        public string USER_ID { get; set; }
        public string NAME { get; set; }
        public string SURNAME { get; set; }
        public string EMAIL { get; set; }
        public string IS_ADMIN { get; set; }
        public Nullable<long> INSERT_USER { get; set; }
        public string INSERT_DATE { get; set; }
        public Nullable<long> LAST_LOGIN_DATE { get; set; }
        public Nullable<long> LAST_PWD_CHANGE_DATE { get; set; }
        public Nullable<short> USER_STATUS { get; set; }
        public byte[] PASSWORD { get; set; }
        public Nullable<short> SINIF { get; set; }
        public string BOLGE_ADI { get; set; }
        public string ROLE_NAME { get; set; }
        public Int64 ROLE_GUID { get; set; }
    }
}
