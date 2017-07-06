using KasifBusiness.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KasifPortalApp
{
    public abstract class BasePage:System.Web.UI.Page
    {
        public SessionInfo ksfSI;
        protected override void OnInit(EventArgs e)
        {
            try
            {
                ksfSI = (SessionInfo)Session["KsfSessionInfo"];
                if (ksfSI == null)
                {
                    Session.Clear();
                    Response.Redirect(Page.GetRouteUrl("login-page", null));
                }

                //Ulaşılmak istenen sayfaya yetki yoksa logout yapılıyor.
                if (!CheckUserPageAuthorization(ksfSI.lstAllowedPages))
                {
                    Session.Clear();
                    Response.Redirect(Page.GetRouteUrl("login-page", null));
                }

                base.OnInit(e);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private bool CheckUserPageAuthorization(List<MenuTreeItemObject> userMenuTree)
        {
            //home.aspx
            //string virtUrl = ((System.Web.Routing.PageRouteHandler)(Request.RequestContext.RouteData.RouteHandler)).VirtualPath.Substring(2);
            //home
            string url = Request.RawUrl.Substring(1);//OgrBilgi
            //OgrBilgi/add => yani alt sayfalar için '/' işaretinden sonraki kısım atılıyor. Kök kontrol ediliyor.
            if (url.Contains("/"))
            {
                url = url.Split('/')[0];
            }

            foreach (var item in userMenuTree)
            {
                if (item.CLASS_NAME.ToUpper().Contains(url.ToUpper()))
                {
                    return true;
                }
            }

            return false;
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public abstract void Page_Load(object sender, EventArgs e);
    }

    
}