using KasifBusiness.DB_Operations.DBObjects;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects;
using KasifBusiness.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCommands = KasifBusiness.DB_Operations.DBObjects.ConstDbCommands.DbCommandList;

namespace KasifBusiness.Business.Login
{
    public class OLogin
    {
        public SessionInfo SessionObj;

        USER_USER userObj;
        List<MenuTreeItemObject> lstMenuTreeObj;
        string userId;
        byte[] hashedPwd;

        public OLogin(string userId, string pwd)
        {
            this.userId = userId;
            this.hashedPwd = KasifHelper.GetSha512HashedData(pwd);
        }

        public bool Execute()
        {
            userObj = CheckUserExist();
            if (userObj != null)
            {
                FillMenuTreeObject();
                FillSessionObject();
                return true;
            }

            return false;
        }

        private USER_USER CheckUserExist()
        {
            List<USER_USER> lstUser = new List<USER_USER>();
            string[] prmNames = new string[] { "P_EMAIL", "P_PASSWORD" };
            object[] prmValues = new object[] { userId, this.hashedPwd };
            DbOperations.RunDbQuery<USER_USER>(ref lstUser, DbCommands.GET_USER_BY_EMAIL, prmNames, prmValues);
            if (lstUser != null && lstUser.Count > 0)
            {
                return lstUser[0];
            }
            else
            {
                return null;
            }
        }

        private bool FillMenuTreeObject()
        {
            string[] prmNames = new string[] { "P_EMAIL"};
            object[] prmValues = new object[] { userId};
            DbOperations.RunDbQuery<MenuTreeItemObject>(ref lstMenuTreeObj, DbCommands.GET_USER_MENUS, prmNames, prmValues);
            if (lstMenuTreeObj != null && lstMenuTreeObj.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void FillSessionObject()
        {
            SessionObj = new SessionInfo();
            SessionObj.Email = userObj.EMAIL;
            SessionObj.IsAdmin = userObj.IS_ADMIN;
            SessionObj.UserId = userObj.USER_ID;
            SessionObj.UserGuid = userObj.GUID;
            SessionObj.MenuTreeItemInfo = lstMenuTreeObj;
            //SessionObj.RoleGuid = roleguid;
            //SessionObj.RoleName = rolename;
            SessionObj.SessionGuid = KasifHelper.GuidFactory();
        }

    }
}
