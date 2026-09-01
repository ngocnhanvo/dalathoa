//Start Doi tac kinh doanh
var format_khachhang = {
    url: 'Controller/JqGrid/JQGridMD_00_DTKDLoad.ashx?ma_module=MD_00_DTKD&ma_menu=MN_01_DTKD&id=null&id_sel=&module_select=1',
    colModel: [
        { key: true, fixed: true, label: 'ID KH', name: 'md_doitackinhdoanh_id', index: ' dtkd.md_doitackinhdoanh_id ', width: 80, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 255 }, frozen: true, formoptions: { label: 'Mã đối tác kinh doanh' } },
        { key: false, fixed: true, label: 'Mã KH', name: 'ma_dtkd', index: ' dtkd.ma_dtkd ', width: 80, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 255 }, frozen: true, formoptions: { label: 'Mã đối tác kinh doanh' } },
        { key: false, fixed: true, label: 'Tên khách hàng', name: 'ten_dtkd', index: ' dtkd.ten_dtkd ', width: 160, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', frozen: false, formoptions: { label: 'Tên đối tác' } },
        { key: false, fixed: true, label: 'Địa chỉ', name: 'diachi', index: ' dtkd.diachi ', width: 160, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', frozen: false, formoptions: { label: 'Địa chỉ' } },
        { key: false, fixed: true, label: 'Điện Thoại', name: 'tel', index: ' dtkd.tel ', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 50 }, frozen: false, formoptions: { label: 'Điện Thoại' } },
        { key: false, fixed: true, label: 'Mã số thuế', name: 'masothue', index: ' dtkd.masothue ', width: 120, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 512 }, frozen: false, formoptions: { label: 'Mã số thuế' } },
    ],
    select: function ($elem, cellSav) {
        const k = format_khachhang.key;
        const $elemFrm = $elem.closest('.FormGrid');
        if ($elem.prop('disabled') != true) {
            $elem.val(cellSav[k]);
            $elem.prev().val(cellSav.md_doitackinhdoanh_id);
            $elemFrm.find('#ten_khachhang').val(cellSav.ten_dtkd);
            $elemFrm.find('#diachi').val(cellSav.diachi);
            $elemFrm.find('#diachigiaohang').val(cellSav.diachi);
            $elemFrm.find('#nguoi_dathang').val(cellSav.daidien);
            $elemFrm.find('#tel').val(cellSav.tel);
            $elemFrm.find('#so_taikhoan').val(cellSav.so_taikhoan);
            $elemFrm.find('#masothue').val(cellSav.masothue);
            //kiotV
            $elemFrm.find('#nguoinhan_kov').val(cellSav.ten_dtkd);
            $elemFrm.find('#sdt_nguoinhan_kov').val(cellSav.tel);
            $elemFrm.find('#diachi_nguoinhan_kov').val(cellSav.diachi);
        }
    },
    create: function (elem) {
        const k = $(elem)[0].keyfmt;
        format_khachhang.key = k ? k : 'ma_dtkd';
        $(elem).keypress(function (e) {
            if (e.which == '13') {
                $(elem).next().click();
            }
        });

        $(elem).combogrid({
            searchIcon: false,
            width: 'auto',
            munit: 'px',
            replaceNull: false,
            url: format_khachhang.url,
            colModel: format_khachhang.colModel,
            postData: {
                _search: true
            },
            appendData: function () {
                let filters = {
                    groupOp: 'OR',
                    rules: [
                        { field: 'dtkd.ma_dtkd', op: "bw", data: $(elem).val() },
                        { field: 'dtkd.ten_dtkd', op: "bw", data: $(elem).val() },
                        { field: 'dtkd.tel', op: "bw", data: $(elem).val() },
                    ]
                };
                return {
                    filters: JSON.stringify(filters)
                };
            },
            select: function (event, ui) {
                format_khachhang.select($(elem), ui.item);
                return false;
            }
        });

        $(elem).addClass('format_vnn');
        $(elem).parent().append(`<span onclick="format_khachhang.search(this)" class="format_dtkd formatsearch" />`);
    },
    search: function (elem, type) {
        const k = format_khachhang.key;
        let $elem = $(elem).prev();
        let load_sp = 0, cellSav = {};
        const gridMasterId = 'gridMD_00_DTKD_cp';
        $('body').append(`
        <div 
            id="dlg_gridSmal_2" 
            style="margin: 4px 0 0 0; background-color: rgba(165, 121, 255, 0.07);" 
            title="Danh mục khách hàng"
        >
            <table id="${gridMasterId}"></table>
            <div id="pager${gridMasterId}"></div>
        </div>
    `);

        let $gridMaster = $(`#${gridMasterId}`);
        let $dialog = $('#dlg_gridSmal_2');
        $dialog.dialog({
            modal: true,
            dialogClass: "dialog_index",
            width: 650,
            height: window.innerHeight - 10,
            open: function (event, ui) {
                $gridMaster.jqGrid({
                    url: format_khachhang.url,
                    editurl: '',
                    height: window.innerHeight - 200,
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
                    pager: `#pager${gridMasterId}`,
                    onSelectRow: function (ids) {
                        cellSav = $gridMaster.getRowData(ids);
                        if (!cellSav[k])
                            return;
                    },
                    colModel: format_khachhang.colModel,
                    beforeRequest: function () {
                        //giữ focus
                        if ($elem.val() != '' & load_sp == 0) {
                            let filters = {
                                groupOp: 'AND',
                                rules: [
                                    { field: `dtkd.${k}`, op: "bw", data: $elem.val() }
                                ]
                            };
                            $gridMaster.jqGrid('getGridParam', 'postData').filters = JSON.stringify(filters);
                        }
                        input_focus = $('input:focus');
                    },
                    ondblClickRow: function () {
                        $('#btn-ok_').click();
                    },
                    gridComplete: function () {
                        $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width() - 5);
                    },
                    loadComplete: function (data) {
                        let top_rowid = $gridMaster.find('tr:nth-child(2)').attr('id') + '';
                        let countrow = $gridMaster.jqGrid('getGridParam', 'records');
                        const $timkiem = $(`.gs_${k}.gs_${gridMasterId}`);
                        if (load_sp == 0) {
                            $timkiem.val($elem.val());
                            $gridMaster.jqGrid('setSelection', top_rowid);
                            $(`.tbl_vnn_${gridMasterId} .ui-search-input input`).each(function () {
                                $(this).attr('autocomplete', 'off');
                                if ($(this).attr('id') == `gs_${k}`)
                                    input_focus = $(this);
                            });
                            load_sp = 1;
                            if (top_rowid != '0' & type == 1 & countrow == 1) {
                                $(`#btn-ok_`).click();
                            }
                        }

                        const $parent = $gridMaster.parent().parent().parent();
                        $parent.off('keydown');
                        $parent.on("keydown", function (e) {
                            let selectedRowId = $gridMaster.jqGrid('getGridParam', 'selrow');
                            let conhanphimmuiten = e.keyCode === 38 | e.keyCode === 40;
                            let nextRowId;

                            if (conhanphimmuiten && !selectedRowId) {
                                selectedRowId = top_rowid;
                                nextRowId = selectedRowId;
                            }
                            else {
                                if (e.keyCode === 13) { // Phím enter
                                    nextRowId = null;
                                    if (selectedRowId) {
                                        if (selectedRowId != '0')
                                            $(`#btn-ok_`).click();
                                    }
                                    e.preventDefault();
                                    return;
                                }
                                else if (e.keyCode === 38) { // Phím mũi tên LÊN
                                    nextRowId = $gridMaster.find('#' + $.jgrid.jqID(selectedRowId)).prev('tr.jqgrow').attr('id');
                                    if (!nextRowId) {
                                        const len = $timkiem[0].value.length;
                                        $timkiem[0].focus();
                                        requestAnimationFrame(() => { $timkiem[0].setSelectionRange(len, len); });
                                    }
                                }
                                else if (e.keyCode === 40) { // Phím mũi tên XUỐNG
                                    nextRowId = $gridMaster.find('#' + $.jgrid.jqID(selectedRowId)).next('tr.jqgrow').attr('id');
                                }
                            }

                            if (nextRowId && conhanphimmuiten) {
                                $gridMaster.jqGrid('setSelection', nextRowId);
                                let rowElement = document.getElementById(nextRowId);
                                if (rowElement) {
                                    scrollInternal($gridMaster.closest('.ui-jqgrid-bdiv'), $(rowElement));
                                }
                                // Ngăn chặn việc cuộn trang mặc định của trình duyệt
                                e.preventDefault();
                            }
                        });

                        Focus_Selection(gridMasterId);
                        //giữ focus end
                        input_focus.focus();
                    },
                    caption: ''
                });
                $gridMaster.jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
                Logo_Center("glyphicon glyphicon-search", true, $dialog.attr('id'));
            },
            close: function () {
                $('#btn-close_').click();
            },
            buttons: [
                {
                    id: 'btn-ok_',
                    text: 'Áp dụng',
                    click: function () {
                        format_khachhang.select($elem, cellSav);
                        $('#btn-close_').click();
                        $elem.focus();
                    }
                },
                {
                    id: 'btn-close_',
                    text: 'Thoát',
                    click: function () {
                        $(this).dialog("destroy").remove();
                    }
                }
            ]
        });
    }
};
//End Doi tac kinh doanh