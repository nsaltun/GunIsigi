using KasifBusiness.DB_Operations.EntityObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.DB_Operations.DBOperations
{
    public class OOgrBilgi
    {
        OGR_BILGI ogrBilgiObj; 
        public OOgrBilgi(OGR_BILGI ogrBilgiObj)
        {
            this.ogrBilgiObj = ogrBilgiObj;
        }

        public void DoJob()
        {
            DbOperations.Insert(this.ogrBilgiObj);
        }

    }
}
