<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="UserTableAdd.aspx.cs" Inherits="KasifPortalApp.Management.Forms.UserTableAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            var slcAdmin = document.getElementById("<%=slcAdmin.ClientID%>");
            
            $(slcAdmin).change(function () {
                var slcSinif = document.getElementById("<%=slcSinif.ClientID%>");
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

        });

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
                            <button type="submit" role="button" id="btnSubmit" class="btn btn-primary">Save changes</button>
                            <a href="<%=GenerateListUrl() %>" role="button" class="btn">Cancel</a>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

</asp:Content>
