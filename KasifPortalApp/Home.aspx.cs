using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace KasifPortalApp
{
    public partial class Home : BasePage
    {
        public string ogrBilgiUrl = "";
        public string DevamsizlikUrl = "";
        public string hocaBilgiUrl = "";
        public string MufredatTakipUrl= "";
        public string etkinlikUrl = "";
        public string dersBilgisiUrl = "";
        public string dersKonuBilgisiUrl= "";
        public string testBilgisiUrl= "";
        public string testTakibiUrl = "";
        public string userTableUrl = "";

        public List<MENU_TILES> lstMenuTiles;

        public override void Page_Load(object sender, EventArgs e)
        {
            ogrBilgiUrl = Page.GetRouteUrl("OgrBilgi-page", null);
            hocaBilgiUrl = Page.GetRouteUrl("HocaBilgi-page", null);
            DevamsizlikUrl = Page.GetRouteUrl("DevamsizlikBilgi-page", null);
            MufredatTakipUrl = Page.GetRouteUrl("MufredatTakip-page", null);
            etkinlikUrl = Page.GetRouteUrl("OgrBilgi-page", null);
            dersBilgisiUrl = Page.GetRouteUrl("DersBilgi-page", null);
            dersKonuBilgisiUrl = Page.GetRouteUrl("DersKonuBilgi-page", null);
            testBilgisiUrl = Page.GetRouteUrl("TestBilgi-page", null);
            testTakibiUrl = Page.GetRouteUrl("TestCozmeDurumu-page", null);
            userTableUrl = Page.GetRouteUrl("UserTable-page", null);

            FillMenuTiles();
        }

        public void FillMenuTiles()
        {
            SessionInfo ksfSi = (SessionInfo)Session["KsfSessionInfo"];
            lstMenuTiles = ksfSi.MenuTiles;
        }
    }
}