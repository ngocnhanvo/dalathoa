//Start Xuất bán
function format_xb(elem) {
    var id_elem = $(elem).attr('id');
    $(elem).keypress(function (e) {
        if (e.which == '13') {
            timkiem_xb(id_elem, 1);
        }
    });

    $(elem).parent().append('<span onclick="timkiem_xb(\'' + id_elem + '\')" ' +
        'class="span_format_donhang glyphicon glyphicon-search" style="position: absolute;margin: 1px 0 0 -21px;background-color: rgb(248, 249, 243);padding: 3.5px;cursor: pointer;border-left: 1px solid rgba(204, 204, 204, 0.63)" />');
}
function timkiem_xb(id_elem, type) {
    var load_sp = 0;
    if ($('#loaichuyen').val() == "XUATB") {
        mokhoa_column('chungtuthamchieu');
        $('body').append('<div id="dlg_gridSmal_2" style="margin: 4px 0 0 0; background-color: rgba(165, 121, 255, 0.07);" title="Danh mục Xuất bán">' +
            '<table id="gridMD_00_XuatBan_cp"></table>' +
            '<div id="pagergridMD_00_XuatBan_cp"></div>' +
            '</div>');

        $('#dlg_gridSmal_2').dialog({
            modal: true,
            dialogClass: "dialog_index",
            width: 650,
            height: window.innerHeight - 10,
            open: function (event, ui) {
                jQuery('#gridMD_00_XuatBan_cp').jqGrid({
                    url: 'Controller/JqGrid/JQGridMD_00_XuatBanLoad.ashx?ma_module=MD_00_XuatBan&ma_menu=MN_01_Xuatban&id=null&id_sel=&module_select=1',
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
                    pager: '#pagergridMD_00_XuatBan_cp',
                    onSelectRow: function (ids) {
                        if (ids.indexOf('<a style=\'color:red\'') <= -1) {
                            cell = $('#gridMD_00_XuatBan_cp').getRowData(ids);
                            if ($('#' + id_elem).prop('disabled') != true) {
                                $('#' + id_elem).val(cell['sochungtu']);
                                $('#denkho').val(cell['tukho_id']);
                                $('#sctdathang').val(cell['sctdathang']);
                                $('#donhang_thamchieu').val(cell['donhang_thamchieu']);
                            }
                        }
                    },
                    colModel: [
                        { key: true, fixed: true, label: 'Số Chứng Từ', name: 'sochungtu', index: 'xb.sochungtu', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Số Chứng Từ' } },
                        { key: false, fixed: true, label: 'SCT đơn hàng', name: 'sctdathang', index: 'xb.sctdathang', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'SCT đơn hàng' } },
                        { key: false, fixed: true, label: 'ĐH tham chiếu', name: 'donhang_thamchieu', index: ' xb.donhang_thamchieu ', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, frozen: false, formoptions: { label: 'ĐH tham chiếu' } },
                        { key: false, fixed: true, label: 'Đối tác kinh doanh', name: 'md_doitackinhdoanh_id', index: ' xb.md_doitackinhdoanh_id ', width: 170, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Đối tác kinh doanh chiếu' } },
                        { key: false, fixed: true, label: 'Địa chỉ', name: 'diachi', index: 'xb.diachi', width: 170, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Địa chỉ' } },
                        { key: false, fixed: true, label: 'Từ kho', name: 'tukho', index: 'xb.tukho', width: 170, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Từ kho' } },
                        { key: false, fixed: true, label: 'Từ kho', name: 'tukho_id', index: 'xb.tukho', width: 170, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Từ kho' } },
                        { key: false, fixed: true, label: 'Mô tả', name: 'mota', index: 'xb.mota', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Mô tả' } },

                    ],
                    postData: {
                        where_ex: function () {
                            return " and xb.trangthai != 'HIEULUC'";
                        }
                    },
                    beforeRequest: function () {
                        //giữ focus
                        if ($('#' + id_elem).val() != '' & load_sp == 0) {
                            $('#gridMD_00_XuatBan_cp').jqGrid('getGridParam', 'postData').filters = '{"groupOp":"AND","rules":[{"field":" xb.sochungtu ","op":"eq","data":"' + $('#' + id_elem).val() + '"}]}';
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
                        var top_rowid = $('#gridMD_00_XuatBan_cp tr:nth-child(2)').attr('id');
                        var countrow = jQuery("#gridMD_00_XuatBan_cp").jqGrid('getGridParam', 'records');
                        if (load_sp == 0) {
                            $('.gs_ma_dtkd.gs_gridMD_00_XuatBan_cp').val($('#' + id_elem).val());
                            $('#gridMD_00_XuatBan_cp').jqGrid('setSelection', $('#' + id_elem).val());
                            load_sp = 1;
                            if (top_rowid.indexOf('<a style=\'color:red\'>') <= -1 & type == 1 & countrow == 1) {
                                $('#denkho').val(cell['tukho']);
                                $('#dlg_gridSmal_2').dialog('destroy').remove();
                            }
                        }
                        Focus_Selection('gridMD_00_XuatBan_cp');
                        //giữ focus end
                        $('.' + input_focus).focus();
                    },
                    caption: ' '
                });
                jQuery('#gridMD_00_XuatBan_cp').jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
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
    else if ($('#loaichuyen').val() == "XUATNB") {
        mokhoa_column('chungtuthamchieu');
        $('body').append('<div id="dlg_gridSmal_2" style="margin: 4px 0 0 0; background-color: rgba(165, 121, 255, 0.07);" title="Danh mục Xuất nội bộ">' +
            '<table id="gridMD_00_Xuatnoibo_cp"></table>' +
            '<div id="pagergridMD_00_Xuatnoibo_cp"></div>' +
            '</div>');

        $('#dlg_gridSmal_2').dialog({
            modal: true,
            dialogClass: "dialog_index",
            width: 650,
            height: window.innerHeight - 10,
            open: function (event, ui) {
                jQuery('#gridMD_00_Xuatnoibo_cp').jqGrid({
                    url: 'Controller/JqGrid/JQGridMD_00_XuatnoiboLoad.ashx?ma_module=MD_00_Xuatnoibo&ma_menu=MN_01_Xuatnoibo&id=null&id_sel=&module_select=1',
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
                    pager: '#pagergridMD_00_Xuatnoibo_cp',
                    onSelectRow: function (ids) {
                        if (ids.indexOf('<a style=\'color:red\'') <= -1) {
                            cell = $('#gridMD_00_Xuatnoibo_cp').getRowData(ids);
                            if ($('#' + id_elem).prop('disabled') != true) {
                                $('#' + id_elem).val(cell['sochungtu']);
                                $('#denkho').val(cell['tukho_id']);
                                $('#sctdathang').val(cell['sctdathang']);
                                $('#donhang_thamchieu').val(cell['donhang_thamchieu']);

                            }
                        }
                    },
                    colModel: [
                        { key: true, fixed: true, label: 'Số Chứng Từ', name: 'sochungtu', index: ' xnb.sochungtu ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Số Chứng Từ' } },
                        { key: false, fixed: true, label: 'SCT đơn hàng', name: 'sctdathang', index: 'xnb.sctdathang', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'SCT đơn hàng' } },
                        { key: false, fixed: true, label: 'ĐH tham chiếu', name: 'donhang_thamchieu', index: ' xnb.donhang_thamchieu ', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, frozen: false, formoptions: { label: 'ĐH tham chiếu' } },
                        { key: false, fixed: true, label: 'Từ kho', name: 'tukho', index: 'xnb.tukho', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Từ kho' } },
                        { key: false, fixed: true, label: 'Từ kho', name: 'tukho_id', index: 'xnb.tukho', width: 120, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Từ kho' } },
                        { key: false, fixed: false, label: 'Mô tả', name: 'mota', index: ' xnb.mota ', width: 140, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Mô tả' } },
                    ],
                    postData: {
                        where_ex: function () {
                            return " and xnb.trangthai != 'HIEULUC'";
                        }
                    },
                    beforeRequest: function () {
                        //giữ focus
                        if ($('#' + id_elem).val() != '' & load_sp == 0) {
                            $('#gridMD_00_Xuatnoibo_cp').jqGrid('getGridParam', 'postData').filters = '{"groupOp":"AND","rules":[{"field":" xnb.sochungtu ","op":"eq","data":"' + $('#' + id_elem).val() + '"}]}';
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
                        var top_rowid = $('#gridMD_00_Xuatnoibo_cp tr:nth-child(2)').attr('id');
                        var countrow = jQuery("#gridMD_00_Xuatnoibo_cp").jqGrid('getGridParam', 'records');
                        if (load_sp == 0) {
                            $('.gs_ma_dtkd.gs_gridMD_00_Xuatnoibo_cp').val($('#' + id_elem).val());
                            $('#gridMD_00_Xuatnoibo_cp').jqGrid('setSelection', $('#' + id_elem).val());
                            load_sp = 1;
                            if (top_rowid.indexOf('<a style=\'color:red\'>') <= -1 & type == 1 & countrow == 1) {
                                $('#dlg_gridSmal_2').dialog('destroy').remove();
                            }
                        }
                        Focus_Selection('gridMD_00_Xuatnoibo_cp');
                        //giữ focus end
                        $('.' + input_focus).focus();
                    },
                    caption: ' '
                });
                jQuery('#gridMD_00_Xuatnoibo_cp').jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
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
	/*else if ($('#loaichuyen').val() == "VANCNB")
	{
		khoa_column('chungtuthamchieu');
	}*/
}
//End Xuất bán
