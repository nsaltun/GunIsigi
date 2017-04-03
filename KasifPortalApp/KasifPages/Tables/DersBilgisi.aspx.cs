using KasifBusiness.Business.KasifPageOperations;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifPortalApp.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;
using static KasifBusiness.DB_Operations.DBObjects.ConstDbCommands;

namespace KasifPortalApp.KasifPages.Tables
{
    public partial class DersBilgisi : BasePage
    {
        public string pageTitle = "Ders Bilgisi";
        public string standardErr = "İşlem Başarılı";
        public string pageName = "DersBilgi-page";

        public override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                PageOperations PageOps = new PageOperations();
                List<DERS_BILGI> lstScreenInfoObj = PageOps.RunQueryForPage<DERS_BILGI>(DbCommandList.GET_DERS_BILGI, null, null);

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
        public static string DeleteCurrentRow(string RowGuid)
        {
            try
            {
                DERS_BILGI dersBilgiObj = new DERS_BILGI();
                dersBilgiObj.GUID = Convert.ToInt64(RowGuid);
                DbOperations.Delete(dersBilgiObj);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}