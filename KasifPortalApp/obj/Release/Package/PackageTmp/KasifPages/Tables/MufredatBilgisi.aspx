<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MufredatBilgisi.aspx.cs" Inherits="KasifPortalApp.KasifPages.Tables.MufredatBilgisi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            DtInit();

            function DtInit() {
                var oTable = $('#dtTable').dataTable();

                $('.dataTables_filter input').attr("placeholder", "Ara...");
                $(".dataTables_length select").wrap("<div class='input-mini'></div>").chosen({
                    disable_search_threshold: 9999999
                });
                $.datepicker.setDefaults({
                    dateFormat: "dd-mm-yy"
                });
                oTable.columnFilter({
                    "sPlaceHolder": "head:after",
                    'sRangeFormat': "{from}{to}",
                    'aoColumns': [
                        null,
                        {
                            type: "text",
                        },
                        {
                            type: "text",
                        },
                        {
                            type: "select",
                            bCaseSensitive: true,
                            values: ['5', '6', '7', '8']
                        },
                        {
                            type: "text",
                        },
                        {
                            type: "text",
                        },
                        {
                            type: "select",
                            bCaseSensitive: true,
                            values: ['Başlamadı','Devam Ediyor','Bitti']
                        },
                    ]
                });
                $("#dtTable").css("width", '100%');
            }

             $(document).on('click', ".delete", function (event) {
                var header = '<%=pageTitle%>';
                var msg = 'Bu kaydı silmek istediğinize emin misiniz?';
                var href = "<%= ResolveClientUrl("~/KasifPages/Tables/MufredatBilgisi.aspx/DeleteCurrentRow") %>";
                var callbackUrl = "<%=Page.GetRouteUrl(pageName,null)%>";
                var postData = {
                    "RowGuid": this.getAttribute('dataId')
                };

                AjaxConfirmModal(header, msg, href, callbackUrl, postData);
               
            });

            <%--$(document).on('click', ".delete", function (event) {
                event.preventDefault();
                var postData = {
                    "RowGuid":this.getAttribute('dataId')
                };
                $.ajax({
                    type: "POST",
                    url: "<%= ResolveClientUrl("~/KasifPages/Tables/MufredatBilgisi.aspx/DeleteCurrentRow") %>",
                    data: JSON.stringify(postData),
                    contentType: "application/json; charset=utf-8",
                    dataType: "JSON",
                    success: function (result) {
                        alert(result.d);
                        window.location="<%=Page.GetRouteUrl(pageName,null)%>"
                    },
                    failure: function (result) {
                        alert('error');
                    },
                    error: function (result) {
                        alert('error');
                    }
                });

            });--%>
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
    <h3>Müfredat Takibi</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">

    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3 class="pull-left"><i class="icon-table"></i><%=pageTitle %></h3>
                <%if (KasifPortalApp.Utilities.UtilityScreenFunctions.ControlFieldAllowed("1",ksfSI) == "1")
                {%>
                <div class="action pull-right">
                    <a href="<%=GenerateAddUrl()%>" class="btn btn-darkblue" style="padding:10px; margin-right:10px;"> <i class=" icon-plus" style="margin-right:10px;"></i>Yeni Ekle</a>
                </div>
                <%} %>
            </div>
            <div class="box-content nopadding">
                <asp:Repeater ID="tblRepeater" runat="server">
                    <HeaderTemplate>
                        <table id="dtTable" class="dataTable-scroll-x table table-hover table-nomargin table-bordered dataTable">
                            <thead>
                                <tr class='thefilter'>
                                    <th>Seçenekler</th>
                                    <th>Hoca</th>
                                    <th>Tarih</th>
                                    <th>Sınıf</th>
                                    <th>Ders Adı</th>
                                    <th>Konu Adı</th>
                                    <th>Takip Durumu</th>
                                </tr>
                                <tr>
                                    <th>Seçenekler</th>
                                    <th>Hoca</th>
                                    <th>Tarih</th>
                                    <th>Sınıf</th>
                                    <th>Ders Adı</th>
                                    <th>Konu Adı</th>
                                    <th>Takip Durumu</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td runat="server" ondatabinding="OnDataBinding">
                                <a href="<%# GenerateEditUrl(DataBinder.Eval(Container.DataItem,"MUFREDAT_GUID").ToString().Trim())%> " class="btn" rel="tooltip" title="Edit"><i class="icon-edit"></i></a>
                                <a href="#" class="btn delete" 
                                    <%--style="visibility:<%# KasifPortalApp.Utilities.UtilityScreenFunctions.ControlActionAuthorized(  DataBinder.Eval(Container.DataItem, "HOCA_GUID").ToString(),ksfSI)%>"--%>
                                    rel="tooltip" title="Delete" dataId="<%#DataBinder.Eval(Container.DataItem, "MUFREDAT_GUID").ToString().Trim() %>"><i class="icon-remove"></i></a>
                            </td>
                            <td><%#DataBinder.Eval(Container.DataItem,"HOCA_AD_SOYAD").ToString()%> </td>
                            <td><%#DataBinder.Eval(Container.DataItem,"TARIH").ToString()%> </td>
                            <td><%#DataBinder.Eval(Container.DataItem,"SINIF").ToString() %></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"DERS_ADI").ToString() %></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"KONU").ToString() %></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"TAKIP_DURUMU").ToString() %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody></table>
                    </FooterTemplate>
                </asp:Repeater>

            </div>
        </div>
    </div>


</asp:Content>
