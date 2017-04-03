using KasifBusiness.DB_Operations.EntityObject;
using System;
using System.Collections.Generic;

namespace KasifBusiness.Objects
{
    public class SessionInfo
    {
        public Int64 SessionGuid { get; set; }
        //public Nullable<Int64> UserId { get; set; }
        public Int64 UserGuid { get; set; }
        public string Email { get; set; }
        public Int64 RoleGuid { get; set; }
        public string RoleName { get; set; }
        public Nullable<Int16> IsAdmin { get; set; }
        public List<MenuTreeItemObject> MenuTreeItemInfo { get; set; }
        public List<MenuTreeItemObject> lstAllowedPages { get; set; }
        public List<MENU_TILES> MenuTiles { get; set; }
        public string userNameSurname {get;set;}
        public Nullable<Int64> HocaGuid { get; set; }
        public Nullable<Int64> OgrenciGuid { get; set; }


    }
}
