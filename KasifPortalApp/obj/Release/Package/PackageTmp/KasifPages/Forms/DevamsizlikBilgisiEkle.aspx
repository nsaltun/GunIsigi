<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DevamsizlikBilgisiEkle.aspx.cs" Inherits="KasifPortalApp.KasifPages.Forms.DevamsizlikBilgisiEkle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            var btnSubmit = document.getElementById("<%=btnSubmit.ClientID%>");
            <%--var slcSinif = document.getElementById("<%=slcSinif.ClientID%>");
            var slcMahalle = document.getElementById("<%=slcMahalle.ClientID%>");
            var slcTip = document.getElementById("<%=slcTip.ClientID%>");--%>
          
            $(btnSubmit).click(function (event) {
                event.preventDefault();
                var postData = SetParameters();
                CallAjaxOnSubmit(postData);
            });
            //Değiştiğinde slcKisi değişsin.
            $(document.getElementById("<%=slcSinif.ClientID%>")).change(function () {
                UpdateKisiler();
            });
            //Değiştiğinde slcKisi değişsin.
            $(document.getElementById("<%=slcMahalle.ClientID%>")).change(function () {
                UpdateKisiler();
            });
            //Değiştiğinde slcKisi değişsin.
            $(document.getElementById("<%=slcTip.ClientID%>")).change(function () {
                UpdateKisiler();
            });

            function UpdateKisiler()
            {
                var slcSinifVal = document.getElementById("<%=slcSinif.ClientID%>").value;
                var slcMahVal = document.getElementById("<%=slcMahalle.ClientID%>").value;
                var slcTipVal = document.getElementById("<%=slcTip.ClientID%>").value;
                var lstKisiler = <%=JsSerialize(lstKisiBilgi) %>;
                var slcKisi = document.getElementById('<%=slcKisi.ClientID%>');
                slcKisi.options.length=0;
                for(var i=0;i<lstKisiler.length;i++)
                {
                    if((lstKisiler[i].BOLGE_ID==slcMahVal) && slcTipVal=="" && slcSinifVal=="" )
                    {
                        var opt = document.createElement('option');
                        opt.text = lstKisiler[i].AD_SOYAD;
                        opt.value = lstKisiler[i].GUID;
                        slcKisi.appendChild(opt);
                    }
                    else if((lstKisiler[i].TIP==slcTipVal) && slcMahVal=="" && slcSinifVal=="" )
                    {
                        var opt = document.createElement('option');
                        opt.text = lstKisiler[i].AD_SOYAD;
                        opt.value = lstKisiler[i].GUID;
                        slcKisi.appendChild(opt);
                    }
                    else if((lstKisiler[i].SINIF==slcSinifVal) && slcMahVal=="" && slcTipVal=="" )
                    {
                        var opt = document.createElement('option');
                        opt.text = lstKisiler[i].AD_SOYAD;
                        opt.value = lstKisiler[i].GUID;
                        slcKisi.appendChild(opt);
                    }
                    else if((lstKisiler[i].BOLGE_ID==slcMahVal) && (lstKisiler[i].TIP==slcTipVal) && slcSinifVal=="")
                    {
                        var opt = document.createElement('option');
                        opt.text = lstKisiler[i].AD_SOYAD;
                        opt.value = lstKisiler[i].GUID;
                        slcKisi.appendChild(opt);
                    }
                    else if((lstKisiler[i].BOLGE_ID==slcMahVal) &&  (lstKisiler[i].SINIF==slcSinifVal) && slcTipVal=="")
                    {
                        var opt = document.createElement('option');
                        opt.text = lstKisiler[i].AD_SOYAD;
                        opt.value = lstKisiler[i].GUID;
                        slcKisi.appendChild(opt);
                    }
                    else if((lstKisiler[i].SINIF==slcSinifVal) && (lstKisiler[i].TIP==slcTipVal) && slcMahVal=="")
                    {
                        var opt = document.createElement('option');
                        opt.text = lstKisiler[i].AD_SOYAD;
                        opt.value = lstKisiler[i].GUID;
                        slcKisi.appendChild(opt);
                    }
                    else if((lstKisiler[i].BOLGE_ID==slcMahVal && slcMahVal!="") && (lstKisiler[i].TIP==slcTipVal && slcTipVal!="") && (lstKisiler[i].SINIF==slcSinifVal && slcSinifVal!=""))
                    {
                        var opt = document.createElement('option');
                        opt.text = lstKisiler[i].AD_SOYAD;
                        opt.value = lstKisiler[i].GUID;
                        slcKisi.appendChild(opt);
                    }
                    if(slcMahVal=="" && slcSinifVal=="" && slcTipVal=="")
                    {
                        var opt = document.createElement('option');
                        opt.text = lstKisiler[i].AD_SOYAD;
                        opt.value = lstKisiler[i].GUID;
                        slcKisi.appendChild(opt);
                    }
                }
            }

            function SetParameters()
            {
                var postData = {
                    "Hafta":document.getElementById('<%=slcHafta.ClientID%>').value,
                    "KisiId": document.getElementById('<%=slcKisi.ClientID%>').value,
                    "Tip": document.getElementById('<%=slcTip.ClientID%>').value,
                    "DevamDurumu": document.getElementById('<%=slcDevamDurumu.ClientID%>').value,
                    "Sebep": document.getElementById('<%=txtSebep.ClientID%>').value
                };
                return postData;
            }

            function CallAjaxOnSubmit(postData)
            {
                $.ajax({
                    type: "POST",
                    url: "<%= ResolveClientUrl("~/KasifPages/Forms/DevamsizlikBilgisiEkle.aspx/ProcessOperation") %>",
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
    <h3>Devamsızlık Bilgisi Ekle</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3><i class="icon-list"></i>Devamsızlık Bilgisi Ekle</h3>
            </div>
            <div class="box-content nopadding">
                <form id="form1" action="#" method="POST" class='form-horizontal form-column form-bordered' runat="server">
                    <div class="span6">
                        <div class="control-group">
                            <label for="select" class="control-label ">Hafta</label>
                            <div class="controls">
                                <select name="select" id="slcHafta" class="input-large" runat="server"></select>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="select" class="control-label ">Sınıf</label>
                            <div class="controls">
                                <select name="select" id="slcSinif" class="input-large" runat="server">
                                    <option value="">Sınıf Seç..</option>
                                    <option value="5">5</option>
                                    <option value="6">6</option>
                                    <option value="7">7</option>
                                    <option value="8">8</option>
                                </select>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="select" class="control-label ">Mahalle</label>
                            <div class="controls">
                                <select name="select" id="slcMahalle" class="input-large" runat="server"></select>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="select" class="control-label ">Tip</label>
                            <div class="controls">
                                <select name="select" id="slcTip" class="input-large" runat="server">
                                    <option value="1">Öğrenci</option>
                                    <option value="2">Hoca</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <div class="control-group">
                            <label for="select" class="control-label ">Kim</label>
                            <div class="controls">
                                <select name="select" id="slcKisi" class="input-large" runat="server"></select>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="select" class="control-label ">Devam Durumu</label>
                            <div class="controls">
                                <select name="select" id="slcDevamDurumu" class="input-large" runat="server">
                                    <option value="0">Geldi</option>
                                    <option value="1">Gelmedi</option>
                                    <option value="1">Geç Geldi</option>
                                </select>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="text field" class="control-label ">Devamsızlık Sebebi</label>
                            <div class="controls">
                                <textarea name="textarea" id="txtSebep" rows="6" class="input-block-level" runat="server"></textarea>
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
