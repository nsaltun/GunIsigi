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

namespace KasifPortalApp.KasifPages.Forms
{
    public partial class TestBilgisiDuzenle : BasePage
    {
        public string pageTitle = "Test Bilgisi Düzenle";
        public string standardErr = "İşlem Başarılı";
        public List<DERS_BILGI> lstDersBilgi;
        public List<DersKonuBilgiObj> lstKonuBilgi;
        List<TestBilgiObj> lstScreenInfoObj = null;
        public string pageName = "TestBilgi-page";

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

            }
            catch (Exception ex)
            {
                standardErr = "İşlem gerçekleştirilirken bir hata oluştu.";
                string errMsg = ex.Message;

                if (ex.InnerException != null)
                {
                    errMsg += " - innerEx : " + ex.InnerException.Message;
                }

                RaisePopUp(errMsg, ResultStatus.Error);

                throw ex;
            }
        }

        private bool LoadParameters()
        {

            PageOperations PageOps = null;
            List<NameValue> lstDataSource = null;

            #region GetTestBilgi
            //Read values from querystring and decrypt..
            string encPostedParam = (String)Page.RouteData.Values["param"];
            string decryptedQueryString = KasifHelper.DecryptStringFromBytes_Aes(encPostedParam);
            PageOps = new PageOperations();
            lstScreenInfoObj = PageOps.RunQueryForPage<TestBilgiObj>(DbCommandList.GET_TEST_BILGI,
                                                                        new string[] { "P_TEST_GUID" },
                                                                        new object[] { decryptedQueryString });
            Page.Session["testGuid"] = lstScreenInfoObj[0].TEST_GUID;
            #endregion

            #region FillSinif
            List<NameValue> lstNameValue = new List<NameValue>();
            if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString())
            {
                lstNameValue.Add(new NameValue
                {
                    Name = lstScreenInfoObj[0].SINIF.ToString(),
                    Value = lstScreenInfoObj[0].SINIF.ToString()
                });
            }
            else
            {
                for (int i = 5; i <= 8; i++)
                {
                    lstNameValue.Add(new NameValue
                    {
                        Name = i.ToString(),
                        Value = i.ToString()
                    });
                }
            }
            slcSinif.DataSource = lstNameValue.ToArray();
            slcSinif.DataTextField = "Name";
            slcSinif.DataValueField = "Value";
            slcSinif.DataBind();


            #endregion

            #region Fill DersAdı
            PageOps = new PageOperations();
            lstDataSource = new List<NameValue>();
            lstDersBilgi = PageOps.RunQueryForPage<DERS_BILGI>(DbCommandList.GET_DERS_BILGI, null, null);
            List<DERS_BILGI> lstCurrentDersBilgiObj = lstDersBilgi.Where<DERS_BILGI>(x => x.SINIF == lstScreenInfoObj[0].SINIF).ToList();

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

            #region Fill KonuAdı
            lstDataSource = null;
            PageOps = null;
            PageOps = new PageOperations();
            lstDataSource = new List<NameValue>();
            lstKonuBilgi = PageOps.RunQueryForPage<DersKonuBilgiObj>(DbCommandList.GET_DERS_KONU_BILGI, null, null);
            List<DersKonuBilgiObj> lstCurrentKonuBilgiObj = lstKonuBilgi.Where<DersKonuBilgiObj>(x => x.DERS_GUID == lstScreenInfoObj[0].DERS_ID).ToList();
            foreach (var item in lstCurrentKonuBilgiObj)
            {
                lstDataSource.Add(new NameValue
                {
                    Name = item.KONU,
                    Value = item.DERS_KONU_GUID.ToString()
                });
            }

            if (lstDataSource != null && lstDataSource.Count > 0)
            {
                slcKonuAdi.DataSource = lstDataSource.ToArray();
                slcKonuAdi.DataTextField = "Name";
                slcKonuAdi.DataValueField = "Value";
                slcKonuAdi.DataBind();
            }
            else
            {
                slcKonuAdi.DataSource = null;
                slcKonuAdi.DataBind();
            }
            #endregion

            #region Fill Hafta (@commented)
            //List<HAFTA_BILGI> lstHaftaBilgi = PageOps.RunQueryForPage<HAFTA_BILGI>(DbCommandList.GET_HAFTA_BILGI, null, null);
            //lstDataSource = new List<NameValue>();
            //foreach (var item in lstHaftaBilgi)
            //{
            //    lstDataSource.Add(new NameValue
            //    {
            //        Name = item.HAFTA_ADI + " : " + item.TARIH,
            //        Value = item.HAFTA_ID.ToString()
            //    });
            //}

            //if (lstDataSource != null && lstDataSource.Count > 0)
            //{
            //    slcHafta.DataSource = lstDataSource.ToArray();
            //    slcHafta.DataTextField = "Name";
            //    slcHafta.DataValueField = "Value";
            //    slcHafta.DataBind();
            //}
            //else
            //{
            //    slcHafta.DataSource = null;
            //    slcHafta.DataBind();
            //}
            #endregion

            return true;
        }

        private void GetInfo()
        {
            try
            {
                //Filling Inputs on the screen
                slcSinif.Value = lstScreenInfoObj[0].SINIF.ToString();
                slcDersAdi.Value = lstScreenInfoObj[0].DERS_ID.ToString();
                slcKonuAdi.Value = lstScreenInfoObj[0].KONU_ID.ToString();
                txtTestNo.Value = lstScreenInfoObj[0].TEST_NO.ToString();
                txtTestAdi.Value = lstScreenInfoObj[0].TEST_ADI;
                txtTarih.Value = lstScreenInfoObj[0].TARIH;
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
                TEST_BILGI TestBilgiObj = new TEST_BILGI();
                TestBilgiObj.GUID = Convert.ToInt64(lstPostData[0]);
                TestBilgiObj.TEST_ADI = lstPostData[1];
                TestBilgiObj.TEST_NO = Convert.ToInt16(lstPostData[2]);
                TestBilgiObj.DERS_ID = Convert.ToInt64(lstPostData[3]);
                TestBilgiObj.DERS_KONU_ID = Convert.ToInt64(lstPostData[4]);
                TestBilgiObj.TARIH = lstPostData[5];
                DbOperations.Update(TestBilgiObj);
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                if (ex.InnerException != null)
                {
                    errMsg += " - inner ex : " + ex.InnerException.Message;
                }
                return false;
            }
        }

        [WebMethod(EnableSession = true)]
        public static string[] ProcessOperation(string TestAdi, string TestNo, string DersId, string KonuId, string Tarih)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string rowGuid = context.Session["testGuid"].ToString();
            string[] postData = new string[] { rowGuid,TestAdi, TestNo, DersId, KonuId, Tarih };
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