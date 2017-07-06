using KasifBusiness.Business.KasifPageOperations;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects.ScreenObjects;
using KasifBusiness.Utilities;
using KasifPortalApp.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;
using static KasifBusiness.DB_Operations.DBObjects.ConstDbCommands;

namespace KasifPortalApp.KasifPages.Tables
{
    public partial class HocaBilgisi : BasePage
    {
        public string pageTitle = "Hoca Bilgisi";
        public string standardErr = "İşlem Başarılı";
        public string pageName = "HocaBilgi-page";

        public override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                PageOperations PageOps = new PageOperations();
                //List<HocaBilgiObj> lstScreenInfoObj = PageOps.RunQueryForPage<HocaBilgiObj>(DbCommandList.GET_HOCA_BILGI, null, null);
                List<HocaBilgiObj> lstScreenInfoObj = null;

                if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString() || ksfSI.RoleName.ToUpperInvariant() == RoleNames.VELI.ToString())
                {
                    lstScreenInfoObj = PageOps.RunQueryForPage<HocaBilgiObj>(DbCommandList.GET_HOCA_BILGI,
                                                                                new string[] { "P_HOCA_ID" },
                                                                                new object[] { ksfSI.HocaGuid });
                }
                else
                {
                    lstScreenInfoObj = PageOps.RunQueryForPage<HocaBilgiObj>(DbCommandList.GET_HOCA_BILGI, null, null);
                }

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

        public string GenerateEditUrl(string key)
        {
            key = KasifHelper.EncryptStringToBytes_Aes(key);
            return Page.GetRouteUrl(pageName + "-edit", new { param = key });
        }

        [WebMethod()]
        public static string[] DeleteCurrentRow(string RowGuid)
        {
            try
            {
                HOCA_BILGI hocaBilgiObj = new HOCA_BILGI();
                hocaBilgiObj.GUID = Convert.ToInt64(RowGuid);
                DbOperations.Delete(hocaBilgiObj);
                return new string[] { "success", "Silme işlemi başarılı" };
            }
            catch (Exception ex)
            {
                return new string[] { ex.Message, ex.Message };
            }

        }

    }
}