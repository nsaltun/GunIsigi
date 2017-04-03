using KasifBusiness.Business.Login;
using KasifBusiness.Objects;
using KasifPortalApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KasifPortalApp.Authentication
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {

            }
            else
            {
                ClearSession();
            }
        }

        private void ClearSession()
        {
            Session.Clear();
        }

        private void DoLogin()
        {
            try
            {
                OLogin LoginOperation = new OLogin(txtEmail.Value, txtPwd.Value);

                if (LoginOperation.Execute())
                {
                    FillSession(LoginOperation);
                    LoadVariables();
                    Redirect();
                }
                else
                {
                    lblError.InnerHtml = "Giriş bilgilerinizi kontrol edip tekrar deneyin.";
                    lblError.Style.Add("display", "block");
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void Redirect()
        {
            Response.Redirect(Page.GetRouteUrl("home-page", null));
        }

        private void FillSession(OLogin LoginOprObj)
        {
            SessionInfo sessionObj = new SessionInfo();
            Session["KsfSessionInfo"] = LoginOprObj.SessionObj;

        }
        private void LoadVariables()
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DoLogin();
        }

        private void RaisePopUp(string msg, ResultStatus resultStatus)
        {
            if (resultStatus == ResultStatus.Success)
            {
                String script = "<script>$(document).ready(function () {showSuccessModal('Login','" + msg + "','" + Page.GetRouteUrl("home-page", null) + "');});</script>";
                ClientScript.RegisterStartupScript(typeof(Page), "ProcessError", script);
            }
            else
            {
                String script = "<script>$(document).ready(function () {showErrorModal( 'Login - Hata','" + msg + "');});</script>";
                ClientScript.RegisterStartupScript(typeof(Page), "ProcessError", script);
            }
        }
    }
}