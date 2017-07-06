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
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static KasifBusiness.DB_Operations.DBObjects.ConstDbCommands;
using static KasifPortalApp.Utilities.UtilityScreenFunctions;

namespace KasifPortalApp.KasifPages.Forms
{
    public partial class HocaBilgisiDuzenle : BasePage
    {
        public string pageTitle = "Hoca Bilgisi Düzenle";
        public string standardErr = "İşlem Başarılı";
        public string pageName = "HocaBilgi-page";

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
                    if (FillParameters())
                    {
                        standardErr = "İşlem Başarılı.";
                        resultStatus = ResultStatus.Success;
                    }
                    else
                    {
                        isOk = false;
                        standardErr = "İşlem gerçekleştirilirken bir hata oluştu.";
                        resultStatus = ResultStatus.Error;
                    }

                    RaisePopUp(standardErr, resultStatus);
                }

            }
            catch (Exception)
            {
                standardErr = "İşlem gerçekleştirilirken bir hata oluştu.";
                RaisePopUp(standardErr, ResultStatus.Error);
            }
        }
        private bool LoadParameters()
        {
            #region FillMahalleBilgisi
            PageOperations PageOps = new PageOperations();
            List<NameValue> lstDataSource = new List<NameValue>();
            List<BOLGE_INFO> lstBolgeInfo = PageOps.RunQueryForPage<BOLGE_INFO>(DbCommandList.GET_BOLGE_INFO, null, null);

            lstDataSource.Clear();
            lstDataSource = new List<NameValue>();

            foreach (var item in lstBolgeInfo)
            {
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
                List<HocaBilgiObj> lstScreenInfoObj = null;
                lstScreenInfoObj = PageOps.RunQueryForPage<HocaBilgiObj>(DbCommandList.GET_HOCA_BILGI,
                                                                            new string[] { "P_HOCA_ID" },
                                                                            new object[] { decryptedQueryString });
                ViewState.Add("hocaGuid", lstScreenInfoObj[0].HOCA_GUID);

                //Filling Inputs on the screen
                //txtOgrAdi.Value = lstScreenInfoObj
                txtAd.Value = lstScreenInfoObj[0].HOCA_ADI;
                txtSoyad.Value = lstScreenInfoObj[0].HOCA_SOYADI;
                slcSinif.Value = lstScreenInfoObj[0].SINIF.ToString();
                slcMahalle.Value = lstScreenInfoObj[0].HOCA_BOLGE_ID.ToString();
                txtTelNo.Value = lstScreenInfoObj[0].TEL_NO;
                txtEmail.Value = lstScreenInfoObj[0].EMAIL;
                txtDogumTarihi.Value = lstScreenInfoObj[0].DOGUM_TARIHI;
                txtDiger.Value = lstScreenInfoObj[0].DIGER;
            }
            catch (Exception ex)
            {
                exErr = ex.Message;
                //return false;
            }
        }


        private bool FillParameters()
        {
            try
            {
                HOCA_BILGI HocaObj = new HOCA_BILGI();
                HocaObj.GUID = Convert.ToInt64(ViewState["hocaGuid"].ToString());
                HocaObj.HOCA_ADI = txtAd.Value;
                HocaObj.HOCA_SOYADI = txtSoyad.Value;
                HocaObj.HOCA_BOLGE_ID = Convert.ToInt64(slcMahalle.Value);
                HocaObj.HOCA_DOGUM_TARIHI = txtDogumTarihi.Value;
                HocaObj.HOCA_EMAIL = txtEmail.Value;
                HocaObj.SINIF = Convert.ToInt16(slcSinif.Value);
                HocaObj.HOCA_TEL = txtTelNo.Value;
                HocaObj.DIGER = txtDiger.Value;

                DbOperations.Update(HocaObj);
                return true;
            }
            catch (Exception ex)
            {
                exErr = ex.Message;
                return false;
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

        public string GenerateListUrl()
        {
            return Page.GetRouteUrl(pageName, null);
        }



    }
}