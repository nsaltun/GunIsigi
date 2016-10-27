using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KasifPortalApp.KasifPages.Forms
{
    public partial class OgrenciBilgisiEkle : System.Web.UI.Page
    {
        public string pageTitle = "Öğrenci Bilgisi Ekle";
        public string standardErr = "İşlem Başarılı";

        bool isOk = true;
        string exErr = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ResultStatus resultStatus = ResultStatus.Error;
                if (!Page.IsPostBack)
                {

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

        private bool FillParameters()
        {
            try
            {
                OGR_BILGI OgrBilgiObj = new OGR_BILGI();
                OgrBilgiObj.BIRT_PLACE = txtDogumYeri.Value;
                OgrBilgiObj.CLASS = Convert.ToInt16(slcSinif.Value);

                OgrBilgiObj.DATE_OF_BIRTH = Convert.ToDateTime(txtDogumTarihi.Value).ToShortDateString();
                OgrBilgiObj.DISTRICT = txtMahalle.Value;
                OgrBilgiObj.NAME = txtOgrAdi.Value;
                OgrBilgiObj.OGR_EMAIL = txtEmail.Value;
                OgrBilgiObj.OGR_ID = 100;
                OgrBilgiObj.OGR_NO = Convert.ToInt32(txtOkulNo.Value);
                OgrBilgiObj.PARENT_EMAIL = txtVeliEmail.Value;
                OgrBilgiObj.PARENT_NAME = txtVeliAdi.Value;
                OgrBilgiObj.PARENT_PHONE = txtVeliTel.Value;
                OgrBilgiObj.PHONE = txtOgrTel.Value;
                OgrBilgiObj.SCHOOL_NAME = txtOkul.Value;
                OgrBilgiObj.SURNAME = txtOgrSoyadi.Value;

                OOgrBilgi OgrBilgi = new OOgrBilgi(OgrBilgiObj);
                OgrBilgi.DoJob();
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
                String script = "<script>$(document).ready(function () {showSuccessModal('" + pageTitle + "','" + msg + "','" + Page.GetRouteUrl("OgrBilgi-page", null) + "');});</script>";
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
            return Page.GetRouteUrl("OgrBilgi-page", null);
        }

        enum ResultStatus
        {
            Success,
            Error
        }

    }
}