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

            routes.MapPageRoute(
           "HocaBilgi-page",
           "HocaBilgi",
           "~/KasifPages/Tables/HocaBilgisi.aspx");
            routes.MapPageRoute(
            "HocaBilgi-page-add",
            "HocaBilgi/add",
            "~/KasifPages/Forms/HocaBilgisiEkle.aspx");
            routes.MapPageRoute(
            "HocaBilgi-page-edit",
            "HocaBilgi/edit/{param}",
            "~/KasifPages/Forms/HocaBilgisiGuncelle.aspx");

            routes.MapPageRoute(
            "DersBilgi-page",
            "DersBilgi",
            "~/KasifPages/Tables/DersBilgisi.aspx");
            routes.MapPageRoute(
            "DersBilgi-page-add",
            "DersBilgi/add",
            "~/KasifPages/Forms/DersBilgisiEkle.aspx");
            routes.MapPageRoute(
            "DersBilgi-page-edit",
            "DersBilgi/edit/{param}",
            "~/KasifPages/Forms/DersBilgisiGuncelle.aspx");

            routes.MapPageRoute(
            "DersKonuBilgi-page",
            "DersKonuBilgi",
            "~/KasifPages/Tables/DersKonuBilgisi.aspx");
            routes.MapPageRoute(
            "DersKonuBilgi-page-add",
            "DersKonuBilgi/add",
            "~/KasifPages/Forms/DersKonuBilgisiEkle.aspx");
            routes.MapPageRoute(
            "DersKonuBilgi-page-edit",
            "DersKonuBilgi/edit/{param}",
            "~/KasifPages/Forms/DersKonuBilgisiGuncelle.aspx");

           

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