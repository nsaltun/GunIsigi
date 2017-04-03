<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenu.ascx.cs" Inherits="KasifPortalApp.Controls.TopMenu" %>


<ul class="main-nav">

    <% 
        if (lstAllMenu != null)
        {
            bool isFirstItem = true;
            foreach (var mainMenu in lstMainMenu)
            {
                if (isFirstItem)
                {
                    isFirstItem = false;
    %>
                    <li class="active">
                        <a href="<%=GetRouteUrl(mainMenu.FILE_NAME,null)%>">
                            <span><%=mainMenu.NODE_DISPLAY_NAME %></span>
                        </a>
                    </li>
    <%
                }
                else
                {
    %>
                    <li>
                        <a href="#" data-toggle="dropdown" class="dropdown-toggle">
                            <span><%=mainMenu.NODE_DISPLAY_NAME %></span>
                        
    <%
        isFirstItem = true;
        foreach (var subMenu in FindSubMenus(mainMenu.NODE_GUID))
        {
            if (isFirstItem)
            {
                isFirstItem = false;
    %>
                            <span class="caret"></span>
                        </a>
                        <ul class="dropdown-menu">
                            <li>
                                <a href="<%=GetRouteUrl(subMenu.FILE_NAME,null)%>">
                                    <%=subMenu.NODE_DISPLAY_NAME %>
                                </a>
                            </li>
    <%
            }
            else
            {
    %>
                            <li>
                                <a href="<%=GetRouteUrl(subMenu.FILE_NAME,null)%>">
                                    <%=subMenu.NODE_DISPLAY_NAME %>
                                </a>
                            </li>
    <%
            }
        }
    %>

                        </ul>
                    </li>
    <%
                } 
        %>
    <%      }
        }

    %>
    
</ul>
