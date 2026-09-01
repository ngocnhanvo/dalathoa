//Start Đối chiếu hàng tồn
function format_dcht(elem) {
    var id_elem = $(elem).attr('id');
    $(elem).keypress(function (e) {
        if (e.which == '13') {
            timkiem_dcht(id_elem, 1);
        }
    });
    $(elem).parent().append('<span onclick="timkiem_dcht(\'' + id_elem + '\')" ' +
        'class="span_format_lenhsx glyphicon glyphicon-search" style="position: absolute;margin: 1px 0 0 -21px;background-color: rgb(248, 249, 243);padding: 3.5px;cursor: pointer;border-left: 1px solid rgba(204, 204, 204, 0.63)" />');
}

function timkiem_dcht(id_elem, type) {
    var load_sp = 0;
    $('body').append('<div id="dlg_gridSmal_2" style="margin: 4px 0 0 0; background-color: rgba(165, 121, 255, 0.07);" title="Danh mục Đối chiếu hàng tồn">' +
        '<table id="gridMD_00_DCHT_cp"></table>' +
        '<div id="pagergridMD_00_DCHT_cp"></div>' +
        '</div>');

    $('#dlg_gridSmal_2').dialog({
        modal: true,
        dialogClass: "dialog_index",
        width: 650,
        height: window.innerHeight - 10,
        open: function (event, ui) {
            jQuery('#gridMD_00_DCHT_cp').jqGrid({
                url: 'Controller/JqGrid/JQGridMD_00_DCHTLoad.ashx?ma_module=MD_00_DCHT&ma_menu=MN_01_DCHT&id=null&id_sel=&module_select=1',
                editurl: '',
                height: window.innerHeight - 220,
                datatype: 'json',
                autowidth: true,
                shrinkToFit: true,
                rownumbers: true,
                viewrecords: true,
                search: true,
                scroll: false,
                rowNum: 100,
                multiselect: false,
                multiboxonly: false,
                rowList: [10, 50, 100, 1000],
                pager: '#pagergridMD_00_DCHT_cp',
                onSelectRow: function (ids) {
                    if (ids != '<a style=\'color:red\'>Not data (404)</a>') {
                        cell = $('#gridMD_00_DCHT_cp').getRowData(ids);
                        if ($('#' + id_elem).prop('disabled') != true) {
                            $('#' + id_elem).val(cell['so_donhang']);
                            $('#donhang_thamchieu').val(cell['donhang_thamchieu']);
                        }
                    }
                },
                colModel: [
                    { key: true, fixed: true, label: 'c_doichieuhangton_id', name: 'c_doichieuhangton_id', index: '  dcht.c_doichieuhangton_id ', width: 100, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'c_doichieuhangton_id' } },
                    { key: false, fixed: true, label: 'Trạng thái', name: 'trangthai', index: 'dsdh.trangthai', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, formatter: Frm_MD_01_DSDatHang_trangthaigui, unformat: disable_formatter, align: 'left', stype: 'select', searchoptions: { sopt: ['bw'], value: { '': '', 'DAGUI': 'Chưa Nhận', 'DANHAN': 'Đã Nhận', 'SOANTHAO': 'Soạn thảo', 'HIEULUC': 'Hiệu lực', 'KETTHUC': 'Kết thúc' } }, edittype: 'select', editoptions: { value: { 'SOANTHAO': 'Soạn thảo', 'HIEULUC': 'Hiệu lực' } }, frozen: false, formoptions: { label: 'Trạng thái' } },
                    { key: false, fixed: true, label: 'Tên', name: 'ten_donhang', index: '  dcht.ten_donhang ', width: 100, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Tên' } },
                    { key: false, fixed: true, label: 'Số đơn hàng', name: 'so_donhang', index: '  dcht.so_donhang ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Số đơn hàng' } },
                    { key: false, fixed: true, label: 'Đơn hàng tham chiếu', name: 'donhang_thamchieu', index: '  dcht.donhang_thamchieu ', width: 170, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Đơn hàng tham chiếu' } },
                    { key: false, fixed: true, label: 'Phiếu lấy hàng tồn', name: 'phieuhangton', index: '  dcht.phieuhangton ', width: 170, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Phiếu lấy hàng tồn' } },

                ],
                postData: {
                    where_ex: function () {
                        if (tengrid0 == 'gridMD_00_PVCNoiBo') {
                            var a_b = " and  dcht.phieuhangton is not null and dsdh.trangthai = 'KETTHUC' or dsdh.trangthai = 'HIEULUC' ";
                            a_b += " and (select count(c_dongdsdh_id) from c_dongdsdh where c_danhsachdathang_id = dsdh.c_danhsachdathang_id and sl_conlai > 0) > 0 ";
                            return a_b;
                        }
                        else {
                            return " and dcht.phieuhangton is not null and (dsdh.trangthai = 'HIEULUC' or dsdh.trangthai = 'DANHAN')";
                        }
                    }
                },
                beforeRequest: function () {
                    //giữ focus
                    if ($('#' + id_elem).val() != '' & load_sp == 0) {
                        if (id_elem == 'c_doichieuhangton_id')
                            $('#gridMD_00_DCHT_cp').jqGrid('getGridParam', 'postData').filters = '{"groupOp":"AND","rules":[{"field":" dcht.so_donhang ","op":"eq","data":"' + $('#' + id_elem).val() + '"}]}';
                        else
                            $('#gridMD_00_DCHT_cp').jqGrid('getGridParam', 'postData').filters = '{"groupOp":"AND","rules":[{"field":" dcht.so_donhang ","op":"eq","data":"' + $('#' + id_elem).val() + '"}]}';
                    }
                    input_focus = $('input:focus').attr('class');
                },
                ondblClickRow: function () {
                    $('#dlg_gridSmal_2').dialog('destroy').remove();
                },
                gridComplete: function () {
                    $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width() - 5);
                },
                loadComplete: function (data) {
                    var top_rowid = $('#gridMD_00_DCHT_cp tr:nth-child(2)').attr('id');
                    var countrow = jQuery("#gridMD_00_DCHT_cp").jqGrid('getGridParam', 'records');
                    if (load_sp == 0) {
                        if (id_elem == 'md_lenhsanxuat_id')
                            $('.gs_sochungtu.gs_gridMD_00_DCHT_cp').val($('#' + id_elem).val());
                        else
                            $('.gs_sochungtu.gs_gridMD_00_DCHT_cp').val($('#' + id_elem).val());

                        $('#gridMD_00_DCHT_cp').jqGrid('setSelection', $('#' + id_elem).val());
                        load_sp = 1;
                        if (top_rowid.indexOf('<a style=\'color:red\'>') <= -1 & type == 1 & countrow == 1) {
                            $('#dlg_gridSmal_2').dialog('destroy').remove();
                        }
                    }
                    Focus_Selection('gridMD_00_DCHT_cp');
                    //giữ focus end
                    $('.' + input_focus).focus();
                },
                caption: ' '
            });
            jQuery('#gridMD_00_DCHT_cp').jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
            Logo_Center("glyphicon glyphicon-search", true, 'dlg_gridSmal_2');
        },
        close: function () {
            $(this).dialog('destroy').remove();
        },
        buttons: [
            {
                id: 'btn-ok_',
                text: 'OK',
                click: function () {
                    $(this).dialog('destroy').remove();
                }
            },
            {
                id: 'btn-close_',
                text: 'Cancel',
                click: function () {
                    $(this).dialog("destroy").remove();
                }
            }
        ]
    });
}
//End Đối chiếu hàng tồn