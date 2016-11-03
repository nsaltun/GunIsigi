using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KasifPortalApp
{
    public partial class Home : System.Web.UI.Page
    {
        public string ogrBilgiUrl = "";
        public string ogrDevamsizlikUrl = "";
        public string hocaDevamsizlikUrl = "";
        public string hocaBilgiUrl = "";
        public string MufredatBilgisiUrl= "";
        public string etkinlikUrl = "";
        public string dersBilgisiUrl = "";
        public string dersKonuBilgisiUrl= "";
        public string testBilgisiUrl= "";
        public string testTakibiUrl = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            ogrBilgiUrl = Page.GetRouteUrl("OgrBilgi-page", null);
            hocaBilgiUrl = Page.GetRouteUrl("HocaBilgi-page", null);
            ogrDevamsizlikUrl = Page.GetRouteUrl("OgrBilgi-page", null);
            hocaDevamsizlikUrl = Page.GetRouteUrl("OgrBilgi-page", null);
            MufredatBilgisiUrl = Page.GetRouteUrl("OgrBilgi-page", null);
            etkinlikUrl = Page.GetRouteUrl("OgrBilgi-page", null);
            dersBilgisiUrl = Page.GetRouteUrl("DersBilgi-page", null);
            dersKonuBilgisiUrl = Page.GetRouteUrl("DersKonuBilgi-page", null);
            testBilgisiUrl = Page.GetRouteUrl("OgrBilgi-page", null);
            testTakibiUrl = Page.GetRouteUrl("OgrBilgi-page", null);


        }
    }
}