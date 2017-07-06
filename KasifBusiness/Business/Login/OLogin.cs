using KasifBusiness.DB_Operations.DBObjects;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects;
using KasifBusiness.Objects.ScreenObjects;
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
        public string msg = "";

        USER_USER userObj;
        List<MenuTreeItemObject> lstMenuTreeObj;
        List<MenuTreeItemObject> lstAllowedPagesObj;
        List<MENU_TILES> lstMenuTile;
        UserTableObj userInfo;
        string userId;
        byte[] hashedPwd;

        public OLogin(string userId, string pwd)
        {
            this.userId = userId;
            this.hashedPwd = KasifHelper.GetSha512HashedData(pwd);
        }

        public bool Execute()
        {
            try
            {
                userObj = CheckUserExist();
                if (userObj != null)
                {
                    if (!FillUserInfo())
                        return false;
                    if (!FillMenuTreeObject())
                        return false;
                    if (!FillMenuTiles())
                        return false;
                    if (!FillAllowedPages())
                        return false;

                    FillSessionObject();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private USER_USER CheckUserExist()
        {
            try
            {
                List<USER_USER> lstUser = new List<USER_USER>();
                string[] prmNames = new string[] { "P_EMAIL", "P_PASSWORD" };
                object[] prmValues = new object[] { userId, this.hashedPwd };
                //string[] prmNames = new string[] { "P_EMAIL" };
                //object[] prmValues = new object[] { userId };
                DbOperations.RunDbQuery<USER_USER>(ref lstUser, DbCommands.GET_USER_BY_EMAIL, prmNames, prmValues);
                if (lstUser != null && lstUser.Count > 0)
                {
                    msg += "CheckUserExist is OK!<br/>";
                    return lstUser[0];
                }
                else
                {
                    msg += "CheckUserExist user not found!<br/>";
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool FillMenuTreeObject()
        {

            try
            {
                string[] prmNames = new string[] { "P_EMAIL" };
                object[] prmValues = new object[] { userId };
                DbOperations.RunDbQuery<MenuTreeItemObject>(ref lstMenuTreeObj, DbCommands.GET_USER_MENUS, prmNames, prmValues);
                if (lstMenuTreeObj != null && lstMenuTreeObj.Count > 0)
                {
                    msg += "FillMenuTree is OK!<br/>";
                    return true;
                }
                else
                {
                    msg += "FillMenuTree not found!<br/>";
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool FillMenuTiles()
        {
            try
            {
                List<MENU_TILES> lstTemp = new List<MENU_TILES>();
                lstMenuTile = new List<MENU_TILES>();
                DbOperations.RunDbQuery<MENU_TILES>(ref lstTemp, DbCommands.GET_MENU_TILES, null, null);
                if (lstTemp != null && lstTemp.Count > 0)
                {
                    if (lstMenuTreeObj != null && lstMenuTreeObj.Count > 0)
                    {
                        foreach (var menuTileItem in lstTemp)
                        {
                            foreach (var menuTreeitem in lstMenuTreeObj)
                            {
                                if ((menuTileItem.NODE_GUID == menuTreeitem.NODE_GUID && menuTileItem.TILE_TYPE == "PRIVATE")
                                    || menuTileItem.TILE_TYPE == "PUBLIC")
                                {
                                    lstMenuTile.Add(menuTileItem);
                                    break;
                                }
                            }
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //Login olan herkesin görebildiği ekranları çeker. ayrıca izin verilen Menu Tree ekranlarını da buradaki listeye ekler.
        private bool FillAllowedPages()
        {
            try
            {
                DbOperations.RunDbQuery<MenuTreeItemObject>(ref lstAllowedPagesObj, DbCommands.GET_ALLOWED_PAGES, null, null);
                if (lstAllowedPagesObj != null && lstAllowedPagesObj.Count > 0)
                {
                    List<MenuTreeItemObject> lstTempMenuTree = new List<MenuTreeItemObject>();
                    MenuTreeItemObject currentObj = new MenuTreeItemObject();
                    lstTempMenuTree.AddRange(lstMenuTreeObj);
                    foreach (MenuTreeItemObject obj in lstAllowedPagesObj)
                    {
                        currentObj = lstTempMenuTree.Where(x => x.NODE_GUID == obj.NODE_GUID).FirstOrDefault();
                        if (currentObj != null)
                            lstTempMenuTree.Remove(currentObj);
                    }

                    lstAllowedPagesObj.AddRange(lstTempMenuTree);


                    msg += "FillAllowedPages is OK!<br/>";
                    return true;
                }
                else
                {
                    msg += "FillAllowedPages not found!<br/>";
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private bool FillUserInfo()
        {
            try
            {
                List<UserTableObj> lstUserInfo = null;
                string[] prmNames = new string[] { "P_EMAIL" };
                object[] prmValues = new object[] { userId };
                DbOperations.RunDbQuery<UserTableObj>(ref lstUserInfo, DbCommands.GET_USER_INFO, prmNames, prmValues);
                if (lstUserInfo != null && lstUserInfo.Count > 0)
                {
                    msg += "FillUserInfo is OK!<br/>";

                    userInfo = lstUserInfo[0];
                    return true;
                }
                else
                {
                    msg += "FillUserInfo not found!<br/>";
                    userInfo = null;
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void FillSessionObject()
        {
            SessionObj = new SessionInfo();
            SessionObj.Email = userObj.EMAIL;
            SessionObj.IsAdmin = userObj.IS_ADMIN;
            //SessionObj.UserId = userObj.USER_ID;
            SessionObj.UserGuid = userObj.GUID;
            SessionObj.MenuTreeItemInfo = lstMenuTreeObj;
            SessionObj.lstAllowedPages = lstAllowedPagesObj;
            SessionObj.MenuTiles = lstMenuTile;
            SessionObj.RoleGuid = userInfo.ROLE_GUID;
            SessionObj.RoleName = userInfo.ROLE_NAME;
            SessionObj.SessionGuid = KasifHelper.GuidFactory();
            SessionObj.userNameSurname = userInfo.NAME + " " + userInfo.SURNAME;
            SessionObj.HocaGuid = userInfo.HOCA_GUID;
            SessionObj.OgrenciGuid = userInfo.OGR_GUID;

        }

    }
}
