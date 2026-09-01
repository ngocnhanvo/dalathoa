<%@ Page Language="C#" %>

<style type="text/css">
    td.auto-style1 {
		padding: 4px;
    }
</style>

<div class="sub_mainpage">
        <div style="margin-left:10px; font-weight:bold;font-size:15px; color:red" id="lblCaption"></div>
        <table id="tb_changepass" style="padding: 10px 0 0 11px;">
            <tr>
                <td>Mật khẩu cũ:</td>
                <td class="auto-style1">
                    <input id="txtMKcu" type="password" name="txtMKcu" /></td>
            </tr>
            <tr>
                <td>Mật khẩu mới:</td>
                <td class="auto-style1"><input type="password" id="txtMKmoi" name="txtMKmoi" /></td>
            </tr>
            <tr>
                <td>Xác nhận mật khẩu:</td>
                <td class="auto-style1"><input type="password" id="txtXNMK" name="txtXNMK" /></td>
            </tr>
            <tr>
                <td>
                
                </td>
                <td class="auto-style1">
                    <input type="button" onclick="doipass()" value="Đồng ý" id="btnChangepass" name="btnChangepass" />
                </td>
            </tr>
        </table>
</div>

<script type="text/javascript">
    function doipass() {
        $(function () {
                $.ajax({
                    type: "POST",
                    url: "<%=Security.UrlBase()%>Controller/PublicFunction/ChangePassword.ashx",
                    data: {
                        oldpassword: $('#txtMKcu').val()
                        , newpassword: $('#txtMKmoi').val()
                        , confirm: $('#txtXNMK').val()
                    },
                    success: function (rs) {
                        $('#lblCaption').html(rs);
                    },
                });
        });
    }
    //--
    $('.sub_mainpage').css('height', window.innerHeight - 104);
</script>