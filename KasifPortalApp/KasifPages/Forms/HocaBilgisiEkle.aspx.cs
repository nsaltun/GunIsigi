using KasifBusiness.Business.KasifPageOperations;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects.ScreenObjects;
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
    public partial class HocaBilgisiEkle : System.Web.UI.Page
    {
        public string pageTitle = "Hoca Bilgisi Ekle";
        public string standardErr = "İşlem Başarılı";
        public string pageName = "HocaBilgi-page";

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


        private bool FillParameters()
        {
            try
            {
                HOCA_BILGI HocaBilgiObj = new HOCA_BILGI();
                HocaBilgiObj.HOCA_ADI = txtAd.Value;
                HocaBilgiObj.HOCA_SOYADI = txtSoyad.Value;
                HocaBilgiObj.HOCA_BOLGE_ID = Convert.ToInt64(slcMahalle.Value);
                HocaBilgiObj.HOCA_DOGUM_TARIHI = txtDogumTarihi.Value;
                HocaBilgiObj.HOCA_EMAIL = txtEmail.Value;
                HocaBilgiObj.SINIF = Convert.ToInt16(slcSinif.Value);
                HocaBilgiObj.HOCA_TEL = txtTelNo.Value;

                DbOperations.Insert(HocaBilgiObj);
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