using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects;
using KasifBusiness.Utilities;
using KasifPortalApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using DbQueryName = KasifBusiness.DB_Operations.DBObjects.ConstDbCommands.DbCommandList;

namespace KasifPortalApp.Management.Tables
{
    public partial class ChangePwd : BasePage
    {
        public string pageTitle = "Şifreni Değiştir";
        public string standardErr = "İşlem Başarılı";
        public string pageName = "ChangePwd-page";
        public SessionInfo KsfSI;
        public List<OGR_BILGI> lstOgrBilgi;
        public List<HOCA_BILGI> lstHocaBilgi;

        public override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                KsfSI = (SessionInfo)Session["KsfSessionInfo"];

                if (!Page.IsPostBack)
                {

                }
                else //Case of Postback
                {
                    if (PerformPwdControls())
                    {
                        DoChangePwd();
                        RaisePopUp(standardErr, ResultStatus.Success);
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

        private bool PerformPwdControls()
        {
            //Check new password length
            if (txtNewPwd.Value.Length < 6 || txtNewPwdAgain.Value.Length < 6)
            {
                RaisePopUp("Yeni şifrenizin uzunluğu en az 6 karakter olmalıdır.", ResultStatus.Error);
                return false;
            }
            //Check new passwords equality
            if (txtNewPwd.Value != txtNewPwdAgain.Value)
            {
                RaisePopUp("Yeni şifreler birbiriyle uyuşmuyor. Lütfen kontrol edip tekrar giriniz.", ResultStatus.Error);
                return false;
            }
            //Check password complexity(includes number and char)
            if (!CheckPasswordComplexity(txtNewPwd.Value))
            {
                RaisePopUp("Şifreniz en az bir harf ve en az bir rakam içermelidir.", ResultStatus.Error);
                return false;
            }
            if (txtNewPwd.Value == txtOldPwd.Value)
            {
                RaisePopUp("Yeni şifre eski şifreyle aynı olamaz.", ResultStatus.Error);
                return false;
            }
            //Check whether old password equal with user input from DB
            if (!CheckOldPasswordCorrectness(txtOldPwd.Value))
            {
                RaisePopUp("Girmiş olduğunuz eski şifre yanlış. Lütfen kontrol edip tekrar giriniz.", ResultStatus.Error);
                return false;
            }


            return true;
        }

        private void DoChangePwd()
        {
            try
            {
                List<USER_USER> lstUser = new List<USER_USER>();
                USER_USER usrObj = new USER_USER();
                DbOperations.FindBy<USER_USER>(ref lstUser, usrObj, x => x.GUID == KsfSI.UserGuid);
                lstUser[0].PASSWORD = KasifHelper.GetSha512HashedData(txtNewPwd.Value);
                lstUser[0].LAST_PWD_CHANGE_DATE = KasifHelper.GetCurrentDate();
                DbOperations.Update(lstUser[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckOldPasswordCorrectness(string oldPwd)
        {
            try
            {
                List<USER_USER> lstUser = new List<USER_USER>();
                string[] prmNames = new string[] { "P_EMAIL", "P_PASSWORD" };
                object[] prmValues = new object[] { KsfSI.Email, KasifHelper.GetSha512HashedData(oldPwd) };
                DbOperations.RunDbQuery<USER_USER>(ref lstUser, DbQueryName.GET_USER_BY_EMAIL, prmNames, prmValues);
                if (lstUser == null || lstUser.Count == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckPasswordComplexity(string newPwd)
        {
            if (newPwd.Any(char.IsLetterOrDigit))//En az bir harf ve en az bir rakam içeriyorsa true döner.
                return true;
            else
                return false;
        }

        private void RaisePopUp(string msg, ResultStatus resultStatus)
        {
            if (resultStatus == ResultStatus.Success)
            {
                String script = "<script>$(document).ready(function () {showSuccessModal('" + pageTitle + "','" + msg + "','" + Page.GetRouteUrl("home-page", null) + "');});</script>";
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

    }
}