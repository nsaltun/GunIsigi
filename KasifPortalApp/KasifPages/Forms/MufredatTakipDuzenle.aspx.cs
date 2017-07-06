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
    public partial class MufredatTakipDuzenle : BasePage
    {
        public string pageTitle = "Müfredat Takibi Bilgisi Düzenle";
        public string standardErr = "İşlem Başarılı";
        public List<DERS_BILGI> lstDersBilgi;
        public List<DersKonuBilgiObj> lstKonuBilgi;
        public string pageName = "MufredatTakip-page";
        List<MufredatTakipObj> lstScreenInfoObj = null;

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
            //Read values from querystring and decrypt..
            string encPostedParam = (String)Page.RouteData.Values["param"];
            string decryptedQueryString = KasifHelper.DecryptStringFromBytes_Aes(encPostedParam);

            PageOperations PageOps = new PageOperations();
            lstScreenInfoObj = PageOps.RunQueryForPage<MufredatTakipObj>(DbCommandList.GET_MUFREDAT_BILGI,
                                                                        new string[] { "P_MUFREDAT_GUID" },
                                                                        new object[] { decryptedQueryString });
            Page.Session["mufredatGuid"] = lstScreenInfoObj[0].MUFREDAT_GUID;

            #region Fill Hoca
            PageOps = new PageOperations();
            List<NameValue> lstDataSource = new List<NameValue>();
            List<HOCA_BILGI> lstHocaBilgi = PageOps.RunQueryForPage<HOCA_BILGI>(DbCommandList.PRM_HOCA, null, null);

            if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString())
            {
                lstHocaBilgi = lstHocaBilgi.Where(x => x.GUID == ksfSI.HocaGuid).ToList();
            }

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
            
            #region FillSinif
            List<NameValue> lstNameValue = new List<NameValue>();
            if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString())
            {
                lstNameValue.Add(new NameValue
                {
                    Name = lstHocaBilgi[0].SINIF.ToString(),
                    Value = lstHocaBilgi[0].SINIF.ToString()
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
            lstDataSource = null;
            PageOps = null;
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
            List<DersKonuBilgiObj> lstCurrentKonuBilgiObj = lstKonuBilgi.Where<DersKonuBilgiObj>(x => x.DERS_GUID == lstScreenInfoObj[0].DERS_GUID).ToList();
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

            #region Fill Hafta (@commented)
            //lstDataSource = null;
            //PageOps = null;
            //PageOps = new PageOperations();
            //lstDataSource = new List<NameValue>();
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
                
                slcHoca.Value = lstScreenInfoObj[0].HOCA_GUID.ToString();
                //Filling Inputs on the screen
                txtTarih.Value = lstScreenInfoObj[0].TARIH;
                slcSinif.Value = lstScreenInfoObj[0].SINIF.ToString();
                slcDersAdi.Value = lstScreenInfoObj[0].DERS_GUID.ToString();
                slcKonuAdi.Value = lstScreenInfoObj[0].KONU_GUID.ToString();
                slcTakipDurumu.Value = lstScreenInfoObj[0].TKP_ID.ToString();
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
                MUFREDAT_TAKIP MufredatTakipObj = new MUFREDAT_TAKIP();
                MufredatTakipObj.GUID = Convert.ToInt64(lstPostData[0]);
                MufredatTakipObj.HOCA_ID = Convert.ToInt64(lstPostData[1]);
                MufredatTakipObj.TARIH = lstPostData[2];
                MufredatTakipObj.SINIF = Convert.ToInt16(lstPostData[3]);
                MufredatTakipObj.DERS_ID = Convert.ToInt64(lstPostData[4]);
                MufredatTakipObj.DERS_KONU_ID = Convert.ToInt64(lstPostData[5]);
                MufredatTakipObj.TAKIP_DURUMU = Convert.ToInt16(lstPostData[6]);
                DbOperations.Update(MufredatTakipObj);
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
        public static string[] ProcessOperation(string Hoca, string Tarih, string Sinif, string DersId, string KonuAdi, string TakipDurumu)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            //var konuAdi = (context.Request["KonuAdi"] != null && context.Request["KonuAdi"] != "") ? Convert.ToString(context.Request["KonuAdi"]) : "";
            //var dersId = (context.Request["DersId"] != null && context.Request["DersId"] != "") ? Convert.ToString(context.Request["DersId"]) : "";
            string rowGuid = context.Session["mufredatGuid"].ToString();
            string[] postData = new string[] { rowGuid, Hoca, Tarih, Sinif, DersId, KonuAdi, TakipDurumu };
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