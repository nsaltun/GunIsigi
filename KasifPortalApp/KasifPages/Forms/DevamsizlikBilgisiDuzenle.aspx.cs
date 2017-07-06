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
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static KasifBusiness.DB_Operations.DBObjects.ConstDbCommands;

namespace KasifPortalApp.KasifPages.Forms
{
    public partial class DevamsizlikBilgisiDuzenle : BasePage
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
            PageOperations PageOps;
            List<NameValue> lstDataSource;

            #region Fill Mahalle
            PageOps = new PageOperations();
            lstDataSource = new List<NameValue>();
            bool isFirstIteration = true;

            List<BOLGE_INFO> lstBolgeInfo = PageOps.RunQueryForPage<BOLGE_INFO>(DbCommandList.GET_BOLGE_INFO, null, null);

            if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString())
            {
                lstBolgeInfo = lstBolgeInfo.Where(x => x.GUID == lstKisiBilgi[0].BOLGE_ID).ToList();
            }

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
            //if (ksfSI.IsAdmin == 1)
            //{
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
            //}
            //else
            //{
            //    NameValue[] NameValueArr = new NameValue[1];
            //    NameValueArr[0] = new NameValue();
            //    NameValueArr[0].Name = "Öğrenci";
            //    NameValueArr[0].Value = "1";

            //    slcTip.DataSource = NameValueArr;
            //    slcTip.DataTextField = "Name";
            //    slcTip.DataValueField = "Value";
            //    slcTip.DataBind();
            //}


            #endregion

            #region FillSinif
            List<NameValue> lstNameValue = new List<NameValue>();
            if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString())
            {
                lstNameValue.Add(new NameValue
                {
                    Name = lstKisiBilgi[0].SINIF.ToString(),
                    Value = lstKisiBilgi[0].SINIF.ToString()
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
                List<DevamsizlikBilgiObj> lstScreenInfoObj = null;
                lstScreenInfoObj = PageOps.RunQueryForPage<DevamsizlikBilgiObj>(DbCommandList.GET_MUFREDAT_BILGI,
                                                                            new string[] { "P_MUFREDAT_GUID" },
                                                                            new object[] { decryptedQueryString });
                //ViewState.Add("dvmszlkGuid", lstScreenInfoObj[0].DEVAMSIZLIK_GUID);
                Page.Session["dvmszlkGuid"] = lstScreenInfoObj[0].DEVAMSIZLIK_GUID;
                FillKisi(lstScreenInfoObj[0].TIP_ID);

                //Filling Inputs on the screen
                txtTarih.Value = lstScreenInfoObj[0].DATE;
                slcSinif.Value = lstScreenInfoObj[0].SINIF.ToString();
                slcMahalle.Value = lstScreenInfoObj[0].BOLGE_ID.ToString();
                slcTip.Value = lstScreenInfoObj[0].TIP_ID.ToString();
                slcKisi.Value = lstScreenInfoObj[0].KISI_GUID.ToString();
                slcDevamDurumu.Value = lstScreenInfoObj[0].DEVAM_DURUMU_ID.ToString();
                txtSebep.Value = lstScreenInfoObj[0].SEBEP;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void FillKisi(int tip)
        {
            try
            {
                #region Fill Kişi

                PageOperations PageOps = new PageOperations();
                List<NameValue> lstDataSource = new List<NameValue>();
                lstKisiBilgi = PageOps.RunQueryForPage<HocaAndOgrenciObj>(DbCommandList.GET_HOCA_AND_OGRENCI, null, null);

                lstDataSource = new List<NameValue>();
                if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString())
                {
                    lstKisiBilgi = lstKisiBilgi.Where(x => x.HOCA_GUID == ksfSI.HocaGuid).ToList();
                    foreach (var item in lstKisiBilgi.Where(x => x.TIP == tip && x.HOCA_GUID == ksfSI.HocaGuid).ToList())//Default olarak öğrenciler dolduruluyor.
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
                    foreach (var item in lstKisiBilgi.Where(x => x.TIP == tip).ToList())//Default olarak öğrenciler dolduruluyor.
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
                DEVAMSIZLIK_BILGI DevamsizlikBilgiObj = new DEVAMSIZLIK_BILGI();
                DevamsizlikBilgiObj.GUID = Convert.ToInt64(lstPostData[0]);
                DevamsizlikBilgiObj.DATE = lstPostData[1];
                DevamsizlikBilgiObj.KISI_ID = Convert.ToInt64(lstPostData[2]);
                DevamsizlikBilgiObj.TIP = Convert.ToInt16(lstPostData[3]);
                DevamsizlikBilgiObj.DURUM = Convert.ToInt16(lstPostData[4]);
                DevamsizlikBilgiObj.SEBEP = lstPostData[5];
                DbOperations.Update(DevamsizlikBilgiObj);
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
        public static string[] ProcessOperation(string Tarih, string KisiId, string Tip, string DevamDurumu, string Sebep)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string rowGuid = context.Session["dvmszlkGuid"].ToString();
            //var konuAdi = (context.Request["KonuAdi"] != null && context.Request["KonuAdi"] != "") ? Convert.ToString(context.Request["KonuAdi"]) : "";
            //var dersId = (context.Request["DersId"] != null && context.Request["DersId"] != "") ? Convert.ToString(context.Request["DersId"]) : "";
            string[] postData = new string[] { rowGuid, Tarih, KisiId, Tip, DevamDurumu, Sebep };
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