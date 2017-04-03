using KasifBusiness.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KasifPortalApp.Utilities
{
    public class UtilityScreenFunctions
    {
        public static string ControlFieldAllowed(string level, SessionInfo ksfSI)
        {
            if (level == "1")
            {
                if (ksfSI.RoleName.ToUpperInvariant() != RoleNames.OGRENCI.ToString() &&
                    ksfSI.RoleName.ToUpperInvariant() != RoleNames.VELI.ToString())
                    return "1";
                else
                    return "0";
            }
            else if (level == "2")
            {
                if (ksfSI.RoleName.ToUpperInvariant() != RoleNames.OGRENCI.ToString() &&
                    ksfSI.RoleName.ToUpperInvariant() != RoleNames.VELI.ToString() &&
                    ksfSI.RoleName.ToUpperInvariant() != RoleNames.HOCA.ToString())
                    return "1";
                else
                    return "0";
            }
            return "1";
        }

        public static bool ControlActionAuthorized(string rowUserId, SessionInfo ksfSI)
        {
            if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.OWNER.ToString() || ksfSI.RoleName.ToUpperInvariant() == RoleNames.ADMIN.ToString())
            {
                return true;
            }
            else
            {
                if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString())
                {
                    if (rowUserId == ksfSI.HocaGuid.ToString())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
    }

    public enum ResultStatus
    {
        Success = 1,
        Error = -1
    }

    public enum RoleNames
    {
        ADMIN,
        OWNER,
        HOCA,
        OGRENCI,
        VELI
    }
    public class NameValue
    {
        private string _Name;
        private string _Value;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }

}