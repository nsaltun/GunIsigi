<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DersBilgisi.aspx.cs" Inherits="KasifPortalApp.KasifPages.Tables.DersBilgisi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {
            DtInit();
            dispatchEvent(new Event('resize'));

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
                            type: "select",
                            bCaseSensitive: true,
                            values: ['5', '6', '7', '8']
                        }
                    ]
                });
                $("#dtTable").css("width", '100%');
            }


            $('.delete').click(function (event) {
                var header = '<%=pageTitle%>';
                var msg = 'Bu kaydı silmek istediğinize emin misiniz?';
                var href = "<%= ResolveClientUrl("~/KasifPages/Tables/DersBilgisi.aspx/DeleteCurrentRow") %>";
                var callbackUrl = "<%=Page.GetRouteUrl(pageName,null)%>";
                var postData = {
                    "RowGuid": this.getAttribute('dataId')
                };

                AjaxConfirmModal(header, msg, href, callbackUrl, postData);
               
            });




            <%--$('.delete').click(function (event) {
                var isConfirmed = showConfirmModalDelete('<%=pageTitle%>', 'Bu kaydı silmek istediğinize emin misiniz?', '<%= Page.GetRouteUrl(pageName, null) %>');
                if (!isConfirmed)
                    return false;
                event.preventDefault();
                var postData = {
                    "RowGuid": this.getAttribute('dataId')
                };
                $.ajax({
                    type: "POST",
                    url: "<%= ResolveClientUrl("~/KasifPages/Tables/DersBilgisi.aspx/DeleteCurrentRow") %>",
                    data: JSON.stringify(postData),
                    contentType: "application/json; charset=utf-8",
                    dataType: "JSON",
                    success: function (result) {
                        alert(result.d);
                        window.location = "<%=Page.GetRouteUrl(pageName,null)%>"
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
    <h3>Ders Bilgisi</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3 class="pull-left"><i class="icon-table"></i><%=pageTitle %></h3>
                <%if (KasifPortalApp.Utilities.UtilityScreenFunctions.ControlFieldAllowed("2", ksfSI) == "1")
                    {%>
                <div class="action pull-right">
                    <a href="<%=GenerateAddUrl()%>" class="btn btn-darkblue" style="padding: 10px; margin-right: 10px;"><i class=" icon-plus" style="margin-right: 10px;"></i>Yeni Ekle</a>
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
                                    <th>Ders Adı</th>
                                    <th>Sınıf</th>
                                </tr>
                                <tr>
                                    <th>Seçenekler</th>
                                    <th>Ders Adı</th>
                                    <th>Sınıf</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%if (KasifPortalApp.Utilities.UtilityScreenFunctions.ControlFieldAllowed("2", ksfSI) == "1")
                                    {%>
                                <%--<a href="#" class="btn" rel="tooltip" title="View"><i class="icon-search"></i></a>--%>
                                <a href="#" class="btn" rel="tooltip" title="Edit"><i class="icon-edit"></i></a>
                                <a href="#" class="btn delete" rel="tooltip" title="Delete" dataid="<%#DataBinder.Eval(Container.DataItem,"GUID").ToString()%>"><i class="icon-remove"></i></a>
                                <%} %>
                            </td>
                            <td><%#DataBinder.Eval(Container.DataItem,"DERS_ADI").ToString()%> </td>
                            <td><%#DataBinder.Eval(Container.DataItem,"SINIF").ToString() %></td>
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
