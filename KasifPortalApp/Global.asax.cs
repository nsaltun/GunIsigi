using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace KasifPortalApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RegisterRoutes(RouteTable.Routes);
        }
        protected void RegisterRoutes(RouteCollection routes)
        {
            routes.MapPageRoute(
            "login-page",
            "login",
            "~/Authentication/LoginPage.aspx");

            routes.MapPageRoute(
            "home-page",
            "home",
            "~/Home.aspx");

            routes.MapPageRoute(
            "OgrBilgi-page",
            "OgrBilgi",
            "~/KasifPages/Tables/OgrenciBilgisi.aspx");
            routes.MapPageRoute(
            "OgrBilgi-page-add",
            "OgrBilgi/add",
            "~/KasifPages/Forms/OgrenciBilgisiEkle.aspx");
            routes.MapPageRoute(
            "OgrBilgi-page-edit",
            "OgrBilgi/edit/{param}",
            "~/KasifPages/Forms/OgrenciBilgisiGuncelle.aspx");
        }





        #region Unused Global.asax Methods
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
        #endregion
    }
}