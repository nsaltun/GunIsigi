using KasifBusiness.Objects;
using KasifBusiness.Objects.CodeMgmt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KasifPortalApp.Controls
{
    public partial class TopMenu : System.Web.UI.UserControl
    {
        public List<MenuTreeItemObject> lstAllMenu;
        public List<MenuTreeItemObject> lstMainMenu = new List<MenuTreeItemObject>();
        public List<MenuTreeItemObject> lstSubmenu = new List<MenuTreeItemObject>();

        protected void Page_Load(object sender, EventArgs e)
        {
            SessionInfo ksfSi = (SessionInfo)Session["KsfSessionInfo"];
            lstAllMenu = ksfSi.MenuTreeItemInfo;

            foreach (var item in lstAllMenu)
            {
                if (item.NODE_TYPE == MenuNodeType.MAIN.ToString())
                {
                    lstMainMenu.Add(item);
                }
                //else if (item.NODE_TYPE == MenuNodeType.SUB.ToString())
                //{
                //    lstSubmenu.Add(item);
                //}
            }

        }

        public List<MenuTreeItemObject> FindSubMenus(long parent_node)
        {
            return lstAllMenu.Where(x => x.PARENT_NODE_GUID == parent_node).ToList();
        }



        public List<MenuTreeItemObject> FindParentNode()
        {



            return null;
        }
    }
}