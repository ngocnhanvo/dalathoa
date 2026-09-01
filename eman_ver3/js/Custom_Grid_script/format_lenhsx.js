//Start Lệnh sản xuất
var format_lenhsx = {
    create: function (elem) {
        $(elem).parent().append(`
            <span
                class="format_lenhsx glyphicon glyphicon-search" 
                style="position:absolute;margin:1px 0 0 -21px;background-color:rgb(248, 249, 243);padding: 3.5px;cursor:pointer;border-left:1px solid rgba(204, 204, 204, 0.63)"
            />`
        );

        $(elem).attr('readonly', true);
        $(elem).css('cursor', 'default');
        $(elem).next().click(function () {
            format_lenhsx.search($(this));
        });

        $(elem).keypress(function (e) {
            if (e.which == '13') {
                format_lenhsx.search($(this), 1);
            }
        });
    },
    search: function ($elem, type) {
        let $id_elem = $elem.prev();
        if ($id_elem.prop('disabled'))
            return;
        let load_sp = 0, multi = false;
        let namegrid = "gridMD_00_Lenhsanxuat_cp";
        $('body').append(`
            <div id="dlg_gridSmal_2" style="margin: 4px 0 0 0; background-color: rgba(165, 121, 255, 0.07);" title="Danh mục Lệnh sản xuất">
                <table id="${namegrid}"></table>
                <div id="pager${namegrid}"></div>
            </div>
        `);

        let gridSel = $('.module_spanselect').attr('id').substring(5);
        let nhapkhoNB = gridSel.startsWith('MD_00_Nhapnoibo');
        let xuatkhoNB = gridSel.startsWith('MD_00_Xuatnoibo');
        let chuyenkhoNB = gridSel.startsWith('MD_00_PVCNoiBo');
        let bosung = $(`#bosung`).val();
        let loaichuyen = $('#loaichuyen').val();
        let xuatVTtheoBOM = bosung == 0 & xuatkhoNB;
        let xuatVTBoSung = bosung == 1 & xuatkhoNB;
        let xuatHangTho = bosung == 3 & xuatkhoNB;
        let nhapKhoHH = bosung == 0 & nhapkhoNB;
        let giaoHangTP = loaichuyen == 'VANCNBCTKGH' & chuyenkhoNB;
        let $dlgGridS2 = $('#dlg_gridSmal_2');
        let $gridLSXCP = $('#gridMD_00_Lenhsanxuat_cp');
        $dlgGridS2.dialog({
            modal: true,
            dialogClass: "dialog_index",
            width: 650,
            height: window.innerHeight - 10,
            open: function (event, ui) {
                $gridLSXCP.jqGrid({
                    url: `Controller/JqGrid/JQGridMD_00_LenhSanXuatKHLoad.ashx?ma_module=MD_00_LenhSanXuatKH&ma_menu=${$('#input_idmenu').val()}`,
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
                    multiselect: multi,
                    multiboxonly: false,
                    rowList: [10, 50, 100, 1000],
                    pager: '#pagergridMD_00_Lenhsanxuat_cp',
                    onSelectRow: function (ids) {
                        let cell = $gridLSXCP.getRowData(ids);
                        $id_elem.val(cell.sochungtu);
                        $('#sctdathang').val(cell['sctdathang']);
                        $('#donhang_thamchieu').val(cell['donhang_thamchieu']);
                        $('#xuatden').val(cell.xuongPhu);
                        $('#nhaptu').val(cell.xuongPhu);
                        
                        if (nhapKhoHH) {
                            $('#denkho').val(cell.khomd);
                        }
                        else if (giaoHangTP) {
                            $('#tukho').val(cell.khomd);
                        }
                    },
                    colModel: [{
                        key: true,
                        xuatExcel: 1,
                        khoaTuyChinh: false,
                        fixed: true,
                        label: 'md_lenhsanxuat2_id',
                        name: 'md_lenhsanxuat2_id',
                        index: ' lsx2.md_lenhsanxuat2_id ',
                        width: 120,
                        editable: false,
                        hidden: true,
                        align: 'left',
                        searchoptions: {
                            sopt: ['bw']
                        },
                        editoptions: {
                            maxLength: 32
                        },
                        formoptions: {
                            label: 'md_lenhsanxuat2_id'
                        }
                    }, {
                        key: false,
                        xuatExcel: 1,
                        khoaTuyChinh: false,
                        fixed: true,
                        label: 'Trạng thái',
                        name: 'trangthai',
                        index: ' lsx2.trangthai ',
                        width: 120,
                        editable: true,
                        editrules: {
                            edithidden: true
                        },
                        hidden: false,
                        formatter: Frm_MD_00_Lenhsanxuat2_trangthai,
                        unformat: disable_formatter,
                        align: 'left',
                        stype: 'select',
                        searchoptions: {
                            sopt: ['bw'],
                            value: ':;SOANTHAO:Chờ xác nhận;HIEULUC:Đã nhận;KETTHUC:Kết thúc'
                        },
                        edittype: 'select',
                        editoptions: {
                            value: 'SOANTHAO:Chờ xác nhận;HIEULUC:Đã nhận;KETTHUC:Kết thúc'
                        },
                        frozen: false,
                        formoptions: {
                            label: 'Trạng thái'
                        }
                    }, {
                        key: false,
                        xuatExcel: 1,
                        khoaTuyChinh: false,
                        fixed: true,
                        label: 'Số chứng từ',
                        name: 'sochungtu',
                        index: ' lsx2.sochungtu ',
                        width: 100,
                        editable: true,
                        editrules: {
                            edithidden: true
                        },
                        hidden: false,
                        align: 'center',
                        searchoptions: {
                            sopt: ['bw']
                        },
                        frozen: false,
                        formoptions: {
                            label: 'Số chứng từ'
                        }
                    }, {
                        key: false,
                        xuatExcel: 1,
                        khoaTuyChinh: false,
                        fixed: true,
                        label: 'Tên LSX',
                        name: 'donhang',
                        index: ' lsx2.donhang ',
                        width: 120,
                        editable: false,
                        hidden: false,
                        align: 'left',
                        searchoptions: {
                            sopt: ['bw']
                        },
                        editoptions: {
                            maxLength: 50
                        },
                        frozen: false,
                        formoptions: {
                            label: 'Tên LSX'
                        }
                    }, {
                        key: false,
                        xuatExcel: 1,
                        khoaTuyChinh: false,
                        fixed: true,
                        label: 'Xưởng SX',
                        name: 'xuongPhu',
                        index: 'pb.ten_phongban',
                        width: 120,
                        editable: true,
                        editrules: {
                            edithidden: true
                        },
                        hidden: false,
                        align: 'left',
                        edittype: 'select',
                        frozen: false,
                        formoptions: {
                            label: 'Xưởng SX'
                        }
                    }, {
                        key: false,
                        xuatExcel: 1,
                        khoaTuyChinh: false,
                        fixed: true,
                        label: 'Ngày bắt đầu',
                        name: 'ngaybatdau',
                        index: ' lsx2.ngaybatdau ',
                        width: 104,
                        editable: true,
                        editrules: {
                            edithidden: true
                        },
                        hidden: false,
                        formatter: esc_date,
                        align: 'center',
                        searchoptions: {
                            sopt: ['cn'],
                            dataInit: function (elem) {
                                search_datetime(elem);
                            }
                        },
                        frozen: false,
                        formatoptions: {
                            srcformat: 'm/d/Y',
                            newformat: format_srcdatetime()
                        },
                        formoptions: {
                            label: 'Ngày bắt đầu'
                        }
                    }, {
                        key: false,
                        xuatExcel: 1,
                        khoaTuyChinh: false,
                        fixed: true,
                        label: 'Ngày kết thúc',
                        name: 'ngayketthuc',
                        index: ' lsx2.ngayketthuc ',
                        width: 104,
                        editable: true,
                        editrules: {
                            edithidden: true
                        },
                        hidden: false,
                        formatter: esc_date,
                        align: 'center',
                        searchoptions: {
                            sopt: ['cn'],
                            dataInit: function (elem) {
                                search_datetime(elem);
                            }
                        },
                        frozen: false,
                        formatoptions: {
                            srcformat: 'm/d/Y',
                            newformat: format_srcdatetime()
                        },
                        formoptions: {
                            label: 'Ngày kết thúc'
                        }
                    }, {
                        key: false,
                        xuatExcel: 1,
                        khoaTuyChinh: false,
                        fixed: true,
                        label: 'Hạn giao hàng',
                        name: 'ngayhoanthanh',
                        index: ' lsx2.ngayhoanthanh ',
                        width: 104,
                        editable: true,
                        editrules: {
                            edithidden: true
                        },
                        hidden: false,
                        formatter: esc_date,
                        align: 'center',
                        searchoptions: {
                            sopt: ['cn'],
                            dataInit: function (elem) {
                                search_datetime(elem);
                            }
                        },
                        frozen: false,
                        formatoptions: {
                            srcformat: 'm/d/Y',
                            newformat: format_srcdatetime()
                        },
                        formoptions: {
                            label: 'Hạn giao hàng'
                        }
                    }, {
                        key: false,
                        label: 'khoMD',
                        name: 'khomd',
                        hidden: true
                    }],
                    postData: {
                        where_ex: function () {
                            let whereAll = `and lsx2.trangthai in ('HIEULUC','KETTHUC')`;
                            if (xuatVTtheoBOM) {
                                return `
                                    ${whereAll}
                                    and EXISTS (
	                                    select 
		                                    top 1 1
	                                    from 
		                                    md_lenhsanxuat_tosx_vattu vt 
		                                    left join md_lenhsanxuat_tosx_cdh cdh on vt.sp2 = cdh.macuoi and vt.sp3 = cdh.mathaydoi 
	                                    where 
		                                    1=1 
		                                    and cdh.lsxCT = lsx2.sochungtu
		                                    and vt.sp1 = cdh.sp1
		                                    and cdh.md_lenhsanxuat_tosx_id = vt.md_lenhsanxuat_tosx_id 
		                                    and isnull(vt.vattu, 0) = 1
                                            and isnull(vt.soluong, 0) > isnull(vt.sl_hanngach, 0)
                                    )
                                `;
                            }
                            else if (xuatVTBoSung) {
                                return `
                                    ${whereAll}
                                `;
                            }
                            else if (xuatHangTho) {
                                return `
                                    ${whereAll}
                                    and EXISTS (
	                                    select 
		                                    top 1 1
	                                    from 
		                                    md_lenhsanxuat_tosx_vattu vt 
		                                    left join md_lenhsanxuat_tosx_cdh cdh on vt.sp2 = cdh.macuoi and vt.sp3 = cdh.mathaydoi 
	                                    where 
		                                    1=1 
		                                    and cdh.lsxCT = lsx2.sochungtu
		                                    and vt.sp1 = cdh.sp1
		                                    and cdh.md_lenhsanxuat_tosx_id = vt.md_lenhsanxuat_tosx_id 
		                                    and isnull(vt.bantp, 0) = 1
                                            and isnull(vt.vattu, 0) = 0
                                            and isnull(vt.soluong, 0) > isnull(vt.sl_hanngach, 0)
                                    )
                                `;
                            }
                            else if (nhapKhoHH) {
                                return `
                                    ${whereAll}
                                    and (
	                                    select 
		                                    sum(cdh.sl_chiato - isnull(cdh.sl_chiato2, 0) - isnull(cdh.sl_datncc, 0) - isnull(cdh.sl_datncc2, 0) - isnull(cdh.sl_danhapkho, 0))
	                                    from 
		                                    md_lenhsanxuat_tosx_cdh cdh
	                                    where 
		                                    1=1 
		                                    and cdh.lsxCT = lsx2.sochungtu
		                                    and cdh.xuongPhu = lsx2.xuongPhu
                                    ) > 0
                                `;
                            }
                            else if (giaoHangTP) {
                                return `
                                    ${whereAll}
                                    and (
	                                    select 
		                                    sum(cdh.sl_danhapkho - isnull(cdh.sl_dagiao, 0))
	                                    from 
		                                    md_lenhsanxuat_tosx_cdh cdh
	                                    where 
		                                    1=1 
		                                    and cdh.lsxCT = lsx2.sochungtu
		                                    and cdh.xuongPhu = lsx2.xuongPhu
                                            and cdh.xuongPhu = cdh.xuongChinh
                                    ) > 0
                                `;
                            }
                            else {
                                return ``;
                            }
                        }
                    },
                    beforeRequest: function () {
                        
                    },
                    ondblClickRow: function () {
                        $dlgGridS2.dialog("close");
                    },
                    gridComplete: function () {
                        $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width() - 5);
                    },
                    loadComplete: function (data) {

                    },
                    caption: ' '
                });
                $gridLSXCP.jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
                Logo_Center("glyphicon glyphicon-search", true, $dlgGridS2.attr('id'));
            },
            close: function () {
                let firstL = false;
                $('#tukho option[value!=""]').each(function (a) {
                    if (this.style.display != 'none' & !firstL) {
                        $(this).prop('selected', true);
                        firstL = true;
                    }
                });
                $(this).dialog('destroy').remove();
            },
            buttons: [
                {
                    id: 'btn-ok_',
                    text: 'OK',
                    click: function () {
                        if (multi == true) {
                            let ids_sel = $('#' + namegrid).jqGrid('getGridParam', 'selarrrow'), sct_pnk = '', sct_dhtc = '', val_dhtc = '';
                            let dhtcs = [];
                            for (var i = 0; i < ids_sel.length; i++) {
                                var cel_s = $('#' + namegrid).getRowData(ids_sel[i]);
                                sct_pnk += cel_s['sochungtu'] + '\n';
                                sct_dhtc += cel_s['sctdathang'] + '\n';
                                val_dhtc += cel_s['donhang_thamchieu'] + '\n';
                                dhtcs.push(cel_s['donhang_thamchieu']);
                            }
                            if (sct_pnk != '') {
                                sct_pnk = sct_pnk.substring(0, sct_pnk.length - 1);
                                sct_dhtc = sct_dhtc.substring(0, sct_dhtc.length - 1);
                                val_dhtc = val_dhtc.substring(0, val_dhtc.length - 1);
                            }

                            $('#chungtu_lenhsx').val(sct_pnk);
                            $('#sctdathang').val(sct_dhtc);
                            $('#donhang_thamchieu').val(val_dhtc);
                            $('#md_lenhsanxuat_tosx_id').val('');
                            khoa_column('sctdathang');

                            if (tengrid0 == 'gridMD_00_PVCNoiBo_PDN') {
                                let dhtcsDis = dhtcs.filter(function onlyUnique(value, index, array) {
                                    return array.indexOf(value) === index;
                                });

                                console.log(dhtcsDis);
                                if (dhtcsDis.length > 1) {
                                    alert('Đơn hàng bạn chọn phải trùng nhau');
                                    return;
                                }
                            }
                        }
                        $(this).dialog("close");
                    }
                },
                {
                    id: 'btn-close_',
                    text: 'Cancel',
                    click: function () {
                        $(this).dialog("close");
                    }
                }
            ]
        });
    }
};
//End Lệnh sản xuất