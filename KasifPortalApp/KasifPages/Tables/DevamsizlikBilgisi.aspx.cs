using KasifBusiness.Business.KasifPageOperations;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects.ScreenObjects;
using KasifPortalApp.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;
using static KasifBusiness.DB_Operations.DBObjects.ConstDbCommands;

namespace KasifPortalApp.KasifPages.Tables
{
    public partial class DevamsizlikBilgisi : BasePage
    {
        public string pageTitle = "Devamsızlık Bilgisi";
        public string standardErr = "İşlem Başarılı";
        public string pageName = "DevamsizlikBilgi-page";

        public override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string[] paramNames = null;
                object[] paramValues = null;
                PageOperations PageOps = new PageOperations();
                List<DevamsizlikBilgiObj> lstScreenInfoObj = null;

                if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString())
                {
                    paramNames = new string[] { "P_HOCA_ID" };
                    paramValues = new object[] { ksfSI.HocaGuid };
                    lstScreenInfoObj = PageOps.RunQueryForPage<DevamsizlikBilgiObj>(DbCommandList.GET_DEVAMSIZLIK_BILGI, paramNames, paramValues);
                }
                else if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.OGRENCI.ToString() || ksfSI.RoleName.ToUpperInvariant() == RoleNames.VELI.ToString())
                {
                    paramNames = new string[] { "P_OGR_ID" };
                    paramValues = new object[] { ksfSI.OgrenciGuid };
                    lstScreenInfoObj = PageOps.RunQueryForPage<DevamsizlikBilgiObj>(DbCommandList.GET_OGR_DEVAMSIZLIK_BILGI, paramNames, paramValues);
                }
                else//Hem Hocaların hem de öğrencilerin devamsızlık bilgilerini getirir.
                {
                    lstScreenInfoObj = PageOps.RunQueryForPage<DevamsizlikBilgiObj>(DbCommandList.GET_DEVAMSIZLIK_BILGI, null, null);
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

        [WebMethod()]
        public static string DeleteCurrentRow(string RowGuid)
        {
            try
            {
                DEVAMSIZLIK_BILGI devamsizlikBilgiObj = new DEVAMSIZLIK_BILGI();
                devamsizlikBilgiObj.GUID = Convert.ToInt64(RowGuid);
                DbOperations.Delete(devamsizlikBilgiObj);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        
    }
}