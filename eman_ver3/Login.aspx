<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="shortcut icon" href="images/logo/favicon.ico" />
    <title>Đăng nhập hệ thống</title>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="css/VNN_css/Login.css" rel="stylesheet" type="text/css" />
    <script src="js/Public_script/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="js/VNN_script/GridFunction_Admin.js" type="text/javascript"></script>
    <script src="https://unpkg.com/html5-qrcode"></script>
</head>
<body class="login">
    <div id="qrcodeBG">
        <div id="qr-reader">
        </div>
        <input type="button" id="btnThoatVid" value="ngưng quét mã QRCode và thoát màn hình này" />
    </div>
    <div class="container-login100">
        <div class="wrap-login100">
            <form class="login100-form validate-form" name="form1" method="post" runat="server" action="Login.aspx" id="form1">
                <table id="tb_from">
                    <tr>
                        <td colspan="3">
                            <span class="login100-form-title">Đăng nhập</span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="height: 10px"></div>
                        </td>
                    </tr>
                    <tr id="trMaTaiKhoan1" runat="server">
                        <td>
                            <div class="wrap-input100 validate-input">
                                <asp:TextBox TabIndex="1" placeholder="Ví dụ: admin" autocomplete="off" CssClass="input100" ID="txtTaiKhoan" runat="server"></asp:TextBox>
                                <span class="label-input100">Tài khoản</span>
                            </div>
                        </td>
                    </tr>
                    <tr id="trMaTaiKhoan2" runat="server">
                        <td>
                            <div style="height: 1px"></div>
                        </td>
                    </tr>
                    <tr id="trMatKhau1" runat="server">
                        <td>
                            <div class="wrap-input100 validate-input">
                                <asp:TextBox TabIndex="2" placeholder="Lưu ý: Nhập đúng chữ hoa chữ thường" CssClass="input100" ID="txtMatKhau" TextMode="Password" runat="server"></asp:TextBox>
                                <span class="label-input100">Mật khẩu</span>
                                <span id="togglePassword">👁️</span>
                            </div>
                        </td>
                    </tr>
                    <tr id="trMatKhau2" runat="server">
                        <td>
                            <div style="height: 1px"></div>
                        </td>
                    </tr>
                    <tr id="trNhoMatKhau" runat="server">
                        <td>
                            <div>
                                <asp:CheckBox ID="chkRememberMe" runat="server" Text="Giữ tôi luôn đăng nhập" CssClass="remember-me-checkbox" />
                            </div>
                        </td>
                    </tr>
                    <tr id="trMaXacThuc1" runat="server">
                        <td style="position: relative">
                            <asp:TextBox TabIndex="3" autocomplete="off" placeholder="Nhập mã xác thực" ID="txtMaXacThuc" runat="server"></asp:TextBox>
                            <img id="btnVideo" src="images/icon/video.png" />
                        </td>
                    </tr>
                    <tr id="trMaXacThuc2" runat="server">
                        <td>
                            <div style="height: 1px"></div>
                        </td>
                    </tr>
                    <tr id="trMaXacThuc3" runat="server">
                        <td>
                            <asp:Button TabIndex="4" ID="btnLogin" CssClass="login100-form-btn" runat="server" Text="Đăng nhập" OnClientClick="setTimeout(()=>{ this.disabled = true; },10);" OnClick="btnLogin_Click" />
                        </td>
                    </tr>
                    <tr id="trMaXacThuc4" runat="server">
                        <td>
                            <asp:Button TabIndex="4" ID="btnVerify" CssClass="fm-button ui-state-default ui-corner-all fm-button-icon-left ui-state-hover" runat="server" Text="Xác nhận" OnClientClick="setTimeout(()=>{ this.disabled = true; },10);" OnClick="btnVerify_Click" />

                            <span id="thoigianxacthuc"></span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblCaption" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </form>
            <div class="login100-more" style="background-image: url('images/Login_images/bg-02.webp');"></div>
        </div>
    </div>

    <script type="text/javascript">
        let time = new Date('<%=Session["time"]%>');
        let inter = setInterval(function () {
            let now = new Date();
            let total = Math.round(120 - ((now - time) / 1000));

            if (total <= 0) {
                clearInterval(inter);
                total = '';
                alert('Login again, please');
                window.location.href = '';
            }
            else {
                total = total + ' giây';
            }

            $('#thoigianxacthuc').html(total);
        }, 1000);

        $('#btnVideo').click(function () {
            $('#qrcodeBG').show();

            var html5QrcodeScanner = new Html5QrcodeScanner("qr-reader", { fps: 10, qrbox: 250 });
            html5QrcodeScanner.render(onScanSuccess);
        });

        $('#btnThoatVid').click(function () {
            let $btn = $('#html5-qrcode-button-camera-stop');
            if ($btn.attr('id')) {
                $btn.click();
            }
            $('#qrcodeBG').hide();
        });

        $('#togglePassword').click(function () {
            // Lấy phần tử textbox mật khẩu thông qua ID của ASP.NET
            // Lưu ý: ID trong ASP.NET khi render ra trình duyệt có thể bị đổi, 
            // nhưng với bản bạn đang dùng thì thường là #txtMatKhau
            const passwordInput = $('#<%=txtMatKhau.ClientID%>');
            const type = passwordInput.attr('type') === 'password' ? 'text' : 'password';

            passwordInput.attr('type', type);

            // Đổi icon khi nhấn (tùy chọn)
            $(this).html(type === 'password' ? '👁️' : '👓');
        });

        let lastResult, countResults = 0;
        function onScanSuccess(decodedText, decodedResult) {
            if (decodedText !== lastResult) {
                ++countResults;
                lastResult = decodedText;
                if (decodedResult.decodedText) {
                    $('#txtMaXacThuc').val(decodedResult.decodedText);
                    $('#btnThoatVid').click();
                    $('#btnVerify').click();
                }
            }
        }
    </script>
</body>
</html>
