<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="OgrenciBilgisiEkle.aspx.cs" Inherits="KasifPortalApp.KasifPages.Forms.OgrenciBilgisiEkle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        //$(window).load(function () {
        //    $('#thankyouModal').modal('show');
        //});



        //$(document).ready(function () {

        //});



        <%--function func2() {
            var url = "<%= ResolveClientUrl("~/KasifPages/Forms/OgrenciBilgisiEkle.aspx") %>";
            var data = "basicString";
            $.post(url, data).done(function () {
                showModal(null, null, null);

            });
        }


        function AjaxOnSubmit() {

            


            $.ajax({
                type: "GET",
                url: "<%= ResolveClientUrl("~/KasifPages/Forms/OgrenciBilgisiEkle.aspx/ExecuteOnSubmit") %>",

                data: '{form1: "' + document.getElementById('form1') + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                success: function (result) {

                },
                failure: function (response) {
                    showModal(null, 'Beklenmeyen bir hata oluştu.', null);
                },
                error: function (response) {
                    showModal(null, 'Beklenmeyen bir hata oluştu.', null);
                }
            });
        }
        function PrepareDataForAjax()
        {
            var aoData;
            aoData.push({ "name": "IsCheckedReqBody", "value": IsCheckedReqBody });
            aoData.push({ "name": "IsChecked", "value": IsChecked });
            aoData.push({ "name": "hour1", "value": hour1 });
            aoData.push({ "name": "hour2", "value": hour2 });
            aoData.push({ "name": "minute1", "value": minute1 });
            aoData.push({ "name": "minute2", "value": minute2 });
            return aoData;
        }--%>




    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
    <h3>Öğrenci Bilgisi Ekle</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3><i class="icon-list"></i>Öğrenci Ekle</h3>
            </div>
            <div class="box-content nopadding">
                <form id="form1" action="#" method="POST" class='form-horizontal form-column form-bordered' runat="server">
                    <div class="span6">
                        <div class="control-group">
                            <label for="textfield" class="control-label">Adı</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtOgrAdi" class="input-xlarge" runat="server" />
                            </div>
                        </div>
                        <div class="control-group ">
                            <label for="txtOgrSoyadi" class="control-label ">Soyadı</label>
                            <div class="controls">
                                <input type="text" name="txtOgrSoyadi" id="txtOgrSoyadi" class="input-xlarge" runat="server" />
                            </div>
                        </div>
                        <!-- Combobox  -->
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
                            <label for="textfield" class="control-label ">Okul</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtOkul" class="input-xlarge" runat="server" />
                            </div>
                        </div>
                        <!-- Combobox  -->
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Mahalle</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtMahalle" class="input-xlarge" runat="server">
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Öğrenci Email</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtEmail" class="input-xlarge" runat="server">
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Veli Email</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtVeliEmail" class="input-xlarge" runat="server">
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
                            <label for="textfield" class="control-label ">Doğum Yeri</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtDogumYeri" class="input-xlarge" runat="server">
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Öğrencinin Telefonu</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtOgrTel"  class="input-xlarge mask_phone" runat="server">
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Veli Adı</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtVeliAdi" class="input-xlarge" runat="server">
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Velinin Telefonu</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtVeliTel" class="input-xlarge mask_phone" runat="server">
                            </div>
                        </div>
                        <div class="control-group">
                            <label for="textfield" class="control-label ">Okul Numarası</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtOkulNo" class="input-xlarge" runat="server">
                            </div>
                        </div>
                    </div>
                    <div class="span12">
                        <div class="form-actions">
                            <button type="submit" role="button" id="btnSubmit" class="btn btn-primary">Save changes</button>
                            <%--<a href="<%=GenerateListUrl() %>" onclick="GoToSubmit(this)" role="button" class="btn notify" data-notify-time="1000" data-notify-title="Success!" data-notify-message="The user has been successfully edited.">Timed fade notification (1second)</a>--%>
                            <a href="<%=GenerateListUrl() %>" role="button" class="btn">Cancel</a>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>

</asp:Content>
