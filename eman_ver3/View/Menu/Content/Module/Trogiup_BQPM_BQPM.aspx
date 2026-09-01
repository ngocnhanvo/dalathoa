<%@ Page Language="C#" %>
    <style type="text/css">
        .sub_mainpage table tr td
        {
            padding:4px;
            color: #0073C0;
        }
    </style>
<div class="sub_mainpage">
    <div class="menu_div_dongkhung">
        <table style="padding: 10px 0 0 11px;width: 100%;">
        <tr><td><label>Phần mềm quản lý doanh nghiệp eMan</label></td></tr>
        <tr><td><label>Bản quyền thuộc về Công ty TNHH Giải Pháp Trực Tuyến</label></td></tr>
        <tr><td><label>Phát triển bởi eDoc Team</label> </td></tr>
        <tr><td><label>Phiên bản eMan 1.0</label> </td></tr>
        <tr><td><label>Hỗ trợ tốt nhất trên các trình duyệt Chrome, Firefox 3 trở lên.</label> </td></tr>
        <tr><td><label>Lưu ý: Phần mềm của chúng tôi không hỗ trợ tất cả các trình duyệt khác, trừ các phiên bản trình duyệt nói trên.</label> </td></tr>
        <tr><td><label>Mọi thông tin chi tiết về sản phẩm quý khách hàng vui lòng tham khảo thêm tại website <a target="_blank" href="http://esc.vn">http://esc.vn</a></label> </td></tr>
        </table>
    </div>
</div>

<script type="text/javascript">
    remove_module_1_2();
    $('.sub_mainpage').css('height', window.innerHeight - $('#input_docaogrid').val() + 112);  
</script>
