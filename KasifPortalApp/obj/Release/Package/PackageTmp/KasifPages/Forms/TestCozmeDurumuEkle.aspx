<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="TestCozmeDurumuEkle.aspx.cs" Inherits="KasifPortalApp.KasifPages.Forms.TestCozmeDurumuEkle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            var btnSubmit = document.getElementById("<%=btnSubmit.ClientID%>");
            var slcSinif = document.getElementById("<%=slcSinif.ClientID%>");
            var slcDers = document.getElementById("<%=slcDersAdi.ClientID%>");
            var slcKonu = document.getElementById("<%=slcKonuAdi.ClientID%>");
          
            $(btnSubmit).click(function (event) {
                event.preventDefault();
                var postData = SetParameters();
                CallAjaxOnSubmit(postData);
            });
          
            $(slcSinif).change(function () {
                var lstDers = <%=JsSerialize(lstDersBilgi) %>;
                var lstKonu = <%=JsSerialize(lstKonuBilgi) %>;
                var lstTest = <%=JsSerialize(lstTestBilgi) %>;
                var lstOgr = <%=JsSerialize(lstOgrBilgi) %>;

                var slcKonu = document.getElementById('<%=slcKonuAdi.ClientID%>');
                slcDers.options.length=0;
                for(var i=0;i<lstDers.length;i++)
                {
                    if(i==0)
                    {
                        var opt = document.createElement('option');
                        opt.text = "Ders Seç..";
                        opt.value = "";
                        slcDers.appendChild(opt);
                    }
                    if(lstDers[i].SINIF==slcSinif.value)
                    {
                        var opt = document.createElement('option');
                        opt.text = lstDers[i].DERS_ADI;
                        opt.value = lstDers[i].GUID;
                        slcDers.appendChild(opt);
                    }
                }

                var slcTest = document.getElementById('<%=slcTestAdi.ClientID%>');
                slcTest.options.length=0;
                for(var i=0;i<lstTest.length;i++)
                {
                    if(lstTest[i].SINIF==slcSinif.value)
                    {
                        var opt = document.createElement('option');
                        opt.text = lstTest[i].TEST_NO+". "+lstTest[i].TEST_ADI;
                        opt.value = lstTest[i].TEST_GUID;
                        slcTest.appendChild(opt);
                    }
                }

                var slcOgr = document.getElementById('<%=slcOgr.ClientID%>');
                slcOgr.options.length=0;
                for(var i=0;i<lstOgr.length;i++)
                {
                    if(lstOgr[i].CLASS==slcSinif.value)
                    {
                        var opt = document.createElement('option');
                        opt.text = lstOgr[i].NAME + " "+ lstOgr[i].SURNAME;
                        opt.value = lstOgr[i].GUID;
                        slcOgr.appendChild(opt);
                    }
                }

                slcKonu.options.length=0;
                var opt2 = document.createElement('option');
                opt2.text = "Konu Seç..";
                opt2.value = "";
                slcKonu.appendChild(opt2);

            });
            $(slcDers).change(function () {
                var lstKonu = <%=JsSerialize(lstKonuBilgi) %>;
                var lstTest = <%=JsSerialize(lstTestBilgi) %>;
                var slcKonu = document.getElementById('<%=slcKonuAdi.ClientID%>');
                var slcTest = document.getElementById('<%=slcTestAdi.ClientID%>');

                slcKonu.options.length=0;
                for(var i=0;i<lstKonu.length;i++)
                {
                    if(i==0)
                    {
                        var opt = document.createElement('option');
                        opt.text = "Konu Seç..";
                        opt.value = "";
                        slcKonu.appendChild(opt);
                    }
                    if(lstKonu[i].DERS_GUID==slcDers.value)
                    {
                        var opt = document.createElement('option');
                        opt.text = lstKonu[i].KONU;
                        opt.value = lstKonu[i].DERS_KONU_GUID;
                        slcKonu.appendChild(opt);
                    }
                }
                slcTest.options.length=0;
                for(var i=0;i<lstTest.length;i++)
                {
                    if(lstTest[i].DERS_ID==slcDers.value)
                    {
                        var opt = document.createElement('option');
                        opt.text = lstTest[i].TEST_NO+". "+lstTest[i].TEST_ADI;
                        opt.value = lstTest[i].TEST_GUID;
                        slcTest.appendChild(opt);
                    }
                }
            });
            $(slcKonu).change(function () {
                var lstTest = <%=JsSerialize(lstTestBilgi) %>;
                var slcKonu = document.getElementById('<%=slcKonuAdi.ClientID%>');
                var slcTest = document.getElementById('<%=slcTestAdi.ClientID%>');

                slcTest.options.length=0;
                for(var i=0;i<lstTest.length;i++)
                {
                    if(lstTest[i].KONU_ID==slcKonu.value)
                    {
                        var opt = document.createElement('option');
                        opt.text = lstTest[i].TEST_NO+". "+lstTest[i].TEST_ADI;
                        opt.value = lstTest[i].TEST_GUID;
                        slcTest.appendChild(opt);
                    }
                }
            });

            function SetParameters()
            {
                var postData = {
                    "TestId":document.getElementById('<%=slcTestAdi.ClientID%>').value,
                    "OgrId":document.getElementById('<%=slcOgr.ClientID%>').value,
                    "Durum":document.getElementById('<%=slcCozmeDurumu.ClientID%>').value,
                    "DogruSayisi": document.getElementById('<%=txtDogruSayisi.ClientID%>').value,
                    "YanlisSayisi": document.getElementById('<%=txtYanlisSayisi.ClientID%>').value,
                    "HaftaId": document.getElementById('<%=slcHafta.ClientID%>').value
                };
                return postData;
            }

            function CallAjaxOnSubmit(postData)
            {
                $.ajax({
                    type: "POST",
                    url: "<%= ResolveClientUrl("~/KasifPages/Forms/TestCozmeDurumuEkle.aspx/ProcessOperation") %>",
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
    <h3>Test Çözme Durumu Ekle</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3><i class="icon-list"></i>Test Çözme Durumu Ekle</h3>
            </div>
            <div class="box-content nopadding">
                <form id="form1" action="#" method="POST" class='form-horizontal form-column form-bordered' runat="server">

                    <div class="span6">
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
                        <div class="control-group">
                            <label for="select" class="control-label ">Test Adı</label>
                            <div class="controls">
                                <select name="select" id="slcTestAdi" class="input-large" runat="server"></select>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="select" class="control-label ">Çözme Durumu</label>
                            <div class="controls">
                                <select name="select" id="slcCozmeDurumu" class="input-large" runat="server">
                                    <option value="1">Çözdü</option>
                                    <option value="2">Çözmedi</option>
                                    <option value="3">Eksik Çözdü</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label for="select" class="control-label ">Öğrenci</label>
                            <div class="controls">
                                <select name="select" id="slcOgr" class="input-large" runat="server"></select>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Doğru Sayısı</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtDogruSayisi" class="input-large" runat="server"></input>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Yanlış Sayısı</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtYanlisSayisi" class="input-large" runat="server"></input>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="select" class="control-label ">Hafta</label>
                            <div class="controls">
                                <select name="select" id="slcHafta" class="input-large" runat="server"></select>
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
