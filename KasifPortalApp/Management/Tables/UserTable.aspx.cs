using KasifBusiness.Business.KasifPageOperations;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects.ScreenObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static KasifBusiness.DB_Operations.DBObjects.ConstDbCommands;
using static KasifPortalApp.Utilities.UtilityScreenFunctions;

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
                deleteUrl = pageName + "/DeleteCurrentRow";
                PageOperations PageOps = new PageOperations();
                List<UserTableObj> lstScreenInfoObj = PageOps.RunQueryForPage<UserTableObj>(DbCommandList.GET_USER_INFO, null, null);

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
        public static string DeleteCurrentRow(string RowGuid, string RoleGuid)
        {
            try
            {
                USER_USER userUSerObj = new USER_USER();
                userUSerObj.GUID = Convert.ToInt64(RowGuid);
                DbOperations.Delete(userUSerObj);

                //USER_ROLE_OWNERSHIP uroObj = new USER_ROLE_OWNERSHIP();
                //uroObj.ROLE_GUID = Convert.ToInt64(RoleGuid);
                //uroObj.USER_GUID = Convert.ToInt64(RowGuid);
                //DbOperations.Delete(uroObj);



                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}