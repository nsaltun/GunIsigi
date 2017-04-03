<%@ Page Title="" Language="C#" MasterPageFile="MainMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="KasifPortalApp.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
    <h1>Ana Sayfa</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="box box-bordered">
        <div class="box-title">
            <h3>
                <i class="icon-reorder"></i>
                Kolay Menü
            </h3>
        </div>
        <div class="box-content" style="margin-left: 0px;">
            <div class="span12">
                <ul class="tiles" style="margin-top: 0px;">
                    <%
                        if (lstMenuTiles != null)
                            foreach (var item in lstMenuTiles)
                            {
                    %>
                    <li class="<%=item.STYLE%>">
                        <a href="<%= item.NODE_GUID!=null && item.NODE_GUID!=0? GetRouteUrl(item.FILE_NAME,null) : "#" %>"><span><i class="<%=item.ICON_NAME %>"></i></span><span class="name"><%=item.DISPLAY_NAME %> </span></a>
                    </li>
                    <%
                            }
                    %>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
