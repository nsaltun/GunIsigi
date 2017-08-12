using KasifBusiness.Business.KasifPageOperations;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects.ScreenObjects;
using KasifBusiness.Utilities;
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
    public partial class DersKonuBilgisiDuzenle : BasePage
    {
        public string pageTitle = "Ders Konu Bilgisi Düzenle";
        public string standardErr = "İşlem Başarılı";
        public List<DERS_BILGI> lstDersBilgi;
        List<DersKonuBilgiObj> lstScreenInfoObj = null;

        public string pageName = "DersKonuBilgi-page";

        bool isOk = true;
        string exErr = "";

        public override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ResultStatus resultStatus = ResultStatus.Error;
                if (!Page.IsPostBack)
                {
                    LoadParameters();
                    GetInfo();
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
            lstDersBilgi = PageOps.RunQueryForPage<DERS_BILGI>(DbCommandList.GET_DERS_BILGI, null, null);
            
            #endregion

            #region FillSinif
            List<NameValue> lstNameValue = new List<NameValue>();

            for (int i = 5; i <= 8; i++)
            {
                lstNameValue.Add(new NameValue
                {
                    Name = i.ToString(),
                    Value = i.ToString()
                });
            }

            slcSinif.DataSource = lstNameValue.ToArray();
            slcSinif.DataTextField = "Name";
            slcSinif.DataValueField = "Value";
            slcSinif.DataBind();


            #endregion

            return true;
        }

        private void GetInfo()
        {
            try
            {
                //Read values from querystring and decrypt..
                string encPostedParam = (String)Page.RouteData.Values["param"];
                string decryptedQueryString = KasifHelper.DecryptStringFromBytes_Aes(encPostedParam);

                PageOperations PageOps = new PageOperations();
                lstScreenInfoObj = PageOps.RunQueryForPage<DersKonuBilgiObj>(DbCommandList.GET_DERS_KONU_BILGI,
                                                                            new string[] { "P_DERS_KONU_GUID" },
                                                                            new object[] { decryptedQueryString });
                
                //ViewState.Add("dvmszlkGuid", lstScreenInfoObj[0].DEVAMSIZLIK_GUID);
                Page.Session["dersKonuGuid"] = lstScreenInfoObj[0].DERS_KONU_GUID;

                //Filling Inputs on the screen
                txtTarih.Value = lstScreenInfoObj[0].TARIH;
                slcSinif.Value = lstScreenInfoObj[0].SINIF.ToString();
                slcDersAdi.Value = lstScreenInfoObj[0].DERS_GUID.ToString();
                txtAd.Value = lstScreenInfoObj[0].KONU;

                #region Fill Ders combobox
                List<DERS_BILGI> lstCurrentDersBilgiObj = lstDersBilgi.Where<DERS_BILGI>(x => x.SINIF.ToString() == slcSinif.Value).ToList();
                List<NameValue> lstDataSource = new List<NameValue>();

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


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool FillParametersAndUpdateDb(string[] lstPostData, ref string errMsg)
        {
            try
            {
                //Convert.ToInt32((DateTime.ParseExact(lstPostData[3], "dd/mm/yyyy", System.Globalization.CultureInfo.InvariantCulture)).ToString("yyyymmdd"))
                
                DERS_KONU_BILGI DersKonuBilgiObj = new DERS_KONU_BILGI();
                DersKonuBilgiObj.GUID = Convert.ToInt64(lstPostData[0]);
                DersKonuBilgiObj.KONU = lstPostData[1];
                DersKonuBilgiObj.DERS_ID = Convert.ToInt64(lstPostData[2]);
                DersKonuBilgiObj.TARIH = KasifHelper.ConvertDateToInt32(lstPostData[3], "dd/mm/yyyy");

                DbOperations.Update(DersKonuBilgiObj);
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }

        [WebMethod]
        public static string[] ProcessOperation(string KonuAdi, string DersId, string Tarih)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            //var konuAdi = (context.Request["KonuAdi"] != null && context.Request["KonuAdi"] != "") ? Convert.ToString(context.Request["KonuAdi"]) : "";
            //var dersId = (context.Request["DersId"] != null && context.Request["DersId"] != "") ? Convert.ToString(context.Request["DersId"]) : "";
            string rowGuid = context.Session["dersKonuGuid"].ToString();
            string[] postData = new string[] { rowGuid, KonuAdi, DersId, Tarih };
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
                if (FillParametersAndUpdateDb(postData, ref errMsg))
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