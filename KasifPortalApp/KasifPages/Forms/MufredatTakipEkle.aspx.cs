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
    public partial class MufredatTakipEkle : System.Web.UI.Page
    {
        public string pageTitle = "Müfredat Takibi Bilgisi Ekle";
        public string standardErr = "İşlem Başarılı";
        public List<DERS_BILGI> lstDersBilgi;
        public List<DersKonuBilgiObj> lstKonuBilgi;
        public string pageName = "MufredatTakip-page";

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
            #region Fill Hoca
            PageOperations PageOps = new PageOperations();
            List<NameValue> lstDataSource = new List<NameValue>();
            List<HOCA_BILGI> lstHocaBilgi = PageOps.RunQueryForPage<HOCA_BILGI>(DbCommandList.PRM_HOCA, null, null);

            lstDataSource = new List<NameValue>();
            foreach (var item in lstHocaBilgi)
            {
                lstDataSource.Add(new NameValue
                {
                    Name = item.HOCA_ADI + " " + item.HOCA_SOYADI,
                    Value = item.GUID.ToString()
                });
            }

            if (lstDataSource != null && lstDataSource.Count > 0)
            {
                slcHoca.DataSource = lstDataSource.ToArray();
                slcHoca.DataTextField = "Name";
                slcHoca.DataValueField = "Value";
                slcHoca.DataBind();
            }
            else
            {
                slcHoca.DataSource = null;
                slcHoca.DataBind();
            }
            #endregion

            #region Fill Hafta
            lstDataSource = null;
            PageOps = null;
            PageOps = new PageOperations();
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

            #region Fill TakipDurumu
            lstDataSource = null;
            PageOps = null;
            PageOps = new PageOperations();
            lstDataSource = new List<NameValue>();
            List<PRM_TAKIP_DURUMU> lstTakipDurumu = PageOps.RunQueryForPage<PRM_TAKIP_DURUMU>(DbCommandList.PRM_TAKIP_DURUMU, null, null);
            foreach (var item in lstTakipDurumu)
            {
                lstDataSource.Add(new NameValue
                {
                    Name = item.DESCRIPTION,
                    Value = item.ID.ToString()
                });
            }

            if (lstDataSource != null && lstDataSource.Count > 0)
            {
                slcTakipDurumu.DataSource = lstDataSource.ToArray();
                slcTakipDurumu.DataTextField = "Name";
                slcTakipDurumu.DataValueField = "Value";
                slcTakipDurumu.DataBind();
            }
            else
            {
                slcTakipDurumu.DataSource = null;
                slcTakipDurumu.DataBind();
            }
            #endregion

            return true;
        }

        private static bool FillParametersAndInsertDb(string[] lstPostData, ref string errMsg)
        {
            try
            {
                MUFREDAT_TAKIP MufredatTakipObj = new MUFREDAT_TAKIP();
                MufredatTakipObj.HOCA_ID = Convert.ToInt64(lstPostData[0]);
                MufredatTakipObj.HAFTA_ID = Convert.ToInt32(lstPostData[1]);
                MufredatTakipObj.SINIF = Convert.ToInt16(lstPostData[2]);
                MufredatTakipObj.DERS_ID = Convert.ToInt64(lstPostData[3]);
                MufredatTakipObj.DERS_KONU_ID = Convert.ToInt64(lstPostData[4]);
                MufredatTakipObj.TAKIP_DURUMU = Convert.ToInt16(lstPostData[5]);
                DbOperations.Insert(MufredatTakipObj);
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
        public static string[] ProcessOperation(string Hoca, string Hafta, string Sinif, string DersId, string KonuAdi, string TakipDurumu)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            //var konuAdi = (context.Request["KonuAdi"] != null && context.Request["KonuAdi"] != "") ? Convert.ToString(context.Request["KonuAdi"]) : "";
            //var dersId = (context.Request["DersId"] != null && context.Request["DersId"] != "") ? Convert.ToString(context.Request["DersId"]) : "";
            string[] postData = new string[] { Hoca, Hafta, Sinif, DersId, KonuAdi, TakipDurumu };
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