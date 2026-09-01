//Start Nhap kho tu NCC
function format_nktncc(elem) {
    var id_elem = $(elem).attr('id');
    $(elem).keypress(function (e) {
        if (e.which == '13') {
            timkiem_nktncc(id_elem, 1);
        }
    });
    $(elem).parent().append('<span onclick="timkiem_nktncc(\'' + id_elem + '\')" ' +
        'class="span_format_lenhsx glyphicon glyphicon-search" style="position: absolute;margin: 1px 0 0 -21px;background-color: rgb(248, 249, 243);padding: 3.5px;cursor: pointer;border-left: 1px solid rgba(204, 204, 204, 0.63)" />');
}

function timkiem_nktncc(id_elem, type) {
    var load_sp = 0, load_sp2 = 0;
    var namegrid = "gridMD_00_DMHHVT_cp";
    $('body').append(`
        <div id="dlg_gridSmal_2" style="margin: 4px 0 0 0; background-color: rgba(165, 121, 255, 0.07);" title="Đơn mua hàng">
            <table id="${namegrid}"></table>
            <div id="pager${namegrid}"></div>
            <table id="${namegrid}_child"></table>
            <div id="pager${namegrid}_child"></div>
        </div>`);

    $('#dlg_gridSmal_2').dialog({
        modal: true,
        dialogClass: "dialog_index",
        width: 650,
        height: window.innerHeight - 10,
        open: function (event, ui) {
            let id_gridMD_00_DMHHVT_cp = null;
            jQuery('#' + namegrid).jqGrid({
                url: 'Controller/JqGrid/JQGridMD_00_DonMuaHangTatCaLoad.ashx?ma_module=MD_00_DonMuaHang&ma_menu=MN_01_DonMuaHang&id=null&id_sel=&module_select=1',
                editurl: '',
                height: window.innerHeight / 2 - 160,
                datatype: 'json',
                autowidth: true,
                shrinkToFit: true,
                rownumbers: true,
                viewrecords: true,
                search: true,
                scroll: false,
                rowNum: 100,
                multiselect: true,
                multiboxonly: true,
                rowList: [10, 50, 100, 1000],
                pager: '#pager' + namegrid,
                onSelectRow: function (ids) {
                    if (ids != '<a style=\'color:red\'>Not data (404)</a>') {
                        cell = $('#' + namegrid).getRowData(ids);
                        if ($('#' + id_elem).prop('disabled') != true) {
                            //$('#' + id_elem).val(cell['sochungtu']);
                            //$('#' + id_elem + '_id').val(cell['c_donmuahang_id']);
                            //$(`#value_khodonhang option:containsExact(${cell['diadiem_giaohang']})`).prop('selected', true);
                        }
                        id_gridMD_00_DMHHVT_cp = ids;
                        checkbox_JQgrid(namegrid, 0);
                        $(`#${namegrid}_child`)[0].triggerToolbar();
                    }
                },
                colModel: [
                    { key: true, fixed: true, label: 'c_donmuahang_id', name: 'c_donmuahang_id', index: ' dmh.c_donmuahang_id ', width: 120, editable: false, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: 'c_donmuahang_id' } },
                    { key: false, fixed: true, label: 'Trạng thái', name: 'md_trangthai_id', index: ' dmh.md_trangthai_id ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, formatter: Frm_MD_00_KHDHJQGS_trangthai, unformat: disable_formatter, align: 'left', stype: 'select', searchoptions: { sopt: ['bw'], value: { '': '', 'SOANTHAO': 'Soạn thảo', 'HIEULUC': 'Hiệu lực' } }, edittype: 'select', editoptions: { value: { 'SOANTHAO': 'Soạn thảo', 'HIEULUC': 'Hiệu lực' } }, frozen: false, formoptions: { label: 'Trạng thái' } },
                    { key: false, fixed: true, label: 'Số Chứng Từ', name: 'sochungtu', index: ' dmh.sochungtu ', width: 100, editable: true, editrules: { edithidden: true }, hidden: true, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 50 }, frozen: false, formoptions: { label: 'Số Chứng Từ' } },
                    { key: false, fixed: true, label: 'Số DMH', name: 'so_donmuahang', index: ' dmh.so_donmuahang ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 50 }, frozen: false },
                    { key: false, fixed: true, label: 'Phiếu nhập kho', name: 'phieunhapkho', index: 'dmh.phieunhapkho', width: 100, editable: true, editrules: { edithidden: true }, hidden: true },
                    { key: false, fixed: true, label: 'Tham chiếu', name: 'sctkehoach', index: 'dmh.sctkehoach', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, frozen: false, formoptions: { label: 'CT Kế Hoạch mua VT' } },
                    { key: false, fixed: true, label: 'Địa điểm giao hàng', name: 'diadiem_giaohang', index: 'dmh.diadiem_giaohang', width: 100, editable: true, editrules: { edithidden: true }, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, frozen: false, formoptions: { label: 'Địa điểm giao hàng' } },
                    { key: false, fixed: true, label: 'Mã NCC', name: 'md_doitackinhdoanh_id', index: 'dtkd.ma_dtkd', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] } },
                    { key: false, fixed: true, label: 'Tên NCC', name: 'ten_dtkd', index: 'dtkd.ten_dtkd', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] } }
                ],
                postData: {
                    where_ex: function () {
                        return " and (dmh.md_trangthai_id = 'HIEULUC' or dmh.md_trangthai_id = 'CHUAXONG')";
                    }
                },
                beforeRequest: function () {
                    //giữ focus
                    input_focus = $('input:focus').attr('class');
                },
                ondblClickRow: function () {

                },
                gridComplete: function () {
                    $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width() - 5);
                },
                loadComplete: function (data) {
                    let top_rowid = $('#' + namegrid + ' tr:nth-child(2)').attr('id');
                    let countrow = jQuery("#" + namegrid).jqGrid('getGridParam', 'records');
                    if (load_sp == 0) {
                        let val_lsxid = $('#' + id_elem).val();
                        let grid = jQuery("#" + namegrid);
                        let grid_ids = grid.jqGrid('getDataIDs');
                        for (var i = 0; i < grid_ids.length; i++) {
                            let rowid = grid_ids[i];
                            let aRow = grid.jqGrid('getRowData', rowid);
                            let book_id = aRow['sochungtu'];
                            if (val_lsxid.indexOf(book_id) > -1) {
                                grid.jqGrid('setSelection', rowid);
                            }
                        }
                        
                        load_sp = 1;
                        if (top_rowid.indexOf('<a style="color:red">') <= -1 & type == 1 & countrow == 1) {
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

            //luoi 2
            jQuery(`#${namegrid}_child`).jqGrid({
                url: 'Controller/JqGrid/ZDongMuaHangLoad.ashx?ma_module=MD_01_DongMuaHangBS&ma_menu=MN_01_MuaHang&module_select=1&id_sel=1',
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
                pager: `pager${namegrid}_child`,
                onSelectRow: function (ids) {

                },
                colModel: [
                    { key: true, fixed: true, label: 'c_donmuahang_cdmh_id', name: 'c_donmuahang_cdmh_id', index: ' cdmh.c_donmuahang_cdmh_id ', width: 110, editable: false, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, frozen: false, formoptions: { label: 'c_donmuahang_cdmh_id' } },
                    { key: false, fixed: true, label: 'Mã hàng', search: false, name: 'md_sanpham_id', index: 'sp.ma_sanpham', width: 95, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { dataInit: function (elem) { format_vattuBTPvaSP(elem); } }, frozen: false, formoptions: { label: 'Mã hàng' } },
                    { key: false, fixed: true, label: 'ĐVT', search: false, name: 'md_donvitinhsanpham_id', index: 'cdmh.md_donvitinhsanpham_id', width: 60, editable: true, editrules: { edithidden: true }, hidden: false, align: 'center' },
                    { key: false, fixed: true, label: 'SL mua', search: false, name: 'sl_dadat', index: ' cdmh.sl_dadat ', width: 70, editable: true, editrules: { edithidden: true }, hidden: false, formatter: vnn_number, unformat: disable_formatter, align: 'right', searchoptions: { sopt: ['en'], dataInit: function (elem) { search_number(elem); } }, editoptions: { defaultValue: '0', dataInit: function (elem) { format_number(elem, 0, 4); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' }, formoptions: { label: 'Số lượng đặt mua' } },
                    { key: false, fixed: true, label: 'SL nhập kho', search: false, name: 'sl_hanngach', index: ' cdmh.sl_hanngach ', width: 70, editable: true, editrules: { edithidden: true }, hidden: false, formatter: vnn_number, unformat: disable_formatter, align: 'right', searchoptions: { sopt: ['en'], dataInit: function (elem) { search_number(elem); } }, editoptions: { defaultValue: '0', dataInit: function (elem) { format_number(elem, 0, 4); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' } },
                    { key: false, fixed: true, label: 'Đơn giá', search: false, name: 'dongiamua', index: ' cdmh.dongiamua ', width: 80, editable: true, editrules: { edithidden: true }, hidden: true, formatter: vnn_number, unformat: disable_formatter, align: 'right', searchoptions: { sopt: ['en'], dataInit: function (elem) { search_number(elem); } }, editoptions: { defaultValue: '0', dataInit: function (elem) { format_number(elem); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' }, formoptions: { label: 'Đơn giá mua' } },
                    { key: false, fixed: true, label: 'Thuế', search: false, name: 'thue', index: 'thue_sp.ten_thue_sanpham', width: 50, editable: true, editrules: { edithidden: true }, hidden: false, align: 'center' },
                    { key: false, fixed: true, label: 'Thành tiền', search: false, name: 'thanhtien', index: ' cdmh.thanhtien ', width: 90, editable: true, editrules: { edithidden: true }, hidden: true, formatter: vnn_number, unformat: disable_formatter, align: 'right', searchoptions: { sopt: ['en'], dataInit: function (elem) { search_number(elem); } }, editoptions: { defaultValue: '0', dataInit: function (elem) { format_number(elem, 0); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' }, formoptions: { label: 'Thành tiền' } },
                    { key: false, fixed: true, label: 'Số DMH', search: false, name: 'so_donmuahang', index: 'dmh.so_donmuahang', width: 95, editable: true, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { dataInit: function (elem) { format_vattuBTPvaSP(elem); } } },
                    { key: false, fixed: true, label: 'Kho', search: false, name: 'khomacdinh', index: 'sp.khomacdinh', width: 60, editable: true, editrules: { edithidden: true }, hidden: true, align: 'center' },
                    { key: false, fixed: true, label: 'Kho tồn', search: false, name: 'khoton', index: 'sp.khomacdinh', width: 60, editable: true, editrules: { edithidden: true }, hidden: true, align: 'center' },
                    { key: false, fixed: true, label: 'Vật tư', search: false, name: 'vattu', index: 'sp.vattu', width: 60, editable: true, editrules: { edithidden: true }, hidden: true, align: 'center' },
                    { key: false, fixed: true, label: 'BTP', search: false, name: 'ban_thanhpham', index: 'sp.ban_thanhpham', width: 60, editable: true, editrules: { edithidden: true }, hidden: true, align: 'center' },
                    { key: false, fixed: true, label: 'c_kehoachdathang_id', search: false, name: 'c_kehoachdathang_id', index: 'khdh.c_kehoachdathang_id', width: 60, editable: true, editrules: { edithidden: true }, hidden: true, align: 'center' },
                    { key: false, fixed: true, label: 'Kho VTs', search: false, name: 'kvts', index: 'sp.vattu', width: 60, editable: true, editrules: { edithidden: true }, hidden: true, align: 'center' }
                ],
                beforeRequest: function () {
                    let ids = jQuery('#' + namegrid).jqGrid('getGridParam', 'selarrrow');
                    if (load_sp2 == 0) {
                        ids = $('#' + id_elem).next().val();
                        load_sp2 = 1;
                    }
                    $(`#${namegrid}_child`).jqGrid('getGridParam', 'postData').id = ids.toString();
                },
                ondblClickRow: function () {

                },
                gridComplete: function () {
                    $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width() - 5);
                },
                loadComplete: function (data) {
                    //giữ focus end
                    $('.' + input_focus).focus();
                },
                caption: 'Dòng mua hàng'
            });
            jQuery(`#${namegrid}_child`).jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
            $(`#gview_${namegrid}_child .ui-jqgrid-title`).css('float', 'left');
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
                    let ids_sel = $('#' + namegrid).jqGrid('getGridParam', 'selarrrow');
                    let sct_pnk = '', id_pnk = '', noigh = '', ncc = '', ten_ncc = '', msg = '';
                    let resetData = function () {
                        sct_pnk = '';
                        id_pnk = '';
                        noigh = '';
                        ncc = '';
                        ten_ncc = '';
                    };

                    if ($('#' + id_elem).prop('disabled') != true) {
                        for (var i = 0; i < ids_sel.length; i++) {
                            var cel_s = $('#' + namegrid).getRowData(ids_sel[i]);
                            sct_pnk += cel_s['sochungtu'] + ' --- ' + cel_s['so_donmuahang'] + '\n';
                            id_pnk += cel_s['c_donmuahang_id'] + ',';
                            let tsx0 = cel_s['diadiem_giaohang'] + '';
                            let tsx1 = cel_s['md_doitackinhdoanh_id'] + '';
                            let tsx2 = cel_s['ten_dtkd'] + '';
                            if (noigh == '') {
                                noigh = tsx0;
                            }
                            else if (noigh != tsx0) {
                                msg = 'Địa điểm giao hàng của các đơn mua hàng không giống nhau.';
                                resetData();
                                break;
                            }

                            if (ncc == '') {
                                ncc = tsx1;
                                ten_ncc = tsx2;
                            }
                            else if (ncc != tsx1) {
                                msg = 'Nhà cung cấp của các đơn mua hàng không giống nhau.';
                                resetData();
                                break;
                            }
                        }
                    }

                    if (msg.length > 0) {
                        alert(msg);
                    }
                    else {
                        let rowsDT = $(`#${namegrid}_child`).jqGrid('getRowData');
                        let ddghs = [];
                        for (let i in rowsDT) {
                            let rowDT = rowsDT[i];
                            let vattu = rowDT.vattu == 'true';
                            let btp = rowDT.ban_thanhpham == 'true';
                            let khdh = rowDT.c_kehoachdathang_id;
                            let kvts = rowDT.kvts.split(',');
                            

                            if (khdh) {
                                let chk = ddghs.filter(function (a) { return a == rowDT.khomacdinh; })[0];
                                if (!chk)
                                    ddghs.push(rowDT.khomacdinh);
                            }
                            else {
                                if (vattu) {
                                    for (let i in kvts) {
                                        let kvt = kvts[i];
                                        let chk = ddghs.filter(function (a) { return a == kvt; })[0];
                                        if (!chk)
                                            ddghs.push(kvt);
                                    }
                                }
                                else {
                                    let chk = ddghs.filter(function (a) { return a == rowDT.khoton; })[0];
                                    if (!chk)
                                        ddghs.push(rowDT.khoton);
                                }
                            }
                        }

                        $('#value_khodonhang option').hide();
                        for (let i in ddghs) {
                            $(`#value_khodonhang option[value="${ddghs[i]}"]`).show();
                            $(`#value_khodonhang option[value="${ddghs[i]}"]`).prop('selected', true);
                        }

                        $('#' + id_elem).val(sct_pnk);
                        $('#' + id_elem).next().val(id_pnk.length > 0 ? id_pnk.substr(0, id_pnk.length - 1) : id_pnk);
                        $(`#value_madtkd`).val(ncc);
                        $(`#value_tendtkd`).val(ten_ncc);
                        $(this).dialog('destroy').remove();
                    }
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
//End Nhap kho tu NCC