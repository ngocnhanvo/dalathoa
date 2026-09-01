//Start Tiem kiem Don Mua Hang
function format_dmh(elem) {
    var id_elem = $(elem).attr('id');
    $(elem).keypress(function (e) {
        if (e.which == '13') {
            timkiem_dmh(id_elem, 1);
        }
    });
    $(elem).parent().append('<span onclick="timkiem_dmh(\'' + id_elem + '\')" ' +
        'class="span_format_lenhsx glyphicon glyphicon-search" style="position: absolute;margin: 1px 0 0 -21px;background-color: rgb(248, 249, 243);padding: 3.5px;cursor: pointer;border-left: 1px solid rgba(204, 204, 204, 0.63)" />');
}

function timkiem_dmh(id_elem, type) {
    var load_sp = 0;
    var namegrid = "gridMD_00_DMHHVT_cp";
    $('body').append('<div id="dlg_gridSmal_2" style="margin: 4px 0 0 0; background-color: rgba(165, 121, 255, 0.07);" title="Danh mục Đối chiếu hàng tồn">' +
        '<table id="' + namegrid + '"></table>' +
        '<div id="pager' + namegrid + '"></div>' +
        '</div>');

    var multi = false;
    if (tengrid0 == 'gridMD_00_HoaDon') {
        multi = true;
    }

    $('#dlg_gridSmal_2').dialog({
        modal: true,
        dialogClass: "dialog_index",
        width: 650,
        height: window.innerHeight - 10,
        open: function (event, ui) {
            jQuery('#' + namegrid).jqGrid({
                url: 'Controller/JqGrid/JQGridMD_00_DonMuaHangLoad.ashx?ma_module=MD_00_DonMuaHang&ma_menu=MN_01_DonMuaHang&id=null&id_sel=&module_select=1',
                editurl: '',
                height: window.innerHeight - 220,
                datatype: 'json',
                autowidth: true,
                shrinkToFit: true,
                rownumbers: true,
                viewrecords: true,
                search: true,
                scroll: false,
                rowNum: 1000,
                multiselect: multi,
                multiboxonly: false,
                rowList: [10, 50, 100, 1000],
                pager: '#pager' + namegrid,
                onSelectRow: function (ids) {
                    checkbox_JQgrid(namegrid, 0);
                    if (ids != '<a style=\'color:red\'>Not data (404)</a>') {
                        cell = $('#' + namegrid).getRowData(ids);
                        if ($('#' + id_elem).prop('disabled') != true) {
                            $('#' + id_elem).val(cell['sochungtu']);
                            $('#' + id_elem + '_id').val(cell['c_donmuahang_id']);
                            mokhoa_column('phieunhapkho');
                        }
                    }
                },
                colModel: [
                    { key: true, fixed: true, label: 'c_donmuahang_id', name: 'c_donmuahang_id', index: ' dmh.c_donmuahang_id ', width: 120, editable: false, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'c_donmuahang_id' } },
                    { key: false, fixed: true, label: 'Trạng thái', name: 'md_trangthai_id', index: ' dmh.md_trangthai_id ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, formatter: Frm_MD_00_KHDHJQGS_trangthai, unformat: disable_formatter, align: 'left', stype: 'select', searchoptions: { sopt: ['bw'], value: { '': '', 'SOANTHAO': 'Soạn thảo', 'HIEULUC': 'Hiệu lực' } }, edittype: 'select', editoptions: { value: { 'SOANTHAO': 'Soạn thảo', 'HIEULUC': 'Hiệu lực' } }, frozen: false, formoptions: { label: 'Trạng thái' } },
                    { key: false, fixed: true, label: 'Số Chứng Từ', name: 'sochungtu', index: ' dmh.sochungtu ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 50 }, frozen: false, formoptions: { label: 'Số Chứng Từ' } },
                    { key: false, fixed: true, label: 'Phiếu nhập kho', name: 'phieunhapkho', index: 'dmh.phieunhapkho', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'center', stype: 'select', searchoptions: { sopt: ['eq'], value: { '': '', ' ': 'Chưa tạo phiếu', 'Đã tạo phiếu': 'Đã tạo phiếu', 'Đã nhập kho': 'Đã nhập kho' } }, frozen: false, formoptions: { label: 'Phiếu nhập kho' } },
                    { key: false, fixed: true, label: 'Tên đối tác', name: 'ten_dtkd', index: 'dtkd.ten_dtkd', width: 170, editable: false, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, frozen: false, formoptions: { label: 'Tên đối tác' } },
                    { key: false, fixed: true, label: 'Đơn hàng tham chiếu', name: 'donhang_thamchieu', index: ' dmh.donhang_thamchieu ', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 150 }, frozen: false, formoptions: { label: 'Đơn hàng tham chiếu' } },
                    { key: false, fixed: true, label: 'CT Kế Hoạch mua VT', name: 'sctkehoach', index: 'dmh.sctkehoach', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, frozen: false, formoptions: { label: 'CT Kế Hoạch mua VT' } },
                    { key: false, fixed: true, label: 'Địa điểm giao hàng', name: 'diadiem_giaohang', index: 'dmh.diadiem_giaohang', width: 100, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, frozen: false, formoptions: { label: 'Địa điểm giao hàng' } },
                ],
                postData: {
                    where_ex: function () {
                        return " and (dmh.md_trangthai_id = 'DAXONG' or dmh.md_trangthai_id = 'CHUAXONG') and dtkd.ten_dtkd = N\'" + $('#md_doitackinhdoanh_id').val() + "\'";

                    }
                },
                beforeRequest: function () {
                    //giữ focus
                    /* if ($('#' + id_elem).val() != '' & load_sp == 0) {
							$('#'+ namegrid).jqGrid('getGridParam', 'postData').filters = '{"groupOp":"AND","rules":[{"field":" dmh.sochungtu ","op":"eq","data":"' + $('#' + id_elem).val() + '"}]}';
                    } */
                    checkbox_JQgrid(namegrid, 1);
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
                    Focus_Selection(namegrid);
                    //giữ focus end
                    $('.' + input_focus).focus();
                },
                caption: ' '
            });
            jQuery('#' + namegrid).jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
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
                    var ids_sel = $('#' + namegrid).jqGrid('getGridParam', 'selarrrow'), sct_pnk = '';
                    for (var i = 0; i < ids_sel.length; i++) {
                        var cel_s = $('#' + namegrid).getRowData(ids_sel[i]);
                        sct_pnk += cel_s['sochungtu'] + '\n';
                    }
                    if (sct_pnk != '')
                        sct_pnk = sct_pnk.substring(0, sct_pnk.length - 1);

                    $('#sct_thamchieu').val(sct_pnk);
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
//End Tiem kiem Don Mua Hang