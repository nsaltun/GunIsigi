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
    public partial class DevamsizlikBilgisiEkle : BasePage
    {
        public string pageTitle = "Devamsızlık Bilgisi Ekle";
        public string standardErr = "İşlem Başarılı";
        public List<HocaAndOgrenciObj> lstKisiBilgi;
        public string pageName = "DevamsizlikBilgi-page";

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
            #region Fill Kişi

            PageOperations PageOps = new PageOperations();
            List<NameValue> lstDataSource = new List<NameValue>();
            lstKisiBilgi = PageOps.RunQueryForPage<HocaAndOgrenciObj>(DbCommandList.GET_HOCA_AND_OGRENCI, null, null);

            lstDataSource = new List<NameValue>();
            if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString())
            {
                lstKisiBilgi = lstKisiBilgi.Where(x => x.TIP == 1 && x.HOCA_GUID == ksfSI.HocaGuid).ToList();
                foreach (var item in lstKisiBilgi.Where(x => x.TIP == 1 && x.HOCA_GUID == ksfSI.HocaGuid).ToList())//Default olarak öğrenciler dolduruluyor.
                {
                    lstDataSource.Add(new NameValue
                    {
                        Name = item.AD_SOYAD,
                        Value = item.GUID.ToString()
                    });
                }
            }
            else
            {
                foreach (var item in lstKisiBilgi.Where(x => x.TIP == 1).ToList())//Default olarak öğrenciler dolduruluyor.
                {
                    lstDataSource.Add(new NameValue
                    {
                        Name = item.AD_SOYAD,
                        Value = item.GUID.ToString()
                    });
                }
            }


            if (lstDataSource != null && lstDataSource.Count > 0)
            {
                slcKisi.DataSource = lstDataSource.ToArray();
                slcKisi.DataTextField = "Name";
                slcKisi.DataValueField = "Value";
                slcKisi.DataBind();
            }
            else
            {
                slcKisi.DataSource = null;
                slcKisi.DataBind();
            }
            #endregion

            #region Fill Hafta
            lstDataSource = null;
            PageOps = null;
            PageOps = new PageOperations();
            lstDataSource = new List<NameValue>();
            List<HAFTA_BILGI> lstHaftaBilgi = PageOps.RunQueryForPage<HAFTA_BILGI>(DbCommandList.GET_HAFTA_BILGI, null, null);
            lstDataSource = new List<NameValue>();
            bool isFirstIteration = true;
            foreach (var item in lstHaftaBilgi)
            {
                if (isFirstIteration)
                {
                    lstDataSource.Add(new NameValue
                    {
                        Name = "Hafta Seç..",
                        Value = ""
                    });
                    isFirstIteration = false;
                }
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

            #region Fill Mahalle
            List<BOLGE_INFO> lstBolgeInfo = PageOps.RunQueryForPage<BOLGE_INFO>(DbCommandList.GET_BOLGE_INFO, null, null);

            lstDataSource.Clear();
            lstDataSource = new List<NameValue>();
            isFirstIteration = true;
            foreach (var item in lstBolgeInfo)
            {
                if (isFirstIteration)
                {
                    lstDataSource.Add(new NameValue
                    {
                        Name = "Mahalle Seç..",
                        Value = ""
                    });
                    isFirstIteration = false;
                }
                lstDataSource.Add(new NameValue
                {
                    Name = item.BOLGE_ADI,
                    Value = item.GUID.ToString()
                });
            }

            if (lstDataSource != null && lstDataSource.Count > 0)
            {
                slcMahalle.DataSource = lstDataSource.ToArray();
                slcMahalle.DataTextField = "Name";
                slcMahalle.DataValueField = "Value";
                slcMahalle.DataBind();
            }
            else
            {
                slcMahalle.DataSource = null;
                slcMahalle.DataBind();
            }
            #endregion

            #region Fill Tip
            if (ksfSI.IsAdmin == 1)
            {
                NameValue[] NameValueArr = new NameValue[2];
                NameValueArr[0] = new NameValue();
                NameValueArr[0].Name = "Öğrenci";
                NameValueArr[0].Value = "1";
                NameValueArr[1] = new NameValue();
                NameValueArr[1].Name = "Hoca";
                NameValueArr[1].Value = "2";

                slcTip.DataSource = NameValueArr;
                slcTip.DataTextField = "Name";
                slcTip.DataValueField = "Value";
                slcTip.DataBind();
            }
            else
            {
                NameValue[] NameValueArr = new NameValue[1];
                NameValueArr[0] = new NameValue();
                NameValueArr[0].Name = "Öğrenci";
                NameValueArr[0].Value = "1";

                slcTip.DataSource = NameValueArr;
                slcTip.DataTextField = "Name";
                slcTip.DataValueField = "Value";
                slcTip.DataBind();
            }


            #endregion

            return true;
        }

        private static bool FillParametersAndInsertDb(string[] lstPostData, ref string errMsg)
        {
            try
            {
                DEVAMSIZLIK_BILGI DevamsizlikBilgiObj = new DEVAMSIZLIK_BILGI();
                DevamsizlikBilgiObj.HAFTA_ID = Convert.ToInt32(lstPostData[0]);
                DevamsizlikBilgiObj.KISI_ID = Convert.ToInt64(lstPostData[1]);
                DevamsizlikBilgiObj.TIP = Convert.ToInt16(lstPostData[2]);
                DevamsizlikBilgiObj.DURUM = Convert.ToInt16(lstPostData[3]);
                DevamsizlikBilgiObj.SEBEP = lstPostData[4];
                DbOperations.Insert(DevamsizlikBilgiObj);
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
        public static string[] ProcessOperation(string Hafta, string KisiId, string Tip, string DevamDurumu, string Sebep)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            //var konuAdi = (context.Request["KonuAdi"] != null && context.Request["KonuAdi"] != "") ? Convert.ToString(context.Request["KonuAdi"]) : "";
            //var dersId = (context.Request["DersId"] != null && context.Request["DersId"] != "") ? Convert.ToString(context.Request["DersId"]) : "";
            string[] postData = new string[] { Hafta, KisiId, Tip, DevamDurumu, Sebep };
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