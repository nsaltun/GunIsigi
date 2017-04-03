<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="ChangePwd.aspx.cs" Inherits="KasifPortalApp.Management.Tables.ChangePwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {

        })

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
    <h3>Şifreni Değiştir</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3><i class="icon-list"></i>Şifreni Değiştir</h3>
            </div>
            <div class="box-content nopadding">
                <form id="form1" action="#" method="POST" class='form-horizontal form-column form-bordered' runat="server">
                    <input type="hidden" runat="server" id ="hiddenField1"/>
                    <input type="hidden" runat="server" id ="hiddenField2"/>
                    <div class="span6">
                        
                        <div class="control-group ">
                            <label for="textfield" class="control-label ">Eski şifre</label>
                            <div class="controls">
                                <input type="password" name="textfield" id="txtOldPwd" class="input-xlarge" runat="server" data-rule-required="true" />
                            </div>
                        </div>
                        <div class="control-group ">
                            <label for="textfield" class="control-label ">Yeni şifre</label>
                            <div class="controls">
                                <input type="password" name="textfield" id="txtNewPwd" class="input-xlarge" runat="server" data-rule-required="true" />
                            </div>
                        </div>
                        <div class="control-group ">
                            <label for="textfield" class="control-label ">Yeni şifre tekrar</label>
                            <div class="controls">
                                <input type="password" name="textfield" id="txtNewPwdAgain" class="input-xlarge" runat="server" data-rule-required="true" />
                            </div>
                        </div>
                    </div>
                    
                    <div class="span12">
                        <div class="form-actions">
                            <button type="submit" role="button" id="btnSubmit" class="btn btn-primary" >Kaydet</button>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

</asp:Content>
