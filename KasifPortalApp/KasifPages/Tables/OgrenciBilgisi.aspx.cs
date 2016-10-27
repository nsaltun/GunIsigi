using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KasifPortalApp.KasifPages.Tables
{
    public partial class OgrenciBilgisi : System.Web.UI.Page
    {
        string pageName = "OgrBilgi-page";
        public string pageDescription = "Öğrenci Bilgisi";


        protected void Page_Load(object sender, EventArgs e)
        {

        }


        public string GenerateAddUrl()
        {
            return Page.GetRouteUrl(pageName + "-add", null);
        }
    }
}