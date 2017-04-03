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
    public partial class TestCozmeDurumuEkle : BasePage
    {
        public string pageTitle = "Test Çözme Durumu Ekle";
        public string standardErr = "İşlem Başarılı";
        public List<DERS_BILGI> lstDersBilgi;
        public List<DersKonuBilgiObj> lstKonuBilgi;
        public List<TestBilgiObj> lstTestBilgi;
        public List<OGR_BILGI> lstOgrBilgi;
        public string pageName = "TestCozmeDurumu-page";

        public override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ResultStatus resultStatus = ResultStatus.Error;
                if (!Page.IsPostBack)
                {
                    LoadParameters();
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
            #region Fill Hafta
            PageOperations PageOps = new PageOperations();
            List<NameValue> lstDataSource = new List<NameValue>();
            List<HOCA_BILGI> lstHocaBilgi = PageOps.RunQueryForPage<HOCA_BILGI>(DbCommandList.PRM_HOCA, null, null);
            lstDataSource = new List<NameValue>();

            List<HAFTA_BILGI> lstHaftaBilgi = PageOps.RunQueryForPage<HAFTA_BILGI>(DbCommandList.GET_HAFTA_BILGI, null, null);
            lstDataSource = new List<NameValue>();
            foreach (var item in lstHaftaBilgi)
            {
                lstDataSource.Add(new NameValue
                {
                    Name = item.HAFTA_ADI + " : " + item.TARIH,
                    Value = item.HAFTA_ID.ToString()
                });
            }

            if (lstDataSource != null && lstDataSource.Count > 0)
            {
                slcHafta.DataSource = lstDataSource.ToArray();
                slcHafta.DataTextField = "Name";
                slcHafta.DataValueField = "Value";
                slcHafta.DataBind();
            }
            else
            {
                slcHafta.DataSource = null;
                slcHafta.DataBind();
            }
            #endregion

            #region Fill DersAdı
            lstDataSource = null;
            PageOps = null;
            PageOps = new PageOperations();
            lstDataSource = new List<NameValue>();
            lstDersBilgi = PageOps.RunQueryForPage<DERS_BILGI>(DbCommandList.GET_DERS_BILGI, null, null);
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

            #region Fill KonuAdı
            lstDataSource = null;
            PageOps = null;
            PageOps = new PageOperations();
            lstDataSource = new List<NameValue>();
            lstKonuBilgi = PageOps.RunQueryForPage<DersKonuBilgiObj>(DbCommandList.GET_DERS_KONU_BILGI, null, null);
            List<DersKonuBilgiObj> lstCurrentKonuBilgiObj = lstKonuBilgi.Where<DersKonuBilgiObj>(x => x.DERS_GUID.ToString() == slcDersAdi.Value).ToList();
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

            #region Test Adı
            lstDataSource = null;
            PageOps = null;
            PageOps = new PageOperations();
            lstDataSource = new List<NameValue>();
            lstTestBilgi = PageOps.RunQueryForPage<TestBilgiObj>(DbCommandList.GET_TEST_BILGI, null, null);
            List<TestBilgiObj> lstCurrentTestBilgi = lstTestBilgi.Where<TestBilgiObj>(x => x.KONU_ID.ToString() == slcKonuAdi.Value).ToList();
            foreach (var item in lstCurrentTestBilgi)
            {
                lstDataSource.Add(new NameValue
                {
                    Name = item.TEST_NO + ". " + item.TEST_ADI,
                    Value = item.TEST_GUID.ToString()
                });
            }

            if (lstDataSource != null && lstDataSource.Count > 0)
            {
                slcTestAdi.DataSource = lstDataSource.ToArray();
                slcTestAdi.DataTextField = "Name";
                slcTestAdi.DataValueField = "Value";
                slcTestAdi.DataBind();
            }
            else
            {
                slcTestAdi.DataSource = null;
                slcTestAdi.DataBind();
            }
            #endregion

            #region Fill Ogrenci

            lstDataSource = null;
            PageOps = null;
            PageOps = new PageOperations();
            lstDataSource = new List<NameValue>();
            lstOgrBilgi = PageOps.RunQueryForPage<OGR_BILGI>(DbCommandList.PRM_OGR, null, null);

            lstDataSource = new List<NameValue>();
            if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString())
            {
                lstOgrBilgi = lstOgrBilgi.Where(x => x.HOCA_GUID == ksfSI.HocaGuid).ToList();
                List<OGR_BILGI> lstCurrentOgrBilgiObj = lstOgrBilgi.Where<OGR_BILGI>(x => x.CLASS.ToString() == slcSinif.Value
                                                                                        && x.HOCA_GUID == ksfSI.HocaGuid).ToList();
                foreach (var item in lstCurrentOgrBilgiObj)//Default olarak öğrenciler dolduruluyor.
                {
                    lstDataSource.Add(new NameValue
                    {
                        Name = item.NAME + " " + item.SURNAME,
                        Value = item.GUID.ToString()
                    });
                }
            }
            else
            {
                foreach (var item in lstOgrBilgi.Where(x => x.CLASS.ToString() == slcSinif.Value).ToList())//Default olarak öğrenciler dolduruluyor.
                {
                    lstDataSource.Add(new NameValue
                    {
                        Name = item.NAME + " " + item.SURNAME,
                        Value = item.GUID.ToString()
                    });
                }
            }


            //List<OGR_BILGI> lstCurrentOgrBilgiObj = lstOgrBilgi.Where<OGR_BILGI>(x => x.CLASS.ToString() == slcSinif.Value).ToList();


            //foreach (var item in lstCurrentOgrBilgiObj)
            //{
            //    lstDataSource.Add(new NameValue
            //    {
            //        Name = item.NAME + " " + item.SURNAME,
            //        Value = item.GUID.ToString()
            //    });
            //}

            if (lstDataSource != null && lstDataSource.Count > 0)
            {
                slcOgr.DataSource = lstDataSource.ToArray();
                slcOgr.DataTextField = "Name";
                slcOgr.DataValueField = "Value";
                slcOgr.DataBind();
            }
            else
            {
                slcOgr.DataSource = null;
                slcOgr.DataBind();
            }
            #endregion

            return true;
        }

        private static bool FillParametersAndInsertDb(string[] lstPostData, ref string errMsg)
        {
            try
            {
                OGR_TEST_REL TestCozmeDurumuObj = new OGR_TEST_REL();
                TestCozmeDurumuObj.TEST_ID = Convert.ToInt64(lstPostData[0]);
                TestCozmeDurumuObj.OGR_ID = Convert.ToInt64(lstPostData[1]);
                TestCozmeDurumuObj.DURUM = Convert.ToInt16(lstPostData[2]);
                TestCozmeDurumuObj.DOGRU_SAYISI = Convert.ToInt32(lstPostData[3]);
                TestCozmeDurumuObj.YANLIS_SAYISI = Convert.ToInt32(lstPostData[4]);
                TestCozmeDurumuObj.HAFTA_ID = Convert.ToInt64(lstPostData[5]);
                DbOperations.Insert(TestCozmeDurumuObj);
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

        [WebMethod]
        public static string[] ProcessOperation(string TestId, string OgrId, string Durum, string DogruSayisi, string YanlisSayisi, string HaftaId)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string[] postData = new string[] { TestId, OgrId, Durum, DogruSayisi, YanlisSayisi, HaftaId };
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