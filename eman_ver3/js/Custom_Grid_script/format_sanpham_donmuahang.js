//Start San pham
var format_sanpham_donmuahang = {
    url: 'Controller/JqGrid/JQGridMD_00_DSHangHoaVGLoad.ashx?ma_module=MD_00_DSHangHoaVG&ma_menu=MN_01_DSHangHoaVG&id=null&id_sel=&module_select=1',
    colModel: [
        { key: true, fixed: true, label: 'Sản Phẩm Id', name: 'md_sanpham_id', index: ' sp.md_sanpham_id ', width: 120, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 255 }, frozen: true, formoptions: { label: 'Sản Phẩm Id' } },
        { key: false, fixed: true, label: 'Mã Sản Phẩm', name: 'ma_sanpham', index: ' sp.ma_sanpham ', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 255 }, frozen: true, formoptions: { label: 'Mã Sản Phẩm' } },
        { key: false, fixed: true, label: 'Mô Tả Tiếng Việt', name: 'mota_tiengviet', index: ' sp.mota_tiengviet ', width: 160, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', frozen: false, formoptions: { label: 'Mô Tả Tiếng Việt' } },
        { key: false, fixed: true, label: 'Đơn Vị Tính', name: 'md_donvitinhsanpham_id', index: 'dvt_sp.ten_dvt', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'Đơn Vị Tính' } },
        { key: false, fixed: true, label: 'Tồn kho', name: 'tonkho', index: 'sp.tonkho', width: 120, editable: false, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] } },
        { key: false, fixed: true, label: 'Đặt hàng', name: 'dathang', index: 'sp.dathang', width: 120, editable: false, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] } }
    ],
    select: function ($elem, cellSav) {
        const $elemFrm = $elem.closest('.FormGrid');
        if ($elem.prop('disabled') != true) {
            const id = cellSav.md_sanpham_id;
            let $gridRef = $(`#gridMD_01_DongmuahangCP`);
            const exist = $gridRef.getRowData(id);
            if (exist.ma_sanpham != undefined) {
                alert(`Đã tồn tại mã ${exist.ma_sanpham}`);
                return;
            }

            $gridRef[0].globalSttCounter = $gridRef[0].globalSttCounter + 1;
            cellSav.sl_dathang = 1;
            $gridRef.jqGrid('addRowData', id, cellSav, 'first');
            $gridRef.editRow(id);
            $gridRef[0].editRowLoad(id);
            $elem.val('');
            $gridRef[0].capNhatNhanhSTT();
            $elem.focus();
        }
    },
    create: function (elem) {
        if (!$(elem)[0].keyPressF) {
            $(elem)[0].keyPressF = 1;
            $(elem).keypress(function (e) {
                if (e.which == '13') {
                    $(elem).next().click();
                }
            });
        }

        $(elem).combogrid({
            searchIcon: false,
            width: 'auto',
            munit: 'px',
            replaceNull: false,
            url: format_sanpham_donmuahang.url,
            colModel: format_sanpham_donmuahang.colModel,
            postData: {
                _search: true
            },
            appendData: function () {
                let filters = {
                    groupOp: 'OR',
                    rules: [
                        { field: 'sp.ma_sanpham', op: "bw", data: $(elem).val() },
                        { field: 'sp.mota_tiengviet', op: "bw", data: $(elem).val() }
                    ]
                };
                return {
                    filters: JSON.stringify(filters)
                };
            },
            open: function (event, ui) {

            },
            select: function (event, ui) {
                format_sanpham_donmuahang.select($(elem), ui.item);
                return false;
            }
        });

        $(elem).addClass('format_vnn');
        $(elem).parent().append(`<span onclick="format_sanpham_donmuahang.search(this)" class="format_dtkd formatsearch" />`);
        setTimeout(function () { $('#tr_sanpham_id').hide(); }, 10);
    },
    search: function (elem, type) {
        let $elem = $(elem).prev();
        let load_sp = 0, cellSav = {};
        const gridMasterId = 'gridMD_00_DSHangHoaVG_cp';
        $('body').append(`
            <div id="dlg_gridSmal_2" style="margin: 4px 0 0 0; background-color: rgba(165, 121, 255, 0.07);" title="Danh mục hàng hóa">
                <table id="${gridMasterId}"></table>
                <div id="pager${gridMasterId}"></div>
            </div>
        `);

        let $gridMaster = $(`#${gridMasterId}`);
        let $dialog = $('#dlg_gridSmal_2');
        $dialog.dialog({
            modal: true,
            dialogClass: "dialog_index",
            width: 530,
            height: window.innerHeight - 10,
            open: function (event, ui) {
                $gridMaster.jqGrid({
                    url: format_sanpham_donmuahang.url,
                    editurl: '',
                    height: window.innerHeight - 200,
                    datatype: 'json',
                    autowidth: true,
                    shrinkToFit: true,
                    rownumbers: true,
                    viewrecords: true,
                    search: true,
                    scroll: false,
                    rowNum: 50,
                    multiselect: false,
                    multiboxonly: false,
                    rowList: [10, 50, 100, 1000],
                    pager: `#pager${gridMasterId}`,
                    onSelectRow: function (ids) {
                        cellSav = $gridMaster.getRowData(ids);
                        if (!cellSav.ma_sanpham)
                            return;
                    },
                    colModel: format_sanpham_donmuahang.colModel,
                    beforeRequest: function () {
                        //giữ focus
                        if ($elem.val() != '' & load_sp == 0) {
                            let filters = {
                                groupOp: 'AND',
                                rules: [
                                    { field: 'sp.ma_sanpham', op: "bw", data: $elem.val() }
                                ]
                            };
                            $gridMaster.jqGrid('getGridParam', 'postData').filters = JSON.stringify(filters);
                        }
                        input_focus = $('input:focus');
                    },
                    ondblClickRow: function () {
                        $(`#btn-ok_`).click();
                    },
                    gridComplete: function () {
                        $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width() - 5);
                    },
                    loadComplete: function (data) {
                        let top_rowid = $gridMaster.find('tr:nth-child(2)').attr('id') + '';
                        let countrow = $gridMaster.jqGrid('getGridParam', 'records');
                        const $timkiem = $(`.gs_ma_sanpham.gs_${gridMasterId}`);
                        if (load_sp == 0) {
                            $timkiem.val($elem.val());
                            input_focus = $timkiem;
                            $gridMaster.jqGrid('setSelection', top_rowid);
                            $(`.tbl_vnn_${gridMasterId} .ui-search-input input`).each(function () {
                                $(this).attr('autocomplete', 'off');
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
                $(this).dialog('destroy').remove();
            },
            buttons: [
                {
                    id: 'btn-ok_',
                    text: 'OK',
                    click: function () {
                        format_sanpham_donmuahang.select($elem, cellSav);
                        $(this).dialog('destroy').remove();
                        $elem.focus();
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
}
//End San pham