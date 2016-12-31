using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Objects.CodeMgmt
{
    public class ResultObject
    {
        public string errorCode { get; set; }
        public string errorMsg { get; set; }
        public string errPrefix { get; set; }
        public bool isOk { get; set; }
    }
}
