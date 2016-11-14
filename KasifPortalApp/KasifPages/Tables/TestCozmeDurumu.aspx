<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="TestCozmeDurumu.aspx.cs" Inherits="KasifPortalApp.KasifPages.Tables.TestCozmeDurumu" %>

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
                        //null,text,select(durum),select(sınıf),text,text,text,text,text,text,text,text,text
                        null,
                        {
                            type: "text",
                        },
                        {
                            type: "select",
                            bCaseSensitive: true,
                            values: ["Cozdu","Cozmedi","Eksik"]
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
                            type: "text",
                        },
                        {
                            type: "text",
                        },
                        {
                            type: "text",
                        },
                        {
                            type: "text",
                        },
                        {
                            type: "text",
                        },
                        {
                            type: "text",
                        },
                        {
                            type: "text",
                        }
                    ]
                });
                $("#dtTable").css("width", '100%');
            }

            $('.delete').click(function (event) {
                event.preventDefault();
                var postData = {
                    "RowGuid": this.getAttribute('dataId')
                };
                $.ajax({
                    type: "POST",
                    url: "<%= ResolveClientUrl("~/KasifPages/Tables/TestCozmeDurumu.aspx/DeleteCurrentRow") %>",
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
            });
        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
    <h3>Test Cozme Durumu</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">

    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3 class="pull-left"><i class="icon-table"></i><%=pageTitle %></h3>
                <div class="action pull-right">
                    <a href="<%=GenerateAddUrl()%>" class="btn btn-darkblue" style="padding: 10px; margin-right: 10px;"><i class=" icon-plus" style="margin-right: 10px;"></i>Yeni Ekle</a>
                </div>

            </div>
            <div class="box-content nopadding">
                <asp:Repeater ID="tblRepeater" runat="server">
                    <HeaderTemplate>
                        <table id="dtTable" class="dataTable-scroll-x table table-hover table-nomargin table-bordered dataTable">
                            <thead>
                                <tr class='thefilter'>
                                    <th>Seçenekler</th>
                                    <th>Öğrenci Ad Soyad</th>
                                    <th>Test Durumu</th>
                                    <th>Sınıf</th>
                                    <th>Test No</th>
                                    <th>Test Adı</th>
                                    <th>Ders Adı</th>
                                    <th>Konu Adı</th>
                                    <th>Doğru Sayısı</th>
                                    <th>Yanlış Sayısı</th>
                                    <th>Mahalle</th>
                                    <th>Hoca Ad Soyad</th>
                                    <th>Hafta</th>
                                </tr>
                                <tr>
                                    <th>Seçenekler</th>
                                    <th>Öğrenci Ad Soyad</th>
                                    <th>Test Durumu</th>
                                    <th>Sınıf</th>
                                    <th>Test No</th>
                                    <th>Test Adı</th>
                                    <th>Ders Adı</th>
                                    <th>Konu Adı</th>
                                    <th>Doğru Sayısı</th>
                                    <th>Yanlış Sayısı</th>
                                    <th>Mahalle</th>
                                    <th>Hoca Ad Soyad</th>
                                    <th>Hafta</th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <a href="#" class="btn" rel="tooltip" title="View"><i class="icon-search"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Edit"><i class="icon-edit"></i></a>
                                <a href="#" class="btn delete" rel="tooltip" title="Delete" dataid="<%#DataBinder.Eval(Container.DataItem,"TEST_REL_GUID").ToString().Trim() %>"><i class="icon-remove"></i></a>
                            </td>
                            <td><%#DataBinder.Eval(Container.DataItem,"OGR_AD_SOYAD").ToString()%> </td>
                            <td><%#DataBinder.Eval(Container.DataItem,"DURUM").ToString()%> </td>
                            <td><%#DataBinder.Eval(Container.DataItem,"SINIF").ToString() %></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"TEST_NO").ToString() %></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"TEST_ADI").ToString() %></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"DERS_ADI").ToString() %></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"KONU").ToString() %></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"DOGRU_SAYISI").ToString() %></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"YANLIS_SAYISI").ToString() %></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"MAHALLE").ToString() %></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"HOCA_AD_SOYAD").ToString() %></td>
                            <td><%#DataBinder.Eval(Container.DataItem,"HAFTA").ToString() %></td>
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
