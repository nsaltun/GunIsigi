using KasifBusiness.Business.KasifPageOperations;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects.ScreenObjects;
using KasifBusiness.Utilities;
using KasifPortalApp.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static KasifBusiness.DB_Operations.DBObjects.ConstDbCommands;
using static KasifPortalApp.Utilities.UtilityScreenFunctions;

namespace KasifPortalApp.KasifPages.Forms
{
    public partial class OgrenciBilgisiDuzenle : BasePage
    {
        public string pageTitle = "Öğrenci Bilgisi Düzenle";
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
            catch (Exception ex)
            {
                standardErr = "İşlem gerçekleştirilirken bir hata oluştu.";
                RaisePopUp(standardErr, ResultStatus.Error);
                throw ex;
            }
        }

        private void GetInfo()
        {
            try
            {
                //Read values from querystring and decrypt..
                string encPostedParam = (String)Page.RouteData.Values["param"];
                string decryptedQueryString = KasifHelper.DecryptStringFromBytes_Aes(encPostedParam);

                PageOperations PageOps = new PageOperations();
                List<PageOgrBilgiObj> lstScreenInfoObj = null;
                lstScreenInfoObj = PageOps.RunQueryForPage<PageOgrBilgiObj>(DbCommandList.GET_PAGE_OGR_BILGI,
                                                                            new string[] { "P_OGR_ID" },
                                                                            new object[] { decryptedQueryString });
                ViewState.Add("ogrGuid", lstScreenInfoObj[0].GUID);

                //Filling Inputs on the screen
                //txtOgrAdi.Value = lstScreenInfoObj
                txtOgrAdi.Value = lstScreenInfoObj[0].NAME;
                txtOgrSoyadi.Value = lstScreenInfoObj[0].SURNAME;
                slcSinif.Value = lstScreenInfoObj[0].CLASS.ToString();
                slcHocaBilgi.Value = lstScreenInfoObj[0].HOCA_GUID.ToString();
                slcMahalle.Value = lstScreenInfoObj[0].BOLGE_ID.ToString();
                txtOkul.Value = lstScreenInfoObj[0].SCHOOL_NAME;
                txtEmail.Value = lstScreenInfoObj[0].OGR_EMAIL;
                txtVeliEmail.Value = lstScreenInfoObj[0].PARENT_EMAIL;
                txtOgrTel.Value = lstScreenInfoObj[0].PHONE;
                txtDogumTarihi.Value = lstScreenInfoObj[0].DATE_OF_BIRTH;
                txtDogumYeri.Value = lstScreenInfoObj[0].BIRT_PLACE;
                txtVeliAdi.Value = lstScreenInfoObj[0].PARENT_NAME;
                txtVeliTel.Value = lstScreenInfoObj[0].PARENT_PHONE;
                txtOkulNo.Value = lstScreenInfoObj[0].OGR_NO.ToString();
                txtDiger.Value = lstScreenInfoObj[0].DIGER;
            }
            catch (Exception ex)
            {
                exErr = ex.Message;
                //return false;
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

            if (ksfSI.RoleName.ToUpperInvariant() == RoleNames.HOCA.ToString())
            {
                lstBolgeInfo = lstBolgeInfo.Where(x => x.GUID == lstScreenInfoObj[0].HOCA_BOLGE_ID).ToList();
            }

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
                OgrBilgiObj.GUID = Convert.ToInt64(ViewState["ogrGuid"].ToString());
                OOgrBilgi OgrBilgi = new OOgrBilgi(OgrBilgiObj);

                //List<OGR_BILGI> lstOgr = new List<OGR_BILGI>();
                //DbOperations.FindBy<OGR_BILGI>(ref lstOgr, OgrBilgiObj, x => x.GUID ==  );



                OgrBilgi.Update();
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