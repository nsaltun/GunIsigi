using KasifBusiness.Business.KasifPageOperations;
using KasifBusiness.Business.User;
using KasifBusiness.DB_Operations.DBObjects;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects;
using KasifBusiness.Objects.CodeMgmt;
using KasifBusiness.Objects.ScreenObjects;
using KasifPortalApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using static KasifBusiness.DB_Operations.DBObjects.ConstDbCommands;
using static KasifPortalApp.Utilities.UtilityScreenFunctions;

namespace KasifPortalApp.Management.Forms
{
    public partial class UserTableAdd : BasePage
    {
        public string pageTitle = "Kullanıcı Bilgisi Ekle";
        public string standardErr = "İşlem Başarılı";
        public string pageName = "UserTable-page";
        public SessionInfo KsfSI;
        
        public List<OgrBilgi> lstOgrBilgi = new List<OgrBilgi>();
        public List<HOCA_BILGI> lstHocaBilgi;

        public override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ResultStatus resultStatus = ResultStatus.Error;
                KsfSI = (SessionInfo)Session["KsfSessionInfo"];

                if (!Page.IsPostBack)
                {
                    LoadParameters();
                }
                else
                {
                    ResultObject result = FillParameters();
                    if (result.isOk)
                    {
                        standardErr = "İşlem Başarılı.";
                        resultStatus = ResultStatus.Success;
                        RaisePopUp(standardErr, resultStatus);
                    }
                    else
                    {
                        standardErr = "İşlem gerçekleştirilirken bir hata oluştu.";
                        resultStatus = ResultStatus.Error;
                        RaisePopUp(result.errorMsg, resultStatus);
                    }

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


            #region Fill User Role Info
            PageOperations PageOps = new PageOperations();
            List<USER_ROLE> lstScreenInfoObj = PageOps.RunQueryForPage<USER_ROLE>(DbCommandList.PRM_USER_ROLES, null, null);
            List<NameValue> lstDataSource = new List<NameValue>();

            foreach (var item in lstScreenInfoObj)
            {
                lstDataSource.Add(new NameValue
                {
                    Name = item.ROLE_NAME,
                    Value = item.GUID.ToString()
                });
            }

            if (lstDataSource != null && lstDataSource.Count > 0)
            {
                slcRole.DataSource = lstDataSource.ToArray();
                slcRole.DataTextField = "Name";
                slcRole.DataValueField = "Value";
                slcRole.DataBind();
            }
            else
            {
                slcRole.DataSource = null;
                slcRole.DataBind();
            }
            #endregion

            #region Fill Mahalle Bilgisi
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

            #region Fill Ogrenci
            PageOps = new PageOperations();
            lstOgrBilgi = PageOps.RunQueryForPage<OgrBilgi>(DbCommandList.PRM_OGR, null, null);
            #endregion

            #region Fill Hoca
            lstDataSource = null;
            PageOps = new PageOperations();
            lstHocaBilgi = PageOps.RunQueryForPage<HOCA_BILGI>(DbCommandList.PRM_HOCA, null, null);
            #endregion

            ViewState.Clear();
            ViewState.Add("lstOgrBilgi", lstOgrBilgi);

            return true;
        }

        private ResultObject FillParameters()
        {
            try
            {
                USER_USER UserObj = new USER_USER();
                if (!String.IsNullOrEmpty(slcSinif.Value))
                {
                    UserObj.SINIF = Convert.ToInt16(slcSinif.Value);
                }
                if (!String.IsNullOrEmpty(slcMahalle.Value))
                {
                    UserObj.BOLGE_ID = Convert.ToInt64(slcMahalle.Value);
                }
                if (!String.IsNullOrEmpty(hiddenOgrId.Value))
                {
                    UserObj.OGR_GUID = Convert.ToInt64(hiddenOgrId.Value);
                    lstOgrBilgi = (List<OgrBilgi>)ViewState["lstOgrBilgi"];
                    UserObj.HOCA_GUID = lstOgrBilgi.Where(x => x.GUID == UserObj.OGR_GUID).ToList()[0].HOCA_GUID;
                }
                if (!String.IsNullOrEmpty(hiddenHocaId.Value))
                {
                    UserObj.HOCA_GUID = Convert.ToInt64(hiddenHocaId.Value);
                }
                UserObj.EMAIL = txtEmail.Value;
                UserObj.INSERT_USER = KsfSI.UserGuid;
                UserObj.IS_ADMIN = slcAdmin.Value == "1" ? short.Parse("1") : short.Parse("0");
                UserObj.NAME = txtAd.Value;
                UserObj.PASSWORD = KasifBusiness.Utilities.KasifHelper.GetSha512HashedData(txtPassword.Value);
                UserObj.SURNAME = txtSoyad.Value;

                OUser oUser = new OUser(UserObj);
                ResultObject result = oUser.OUserOperation();

                if (result.isOk)
                {
                    USER_ROLE_OWNERSHIP uRo = new USER_ROLE_OWNERSHIP();
                    uRo.USER_GUID = UserObj.GUID;
                    uRo.ROLE_GUID = Convert.ToInt64(slcRole.Value);
                    DbOperations.Insert(uRo);
                }
                return result;
            }
            catch (Exception ex)
            {
                ResultObject result = new ResultObject();
                result.errorMsg = ex.Message;
                result.errPrefix = "Beklenmeyen Hata. ";
                result.isOk = false;
                return result;
            }
        }

        private void RaisePopUp(string msg, ResultStatus resultStatus)
        {
            if (resultStatus == ResultStatus.Success)
            {
                String script = "<script>$(document).ready(function () {showSuccessModal('" + pageTitle + "','" + msg.Replace("\r\n", "") + "','" + Page.GetRouteUrl(pageName, null) + "');});</script>";
                ClientScript.RegisterStartupScript(typeof(Page), "ProcessError", script);
            }
            else
            {

                String script = "<script>$(document).ready(function () {showErrorModal(\"" + pageTitle + " - Hata\", \"" + msg.Replace("\r\n", "") + "\");});</script>";
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

    }
}