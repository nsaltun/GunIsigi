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
        public string dersHaftasiUrl = "";
        public string etkinlikUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            ogrBilgiUrl = Page.GetRouteUrl("OgrBilgi-page", null);
        }
    }
}