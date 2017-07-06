using KasifBusiness.Business.KasifPageOperations;
using KasifBusiness.Business.User;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects;
using KasifBusiness.Objects.ScreenObjects;
using KasifPortalApp.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;
using static KasifBusiness.DB_Operations.DBObjects.ConstDbCommands;

namespace KasifPortalApp.Management.Tables
{
    public partial class UserTable : BasePage
    {
        public string pageTitle = "Kullanıcı Bilgisi";
        public string standardErr = "İşlem Başarılı";
        public string pageName = "UserTable-page";
        public string deleteUrl = "";

        public override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SessionInfo sessionInfo = (SessionInfo)Session["KsfSessionInfo"];

                deleteUrl = pageName + "/DeleteCurrentRow";
                PageOperations PageOps = new PageOperations();
                List<UserTableObj> lstScreenInfoObj = PageOps.RunQueryForPage<UserTableObj>(DbCommandList.GET_USER_INFO, null, null);

                //Eğer bu oturumdaki kullanıcı adminse ve Owner rolüne sahipse : bu user'ın guid bilgileri boş set edilsin.
                if (sessionInfo.IsAdmin == 1 && sessionInfo.RoleGuid == 2)
                {
                    //List<UserTableObj> lstCantDeleted = (List<UserTableObj>)lstScreenInfoObj.Select(x => x.ROLE_GUID == 2 && x.IS_ADMIN == "Evet");
                }

                //lstScreenInfoObj.Where(x=>x.ROLE_GUID==2 && sessionInfo.IsAdmin && sessionInfo.RoleGuid==2?)



                tblRepeater.DataSource = lstScreenInfoObj;
                tblRepeater.DataBind();
            }
            catch (Exception ex)
            {
                standardErr = "İşlem gerçekleştirilirken bir hata oluştu.";
                RaisePopUp(ex.Message, ResultStatus.Error);
            }
        }

        private void RaisePopUp(string msg, ResultStatus resultStatus)
        {
            if (resultStatus == ResultStatus.Success)
            {
                String script = "<script>$(document).ready(function () {showSuccessModal('" + pageTitle + "','" + msg + "','" + Page.GetRouteUrl(pageName, null) + "');});</script>";
                ClientScript.RegisterStartupScript(typeof(Page), "ProcessError", script);
            }
            else
            {
                String script = "<script>$(document).ready(function () {showErrorModal('" + pageTitle + " - Hata','" + msg + "');});</script>";
                ClientScript.RegisterStartupScript(typeof(Page), "ProcessError", script);
            }

        }

        public string GenerateAddUrl()
        {
            return Page.GetRouteUrl(pageName + "-add", null);
        }

        [WebMethod()]
        public static string[] DeleteCurrentRow(string RowGuid, string RoleGuid)
        {
            try
            {
                USER_USER userUSerObj = new USER_USER();
                userUSerObj.GUID = Convert.ToInt64(RowGuid);
                DbOperations.Delete(userUSerObj);

                long lRoleGuid = Convert.ToInt64(RoleGuid);
                long lRowGuid = Convert.ToInt64(RowGuid);
                OUserRoleOwnership oUro = new OUserRoleOwnership();
                USER_ROLE_OWNERSHIP uroObj = oUro.FindByKey(x => x.ROLE_GUID == lRoleGuid &&
                                    x.USER_GUID == lRowGuid);
                DbOperations.Delete(uroObj);

                return new string[] { "success", "Silme işlemi başarılı" };
            }
            catch (Exception ex)
            {
                return new string[] { ex.Message, ex.Message };
            }

        }
    }
}