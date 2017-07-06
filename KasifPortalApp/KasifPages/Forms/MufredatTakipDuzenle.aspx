<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="MufredatTakipDuzenle.aspx.cs" Inherits="KasifPortalApp.KasifPages.Forms.MufredatTakipDuzenle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            var btnSubmit = document.getElementById("<%=btnSubmit.ClientID%>");
            var slcSinif = document.getElementById("<%=slcSinif.ClientID%>");
            var slcDers = document.getElementById("<%=slcDersAdi.ClientID%>");
          
            $(btnSubmit).click(function (event) {
                event.preventDefault();
                var postData = SetParameters();
                CallAjaxOnSubmit(postData);
            });
          
            $(slcSinif).change(function () {
                var lstDers = <%=JsSerialize(lstDersBilgi) %>;
                var lstKonu = <%=JsSerialize(lstKonuBilgi) %>;
                var slcKonu = document.getElementById('<%=slcKonuAdi.ClientID%>');
                slcDers.options.length=0;
                for(var i=0;i<lstDers.length;i++)
                {
                    if(lstDers[i].SINIF==slcSinif.value)
                    {
                        var opt = document.createElement('option');
                        opt.text = lstDers[i].DERS_ADI;
                        opt.value = lstDers[i].GUID;
                        slcDers.appendChild(opt);
                    }
                }
                slcKonu.options.length=0;
                for(var i=0;i<lstKonu.length;i++)
                {
                    if(lstKonu[i].DERS_GUID==slcDers.value)
                    {
                        var opt = document.createElement('option');
                        opt.text = lstKonu[i].KONU;
                        opt.value = lstKonu[i].DERS_KONU_GUID;
                        slcKonu.appendChild(opt);
                    }
                }


            });
            $(slcDers).change(function () {
                var lstDers = <%=JsSerialize(lstDersBilgi) %>;
                var lstKonu = <%=JsSerialize(lstKonuBilgi) %>;
                var slcKonu = document.getElementById('<%=slcKonuAdi.ClientID%>');
                slcKonu.options.length=0;
                for(var i=0;i<lstKonu.length;i++)
                {
                    if(lstKonu[i].DERS_GUID==slcDers.value)
                    {
                        var opt = document.createElement('option');
                        opt.text = lstKonu[i].KONU;
                        opt.value = lstKonu[i].DERS_KONU_GUID;
                        slcKonu.appendChild(opt);
                    }
                }
            });

            function SetParameters()
            {
                var postData = {
                    "Hoca":document.getElementById('<%=slcHoca.ClientID%>').value,
                    "Tarih":document.getElementById('<%=txtTarih.ClientID%>').value,
                    "Sinif":document.getElementById('<%=slcSinif.ClientID%>').value,
                    "DersId": document.getElementById('<%=slcDersAdi.ClientID%>').value,
                    "KonuAdi": document.getElementById('<%=slcKonuAdi.ClientID%>').value,
                    "TakipDurumu": document.getElementById('<%=slcTakipDurumu.ClientID%>').value
                };
                return postData;
            }

            function CallAjaxOnSubmit(postData)
            {
                $.ajax({
                    type: "POST",
                    url: "<%= ResolveClientUrl("~/KasifPages/Forms/MufredatTakipDuzenle.aspx/ProcessOperation") %>",
                    data: JSON.stringify(postData),
                    contentType: "application/json; charset=utf-8",
                    dataType: "JSON",
                    success: function (result) {
                        if(result.d[0] == 'success')
                            showSuccessModal( '<%=pageTitle%>',result.d[1], '<%= Page.GetRouteUrl(pageName, null) %>');
                        else
                            showErrorModal('<%=pageTitle%> - Hata',result.d[1]);
                        e.preventDefault();
                    },
                    failure: function (response) {
                        showErrorModal('<%=pageTitle%> - Hata','Beklenmeyen bir hata oluştu');
                        e.preventDefault();
                    },
                    error: function (response) {
                        showErrorModal('<%=pageTitle%> - Hata','Beklenmeyen bir hata oluştu');
                        e.preventDefault();
                    }
                });
            }
        });

            


    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
    <h3>Müfredat Takibi Bilgisi Düzenle</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3><i class="icon-list"></i>Müfredat Takibi Bilgisi Düzenle</h3>
            </div>
            <div class="box-content nopadding">
                <form id="form1" action="#" method="POST" class='form-horizontal form-column form-bordered' runat="server">
                    <input type="hidden" id="hiddenDersId" runat="server" />
                    <input type="hidden" id="hiddenKonuAdi" runat="server" />

                    <div class="span6">
                        <div class="control-group">
                            <label for="select" class="control-label ">Hoca Adı</label>
                            <div class="controls">
                                <select name="select" id="slcHoca" class="input-large" runat="server"></select>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Tarih</label>
                            <div class="controls">
                                <div class="input-append">
                                    <input type="text" class="input-large datepick" id="txtTarih" rel="4" runat="server" />
                                    <span class="add-on"><i class="icon-calendar"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="select" class="control-label ">Sınıf</label>
                            <div class="controls">
                                <select name="select" id="slcSinif" class="input-large" runat="server">
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label for="select" class="control-label ">Ders Adı</label>
                            <div class="controls">
                                <select name="select" id="slcDersAdi" class="input-large" runat="server"></select>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="select" class="control-label ">Konu Adı</label>
                            <div class="controls">
                                <select name="select" id="slcKonuAdi" class="input-large" runat="server"></select>
                            </div>
                        </div>
                        <!-- Combobox  -->
                        <div class="control-group">
                            <label for="select" class="control-label ">Takip Durumu</label>
                            <div class="controls">
                                <select name="select" id="slcTakipDurumu" class="input-large" runat="server"></select>
                            </div>
                        </div>
                    </div>
                    <div class="span12">
                        <div class="form-actions">
                            <button type="submit" role="button" id="btnSubmit" class="btn btn-primary" runat="server">Kaydet</button>
                            <a href="<%=GenerateListUrl() %>" role="button" class="btn">Geri dön</a>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

</asp:Content>
