<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="UserTableAdd.aspx.cs" Inherits="KasifPortalApp.Management.Forms.UserTableAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            var slcAdmin = document.getElementById("<%=slcAdmin.ClientID%>");
            var slcSinif = document.getElementById("<%=slcSinif.ClientID%>");
            $(slcAdmin).change(function () {
                var slcMahalle = document.getElementById("<%=slcMahalle.ClientID%>");
                if (slcAdmin.value == "1") {
                    $(slcSinif).hide(10);
                    $(slcMahalle).hide(10);
                }
                else {
                    $(slcSinif).show(10);
                    $(slcMahalle).show(10);
                }
            });

            $("#divOgr").hide();

            var slcRole = document.getElementById("<%=slcRole.ClientID%>");
            $(slcRole).change(function () {
                 if(slcRole.selectedOptions[0].text.toUpperCase()=="OGRENCI" || slcRole.selectedOptions[0].text.toUpperCase()=="VELI")
                {
                    GetOgrenci();
                }
                else if(slcRole.selectedOptions[0].text.toUpperCase()=="HOCA" )
                {
                    GetHoca();
                }
                else
                {
                    var slcOgr = document.getElementById('<%=slcOgrenci.ClientID%>');
                    slcOgr.options.length=0;
                    slcOgr.setAttribute("data-rule-required","false");
                    $("#divOgr").hide(100);
                }
            });

            $(slcSinif).change(function () {
                if(slcRole.selectedOptions[0].text.toUpperCase()=="OGRENCI" || slcRole.selectedOptions[0].text.toUpperCase()=="VELI")
                {
                    GetOgrenci();
                }
                else if(slcRole.selectedOptions[0].text.toUpperCase()=="HOCA" )
                {
                    GetHoca();
                }
                else
                {
                    var slcOgr = document.getElementById('<%=slcOgrenci.ClientID%>');
                    slcOgr.options.length=0;
                    slcOgr.setAttribute("data-rule-required","false");
                    $("#divOgr").hide(100);
                }
            });

            function GetOgrenci()
            {
                $("#divOgr").show(100);
                var slcOgr = document.getElementById('<%=slcOgrenci.ClientID%>');
                var lstOgr = <%=JsSerialize(lstOgrBilgi) %>;
                document.getElementById("<%=lblOgr.ClientID%>").innerHTML = "Öğrenci";
                slcOgr.options.length=0;
                slcOgr.setAttribute("data-rule-required","true");
                for(var i=0;i<=lstOgr.length;i++)
                {
                    if(i==0)
                    {
                        var opt = document.createElement('option');
                        opt.text = "Bir öğrenci seçin..";
                        opt.value = "";
                        slcOgr.appendChild(opt);
                    }
                    else if(lstOgr[i-1].CLASS==slcSinif.value)
                    {
                        var opt = document.createElement('option');
                        opt.text = lstOgr[i-1].NAME + " "+ lstOgr[i-1].SURNAME;
                        opt.value = lstOgr[i-1].GUID;
                        slcOgr.appendChild(opt);
                    }
                }
            }

            function GetHoca()
            {
                $("#divOgr").show(100);
                var slcOgr = document.getElementById('<%=slcOgrenci.ClientID%>');
                var lstOgr = <%=JsSerialize(lstHocaBilgi) %>;
                document.getElementById("<%=lblOgr.ClientID%>").innerHTML = "Hoca";
                slcOgr.options.length=0;
                slcOgr.setAttribute("data-rule-required","true");
                for(var i=0;i<=lstOgr.length;i++)
                {
                    if(i==0)
                    {
                        var opt = document.createElement('option');
                        opt.text = "Bir hoca seçin..";
                        opt.value = "";
                        slcOgr.appendChild(opt);
                    }
                    else if(lstOgr[i-1].SINIF==slcSinif.value)
                    {
                        var opt = document.createElement('option');
                        opt.text = lstOgr[i-1].HOCA_ADI + " "+ lstOgr[i-1].HOCA_SOYADI;
                        opt.value = lstOgr[i-1].GUID;
                        slcOgr.appendChild(opt);
                    }
                }
            }

        });

        function OnSubmit(e)
        {
            var slcRole = document.getElementById("<%=slcRole.ClientID%>");
            var slcOgr = document.getElementById("<%=slcOgrenci.ClientID%>");

            if(slcRole.selectedOptions[0].text.toUpperCase()=="OGRENCI" || slcRole.selectedOptions[0].text.toUpperCase()=="VELI")
            {
                if(slcOgr.value=="")
                {
                    showErrorModal("<%=pageTitle%> - Hata", "Öğrenci ve Veli rolü seçildiği zaman öğrenci bilgisi de seçilmelidir.");
                    return false;
                }
                document.getElementById("<%=hiddenOgrId.ClientID%>").value = slcOgr.value;
            }
            else if(slcRole.selectedOptions[0].text.toUpperCase()=="HOCA")
            {
                if(slcOgr.value=="")
                {
                    showErrorModal("<%=pageTitle%> - Hata", "Hoca rolü seçildiği zaman hoca bilgisi de seçilmelidir.");
                    return false;
                }
                document.getElementById("<%=hiddenHocaId.ClientID%>").value = slcOgr.value;
            }

        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
    <h3>Kullanıcı Bilgisi Ekle</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3><i class="icon-list"></i>Kullanıcı Ekle</h3>
            </div>
            <div class="box-content nopadding">
                <form id="form1" action="#" method="POST" class='form-horizontal form-column form-bordered' runat="server">
                    <input type="hidden" runat="server" id ="hiddenHocaId"/>
                    <input type="hidden" runat="server" id ="hiddenOgrId"/>
                    <div class="span6">
                        <div class="control-group">
                            <label for="select" class="control-label ">Admin</label>
                            <div class="controls">
                                <select name="select" id="slcAdmin" class="input-large" runat="server">
                                    <option value="0">Hayır</option>
                                    <option value="1">Evet</option>
                                </select>
                            </div>
                        </div>
                        <%--<div class="check-line">
                            <input type="checkbox" id="chkAdmin" class='icheck-me' data-skin="flat" data-color="green" runat="server" />
                            <label class='inline' for="chkAdmin">Admin</label>
                        </div>--%>
                        <div class="control-group ">
                            <label for="textfield" class="control-label ">Email</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtEmail" class="input-xlarge" runat="server" data-rule-required="true" />
                            </div>
                        </div>
                        <div class="control-group ">
                            <label for="textfield" class="control-label ">Şifre</label>
                            <div class="controls">
                                <input type="password" name="textfield" id="txtPassword" class="input-xlarge" runat="server" data-rule-required="true" />
                            </div>
                        </div>

                        <div class="control-group">
                            <label for="select" class="control-label ">Rol</label>
                            <div class="controls">
                                <select name="select" id="slcRole" class="input-large" runat="server"></select>
                            </div>
                        </div>
                        <div class="control-group" id="divOgr">
                            <label for="select" class="control-label" runat="server" id="lblOgr">Öğrenci</label>
                            <div class="controls">
                                <select name="select" id="slcOgrenci" class="input-large" runat="server"></select>
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <!-- Combobox  -->
                        <div class="control-group ">
                            <label for="textfield" class="control-label ">Ad</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtAd" class="input-xlarge" runat="server" data-rule-required="true" />
                            </div>
                        </div>
                        <div class="control-group ">
                            <label for="textfield" class="control-label ">Soyad</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtSoyad" class="input-xlarge" runat="server" data-rule-required="true" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Sınıf</label>
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
                            <label for="select" class="control-label ">Mahalle</label>
                            <div class="controls">
                                <select name="select" id="slcMahalle" class="input-large" runat="server"></select>
                            </div>
                        </div>
                    </div>
                    <div class="span12">
                        <div class="form-actions">
                            <button type="submit" role="button" id="btnSubmit" class="btn btn-primary" onclick="return OnSubmit(this);">Kaydet</button>
                            <a href="<%=GenerateListUrl() %>" role="button" class="btn">Cancel</a>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

</asp:Content>
