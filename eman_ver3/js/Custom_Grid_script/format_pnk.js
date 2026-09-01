//start CA_01_ThemPNK
function format_pnk(elem) {
    var id_elem = $(elem).attr('id');
    $(elem).keypress(function (e) {
        if (e.which == '13') {
            CA_01_ThemPNK(id_elem, 1);
        }
    });
    $(elem).parent().append('<span onclick="timkiem_pnk(\'' + id_elem + '\')" ' +
        'class="span_format_lenhsx glyphicon glyphicon-search" style="position: absolute;margin: 1px 0 0 -21px;background-color: rgb(248, 249, 243);padding: 3.5px;cursor: pointer;border-left: 1px solid rgba(204, 204, 204, 0.63)" />');
}


function timkiem_pnk(id_elem, type) {
    var load_sp = 0;
    var namegrid = "gridMD_00_NhapkhotuNCC_cp";

    $('body').append('<div id="dlg_gridSmal_2" title="Tìm kiếm phiếu nhập kho">' +
        '<div class="dlg_content">' +
        '<div class="err_MD_00_NhapkhotuNCC"></div>' +
        '<table id="' + namegrid + '"></table>' +
        '<div id="pager' + namegrid + '"></div>' +

        '<table id="gridMD_01_DHNCC_cp"></table>' +
        '<div id="pagergridMD_01_DHNCC_cp"></div>' +
        '</div>' +
        '</div>');

    var multi = false;
    if (tengrid0 == 'gridMD_00_HoaDon') {
        multi = true;
    }

    var input_focus = '';
    $('#dlg_gridSmal_2').dialog({
        modal: true,
        dialogClass: "dialog_index",
        width: 800,
        height: window.innerHeight - 10,
        open: function (event, ui) {
            //luoi 1
            url = 'Controller/JqGrid/JQGridMD_00_NhapkhotuNCCLoad.ashx?ma_module=MD_00_NhapkhotuNCC&ma_menu=MN_01_DSDH&module_select=1&id_sel=1';

            $('#' + namegrid).jqGrid({
                url: url,
                height: window.innerHeight / 2 - 160,
                autowidth: true,
                datatype: 'json',
                shrinkToFit: true,
                rownumbers: true,
                viewrecords: true,
                search: true,
                scroll: false,
                rowNum: 50,
                multiselect: multi,
                multiboxonly: true,
                rowList: [1000],
                pager: '#pager' + namegrid,
                onSelectRow: function (ids) {
                    try {
                        checkbox_JQgrid(namegrid, 0);
                        cell = $('#' + namegrid).getRowData(ids);
                        if ($('#' + id_elem).prop('disabled') != true) {
                            /* 							$('#' + id_elem).val(cell['sochungtu']);
                                                        $('#donhang_thamchieu').val(cell['so_po']); */
                        }
                        $('#gridMD_01_DHNCC_cp')[0].triggerToolbar();
                    }
                    catch (r) {

                    }
                },
                colModel: [
                    { key: true, fixed: true, label: 'md_nhapkho_ncc_id', name: 'md_nhapkho_ncc_id', index: ' ncc.md_nhapkho_ncc_id ', width: 120, editable: false, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'md_nhapkho_ncc_id' } },
                    { key: false, fixed: true, label: 'c_donmuahang_id', name: 'c_donmuahang_id', index: ' ncc.c_donmuahang_id ', width: 120, editable: false, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'c_donmuahang_id' } },
                    { key: false, fixed: true, label: 'Trạng thái', name: 'trangthai', index: ' ncc.trangthai ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, formatter: Frm_MD_00_KHDHJQGS_trangthai, unformat: disable_formatter, align: 'left', stype: 'select', searchoptions: { sopt: ['bw'], value: { '': '', 'SOANTHAO': 'Soạn thảo', 'HIEULUC': 'Hiệu lực' } }, edittype: 'select', editoptions: { value: { 'SOANTHAO': 'Soạn thảo', 'HIEULUC': 'Hiệu lực', 'CHUACHUYENHET': 'Chưa xong' } }, frozen: false, formoptions: { label: 'Trạng thái' } },
                    { key: false, fixed: true, label: 'Số chứng từ', name: 'sochungtu', index: ' ncc.sochungtu ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { defaultValue: load_sct('PNK') }, frozen: false, formoptions: { label: 'Số chứng từ' } },
                    { key: false, fixed: true, label: 'Đơn mua hàng', name: 'donmuahang', index: 'dmh.sochungtu', width: 100, editable: false, hidden: false, align: 'center', searchoptions: { sopt: ['bw'] }, frozen: false, formoptions: { label: 'Đơn mua hàng' } },
                    { key: false, fixed: true, label: 'Đối tác kinh doanh', name: 'ten_dtkd', index: 'ncc.ten_dtkd', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', frozen: false, formoptions: { label: 'Đối tác kinh doanh' } },
                    { key: false, fixed: true, label: 'Kho', name: 'kho', index: 'kho.ten_kho', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'select', editoptions: { value: { '07010AF94B7B4E129DA3071B3786DCC7': 'KHO VẬT TƯ', 'F9853033611E40B28C9C9059897F31B7': 'KHO TEM-BAO BÌ', '15C2B297679D4050B0CB14E16A528854': 'KHO PALLET', 'FE561457AD4D42D79FB38AC512BCE402': 'KHO THÀNH PHẨM', 'A6CEDB9BD33A4EC8981A8103AD17DB77': 'KHO TỒN THÀNH PHẨM' } }, frozen: false, formoptions: { label: 'Kho' } },
                    { key: false, fixed: true, label: 'CT tham chiếu', name: 'sctdathang', index: 'ncc.sctdathang', width: 100, editable: false, hidden: false, align: 'center', searchoptions: { sopt: ['bw'] }, frozen: false, formoptions: { label: 'CT tham chiếu' } },
                    { key: false, fixed: true, label: 'Đơn hàng tham chiếu', name: 'donhang_thamchieu', index: ' ncc.donhang_thamchieu ', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', frozen: false, formoptions: { label: 'Đơn hàng tham chiếu' } },
                    { key: false, fixed: true, label: 'Số phiếu XNNK', name: 'phieuXNNK', index: ' ncc.phieuXNNK ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 50 }, frozen: false, formoptions: { label: 'Số phiếu XNNK' } },
                    { key: false, fixed: true, label: 'Ngày chuyển', name: 'ngaychuyen', index: ' ncc.ngaychuyen ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, formatter: esc_date, align: 'center', searchoptions: { sopt: ['cn'], dataInit: function (elem) { search_datetime(elem); } }, editoptions: { dataInit: function (elem) { format_datetime(elem); } }, frozen: false, formatoptions: { srcformat: 'm/d/Y', newformat: format_srcdatetime() }, formoptions: { label: 'Ngày chuyển' } },
                    { key: false, fixed: true, label: 'md_doitackinhdoanh_id', name: 'md_doitackinhdoanh_id', index: 'ncc.md_doitackinhdoanh_id', width: 120, editable: false, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'md_doitackinhdoanh_id' } },
                    { key: false, fixed: true, label: 'Người dùng / Liên hệ', name: 'nguoidung', index: ' ncc.nguoidung ', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', editoptions: { maxLength: 50 }, frozen: false, formoptions: { label: 'Người dùng / Liên hệ' } },
                    { key: false, fixed: true, label: 'Người tạo HT', name: 'nguoitao', index: ' ncc.nguoitao ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'Người tạo HT' } },
                    { key: false, fixed: true, label: 'Vai trò tạo HT', name: 'vaitrotao', index: ' ncc.vaitrotao ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'Vai trò tạo HT' } },
                    { key: false, fixed: true, label: 'Bộ phận tạo HT', name: 'bophantao', index: ' ncc.bophantao ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'Bộ phận tạo HT' } },
                    { key: false, fixed: true, label: 'Người cập nhật HT', name: 'nguoicapnhat', index: ' ncc.nguoicapnhat ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'Người cập nhật HT' } },
                    { key: false, fixed: true, label: 'Vai trò cập nhật HT', name: 'vaitrocapnhat', index: ' ncc.vaitrocapnhat ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'Vai trò cập nhật HT' } },
                    { key: false, fixed: true, label: 'Bộ phận cập nhật HT', name: 'bophancapnhat', index: ' ncc.bophancapnhat ', width: 95, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'Bộ phận cập nhật HT' } },
                    { key: false, fixed: true, label: 'Người tạo', name: 'value_nguoitao', index: ' ncc.value_nguoitao ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, formoptions: { label: 'Người tạo' } },
                    { key: false, fixed: true, label: 'Vai trò tạo', name: 'value_vaitrotao', index: ' ncc.value_vaitrotao ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, formoptions: { label: 'Vai trò tạo' } },
                    { key: false, fixed: true, label: 'Bộ phận tạo', name: 'value_bophantao', index: ' ncc.value_bophantao ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, formoptions: { label: 'Bộ phận tạo' } },
                    { key: false, fixed: true, label: 'Người cập nhật', name: 'value_nguoicapnhat', index: ' ncc.value_nguoicapnhat ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, formoptions: { label: 'Người cập nhật' } },
                    { key: false, fixed: true, label: 'Vai trò cập nhật', name: 'value_vaitrocapnhat', index: ' ncc.value_vaitrocapnhat ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, formoptions: { label: 'Vai trò cập nhật' } },
                    { key: false, fixed: true, label: 'Bộ phận cập nhật', name: 'value_bophancapnhat', index: ' ncc.value_bophancapnhat ', width: 95, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, formoptions: { label: 'Bộ phận cập nhật' } },
                    { key: false, fixed: true, label: 'Ngày tạo', name: 'ngaytao', index: ' ncc.ngaytao ', width: 90, editable: false, hidden: true, formatter: esc_date, align: 'center', searchoptions: { sopt: ['cn'], dataInit: function (elem) { search_datetime(elem); } }, editoptions: { dataInit: function (elem) { format_datetime(elem); } }, formatoptions: { srcformat: 'm/d/Y', newformat: format_srcdatetime() }, formoptions: { label: 'Ngày tạo' } },
                    { key: false, fixed: true, label: 'Ngày cập nhật', name: 'ngaycapnhat', index: ' ncc.ngaycapnhat ', width: 90, editable: false, hidden: true, formatter: esc_date, align: 'center', searchoptions: { sopt: ['cn'], dataInit: function (elem) { search_datetime(elem); } }, editoptions: { dataInit: function (elem) { format_datetime(elem); } }, formatoptions: { srcformat: 'm/d/Y', newformat: format_srcdatetime() }, formoptions: { label: 'Ngày cập nhật' } },
                    { key: false, fixed: true, label: 'Mô tả', name: 'mota', index: ' ncc.mota ', width: 150, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', formoptions: { label: 'Mô tả' } },
                    { key: false, fixed: true, label: 'check_hieuluc', name: 'check_hieuluc', index: 'ncc.check_hieuluc', width: 100, editable: false, hidden: true, formatter: 'checkbox', align: 'center', searchoptions: { sopt: ['bw'] }, edittype: 'checkbox', editoptions: { value: 'True:False', defaultValue: 'False' }, frozen: false, formoptions: { label: 'check_hieuluc' } },
                    { key: false, fixed: true, label: 'Hoạt động', name: 'hoatdong', index: ' ncc.hoatdong ', width: 75, editable: false, hidden: true, formatter: 'checkbox', align: 'center', stype: 'select', searchoptions: { sopt: ['bw'], value: ':Tất cả;1:Có;0:Không' }, edittype: 'checkbox', editoptions: { value: 'True:False', defaultValue: 'False' }, formoptions: { label: 'Hoạt động' } },

                ],
                beforeRequest: function () {
                    var str = $('#sct_thamchieu').val(), sct_tc = '';
                    var res = str.split("\n");

                    for (var nht = 0; nht <= res.length - 1; nht++) {
                        if (nht == 0) {
                            sct_tc = ' and dmh.sochungtu like N\'' + res[nht] + '\' ';
                        }
                        else {
                            sct_tc += ' or dmh.sochungtu like N\'' + res[nht] + '\' ';
                        }
                    }

                    $('#' + namegrid).jqGrid('getGridParam', 'postData').where_ex = ' and ncc.trangthai = \'HIEULUC\' ' + sct_tc + ' ';
					/*if ($('#' + id_elem).val() != '' & load_sp == 0) {
						$('#'+ namegrid).jqGrid('getGridParam', 'postData').filters = '{"groupOp":"AND","rules":[{"field":" ncc.sochungtu ","op":"eq","data":"' + $('#' + id_elem).val() + '"}]}';
                    }*/
                    input_focus = $('input:focus').attr('class');
                },
                ondblClickRow: function () {
                    if (tengrid0 == 'gridMD_00_HoaDon') {

                    }
                    else {
                        $('#dlg_gridSmal_2').dialog('destroy').remove();
                    }
                },
                gridComplete: function () {
                    $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width() - 5);
                },
                loadComplete: function (data) {
                    var top_rowid = $('#' + namegrid + ' tr:nth-child(2)').attr('id');
                    var countrow = jQuery("#" + namegrid).jqGrid('getGridParam', 'records');
                    if (load_sp == 0) {
                        $('.gs_sochungtu.gs_' + namegrid).val($('#' + id_elem).val());
                        $('#' + namegrid).jqGrid('setSelection', $('#' + id_elem).val());
                        load_sp = 1;
                        if (top_rowid.indexOf('<a style=\'color:red\'>') <= -1 & type == 1 & countrow == 1) {
                            $('#dlg_gridSmal_2').dialog('destroy').remove();
                        }
                    }
                    //giữ focus end
                    checkbox_JQgrid(namegrid, 1);
                    $('.' + input_focus).focus();
                },
                caption: 'Đơn hàng'
            });
            $('#' + namegrid).jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
            $('#gview_' + namegrid + ' .ui-jqgrid-title').css('float', 'left');

            //luoi 2
            jQuery('#gridMD_01_DHNCC_cp').jqGrid({
                url: 'Controller/JqGrid/JQGridMD_01_DHNCCLoad.ashx?ma_module=MD_01_DHNCC&ma_menu=MN_01_DDSDH&module_select=1&id_sel=1',
                height: window.innerHeight / 2 - 160,
                autowidth: true,
                datatype: 'json',
                shrinkToFit: true,
                rownumbers: true,
                viewrecords: true,
                search: true,
                scroll: false,
                rowNum: 50,
                multiselect: false,
                multiboxonly: false,
                rowList: [1000],
                pager: 'pagergridMD_01_DHNCC_cp',
                onSelectRow: function (ids) {

                },
                colModel: [
                    { key: true, fixed: true, label: 'md_nhapkho_ncc_dh_id', name: 'md_nhapkho_ncc_dh_id', index: ' ncc_dh.md_nhapkho_ncc_dh_id ', width: 120, editable: false, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: true, formoptions: { label: 'md_nhapkho_ncc_dh_id' } },
                    { key: false, fixed: true, label: 'md_nhapkho_ncc_id', name: 'md_nhapkho_ncc_id', index: ' ncc_dh.md_nhapkho_ncc_id ', width: 120, editable: false, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: true, formoptions: { label: 'md_nhapkho_ncc_id' } },
                    { key: false, fixed: true, label: 'STT', name: 'STT', index: ' ncc_dh.STT ', width: 120, editable: false, hidden: true, formatter: vnn_number, unformat: disable_formatter, align: 'left', searchoptions: { sopt: ['en'], dataInit: function (elem) { search_number(elem); } }, editoptions: { dataInit: function (elem) { format_number(elem, 1); } }, frozen: true, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' }, formoptions: { label: 'STT' } },
                    { key: false, fixed: true, label: 'Mã sản phẩm', name: 'md_sanpham_id', index: 'sp.ma_sanpham', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { dataInit: function (elem) { format_sanpham(elem); } }, frozen: true, formoptions: { label: 'Mã sản phẩm' } },
                    { key: false, fixed: true, label: 'Tên hàng', name: 'mota_tiengviet', index: ' ncc_dh.mota_tiengviet ', width: 240, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', frozen: false, formoptions: { label: 'Tên hàng' } },
                    { key: false, fixed: true, label: 'Tổng số lượng', name: 'tong_sl_dat', index: ' ncc_dh.tong_sl_dat ', width: 90, editable: true, editrules: { edithidden: true }, hidden: false, formatter: vnn_number, unformat: disable_formatter, align: 'right', searchoptions: { sopt: ['en'], dataInit: function (elem) { search_number(elem); } }, editoptions: { dataInit: function (elem) { format_number(elem, 0, 4); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' }, formoptions: { label: 'Tổng số lượng' } },
                    { key: false, fixed: true, label: 'SL đã nhập', name: 'sl_danhap', index: ' ncc_dh.sl_danhap ', width: 90, editable: true, editrules: { edithidden: true }, hidden: false, formatter: vnn_number, unformat: disable_formatter, align: 'right', searchoptions: { sopt: ['en'], dataInit: function (elem) { search_number(elem); } }, editoptions: { dataInit: function (elem) { format_number(elem, 0, 4); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' }, formoptions: { label: 'SL đã nhập' } },
                    { key: false, fixed: true, label: 'SL thực nhập', name: 'sl_nhap', index: ' ncc_dh.sl_nhap ', width: 90, editable: true, editrules: { edithidden: true }, hidden: false, formatter: vnn_number, unformat: disable_formatter, align: 'right', searchoptions: { sopt: ['en'], dataInit: function (elem) { search_number(elem); } }, editoptions: { dataInit: function (elem) { format_number(elem, 0, 4); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' }, formoptions: { label: 'SL thực nhập' } },
                    { key: false, fixed: true, label: 'SLDK nhập kho', name: 'sl_muonnhap', index: 'ncc_dh.sl_muonnhap', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, formatter: vnn_number, unformat: disable_formatter, align: 'right', searchoptions: { sopt: ['bw'], dataInit: function (elem) { search_number(elem); } }, editoptions: { dataInit: function (elem) { format_number(elem, 0, 4); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' }, formoptions: { label: 'SLDK nhập kho' } },
                    { key: false, fixed: true, label: 'Đơn vị tính', name: 'md_donvitinhsanpham_id', index: 'dv.ten_dvt', width: 90, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'select', editoptions: { value: { 'B39508EBF37A4FC6A3F72272CCDEC9D3': 'bịch', 'C658EC9C5D4D489E8D468B4786597C8B': 'Bình', 'C6902C484D7C42E5A0053AEDD4083B20': 'Bô', 'EA4E867353894D6585E527B6AE746290': 'Bộ', '3C6BD157D2C744DDB40FED430FA5AD12': 'Cái', '7F078B119A9549A98CBC53D7090C1D63': 'Cây', 'C98AB69DDB8347EDBE83033C5121058F': 'Chai', '9A1496BF55E04C46845F14B79CDD15DF': 'Con', '3EC612D6A8BE419D9E5234D8AC760A3E': 'Cục', 'CFB742A9141F4431881369B65B60343D': 'Cuôn', '4EF5272F159643C3B8E12B8B860AE9E8': 'Cuộn', '87AF5D27A7C64072AB84E73E60B6EE83': 'Dây', 'CF92EE9615AA4319A47557BFD0A32E94': 'Đôi', '922268EF47734BD094ECFD2E816E3196': 'gam', '212F8315EBA84C2688BF3C96FA1A4518': 'Hộp', '00E6D6D5AF254645AD1AFAB7FF9EC7E3': 'Kg', '68FFFE9AB5EF4531972B82E78C1F1457': 'Lít', 'B8BF8A890627472C97DF78E0C0593159': 'Lố', 'BCDE67CC992F4689BA09C6776CD7F5D8': 'Lọ', 'A9188839AD8C4200842CBBFD1B2ECC87': 'm', 'BFDD1E869A274F54A62FFF17FE37CCCE': 'm2', '82BB86D1D2204A689871308CF329588B': 'M3', 'CD418A38920A4E7C91B9AC447B99999E': 'Mét', '7d1c5224937b0f0b45bf7789fe41274b': 'pc', '4c08913293d66145064e50475e016456': 'set', 'FC643CEC8DBF4B81824C45C51C7E3163': 'Sợi', '3635630D77384D2191672A1AB571EF4E': 'Tấm', 'E7741B8A7C7B4AF5B9B814FA3AA53D24': 'Thanh', '7DC2501158CA41FA88095C466A4931E1': 'Tờ', '3B4B27A706A5489B93076847F46E63DA': 'Tuýp', '70450FE48161485DA9246881965867EB': 'Viên' } }, frozen: false, formoptions: { label: 'Đơn vị tính' } },
                    { key: false, fixed: true, label: 'Ghi chú', name: 'ghichu_donvi2', index: 'ncc_dh.ghichu_donvi2', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', frozen: false, formoptions: { label: 'Ghi chú' } },
                    { key: false, fixed: true, label: 'Đã chuyển hết', name: 'check_kho', index: 'ncc_dh.check_kho', width: 100, editable: false, hidden: false, formatter: 'checkbox', align: 'center', stype: 'select', searchoptions: { sopt: ['bw'], value: ':Tất cả;1:Có;0:Không' }, edittype: 'checkbox', editoptions: { value: 'True:False', defaultValue: 'False' }, frozen: false, formoptions: { label: 'Đã chuyển hết' } },
                    { key: false, fixed: true, label: 'mota_tienganh', name: 'mota_tienganh', index: ' ncc_dh.mota_tienganh ', width: 120, editable: false, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', frozen: false, formoptions: { label: 'mota_tienganh' } },
                    { key: false, fixed: true, label: 'Lô Kho', name: 'khoden', index: 'ncc_dh.khoden', width: 120, editable: false, hidden: true, align: 'left', stype: 'select', searchoptions: { sopt: ['bw'], value: { '': '', '07010AF94B7B4E129DA3071B3786DCC7': 'KHO VẬT TƯ', 'F9853033611E40B28C9C9059897F31B7': 'KHO TEM-BAO BÌ', '15C2B297679D4050B0CB14E16A528854': 'KHO PALLET', 'FE561457AD4D42D79FB38AC512BCE402': 'KHO THÀNH PHẨM', 'A6CEDB9BD33A4EC8981A8103AD17DB77': 'KHO TỒN THÀNH PHẨM' } }, edittype: 'select', editoptions: { value: { '': '', '07010AF94B7B4E129DA3071B3786DCC7': 'KHO VẬT TƯ', 'F9853033611E40B28C9C9059897F31B7': 'KHO TEM-BAO BÌ', '15C2B297679D4050B0CB14E16A528854': 'KHO PALLET', 'FE561457AD4D42D79FB38AC512BCE402': 'KHO THÀNH PHẨM', 'A6CEDB9BD33A4EC8981A8103AD17DB77': 'KHO TỒN THÀNH PHẨM' } }, frozen: false, formoptions: { label: 'Lô Kho' } },
                    { key: false, fixed: true, label: 'Quy cách', name: 'quycach', index: ' ncc_dh.quycach ', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 50 }, frozen: false, formoptions: { label: 'Quy cách' } },
                    { key: false, fixed: true, label: 'Người tạo HT', name: 'nguoitao', index: ' ncc_dh.nguoitao ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'Người tạo HT' } },
                    { key: false, fixed: true, label: 'Vai trò tạo HT', name: 'vaitrotao', index: ' ncc_dh.vaitrotao ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'Vai trò tạo HT' } },
                    { key: false, fixed: true, label: 'Bộ phận tạo HT', name: 'bophantao', index: ' ncc_dh.bophantao ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'Bộ phận tạo HT' } },
                    { key: false, fixed: true, label: 'Người cập nhật HT', name: 'nguoicapnhat', index: ' ncc_dh.nguoicapnhat ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'Người cập nhật HT' } },
                    { key: false, fixed: true, label: 'Vai trò cập nhật HT', name: 'vaitrocapnhat', index: ' ncc_dh.vaitrocapnhat ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'Vai trò cập nhật HT' } },
                    { key: false, fixed: true, label: 'Bộ phận cập nhật HT', name: 'bophancapnhat', index: ' ncc_dh.bophancapnhat ', width: 95, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'Bộ phận cập nhật HT' } },
                    { key: false, fixed: true, label: 'Người tạo', name: 'value_nguoitao', index: ' ncc_dh.value_nguoitao ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, formoptions: { label: 'Người tạo' } },
                    { key: false, fixed: true, label: 'Vai trò tạo', name: 'value_vaitrotao', index: ' ncc_dh.value_vaitrotao ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, formoptions: { label: 'Vai trò tạo' } },
                    { key: false, fixed: true, label: 'Bộ phận tạo', name: 'value_bophantao', index: ' ncc_dh.value_bophantao ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, formoptions: { label: 'Bộ phận tạo' } },
                    { key: false, fixed: true, label: 'Người cập nhật', name: 'value_nguoicapnhat', index: ' ncc_dh.value_nguoicapnhat ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, formoptions: { label: 'Người cập nhật' } },
                    { key: false, fixed: true, label: 'Vai trò cập nhật', name: 'value_vaitrocapnhat', index: ' ncc_dh.value_vaitrocapnhat ', width: 90, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, formoptions: { label: 'Vai trò cập nhật' } },
                    { key: false, fixed: true, label: 'Bộ phận cập nhật', name: 'value_bophancapnhat', index: ' ncc_dh.value_bophancapnhat ', width: 95, editable: false, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, formoptions: { label: 'Bộ phận cập nhật' } },
                    { key: false, fixed: true, label: 'Ngày tạo', name: 'ngaytao', index: ' ncc_dh.ngaytao ', width: 90, editable: false, hidden: true, formatter: esc_date, align: 'center', searchoptions: { sopt: ['cn'], dataInit: function (elem) { search_datetime(elem); } }, editoptions: { dataInit: function (elem) { format_datetime(elem); } }, formatoptions: { srcformat: 'm/d/Y', newformat: format_srcdatetime() }, formoptions: { label: 'Ngày tạo' } },
                    { key: false, fixed: true, label: 'Ngày cập nhật', name: 'ngaycapnhat', index: ' ncc_dh.ngaycapnhat ', width: 90, editable: false, hidden: true, formatter: esc_date, align: 'center', searchoptions: { sopt: ['cn'], dataInit: function (elem) { search_datetime(elem); } }, editoptions: { dataInit: function (elem) { format_datetime(elem); } }, formatoptions: { srcformat: 'm/d/Y', newformat: format_srcdatetime() }, formoptions: { label: 'Ngày cập nhật' } },
                    { key: false, fixed: true, label: 'Mô tả', name: 'mota', index: ' ncc_dh.mota ', width: 150, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', formoptions: { label: 'Mô tả' } },
                    { key: false, fixed: true, label: 'Hoạt động', name: 'hoatdong', index: ' ncc_dh.hoatdong ', width: 75, editable: false, hidden: true, formatter: 'checkbox', align: 'center', stype: 'select', searchoptions: { sopt: ['bw'], value: ':Tất cả;1:Có;0:Không' }, edittype: 'checkbox', editoptions: { value: 'True:False', defaultValue: 'False' }, formoptions: { label: 'Hoạt động' } },

                ],
                beforeRequest: function () {
                    $('#gridMD_01_DHNCC_cp').jqGrid('getGridParam', 'postData').id = $('#gridMD_00_NhapkhotuNCC_cp').jqGrid('getGridParam', 'selrow');
                    //$('#gridMD_01_CacDongVanChuyen_cp').jqGrid('getGridParam', 'postData').where_ex = " and  ";
                    input_focus = $('input:focus').attr('class');
                },
                ondblClickRow: function () {
                    $('#dlg_gridSmal_2').dialog('destroy').remove();
                },
                gridComplete: function () {
                    $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width() - 5);
                },
                loadComplete: function (data) {
                    //giữ focus end
                    $('.' + input_focus).focus();
                },
                caption: 'Các dòng hàng'
            });
            jQuery('#gridMD_01_DHNCC_cp').jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
            $('#gview_gridMD_01_DHNCC_cp .ui-jqgrid-title').css('float', 'left');

            Logo_Center("glyphicon glyphicon-search", true, 'dlg_gridSmal_2');
        },
        close: function () {
            $(this).dialog('destroy').remove();
        },
        buttons: [
            {
                id: 'btn-ok',
                text: 'Đồng ý',
                click: function () {
                    var ids_sel = $('#' + namegrid).jqGrid('getGridParam', 'selarrrow'), sct_pnk = '';
                    for (var i = 0; i < ids_sel.length; i++) {
                        var cel_s = $('#' + namegrid).getRowData(ids_sel[i]);
                        sct_pnk += cel_s['sochungtu'] + '\n';
                    }
                    if (sct_pnk != '')
                        sct_pnk = sct_pnk.substring(0, sct_pnk.length - 1);

                    $('#phieunhapkho').val(sct_pnk);
                    $(this).dialog('destroy').remove();
                }
            },
            {
                id: 'btn-close',
                text: 'Thoát',
                click: function () {
                    $(this).dialog('destroy').remove();
                }
            }
        ]
    });
}
//end CA_01_ThemPNK