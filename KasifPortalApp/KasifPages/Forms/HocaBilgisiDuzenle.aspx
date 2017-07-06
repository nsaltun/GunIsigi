<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="HocaBilgisiDuzenle.aspx.cs" Inherits="KasifPortalApp.KasifPages.Forms.HocaBilgisiDuzenle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {

        });

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
    <h3>Hoca Bilgisi Düzenle</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3><i class="icon-list"></i>Hoca Bilgisi Düzenle</h3>
            </div>
            <div class="box-content nopadding">
                <form id="form1" action="#" method="POST" class='form-horizontal form-column form-bordered' runat="server">
                    <div class="span6">
                        <div class="control-group">
                            <label for="textfield" class="control-label">Adı</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtAd" class="input-xlarge" runat="server" />
                            </div>
                        </div>
                        <div class="control-group ">
                            <label for="txtSoyad" class="control-label ">Soyadı</label>
                            <div class="controls">
                                <input type="text" name="txtSoyad" id="txtSoyad" class="input-xlarge" runat="server" />
                            </div>
                        </div>
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
                            <label for="select" class="control-label ">Mahalle</label>
                            <div class="controls">
                                <select name="select" id="slcMahalle" class="input-large" runat="server"></select>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Tel No</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtTelNo" class="input-xlarge mask_phone" runat="server"/>
                            </div>
                        </div>
                    </div>
                    <div class="span6">
                        <!-- DateTime input -->
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Doğum Tarihi</label>
                            <div class="controls">
                                <div class="input-append">
                                    <input type="text" class="input-large datepick" id="txtDogumTarihi" rel="4" runat="server" />
                                    <span class="add-on"><i class="icon-calendar"></i></span>
                                </div>
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Email</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtEmail" class="input-xlarge" runat="server" />
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textarea" class="control-label ">Diğer Bilgiler</label>
                            <div class="controls">
                                <textarea name="textarea" id="txtDiger" rows="6" class="input-block-level" runat="server"></textarea>
                            </div>
                        </div>
                    </div>
                    <div class="span12">
                        <div class="form-actions">
                            <button type="submit" role="button" id="btnSubmit" class="btn btn-primary">Kaydet</button>
                            <%--<a href="<%=GenerateListUrl() %>" onclick="GoToSubmit(this)" role="button" class="btn notify" data-notify-time="1000" data-notify-title="Success!" data-notify-message="The user has been successfully edited.">Timed fade notification (1second)</a>--%>
                            <a href="<%=GenerateListUrl() %>" role="button" class="btn">Geri dön</a>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

</asp:Content>
