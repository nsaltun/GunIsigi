using KasifBusiness.Business.KasifPageOperations;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects.ScreenObjects;
using KasifPortalApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static KasifBusiness.DB_Operations.DBObjects.ConstDbCommands;
using static KasifPortalApp.Utilities.UtilityScreenFunctions;

namespace KasifPortalApp.KasifPages.Forms
{
    public partial class DersKonuBilgisiEkle : System.Web.UI.Page
    {
        public string pageTitle = "Ders Konu Bilgisi Ekle";
        public string standardErr = "İşlem Başarılı";
        public List<DERS_BILGI> lstDersBilgi;

        public string pageName = "DersKonuBilgi-page";

        bool isOk = true;
        string exErr = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ResultStatus resultStatus = ResultStatus.Error;
                if (!Page.IsPostBack)
                {
                    LoadParameters();
                }
                else
                {
                    string konuAdi = hiddenKonuAdi.Value;
                    string dersId = hiddenDersId.Value;

                }
                //else
                //{
                //    if (FillParameters())
                //    {
                //        standardErr = "İşlem Başarılı.";
                //        resultStatus = ResultStatus.Success;
                //    }
                //    else
                //    {
                //        isOk = false;
                //        standardErr = "İşlem gerçekleştirilirken bir hata oluştu.";
                //        resultStatus = ResultStatus.Error;
                //    }

                //    RaisePopUp(standardErr, resultStatus);
                //}

            }
            catch (Exception)
            {
                standardErr = "İşlem gerçekleştirilirken bir hata oluştu.";
                RaisePopUp(standardErr, ResultStatus.Error);
            }
        }
        private bool LoadParameters()
        {
            #region FillDersAdı
            PageOperations PageOps = new PageOperations();
            List<NameValue> lstDataSource = new List<NameValue>();
            lstDersBilgi = PageOps.RunQueryForPage<DERS_BILGI>(DbCommandList.GET_DERS_BILGI, null, null);

            lstDataSource = new List<NameValue>();

            List<DERS_BILGI> lstCurrentDersBilgiObj = lstDersBilgi.Where<DERS_BILGI>(x => x.SINIF.ToString() == slcSinif.Value).ToList();


            foreach (var item in lstCurrentDersBilgiObj)
            {
                lstDataSource.Add(new NameValue
                {
                    Name = item.DERS_ADI,
                    Value = item.GUID.ToString()
                });
            }

            if (lstDataSource != null && lstDataSource.Count > 0)
            {
                slcDersAdi.DataSource = lstDataSource.ToArray();
                slcDersAdi.DataTextField = "Name";
                slcDersAdi.DataValueField = "Value";
                slcDersAdi.DataBind();
            }
            else
            {
                slcDersAdi.DataSource = null;
                slcDersAdi.DataBind();
            }
            #endregion

            return true;
        }


        private static bool FillParametersAndInsertDb(string[] lstPostData, ref string errMsg)
        {
            try
            {
                DERS_KONU_BILGI DersKonuBilgiObj = new DERS_KONU_BILGI();
                DersKonuBilgiObj.KONU = lstPostData[0];
                DersKonuBilgiObj.DERS_ID = Convert.ToInt64(lstPostData[1]);

                DbOperations.Insert(DersKonuBilgiObj);
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

        [WebMethod]
        public static string[] ProcessOperation(string KonuAdi, string DersId)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            //var konuAdi = (context.Request["KonuAdi"] != null && context.Request["KonuAdi"] != "") ? Convert.ToString(context.Request["KonuAdi"]) : "";
            //var dersId = (context.Request["DersId"] != null && context.Request["DersId"] != "") ? Convert.ToString(context.Request["DersId"]) : "";
            string[] postData = new string[] { KonuAdi, DersId };
            string errorMessage = "";
            bool bControl = ProcessRequest(postData, ref errorMessage);
            if (bControl)
            {
                return new string[] { "success", "İşlem Başarılı." };
            }
            else
                return new string[] { "fail", errorMessage };
        }

        private static bool ProcessRequest(string[] postData, ref string errMsg)
        {
            try
            {
                if (FillParametersAndInsertDb(postData, ref errMsg))
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }



        #region Utilities 
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

        public string GenerateListUrl()
        {
            return Page.GetRouteUrl(pageName, null);
        }

        public string JsSerialize(object o)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(o);
        }
        #endregion
    }
}