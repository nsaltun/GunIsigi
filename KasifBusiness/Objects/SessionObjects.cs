using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KasifBusiness.Objects
{
    public class SessionObjects
    {
        public Int64 SessionGuid { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public Int64 RoleGuid { get; set; }
        public string RoleName { get; set; }
        public Nullable<Int16> IsAdmin { get; set; }
        public MenuTreeItemObject[] MenuTreeItemInfo { get; set; }

    }
}
