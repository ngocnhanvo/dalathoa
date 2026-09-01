<%@ Page Language="C#" %>

<%@ Import Namespace="System.Data.Linq" %>
<%@ Import Namespace="DataAcess" %>
<% 
    var db = new EntityContext();
    string idtk = Security.id_taikhoan(Context);
    var tk = db.ad_user.SingleOrDefault(p => p.ad_user_id.Equals(idtk));
    var ttc = db.ad_systemconfig.FirstOrDefault();
%>

<style type="text/css">
    #frm_system table tr td {
        padding: 10px;
    }

    table#tb_system {
        padding: 15px 10px;
        border: 2px solid rgb(63, 125, 150);
        border-radius: 0px 0px 12px 12px;
        margin: 30px auto;
        background-color: #FFF;
        max-width: 500px;
        width: 100%;
        user-select: none;
    }

    table#tb_system2 {
        padding: 29px 0px 9px 34px;
        border: 2px solid rgb(63, 125, 150);
        border-radius: 0px 0px 12px 12px;
        margin: -21px 0px 0px 0px;
    }

    .btnPress {
        width: 100%;
        padding: 5px;
    }

    input#logo, input#logo_tc {
        cursor: pointer;
    }

    input#txt_Logo, input#txt_Logo_tc {
        display: none;
        opacity: 0;
        position: absolute;
    }

    .tieude {
        margin: 14px 0px 3px 30px;
        padding: 9px;
        width: 180px;
        background-color: rgb(63, 125, 150);
        border-radius: 14px;
        color: White;
        z-index: 200;
        text-align: center;
    }

    form#frm_system {
        z-index: -1 !important;
        padding: 6px 12px 18px 30px;
    }

    #tb_system tr td label {
        color: #0A64A0;
        position: relative;
        top: -6px;
    }

    #tb_system tr td input, #tb_system tr td select {
        width: 100%;
    }

    input#txt_fileupload {
        width: 100%;
    }

    #btn_changeinformation:hover {
        opacity: 1;
        cursor: pointer;
        color: #c00000;
    }

    #nhan_cauhinhhethong {
        background-color: #DEF0F3;
    }
</style>

<div class="sub_mainpage">
    <form runat="server" id="frm_system" name="frm_system" action="Controller/PublicFunction/ConfigSystem.ashx?oper=configSystem" method="post">
        <div>
            <div id="main_hethong">
                <table id="tb_system">
                    <tr>
                        <td>
                            <label>Tên công ty </label><br />
                            <input type="text" id="txt_tencongty" name="txt_tencongty" value="" runat="server" />
                        </td>

                        <td>
                            <label>Logo</label><br />
                            <input type="file" id="txt_Logo" onchange="hienthiduongdan('Logo')" onmouseover="hienthilogo('Logo')" name="txt_Logo" runat="server" />
                            <input type="text" id="Logo" onclick="$('#txt_Logo').click();" name="Logo" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label>Địa chỉ</label><br />
                            <input type="text" id="txt_diachi" name="txt_diachi" runat="server" />
                        </td>

                        <td>
                            <label>Điện thoại</label><br />
                            <input type="text" id="txt_dienthoai" name="txt_dienthoai" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label>Version</label><br />
                            <input type="text" id="txt_Fax" name="txt_Fax" runat="server" />
                        </td>

                        <td>
                            <label>Bật SSL</label><br />
                            <select id="txt_website" name="txt_website" runat="server">
                                <option value="0">Tắt</option>
                                <option value="1">Bật</option>
                            </select>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label>Email server </label><br />
                            <input type="text" id="txt_emailserver" name="txt_emailserver" runat="server" />
                        </td>

                        <td>
                            <label>Port </label><br />
                            <input type="text" id="txt_port" name="txt_port" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label>Tài khoản email </label><br />
                            <input type="text" id="txt_email" name="txt_email" runat="server" />
                        </td>

                        <td>
                            <label>Mật khẩu </label><br />
                            <input type="password" id="txt_matkhau" name="txt_matkhau" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label>Định dạng ngày </label><br />
                            <input type="text" id="txt_format_ngay" name="txt_format_ngay" runat="server" />
                        </td>

                        <td>
                            <label>Định dạng số </label><br />
                            <input type="text" id="txt_format_so" name="txt_format_so" runat="server" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <input type="button" class="btnPress" onclick="themthongtinchung()" value="Lưu thông tin đã thiết lập" id="btn_changeinformation" name="btn_changeinformation" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <input type="button" class="btnPress" onclick="xoaRacChuongTrinh()" value="Xóa rác chương trình" id="btnClean" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</div>
<script type="text/javascript">
    function themthongtinchung() {
        $('.sub_mainpage').prepend('<div class="nhan_loading">&nbsp;</div>');
        $('#frm_system').ajaxSubmit({
            success: function (response) {
                $('.nhan_loading').remove();
                alert(response);
            }
        });
    }

    function xoaRacChuongTrinh() {
        $('.sub_mainpage').prepend('<div class="nhan_loading">&nbsp;</div>');
        $.post('Controller/PublicFunction/ConfigSystem.ashx?oper=clean', function (result) {
            $('.nhan_loading').remove();
            alert(result);
        });
    }

    $.ajax({
        type: 'GET',
        url: 'Controller/PublicFunction/ConfigSystem.ashx?oper=loadData',
        success: function (response) {
            response = response.split('#');
            $('#txt_tencongty').val(response[0]);
            $('#txt_diachi').val(response[1]);
            $('#txt_dienthoai').val(response[2]);
            $('#txt_Fax').val(response[3]);
            $('#txt_website').val(response[4]);
            $('#txt_emailserver').val(response[5]);
            $('#txt_email').val(response[7]);
            $('#txt_matkhau').val(response[8]);
            $('#txt_port').val(response[6]);
            $('#Logo').val(response[9]);
            $('#txt_tencanhbao').val(response[10]);
            $('#txt_soluong_grid').val(response[11]);
            $('#txt_soluong_grid_2').val(response[12]);
            $('#Logo_tc').val(response[13]);
            $('#txt_url_linq').val(response[14]);
            $('#txt_ten_db').val(response[15]);
            $('#txt_ten_linq').val(response[16]);
            $('#txt_ten_connectstring').val(response[17]);
            $('#txt_format_ngay').val(response[18]);
            $('#txt_format_so').val(response[19]);
            $('#txt_connectstring_anco').val(response[20]);
            $('#txt_domain').val(response[21]);
            $('#txt_email_hotro').val(response[22]);
        }
    });

    function hienthilogo(e) {
        $('#' + e).css("cursor", "pointer");
    }

    function hienthiduongdan(e) {
        $('#' + e).val('' + $('#txt_' + e).val().replace('C:\\fakepath\\', ''));
    }

    $('#nhan-cho').remove();
    $('.sub_mainpage').css('height', window.innerHeight - $('#input_docaogrid').val() + 112);
</script>
