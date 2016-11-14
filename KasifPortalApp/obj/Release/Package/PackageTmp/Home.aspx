<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="KasifPortalApp.Home" %>

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
                    <li class="red high long">
                        <a href="<%=ogrBilgiUrl %>"><span><i class="icon-envelope"></i></span><span class="name">Öğrenci Bilgisi </span></a>
                    </li>
                    <li class="lime long">
                        <a href="<%=hocaBilgiUrl %>"><span><i class="icon-comment"></i></span><span class="name">Hoca Bilgisi</span></a>
                    </li>
                    <li class="orange long">
                        <a href="<%=DevamsizlikUrl %>"><span><i class="icon-shopping-cart"></i></span><span class="name">Devamsızlık Bilgisi</span></a>
                    </li>
                    <li class="green long">
                        <a href="<%=MufredatTakipUrl %>"><span><i class="icon-globe"></i></span><span class="name">Müfredat Takibi</span></a>
                    </li>
                    <li class="green">
                        <a href="<%=dersBilgisiUrl %>"><span><i class="icon-signout"></i></span><span class="name">Ders Bilgisi</span></a>
                    </li>
                    <li class="teal">
                        <a href="<%=dersKonuBilgisiUrl %>"><span><i class="icon-signout"></i></span><span class="name">Ders Konu Bilgisi</span></a>
                    </li>
                    <li class="red">
                        <a href="#"><span><i class="icon-signout"></i></span><span class="name">Test Takibi</span></a>
                    </li>
                    <li class="orange">
                        <a href="#"><span><i class="icon-signout"></i></span><span class="name">Test Bilgisi</span></a>
                    </li>
                    <li class="teal long">
                        <a href="#"><span><i class="icon-cloud-upload"></i></span><span class="name">Etkinlikler</span></a>
                    </li>
                    <li class="blue">
                        <a href="#"><span><i class="icon-signout"></i></span><span class="name">Sign out</span></a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
