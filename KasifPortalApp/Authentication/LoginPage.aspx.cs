using KasifBusiness.Business.Login;
using KasifBusiness.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
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
                DoLogin();
            }
        }

        private void DoLogin()
        {
            OLogin LoginOperation = new OLogin(txtEmail.Value, txtPwd.Value);
            if (LoginOperation.Execute())
            {
                FillSession(LoginOperation);
                LoadVariables();
                Redirect();
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

    }
}