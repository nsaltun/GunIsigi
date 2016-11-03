<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DersKonuBilgisiEkle.aspx.cs" Inherits="KasifPortalApp.KasifPages.Forms.DersKonuBilgisiEkle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            var slcSinif = document.getElementById("<%=slcSinif.ClientID%>");
            var btnSubmit = document.getElementById("<%=btnSubmit.ClientID%>");
          
            $(btnSubmit).click(function () {
                var postData = SetParameters();
                CallAjaxOnSubmit(postData);
            });
          
            $(slcSinif).change(function () {
                var lstDers = <%=JsSerialize(lstDersBilgi) %>;
                var slcDers = document.getElementById('<%=slcDersAdi.ClientID%>');
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
            });

            function SetParameters()
            {
                //var postData=[];
                var konuAdi = document.getElementById('<%=txtAd.ClientID%>').value;
                var dersId = document.getElementById('<%=slcDersAdi.ClientID%>').value;
                //postData.push( { "name": "konuAdi", "value": konuAdi });
                //postData.push( { "name": "dersId", "value": dersId });
                

                var postData = {
                    "KonuAdi": konuAdi,
                    "DersId": dersId
                };


                return postData;
            }

            function CallAjaxOnSubmit(postData)
            {
                $.ajax({
                    type: "POST",
                    url: "<%= ResolveClientUrl("~/KasifPages/Forms/DersKonuBilgisiEkle.aspx/ProcessOperation") %>",
                    data: JSON.stringify(postData),
                    contentType: "application/json; charset=utf-8",
                    dataType: "JSON",
                    success: function (result) {
                        if(result.d[0] == 'success')
                            showErrorModal('<%=pageTitle%> - Hata',result.d[1]);
                            //showSuccessModal( '<%=pageTitle%>',result.d[1], '<%= Page.GetRouteUrl(pageName, null) %>');
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
    <h3>Ders Konu Bilgisi Ekle</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3><i class="icon-list"></i>Ders Konusu Ekle</h3>
            </div>
            <div class="box-content nopadding">
                <form id="form1" action="#" method="POST" class='form-horizontal form-column form-bordered' runat="server">
                    <input type="hidden" id="hiddenDersId" runat="server" />
                    <input type="hidden" id="hiddenKonuAdi" runat="server" />
                    
                    <div class="span6">

                        <!-- Combobox  -->
                        <div class="control-group">
                            <label for="select" class="control-label ">Sınıf</label>
                            <div class="controls">
                                <select name="select" id="slcSinif" class="input-large" runat="server">
                                    <option value="5">5</option>
                                    <option value="6">6</option>
                                    <option value="7">7</option>
                                    <option value="8">8</option>
                                </select>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label">Konu Adı</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtAd" class="input-xlarge" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <!-- Combobox  -->
                        <div class="control-group">
                            <label for="select" class="control-label ">Ders Adı</label>
                            <div class="controls">
                                <select name="select" id="slcDersAdi" class="input-large" runat="server"></select>
                            </div>
                        </div>
                    </div>
                    <div class="span12">
                        <div class="form-actions">
                            <button type="submit" role="button" id="btnSubmit" class="btn btn-primary" runat="server">Save changes</button>
                            <a href="<%=GenerateListUrl() %>" role="button" class="btn">Cancel</a>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

</asp:Content>
