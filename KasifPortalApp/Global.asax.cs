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
            "~/KasifPages/Forms/OgrenciBilgisiDuzenle.aspx");

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
            "~/KasifPages/Forms/HocaBilgisiDuzenle.aspx");

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
            "~/KasifPages/Forms/DersKonuBilgisiDuzenle.aspx");

            routes.MapPageRoute(
            "MufredatTakip-page",
            "MufredatTakip",
            "~/KasifPages/Tables/MufredatBilgisi.aspx");
            routes.MapPageRoute(
            "MufredatTakip-page-add",
            "MufredatTakip/add",
            "~/KasifPages/Forms/MufredatTakipEkle.aspx");
            routes.MapPageRoute(
            "MufredatTakip-page-edit",
            "MufredatTakip/edit/{param}",
            "~/KasifPages/Forms/MufredatTakipDuzenle.aspx");

            routes.MapPageRoute(
           "DevamsizlikBilgi-page",
           "DevamsizlikBilgi",
           "~/KasifPages/Tables/DevamsizlikBilgisi.aspx");
            routes.MapPageRoute(
            "DevamsizlikBilgi-page-add",
            "DevamsizlikBilgi/add",
            "~/KasifPages/Forms/DevamsizlikBilgisiEkle.aspx");
            routes.MapPageRoute(
            "DevamsizlikBilgi-page-edit",
            "DevamsizlikBilgi/edit/{param}",
            "~/KasifPages/Forms/DevamsizlikBilgisiDuzenle.aspx");

            routes.MapPageRoute(
           "TestBilgi-page",
           "TestBilgi",
           "~/KasifPages/Tables/TestBilgisi.aspx");
            routes.MapPageRoute(
            "TestBilgi-page-add",
            "TestBilgi/add",
            "~/KasifPages/Forms/TestBilgisiEkle.aspx");
            routes.MapPageRoute(
            "TestBilgi-page-edit",
            "TestBilgi/edit/{param}",
            "~/KasifPages/Forms/TestBilgisiDuzenle.aspx");

            routes.MapPageRoute(
           "TestCozmeDurumu-page",
           "TestCozmeDurumu",
           "~/KasifPages/Tables/TestCozmeDurumu.aspx");
            routes.MapPageRoute(
            "TestCozmeDurumu-page-add",
            "TestCozmeDurumu/add",
            "~/KasifPages/Forms/TestCozmeDurumuEkle.aspx");
            routes.MapPageRoute(
            "TestCozmeDurumu-page-edit",
            "TestCozmeDurumu/edit/{param}",
            "~/KasifPages/Forms/TestCozmeDurumuDuzenle.aspx");

            routes.MapPageRoute(
           "UserTable-page",
           "UserTable",
           "~/Management/Tables/UserTable.aspx");
            routes.MapPageRoute(
            "UserTable-page-add",
            "UserTable/add",
            "~/Management/Forms/UserTableAdd.aspx");
            routes.MapPageRoute(
            "UserTable-page-edit",
            "UserTable/edit/{param}",
            "~/Management/Forms/UserTableEdit.aspx");

            routes.MapPageRoute(
            "ChangePwd-page",
            "ChangePwd",
            "~/Management/Tables/ChangePwd.aspx");



            Utilities.RouteHandler.Routes = routes;

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