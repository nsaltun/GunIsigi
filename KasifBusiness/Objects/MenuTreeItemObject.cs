using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Objects
{
    public class MenuTreeItemObject
    {
        public string AUTH_LEVEL { get; set; }
        public string CLASS_NAME { get; set; }
        public string FILE_NAME { get; set; }
        public string NODE_DISPLAY_NAME { get; set; }
        public long NODE_GUID { get; set; }
        public Nullable<short> NODE_POSITION { get; set; }
        public string NODE_TYPE { get; set; }
        public Nullable<long> PARENT_NODE_GUID { get; set; }
        public string NODE_VISIBILITY { get; set; }
    }
}
