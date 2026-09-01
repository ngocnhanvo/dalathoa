//Start Đơn hàng
var format_donhang = {
    url: 'Controller/JqGrid/JQGridMD_00_DSDHTCJQGSLoad.ashx?ma_module=MD_00_DSDHTCJQGS&ma_menu=MN_01_DSDHTCJQGS&id=null&id_sel=&module_select=1',
    colModel: [
        { key: true, fixed: true, label: 'ID', name: 'c_danhsachdathang_id', index: ' dsdh.c_danhsachdathang_id ', width: 80, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 255 } },
        { key: false, fixed: true, label: 'Số phiếu', name: 'sochungtu', index: ' dsdh.sochungtu ', width: 80, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 255 } },
        { key: false, fixed: true, label: 'Tên khách', name: 'ten_khachhang', index: ' dtkd.ten_dtkd ', width: 130, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', frozen: false, formoptions: { label: 'Tên đối tác' } },
        { key: false, fixed: true, label: 'Địa chỉ', name: 'diachigiaohang', index: ' dtkd.diachi ', width: 130, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, edittype: 'textarea', frozen: false, formoptions: { label: 'Địa chỉ' } },
        { key: false, fixed: true, label: 'Điện Thoại', name: 'tel', index: ' dtkd.tel ', width: 120, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 50 }, frozen: false, formoptions: { label: 'Điện Thoại' } },
        { key: false, fixed: true, label: 'Tổng tiền', name: 'total', index: ' dsdh.total ', width: 90, editable: true, editrules: { edithidden: true }, hidden: false, align: 'right', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 512 }, frozen: false, formatter: vnn_number, unformat: disable_formatter, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' } },
        { key: false, fixed: true, label: 'Trạng thái thanh toán', name: 'trangthaithanhtoan', index: 'dsdh.trangthaithanhtoan', width: 120, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] } },
        { key: false, fixed: true, label: 'Trạng thái đơn hàng', name: 'trangthai', index: 'dsdh.trangthai', width: 120, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] } },
        { key: false, fixed: true, label: 'Ghi chú', name: 'mota', index: 'dsdh.mota', width: 120, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] } },
        { key: false, fixed: true, label: 'Tên người nhận', name: 'ten_nguoinhan', index: 'ttnh.hoten', width: 120, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] } },
        { key: false, fixed: true, label: 'Địa chỉ nhận', name: 'diachi_nguoinhan', index: 'ttnh.diachi', width: 120, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] } },
        { key: false, fixed: true, label: 'SĐT', name: 'sdt_nguoinhan', index: 'ttnh.sdt', width: 120, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] } },
    ],
    select: function ($elem, cellSav) {
        const $elemFrm = $elem.closest('.FormGrid');
        if ($elem.prop('disabled') != true) {
            $elem.val(cellSav.sochungtu);
            $elemFrm.find('#ten_dtkd_kov').val(cellSav.ten_khachhang);
            $elemFrm.find('#diachi_kov').val(cellSav.diachigiaohang);
            $elemFrm.find('#sodienthoai_kov').val(cellSav.tel);
            $elemFrm.find('#trangthaidonhang_kov').val(cellSav.trangthai);
            $elemFrm.find('#trangthaithanhtoan_kov').val(cellSav.trangthaithanhtoan);
            $elemFrm.find('#mota_kov').val(cellSav.mota);
            $elemFrm.find('#ten_nguoinhan_kov').val(cellSav.ten_nguoinhan);
            $elemFrm.find('#diachi_nguoinhan_kov').val(cellSav.diachi_nguoinhan);
            $elemFrm.find('#sodienthoai_nguoinhan_kov').val(cellSav.sdt_nguoinhan);
            showLoad();
            $.get(`Controller/JqGrid/JQGridMD_01_DDSDHTNJQGSLoad.ashx`, {
                ma_module: 'MD_01_DDSDHTNJQGS',
                ma_menu: 'MN_01_DSDHTCJQGS',
                _search: true,
                rows: 9999999,
                page: 1,
                sidx: "",
                sord: "asc",
                id_sel: "",
                module_select: "1",
                filters: JSON.stringify({ "groupOp": "AND", "rules": [] }),
                id: cellSav.c_danhsachdathang_id
            }, function (rs) {
                rs = JSON.parse(rs);
                let $gridRef = $(`#gridMD_01_DongmuahangCP`);
                $gridRef.jqGrid("clearGridData");
                for (let i in rs.rows) {
                    let row = rs.rows[i];
                    $gridRef.jqGrid('addRowData', row.md_sanpham_id, row, 'last');
                    $gridRef.editRow(row.md_sanpham_id);
                    $gridRef[0].editRowLoad(row.md_sanpham_id);
                }
            }).always(function () {
                hideLoad();
            });
        }
    },
    create: function (elem) {
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
            url: format_donhang.url,
            colModel: format_donhang.colModel,
            postData: {
                _search: true
            },
            appendData: function () {
                let filters = {
                    groupOp: 'OR',
                    rules: [
                        { field: 'dsdh.sochungtu', op: "bw", data: $(elem).val() },
                        { field: 'dtkd.ten_dtkd', op: "bw", data: $(elem).val() },
                        { field: 'dtkd.tel', op: "bw", data: $(elem).val() },
                    ]
                };
                return {
                    filters: JSON.stringify(filters)
                };
            },
            select: function (event, ui) {
                format_donhang.select($(elem), ui.item);
                return false;
            }
        });

        $(elem).addClass('format_vnn');
        $(elem).parent().append(`<span onclick="format_donhang.search(this)" class="format_dtkd formatsearch" />`);
    },
    search: function (elem, type) {
        let $elem = $(elem).prev();
        let load_sp = 0, cellSav = {};
        const gridMasterId = 'gridMD_00_DTKD_NCC_cp';
        $('body').append(`
        <div 
            id="dlg_gridSmal_2" 
            style="margin: 4px 0 0 0; background-color: rgba(165, 121, 255, 0.07);" 
            title="Danh mục đơn hàng"
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
                    url: format_donhang.url,
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
                        if (!cellSav.ma_dtkd)
                            return;
                    },
                    colModel: format_donhang.colModel,
                    beforeRequest: function () {
                        //giữ focus
                        if ($elem.val() != '' & load_sp == 0) {
                            let filters = {
                                groupOp: 'AND',
                                rules: [
                                    { field: 'dsdh.sochungtu', op: "bw", data: $elem.val() }
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
                        const $timkiem = $(`.gs_sochungtu.gs_${gridMasterId}`);
                        if (load_sp == 0) {
                            $timkiem.val($elem.val());
                            $gridMaster.jqGrid('setSelection', top_rowid);
                            $(`.tbl_vnn_${gridMasterId} .ui-search-input input`).each(function () {
                                $(this).attr('autocomplete', 'off');
                                if ($(this).attr('id') == 'gs_sochungtu')
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
                $(this).dialog('destroy').remove();
            },
            buttons: [
                {
                    id: 'btn-ok_',
                    text: 'Áp dụng',
                    click: function () {
                        format_donhang.select($elem, cellSav);
                        $(this).dialog('destroy').remove();
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
//End Đơn hàng