<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="OgrenciBilgisi.aspx.cs" Inherits="KasifPortalApp.KasifPages.Tables.OgrenciBilgisi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function DtInit() {
            var opt = {
                "sPaginationType": "full_numbers",
                "oLanguage": {
                    "sSearch": "<span>Ara:</span> ",
                    "sInfo": "Toplam <span>_TOTAL_</span> kayıttan <span>_START_</span> - <span>_END_</span> arası gösteriliyor.",
                    "sLengthMenu": "_MENU_ <span>'ar satır</span>"
                },
                'sDom': "lfrtip",
                'aoColumnDefs': [
                { 'bSortable': false, 'aTargets': [0, 5] }
                ],
                'oColVis': {
                    "buttonText": "Change columns <i class='icon-angle-down'></i>"
                },
                'oTableTools': {
                    "sSwfPath": "js/plugins/datatable/swf/copy_csv_xls_pdf.swf"
                }
            };
            var oTable = $('.usertable').dataTable(opt);

            $('.dataTables_filter input').attr("placeholder", "Ara...");
            $(".dataTables_length select").wrap("<div class='input-mini'></div>").chosen({
                disable_search_threshold: 9999999
            });
            $("#check_all").click(function (e) {
                $('input', oTable.fnGetNodes()).prop('checked', this.checked);
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
                    values: ['Active', 'Inactive', 'Disabled']
                },
                {
                    type: "date-range"
                },
                null
                ]
            });
            $(".usertable").css("width", '100%');
        }


        $(document).ready(function () {
            DtInit();



        });

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
    <h3>Öğrenci Bilgisi</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <%--<div class="box-title">
        <h3>
            <i class="icon-reorder"></i>
            Öğrenci Bilgisi
        </h3>
    </div>--%>
    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3 class="pull-left"><i class="icon-table"></i><%=pageDescription %></h3>
                <div class="action pull-right">
                    <a href="<%=GenerateAddUrl()%>" class="btn btn-darkblue" style="margin-top: 5px; margin-bottom: 5px;"><i class=" icon-plus "></i> Yeni Ekle</a>&nbsp;&nbsp;&nbsp;
                </div>

            </div>

            <div class="box-content nopadding">
                <table class="table table-hover table-nomargin table-bordered usertable">
                    <thead>
                        <tr class='thefilter'>
                            <th class='with-checkbox'></th>
                            <th>Username</th>
                            <th>Email</th>
                            <th class='hidden-350'>Status</th>
                            <th class='hidden-1024'>Member since</th>
                            <th class='hidden-480'>Options</th>
                        </tr>
                        <tr>
                            <th class='with-checkbox'>
                                <input type="checkbox" name="check_all" id="check_all"></th>
                            <th>Username</th>
                            <th>Email</th>
                            <th class='hidden-350'>Status</th>
                            <th class='hidden-1024'>Member since</th>
                            <th class='hidden-480'>Options</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="with-checkbox">
                                <input type="checkbox" name="check" value="1">
                            </td>
                            <td>John Doe</td>
                            <td>john.doe@johndoe.com</td>
                            <td class='hidden-350'><span class="label label-satgreen">Active</span></td>
                            <td class='hidden-1024'>03-07-2013</td>
                            <td class='hidden-480'>
                                <a href="#" class="btn" rel="tooltip" title="View"><i class="icon-search"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Edit"><i class="icon-edit"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Delete"><i class="icon-remove"></i></a>
                            </td>
                        </tr>
                        <tr>
                            <td class="with-checkbox">
                                <input type="checkbox" name="check" value="1">
                            </td>
                            <td>Jane Doe</td>
                            <td>jane.doe@johndoe.com</td>
                            <td class='hidden-350'><span class="label label-satgreen">Active</span></td>
                            <td class='hidden-1024'>02-07-2013</td>
                            <td class='hidden-480'>
                                <a href="#" class="btn" rel="tooltip" title="View"><i class="icon-search"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Edit"><i class="icon-edit"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Delete"><i class="icon-remove"></i></a>
                            </td>
                        </tr>
                        <tr>
                            <td class="with-checkbox">
                                <input type="checkbox" name="check" value="1">
                            </td>
                            <td>Peter C. Thomas</td>
                            <td>PeterCThomas@einrot.com</td>
                            <td class='hidden-350'><span class="label">Disabled</span></td>
                            <td class='hidden-1024'>19-06-2013</td>
                            <td class='hidden-480'>
                                <a href="#" class="btn" rel="tooltip" title="View"><i class="icon-search"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Edit"><i class="icon-edit"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Delete"><i class="icon-remove"></i></a>
                            </td>
                        </tr>
                        <%--<tr>
                            <td class="with-checkbox">
                                <input type="checkbox" name="check" value="1">
                            </td>
                            <td>Dolores R. Anderson</td>
                            <td>DoloresRAnderson@einrot.com</td>
                            <td class='hidden-350'><span class="label label-satgreen">Active</span></td>
                            <td class='hidden-1024'>19-06-2013</td>
                            <td class='hidden-480'>
                                <a href="#" class="btn" rel="tooltip" title="View"><i class="icon-search"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Edit"><i class="icon-edit"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Delete"><i class="icon-remove"></i></a>
                            </td>
                        </tr>
                        <tr>
                            <td class="with-checkbox">
                                <input type="checkbox" name="check" value="1">
                            </td>
                            <td>Cathleen K. King</td>
                            <td>CathleenKKing@cuvox.de</td>
                            <td class='hidden-350'><span class="label label-satgreen">Active</span></td>
                            <td class='hidden-1024'>30-06-2013</td>
                            <td class='hidden-480'>
                                <a href="#" class="btn" rel="tooltip" title="View"><i class="icon-search"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Edit"><i class="icon-edit"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Delete"><i class="icon-remove"></i></a>
                            </td>
                        </tr>
                        <tr>
                            <td class="with-checkbox">
                                <input type="checkbox" name="check" value="1">
                            </td>
                            <td>Leslie M. Martinez</td>
                            <td>LeslieMMartinez@cuvox.de</td>
                            <td class='hidden-350'><span class="label label-satgreen">Active</span></td>
                            <td class='hidden-1024'>18-06-2013</td>
                            <td class='hidden-480'>
                                <a href="#" class="btn" rel="tooltip" title="View"><i class="icon-search"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Edit"><i class="icon-edit"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Delete"><i class="icon-remove"></i></a>
                            </td>
                        </tr>
                        <tr>
                            <td class="with-checkbox">
                                <input type="checkbox" name="check" value="1">
                            </td>
                            <td>Rochelle J. Worsham</td>
                            <td>RochelleJWorsham@cuvox.de</td>
                            <td class='hidden-350'><span class="label label-lightred">Inactive</span></td>
                            <td class='hidden-1024'>16-06-2013</td>
                            <td class='hidden-480'>
                                <a href="#" class="btn" rel="tooltip" title="View"><i class="icon-search"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Edit"><i class="icon-edit"></i></a>
                                <a href="#" class="btn" rel="tooltip" title="Delete"><i class="icon-remove"></i></a>
                            </td>
                        </tr>--%>
                    </tbody>
                </table>
            </div>
        </div>
    </div>


</asp:Content>
