using KasifBusiness.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KasifPortalApp
{
    public abstract class BasePage:System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            try
            {
                SessionInfo KsfSI = (SessionInfo)Session["KsfSessionInfo"];

                if (KsfSI == null)
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