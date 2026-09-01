//Start Doi tac kinh doanh tong hop
function format_dtkdth(elem) {
    var id_elem = $(elem).attr('id');
    $(elem).keypress(function (e) {
        if (e.which == '13') {
            timkiem_dtkdth(id_elem, 1);
        }
    });
    $(elem).parent().append('<span onclick="timkiem_dtkdth(\'' + id_elem + '\')" ' +
        'class="span_format_dtkd glyphicon glyphicon-search" style="position: absolute;margin: 1px 0 0 -21px;background-color: rgb(248, 249, 243);padding: 3.5px;cursor: pointer;border-left: 1px solid rgba(204, 204, 204, 0.63)" />');
}

function timkiem_dtkdth(id_elem, type) {
    var load_sp = 0;
    $('body').append('<div id="dlg_gridSmal_2" style="margin: 4px 0 0 0; background-color: rgba(165, 121, 255, 0.07);" title="Danh mục đối tác kinh doanh">' +
        '<table id="gridMD_00_DTKD_cp"></table>' +
        '<div id="pagergridMD_00_DTKD_cp"></div>' +
        '</div>');

    $('#dlg_gridSmal_2').dialog({
        modal: true,
        dialogClass: "dialog_index",
        width: 650,
        height: window.innerHeight - 10,
        open: function (event, ui) {
            jQuery('#gridMD_00_DTKD_cp').jqGrid({
                url: 'Controller/JqGrid/JQGridMD_00_DTKDLoad_Hand.ashx?ma_module=MD_00_DTKD&ma_menu=MN_01_DTKD&id=null&id_sel=&module_select=1',
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
                pager: '#pagergridMD_00_DTKD_cp',
                onSelectRow: function (ids) {
                    if (ids != '<a style=\'color:red\'>Not data (404)</a>') {
                        cell = $('#gridMD_00_DTKD_cp').getRowData(ids);
                        if ($('#' + id_elem).prop('disabled') != true) {
                            $('#' + id_elem).val(cell['ma_dtkd']);
                            $('#ten_dtkd').val(cell['ten_dtkd']);
                        }
                    }
                },
                colModel: [
                    { key: true, fixed: true, label: 'Mã đối tác', name: 'ma_dtkd', index: 'A.ma_dtkd', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 255 }, frozen: true, formoptions: { label: 'Mã đối tác kinh doanh' } },
                    { key: false, fixed: true, label: 'Tên đối tác', name: 'ten_dtkd', index: 'A.ten_dtkd', width: 160, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', frozen: false, formoptions: { label: 'Tên đối tác' } },
                    { key: false, fixed: true, label: 'Địa chỉ', name: 'diachi', index: 'A.diachi', width: 160, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', frozen: false, formoptions: { label: 'Địa chỉ' } },
                    { key: false, fixed: true, label: 'Là nhà cung cấp', name: 'isncc', index: 'A.isncc', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'center', stype: 'select', searchoptions: { value: ':Tất cả;True:Có;False:Không' }, editoptions: { value: 'True:False', defaultValue: 'False' }, edittype: 'checkbox', formatter: 'checkbox', frozen: false, formoptions: { label: 'Là nhà cung cấp' } },
                ],
                beforeRequest: function () {
                    //giữ focus
                    if ($('#' + id_elem).val() != '' & load_sp == 0) {
                        $('#gridMD_00_DTKD_cp').jqGrid('getGridParam', 'postData').filters = '{"groupOp":"AND","rules":[{"field":" A.ma_dtkd ","op":"eq","data":"' + $('#' + id_elem).val() + '"}]}';
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
                    var top_rowid = $('#gridMD_00_DTKD_cp tr:nth-child(2)').attr('id');
                    var countrow = jQuery("#gridMD_00_DTKD_cp").jqGrid('getGridParam', 'records');
                    if (load_sp == 0) {
                        $('.gs_ma_dtkd.gs_gridMD_00_DTKD_cp').val($('#' + id_elem).val());
                        $('#gridMD_00_DTKD_cp').jqGrid('setSelection', $('#' + id_elem).val());
                        load_sp = 1;
                        if (top_rowid.indexOf('<a style=\'color:red\'>') <= -1 & type == 1 & countrow == 1) {
                            $('#bg_' + id_elem).val(cell['banggia_id']);
                            $('#dlg_gridSmal_2').dialog('destroy').remove();
                        }
                    }
                    Focus_Selection('gridMD_00_DTKD_cp');
                    //giữ focus end
                    $('.' + input_focus).focus();
                },
                caption: ' '
            });
            jQuery('#gridMD_00_DTKD_cp').jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
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
//End Doi tac kinh doanh tong hop