<%@ Page Language="C#" %>
<style type="text/css">
        .nhan-select
        {
            width:150px;
            margin-left:3px;
        }
        #btn_in
        {
            font-size: large;
            border-radius: 10px;
            width: 100px;
            background-color: #8BAADA !important;
        }
         #btn_in:hover
         {
             background-color:Blue !important;
         }
         label
         {
             font-size: 13px!important;
            color: #0073C0 !important;
         }
         
        #nhan-chonphongban
        {
            margin-left:33px
        }
        .send_hotro
        {
            margin-left: 23px;
            margin-top: 14px;
            width: 65px;
            height: 30px;
            font-family: cursive;
            background-color: #6DB7CE;
            border-radius: 7px;
        }
        .send_hotro:hover
        {
            background-color:rgb(115, 115, 209) !important;
             color:White;
             padding:1px;
        }
        
        div.sub_mainpage table tr td
        {
            padding:4px;
            color: #0073C0;
        }
    </style>

<div class="sub_mainpage">
    <div class="menu_div_dongkhung">
        <table style="padding: 10px 0 0 11px;width: 100%;">
        <tr><td>Công ty TNHH Giải pháp trực tuyến</td></tr>
        <tr><td>OnlinE Solution Co. Ltd (ESC)</td></tr>

        <tr><td>A16 đường số 18,P. Hiệp Bình Chánh, Q.Thủ Đức</td></tr>
        <tr><td>Tel: (84-8) 6284 30 60 - 629 44 808</td></tr>
        <tr><td>Fax: (84-8) 6258 14 09</td></tr>
        </table>

        <table style="padding: 10px 0 0 11px;">

        <tr>
         <td>Tiêu đề:</td>
         <td><input id="tieude" name="tieude" /><a style="margin-left:20px">Người liên hệ</a>
         <input id="email" name="email" style="text-align:center" value="e-sales@edoc.com.vn" /></td>
         <td></td>
         </tr>
         <tr>
        <td valign="top" style="margin:10px">Nội dung:</td>
        <td ><textarea rows="2" cols="2" style="width: 500;height: 131; float:left" id="noidung" name="noidung"></textarea></td>
        <td valign="bottom">
        <input class="send_hotro" onclick="send_hotro()" name="send_hotro" type="button" value="Gửi đi" />
        <br />
        <input class="send_hotro" onclick="lammoi_hotro()" name="send_hotro" type="button" value="Làm mới" />
        </td>
        </tr>
        <tr>
            <td>
            </td>

            <td>
                <div style="margin-top:10px" id="thongbao"></div>
            </td>
        </tr>
        </table>
    </div>
</div>
<script type="text/javascript"> 
    function send_hotro() {
        $.ajax({
            type: 'post',
            url: 'action/Send_hotro.ashx?email=' + $('#email').val() + '&noidung=' + $('#noidung').val() + '&tieude=' + $('#tieude').val(),
            success: function (response) {
                if (response == 1) {
                    $('#thongbao').empty();
                    $('#thongbao').append('<a style="color:blue">đã gửi thông báo cần hỗ trợ đến "' + $('#email').val() + '"</a>');
                }
                else if (response == 2) {
                    $('#thongbao').empty();
                    $('#thongbao').append('<a style="color:red">Phải nhập đầy đủ thông tin cho các trường dữ liệu</a>');
                }
                else {
                    $('#thongbao').empty();
                    $('#thongbao').append('<a style="color:red">gửi thất bại!!! lỗi đường truyền</a>');
                }
            }
        })
    }
    //--
    function lammoi_hotro() {
        $('#noidung').val('');
        $('#tieude').val('');
        $('#thongbao').empty('');
    }
    //--
    remove_module_1_2();
    $('.sub_mainpage').css('height', window.innerHeight - $('#input_docaogrid').val() + 112); 
</script>
