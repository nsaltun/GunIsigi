<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.Master" AutoEventWireup="true" CodeBehind="DersBilgisiEkle.aspx.cs" Inherits="KasifPortalApp.KasifPages.Forms.DersBilgisiEkle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {

        });

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphTitle" runat="server">
    <h3>Ders Bilgisi Ekle</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphContent" runat="server">
    <div class="span12">
        <div class="box box-color box-bordered">
            <div class="box-title">
                <h3><i class="icon-list"></i>Ders Ekle</h3>
            </div>
            <div class="box-content nopadding">
                <form id="form1" action="#" method="POST" class='form-horizontal form-column form-bordered' runat="server">
                    <div class="span6">
                        <div class="control-group">
                            <label for="textfield" class="control-label">Ders Adı</label>
                            <div class="controls">
                                <input type="text" name="textfield" id="txtAd" class="input-xlarge" runat="server" />
                            </div>
                        </div>
                    </div>
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
