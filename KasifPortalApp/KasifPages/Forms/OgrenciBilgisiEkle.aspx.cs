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
    public partial class OgrenciBilgisiEkle : BasePage
    {
        public string pageTitle = "Öğrenci Bilgisi Ekle";
        public string standardErr = "İşlem Başarılı";
        public string pageName = "OgrBilgi-page";

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
            catch (Exception ex)
            {
                standardErr = "İşlem gerçekleştirilirken bir hata oluştu.";
                RaisePopUp(standardErr, ResultStatus.Error);
                throw ex;
            }
        }
        private bool LoadParameters()
        {
            #region FillHocaBilgisi
            PageOperations PageOps = new PageOperations();
            List<HocaBilgiObj> lstScreenInfoObj = PageOps.RunQueryForPage<HocaBilgiObj>(DbCommandList.GET_HOCA_BILGI, null, null);
            List<NameValue> lstDataSource = new List<NameValue>();

            //Login olan kullanıcı Hoca rolündeyse sadece o hocanın bilgisi gelir.
            if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString())
            {
                lstScreenInfoObj = lstScreenInfoObj.Where(x => x.HOCA_GUID == ksfSI.HocaGuid).ToList();
            }

            foreach (var item in lstScreenInfoObj)
            {
                lstDataSource.Add(new NameValue
                {
                    Name = item.HOCA_ADI + " " + item.HOCA_SOYADI + " - " + item.SINIF + ". sınıf",
                    Value = item.HOCA_GUID.ToString()
                });
            }

            if (lstDataSource != null && lstDataSource.Count > 0)
            {
                slcHocaBilgi.DataSource = lstDataSource.ToArray();
                slcHocaBilgi.DataTextField = "Name";
                slcHocaBilgi.DataValueField = "Value";
                slcHocaBilgi.DataBind();
            }
            else
            {
                slcHocaBilgi.DataSource = null;
                slcHocaBilgi.DataBind();
            }
            #endregion

            #region FillMahalleBilgisi
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
                OGR_BILGI OgrBilgiObj = new OGR_BILGI();
                OgrBilgiObj.BIRT_PLACE = txtDogumYeri.Value;
                if (!String.IsNullOrEmpty(slcSinif.Value))
                {
                    OgrBilgiObj.CLASS = Convert.ToInt16(slcSinif.Value);
                }
                OgrBilgiObj.DATE_OF_BIRTH = txtDogumTarihi.Value;
                OgrBilgiObj.BOLGE_ID = Convert.ToInt64(slcMahalle.Value);
                OgrBilgiObj.NAME = txtOgrAdi.Value;
                OgrBilgiObj.OGR_EMAIL = txtEmail.Value;
                OgrBilgiObj.OGR_ID = 100;
                if (!String.IsNullOrEmpty(txtOkulNo.Value))
                {
                    OgrBilgiObj.OGR_NO = Convert.ToInt32(txtOkulNo.Value);
                }
                OgrBilgiObj.PARENT_EMAIL = txtVeliEmail.Value;
                OgrBilgiObj.PARENT_NAME = txtVeliAdi.Value;
                OgrBilgiObj.PARENT_PHONE = txtVeliTel.Value;
                OgrBilgiObj.PHONE = txtOgrTel.Value;
                OgrBilgiObj.SCHOOL_NAME = txtOkul.Value;
                OgrBilgiObj.SURNAME = txtOgrSoyadi.Value;
                OgrBilgiObj.HOCA_GUID = Convert.ToInt64(slcHocaBilgi.Value);
                OgrBilgiObj.DIGER = txtDiger.Value;

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