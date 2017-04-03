<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="~/LoginPage.aspx.cs" Inherits="KasifPortalApp.Authentication.LoginPage" EnableViewState="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
    <!-- Apple devices fullscreen -->
    <meta name="apple-mobile-web-app-capable" content="yes">
    <!-- Apple devices fullscreen -->
    <meta names="apple-mobile-web-app-status-bar-style" content="black-translucent">

    <title>Kâşif Portal - Giriş</title>

    <!-- Bootstrap -->
    <link href="<%= ResolveClientUrl("~/Common/Template/css/bootstrap.min.css") %>" rel="stylesheet" />
    <!-- Bootstrap responsive -->
    <link href="<%= ResolveClientUrl("~/Common/Template/css/bootstrap-responsive.min.css") %>" rel="stylesheet" />
    <!-- icheck -->
    <link href="<%= ResolveClientUrl("~/Common/Template/css/plugins/icheck/all.css") %>" rel="stylesheet" />
    <!-- Theme CSS -->
    <link href="<%= ResolveClientUrl("~/Common/Template/css/style.css") %>" rel="stylesheet" />
    <!-- Color CSS -->
    <link href="<%= ResolveClientUrl("~/Common/Template/css/themes.css") %>" rel="stylesheet" />


    <!-- jQuery -->
    <script src="<%= ResolveClientUrl("~/Common/Template/js/jquery.min.js")%>"></script>

    <!-- Nice Scroll -->
    <script src="<%= ResolveClientUrl("~/Common/Template/js/plugins/nicescroll/jquery.nicescroll.min.js") %>"></script>
    <!-- Validation -->
    <script src="<%= ResolveClientUrl("~/Common/Template/js/plugins/validation/jquery.validate.min.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/Common/Template/js/plugins/validation/additional-methods.min.js") %>"></script>
    <!-- icheck -->
    <script src="<%= ResolveClientUrl("~/Common/Template/js/plugins/icheck/jquery.icheck.min.js") %>"></script>
    <!-- Bootstrap -->
    <script src="<%= ResolveClientUrl("~/Common/Template/js/bootstrap.min.js") %>"></script>
    <script src="<%= ResolveClientUrl("~/Common/Template/js/eakroko.js") %>"></script>

    <!--[if lte IE 9]>
		<script src="js/plugins/placeholder/jquery.placeholder.min.js"></script>
		<script>
			$(document).ready(function() {
				$('input, textarea').placeholder();
			});
		</script>
	<![endif]-->


    <!-- Favicon -->
    <link rel="shortcut icon" href="<%= ResolveClientUrl("~/Common/Template/img/favicon.ico") %>">
    <!-- Apple devices Homescreen icon -->
    <link rel="apple-touch-icon-precomposed" href="<%= ResolveClientUrl("~/Common/Template/img/apple-touch-icon-precomposed.png") %>">

    <script>
        function showErrorModal(header, msg) {
            $('#errorModal > .modal-header > h3 > span#lblErrorModalHeader').html(header);
            $('#errorModal > .modal-body').html(msg);
            $('#errorModal').modal({
                backdrop: true,
                keyboard: false
            });
        }

    </script>

</head>
<body class="login">
    <div class="wrapper">
        <h1><a href="#">
            <img src="<%= ResolveClientUrl("~/Common/Template/img/logo-big.png") %>" alt="" class="retina-ready">KÂŞİF PORTAL</a></h1>
        <div class="login-body">
            <h2>GİRİŞ</h2>
            <form runat="server" method="get" class="form-validate" id="test" novalidate="novalidate">
                <div class="control-group">
                    <div class="email controls">
                        <input type="text" id="txtEmail" runat="server" name="uemail" placeholder="Email" class="input-block-level" data-rule-required="true" data-rule-email="true">
                    </div>
                </div>
                <div class="control-group">
                    <div class="pw controls">
                        <input type="password" id="txtPwd" runat="server" name="upw" placeholder="Şifre" class="input-block-level" data-rule-required="true">
                    </div>
                </div>
                <div class="submit">
                    <%--<div class="remember">
						<div class="icheckbox_square-blue" style="position: relative;">
                            <input type="checkbox" name="remember" class="icheck-me" data-skin="square" data-color="blue" id="remember" style="position: absolute; top: -10%; left: -10%; display: block; width: 120%; height: 120%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;"><ins style="position: absolute; top: -10%; left: -10%; display: block; width: 120%; height: 120%; margin: 0px; padding: 0px; background: rgb(255, 255, 255); border: 0px; opacity: 0;"></ins>
						</div> 
                        <label for="remember">Beni Hatırla</label>
					</div>--%>
                    <%--<input runat="server" type="submit" value="Giriş" class="btn btn-primary">--%>
                    <asp:Button ID="btnSubmit" runat="server" Text="Giriş" CssClass="btn btn-primary" OnClick="btnSubmit_Click" />
                </div>
                <div>
                    <label id="lblError" runat="server" style="display:none;color:red;">test</label>
                </div>
            </form>
            <div class="forget">
                <a href="#"><span>Şifremi Unuttum?</span></a>
            </div>
        </div>
    </div>
    <div id="errorModal" class="modal hide fade">
        <div class="modal-header">
            <h3><span id="lblErrorModalHeader" style="color: red;"></span>
            </h3>
        </div>
        <div class="modal-body">
        </div>
        <div class="modal-footer">
            <button class="btn btn-primary" data-dismiss="modal" onclick="$('#errorModal').modal('hide');">OK</button>
        </div>
        <div class="modal-footer-strip">
        </div>
    </div>
</body>
</html>
