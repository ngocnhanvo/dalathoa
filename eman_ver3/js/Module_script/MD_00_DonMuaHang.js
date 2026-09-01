//Add function at here (don't remove this line, please)

//start CA_01_TraVeSoanThaoDMH
function CA_01_TraVeSoanThaoDMH(tengrid, id_parent, ma_case, Form_infor, Model_infor, load_stt) {
    let masterId = $(`#${tengrid}`).jqGrid('getGridParam', 'selrow');

    $(`#${tengrid}`).jqGrid('getGridParam', 'selarrrow').toString();

    let CLform_infor = new get_Forminfor(ma_case, Form_infor);
    let ma_module = tengrid.replace('grid', '');
    $('body').append(`
        <div id="dlg_gridSmall" title="${CLform_infor.tieude}">
		    <div class="dlg_content">Thực hiện chức năng này?</div>
	    </div>
    `);

    let $dlgGrid = $('#dlg_gridSmall');
    let $dlgGridContent = $dlgGrid.find('.dlg_content');
    let dlgW = Number(CLform_infor.dodai);
    $dlgGrid.dialog({
        modal: true,
        width: window.innerWidth >= dlgW + 50 ? dlgW : dlgW - 50,
        height: CLform_infor.docao,
        open: function (event, ui) {
            Logo_Center(CLform_infor.logo, CLform_infor.canhgiua);
        },
        close: function () {
            $(this).dialog('destroy').remove();
        },
        buttons: [
            {
                id: 'btn-ok',
                text: 'Đồng ý',
                click: function () {
                    $('#btn-ok').button('option', 'disabled', true);
                    $dlgGridContent.prepend('<div class="nhan_loading"></div>');
                    $.post(`Controller/JQGridModify/JQGrid${ma_module}Modify.ashx?oper=${ma_case}&ma_module=${ma_module}`,
                        { id: masterId }, function (result) {
                            $dlgGridContent.html(result);
                            if (result.lastIndexOf('error') <= -1) {
                                $(`#${tengrid}`)[0].triggerToolbar();
                                Close_Form($dlgGrid, CLform_infor.dongform);
                            }
                        });
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
//end CA_01_TraVeSoanThaoDMH

//start CA_01_SuaDMHHVT
function CA_01_SuaDMHHVT(tengrid, id_parent, ma_case, Form_infor, Model_infor, load_stt) {
    showLoad();
    let masterId = $('#' + tengrid).jqGrid('getGridParam', 'selrow');
    let CLform_infor = new get_Forminfor(ma_case, Form_infor);
    let ma_module = tengrid.replace('grid', '');
    let gridId = 'gridMD_01_DongmuahangCP';
    const labelS = `padding-left: 10px; right:10px; position:relative; font-weight:normal; color:#000;`;
    $('body').append(`
        <div id="dlg_gridSmall" title="${CLform_infor.tieude}">
	        <div class="dlg_content" style="width: 100%; display: flex;">
                <div class="colLeft" style="width: 900px; float: left; background-color: #f7f7f7;">
                    <div style="position: relative; width: 300px; margin: 10px; align-items: center; display: flex;">
                        <input 
                            type="text" 
                            id="mahang_kiotv" 
                            name="mahang_kiotv" 
                            class="FormElement ui-widget-content ui-corner-all ui-autocomplete-input format_vnn" 
                            style="width: 100%;padding: 8px;border-radius: 4px;"
                            placeholder="Tìm hàng hóa theo mã hoặc tên" 
                        />
                    </div>
		            <table id="${gridId}"></table>
                </div>
            
                <div class="colRight FormGrid" style="
                    width: calc(100% - 900px); 
                    min-width: 500px;
                    float: left; 
                    background-color: #f7f7f7; 
                    padding: 20px 30px; 
                    box-sizing: border-box; 
                    font-family: Arial, sans-serif; 
                    font-size: 13px;
                    border-left: 1px solid #ccc;
                ">
                    <div style="display: flex; flex-direction: column; gap: 16px;">
                        <div style="display: flex; align-items: center; gap: 15px; width: 100%;">
                            <div style="display: flex; align-items: center; flex: 1;">
                                <label style="${labelS}">Trạng thái</label>
                                <select id="md_trangthai_id_kov" disabled style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px;">
                                    <option>Soạn thảo</option>
                                </select>
                            </div>
                            <div style="display: flex; align-items: center; flex: 1;">
                                <label style="${labelS}">Số Phiếu</label>
                                <input id="sochungtu_kov" type="text" value="tự động" disabled style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #e6e6e6; color: #333;" />
                            </div>
                        </div>

                        <div style="display: flex; align-items: center; gap: 15px; width: 100%;">
                            <div style="display: flex; align-items: center; flex: 1;">
                                <label style="${labelS}">Mã NCC</label>
                                <div style="flex: 1; display: flex; position: relative; align-items: center; flex: 1;">
                                    <input id="md_doitackinhdoanh_id_kov" type="text" style="width: 100%; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #fff;" />
                                </div>
                            </div>
                            <div style="display: flex; align-items: center; flex: 1;">
                                <label style="${labelS}">Số điện thoại</label>
                                <div style="flex: 1; display: flex; position: relative; align-items: center; flex: 1;">
                                    <input id="tel_kov" type="text" disabled style="width: 100%; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #e6e6e6; color: #333;" />
                                </div>
                            </div>
                        </div>

                        <div style="display: flex; align-items: center;">
                            <label style="${labelS}">Tên NCC</label>
                            <input id="ten_dtkd_kov" type="text" disabled style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #e6e6e6; color: #333;" />
                        </div>

                        <div style="display: flex; align-items: center;">
                            <label style="${labelS}">Địa chỉ</label>
                            <div style="flex: 1; display: flex; position: relative; align-items: center;">
                                <input id="diachi_kov" type="text" disabled style="width: 100%; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #e6e6e6; color: #333;" />
                            </div>
                        </div>

                        <div style="display: flex; align-items: center; gap: 15px; width: 100%;">
                            <div style="display: flex; align-items: center; flex: 1;">
                                <label style="${labelS}">Ngày mua</label>
                                <div style="flex: 1; display: flex; position: relative; align-items: center;">
                                    <input id="ngaydonhang_kov" type="text" style="width: 100%; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px;" />
                                </div>
                            </div>
                            <div style="display: flex; align-items: center; flex: 1;">
                                <label style="${labelS}">Ngày nhập</label>
                                <div style="flex: 1; display: flex; position: relative; align-items: center;">
                                    <input id="ngaygiaohang_kov" type="text" style="width: 100%; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px;" />
                                </div>
                            </div>
                        </div>

                        <div style="display: flex; align-items: center; gap: 15px; width: 100%;">
                            <div style="display: flex; align-items: center;flex: 1;">
                                <label style="${labelS}">Ngày thanh toán</label>
                                <div style="flex: 1; display: flex; position: relative; align-items: center;">
                                    <input id="ngaythanhtoan_kov" type="text" style="width: 100%; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px;" />
                                </div>
                            </div>
                            <div style="display: flex; align-items: center;flex: 1;">
                                <label style="${labelS}">Nơi nhập</label>
                                <input id="diadiem_giaohang_kov" type="text" style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #fff;" />
                            </div>
                        </div>

                        <div style="display: flex; align-items: center; gap: 15px; width: 100%;">
                            <div style="display: flex; align-items: center;flex: 1;">
                                <label style="${labelS}">HT Thanh toán</label>
                                <select id="hinhthucthanhtoan_kov" style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #fff;">
                                    <option>Chuyển khoản</option>
                                </select>
                            </div>
                            <div style="display: flex; align-items: center;flex: 1;">
                                <label style="${labelS}">-</label>
                                <select id="md_dieukienthanhtoan_id_kov" style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #fff;">
                                    <option>Thanh toán ngay khi đặt hàng</option>
                                </select>
                            </div>
                        </div>

                        <div style="display: flex; align-items: center;">
                            <label style="${labelS}">Ghi chú</label>
                            <textarea id="mota_kov" type="text" style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #fff;" />
                        </div>

                        <hr style="border: 0; border-top: 1px dashed #ccc; margin: 10px 0 5px 0;" />

                        <div style="display: flex; align-items: center; margin: 0px 0px 0px auto;">
                            <label style="${labelS}">Tổng tiền</label>
                            <input id="tongtien_kov" type="text" value="0" readonly style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; color: #333; text-align: right; font-weight: bold; padding-right: 28px !important;" />
                        </div>

                        <div style="display: flex; align-items: center; margin: 0px 0px 0px auto;">
                            <label style="${labelS}">Giảm giá</label>
                            <input id="giamgia_kov" type="text" value="0" style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; text-align: right; padding-right: 28px !important;" />
                        </div>

                        <div style="display: flex; align-items: center; margin: 0px 0px 0px auto;">
                            <label style="${labelS}">Chi phí</label>
                            <input id="chiphi_kov" type="text" value="0" style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; text-align: right; padding-right: 28px !important;" />
                        </div>

                        <div style="display: flex; align-items: center; margin: 0px 0px 0px auto;">
                            <label style="${labelS}; font-weight: bold; color: #0078d4;">Cần trả</label>
                            <input id="cantra_kov" type="text" value="0" readonly style="flex: 1; padding: 8px; border: 1px solid #0078d4; border-radius: 2px; color: #0078d4; text-align: right; font-weight: bold; padding-right: 28px !important;" />
                        </div>
                    </div>
                </div>
	        </div>
        </div>
    `);

    let input_focus = '', $gridLeft = $(`#${gridId}`);
    $('#dlg_gridSmall').dialog({
        modal: true,
        width: window.innerWidth - 5,
        height: window.innerHeight,
        open: function (event, ui) {
            $('#dlg_gridSmall').css('height', '');
            $('#hinhthucthanhtoan_kov').html($('#gs_hinhthucthanhtoan').html());
            $('#md_dieukienthanhtoan_id_kov').html($('#gs_md_dieukienthanhtoan_id').html());
            $('#md_trangthai_id_kov').html($('#gs_md_trangthai_id').html());
            format_sanpham_donmuahang.create($('#mahang_kiotv'));
            format_datetime($('#ngaydonhang_kov'));
            format_datetime($('#ngaygiaohang_kov'));
            format_datetime($('#ngaythanhtoan_kov'));
            format_nhacungcap.create($('#md_doitackinhdoanh_id_kov'));
            format_number($('#tongtien_kov'), 1);
            format_number($('#giamgia_kov'), 1);
            format_number($('#chiphi_kov'), 1);
            format_number($('#cantra_kov'), 1);
            $.post(`Controller/JQGridModify/JQGridMD_00_DonMuaHangModify.ashx`, { oper: 'loadEdit', id: masterId }, function (dataM) {
                dataM = JSON.parse(dataM);
                if (!dataM.ok) {
                    alert(dataM.msg);
                    return;
                }

                $('#md_trangthai_id_kov').val(dataM.master.trangthai);
                $('#sochungtu_kov').val(dataM.master.sochungtu);
                $('#md_doitackinhdoanh_id_kov').val(dataM.master.ma_ncc);
                $('#ten_dtkd_kov').val(dataM.master.ten_ncc);
                $('#tel_kov').val(dataM.master.sdt);
                $('#diachi_kov').val(dataM.master.diachi);
                $('#ngaydonhang_kov').val(dataM.master.ngaydonhang);
                $('#ngaygiaohang_kov').val(dataM.master.ngaygiaohang);
                $('#ngaythanhtoan_kov').val(dataM.master.ngaythanhtoan);
                $('#diadiem_giaohang_kov').val(dataM.master.diadiem_giaohang);
                $('#hinhthucthanhtoan_kov').val(dataM.master.hinhthucthanhtoan);
                $('#md_dieukienthanhtoan_id_kov').val(dataM.master.md_dieukienthanhtoan_id);
                $('#mota_kov').val(dataM.master.mota);
                $('#tongtien_kov').val(dataM.master.tong_tienhang);
                $('#giamgia_kov').val(dataM.master.giamgia);
                $('#chiphi_kov').val(dataM.master.chiphi);
                $('#cantra_kov').val(dataM.master.tong_tatca);
                $gridLeft[0].editRowLoad = function (id) {
                    let $inputs = $(`#${id}_sl_dathang, #${id}_gianhap`);
                    // 2. Thao tác đồng thời cho cả 2 input
                    $inputs.attr('autocomplete', 'off');
                    // 3. Sử dụng vòng lặp each() để gán sự kiện focusout cho từng input mà không lo trùng lặp
                    $inputs.each(function () {
                        this.style.setProperty('text-align', 'right', 'important');
                        this.style.setProperty('padding-right', '25px', 'important');
                        if (!this.focusoutF) {
                            this.focusoutF = 1;
                            $(this).focusout(function () {
                                $gridLeft[0].capnhatThanhTien(id);
                            });
                        }
                    });
                    $inputs.first().focus();
                };
                $gridLeft.jqGrid({
                    height: window.innerHeight - 195,
                    data: dataM.details,
                    datatype: 'local',
                    autowidth: true,
                    shrinkToFit: true,
                    rownumbers: true,
                    viewrecords: true,
                    search: true,
                    scroll: false,
                    rowNum: 999999,
                    multiselect: false,
                    multiboxonly: false,
                    rowList: [10, 50, 100, 1000],
                    postData: {
                        _search: true
                    },
                    colModel: [
                        { key: true, fixed: true, label: `md_sanpham_id`, name: 'md_sanpham_id', width: 120, editable: false, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: `c_dongdsdh_id` } },
                        { key: false, fixed: true, label: `Mã hàng`, name: 'ma_sanpham', width: 100, editable: false, editrules: { edithidden: true }, hidden: false, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { dataInit: function (elem) { format_sanpham.create(elem); } }, frozen: false, formoptions: { label: `Mã hàng` } },
                        { key: false, fixed: true, label: `Tên hàng`, name: 'mota_tiengviet', width: 200, editable: false, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 255 }, frozen: false, formoptions: { label: `Tên hàng` } },
                        { key: false, fixed: true, label: `ĐVT`, name: 'md_donvitinhsanpham_id', width: 70, editable: false, editrules: { edithidden: true }, hidden: false, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 255 }, frozen: false, formoptions: { label: `Tên hàng` } },
                        { key: false, fixed: true, label: `SL đặt mua`, name: 'sl_dathang', index: 'dsdh.sl_dathang ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, formatter: vnn_number, unformat: disable_formatter, align: 'right', searchoptions: { sopt: ['en'], dataInit: function (elem) { search_number(elem); } }, editoptions: { defaultValue: '1', dataInit: function (elem) { format_number(elem, 1); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 0, suffix: '' }, formoptions: { label: `SL Đặt Hàng` } },
                        { key: false, fixed: true, label: `Giá mua (VNĐ)`, name: 'gianhap', index: ' dsdh.gianhap ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, formatter: vnn_number, unformat: disable_formatter, align: 'right', searchoptions: { sopt: ['en'] }, editoptions: { defaultValue: '1', dataInit: function (elem) { format_number(elem, 1); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' }, formoptions: { label: `Giá bán (VNĐ)` } },
                        { key: false, fixed: true, label: `Thành tiền`, name: 'thanhtien', index: '0', width: 100, editable: false, hidden: false, formatter: vnn_number, unformat: disable_formatter, align: 'right', search: false, searchoptions: { sopt: ['disable'] }, editoptions: { dataInit: function (elem) { format_number(elem, 1); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' }, formoptions: { label: `Thành tiền` } },
                        {
                            key: false, fixed: true, label: `Xóa`, name: 'del', align: 'center', width: 70, formatter: function (cellvalue, options, rowObject) {
                                setTimeout(function () {
                                    $(`#btn-del-inline-${options.rowId}`).click(function () {
                                        $gridLeft.jqGrid('delRowData', options.rowId);
                                        $gridLeft[0].capnhatTongtien();
                                    });
                                }, 100);
                                return `<button type="button" id="btn-del-inline-${options.rowId}" style="background: none; border: none; cursor: pointer; color: #ff4d4d; font-size: 10px; padding: 0;" title="Xóa dòng này">❌</button>`;
                            }
                        },
                    ],
                    beforeRequest: function () {

                    },
                    gridComplete: function () {
                        $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width() - 25);
                    },
                    loadComplete: function (data) {
                        for (let index in data.rows) {
                            let row = data.rows[index];
                            $gridLeft.editRow(row.md_sanpham_id);
                            $gridLeft[0].editRowLoad(row.md_sanpham_id);
                        }
                    },
                    caption: ''
                });
            })
                .fail(function () {

                })
                .always(function () {
                    hideLoad();
                });

            Logo_Center(CLform_infor.logo, CLform_infor.canhgiua);
            $gridLeft[0].capnhatThanhTien = function (rowid) {
                var sl = Number($(`#${rowid}_sl_dathang`).val()) || 0;
                var gia = Number($(`#${rowid}_gianhap`).val()) || 0;
                var thanhTienMoi = sl * gia;
                $gridLeft.jqGrid('setCell', rowid, 'thanhtien', thanhTienMoi);
                $gridLeft[0].capnhatTongtien();
            };

            $gridLeft[0].capnhatTongtien = function () {
                const datas = $gridLeft.jqGrid('getRowData');
                let tongtien = 0, cantra = 0, giamgia = Number($('#giamgia_kov').val()), chiphi = Number($('#chiphi_kov').val());
                for (let i in datas) {
                    const data = datas[i];
                    let thanhtien = Number(data.thanhtien.replaceAll(vnn_formatnumber()[1], '').replaceAll(',', '.'));
                    tongtien += thanhtien;
                }
                cantra = tongtien - (isNaN(giamgia) ? 0 : giamgia) + (isNaN(chiphi) ? 0 : chiphi);
                $('#tongtien_kov').val(tongtien);
                $('#cantra_kov').val(cantra);
            };

            $('#giamgia_kov, #chiphi_kov').on('change', $gridLeft[0].capnhatTongtien);
        },
        close: function () {
            $(this).dialog('destroy').remove();
        },
        buttons: [
            {
                id: 'btn-ok',
                text: 'Đồng ý',
                click: function () {
                    const details = [];
                    const datas = $gridLeft.jqGrid('getRowData');
                    for (let i in datas) {
                        const data = datas[i];
                        details.push({
                            md_sanpham_id: data.md_sanpham_id,
                            sl: $(`#${data.md_sanpham_id}_sl_dathang`).val(),
                            gia: $(`#${data.md_sanpham_id}_gianhap`).val()
                        });
                    }

                    showLoad();
                    $.post('Controller/JQGridModify/JQGridMD_00_DonMuaHangModify.ashx', {
                        oper: 'editKOV',
                        details: JSON.stringify(details),
                        master: JSON.stringify({
                            md_doitackinhdoanh_id: $('#md_doitackinhdoanh_id_kov').val(),
                            ngaydonhang: $('#ngaydonhang_kov').val(),
                            ngaygiaohang: $('#ngaygiaohang_kov').val(),
                            ngaythanhtoan: $('#ngaythanhtoan_kov').val(),
                            diadiem_giaohang: $('#diadiem_giaohang_kov').val(),
                            hinhthucthanhtoan: $('#hinhthucthanhtoan_kov').val(),
                            md_dieukienthanhtoan_id: $('#md_dieukienthanhtoan_id_kov').val(),
                            giamgia: $('#giamgia_kov').val(),
                            chiphi: $('#chiphi_kov').val(),
                            mota: $('#mota_kov').val(),
                            id: masterId
                        })
                    }, function (rs) {
                        rs = JSON.parse(rs);
                        if (rs.ok) {
                            id_parent1 = null;
                            window[`id_${ma_module}`] = rs.idnew;
                            $(`#${tengrid}`)[0].triggerToolbar();
                            $('#btn-close').click();
                        }
                        else {
                            alert(rs.msg);
                        }
                    }).fail(function (xhr, status, error) {
                        alert("Đã có lỗi xảy ra từ server: " + error);
                    }).always(function () {
                        hideLoad();
                    });
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
//end CA_01_SuaDMHHVT

//start CA_01_THemmoiDMHHVT
function CA_01_THemmoiDMHHVT(tengrid, id_parent, ma_case, Form_infor, Model_infor, load_stt) {
    let masterId = $('#' + tengrid).jqGrid('getGridParam', 'selrow');
    let CLform_infor = new get_Forminfor(ma_case, Form_infor);
    let ma_module = tengrid.replace('grid', '');
    let gridId = 'gridMD_01_DongmuahangCP';
    const labelS = `padding-left: 10px; right:10px; position:relative; font-weight:normal; color:#000;`;
    $('body').append(`
        <div id="dlg_gridSmall" title="${CLform_infor.tieude}">
	        <div class="dlg_content" style="width: 100%; display: flex;">
                <div class="colLeft" style="width: 900px; float: left; background-color: #f7f7f7;">
                    <div style="position: relative; width: 300px; margin: 10px; align-items: center; display: flex;">
                        <input 
                            type="text" 
                            id="mahang_kiotv" 
                            name="mahang_kiotv" 
                            class="FormElement ui-widget-content ui-corner-all ui-autocomplete-input format_vnn" 
                            style="width: 100%;padding: 8px;border-radius: 4px;"
                            placeholder="Tìm hàng hóa theo mã hoặc tên" 
                        />
                    </div>
		            <table id="${gridId}"></table>
                </div>
            
                <div class="colRight FormGrid" style="
                    width: calc(100% - 900px); 
                    min-width: 500px;
                    float: left; 
                    background-color: #f7f7f7; 
                    padding: 20px 30px; 
                    box-sizing: border-box; 
                    font-family: Arial, sans-serif; 
                    font-size: 13px;
                    border-left: 1px solid #ccc;
                ">
                    <div style="display: flex; flex-direction: column; gap: 16px;">
                        <div style="display: flex; align-items: center; gap: 15px; width: 100%;">
                            <div style="display: flex; align-items: center; flex: 1;">
                                <label style="${labelS}">Trạng thái</label>
                                <select id="md_trangthai_id_kov" disabled style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px;">
                                    <option>Soạn thảo</option>
                                </select>
                            </div>
                            <div style="display: flex; align-items: center; flex: 1;">
                                <label style="${labelS}">Số Phiếu</label>
                                <input id="sochungtu_kov" type="text" value="tự động" disabled style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #e6e6e6; color: #333;" />
                            </div>
                        </div>

                        <div style="display: flex; align-items: center; gap: 15px; width: 100%;">
                            <div style="display: flex; align-items: center; flex: 1;">
                                <label style="${labelS}">Mã NCC</label>
                                <div style="flex: 1; display: flex; position: relative; align-items: center; flex: 1;">
                                    <input id="md_doitackinhdoanh_id_kov" type="text" style="width: 100%; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #fff;" />
                                </div>
                            </div>
                            <div style="display: flex; align-items: center; flex: 1;">
                                <label style="${labelS}">Số điện thoại</label>
                                <div style="flex: 1; display: flex; position: relative; align-items: center; flex: 1;">
                                    <input id="tel_kov" type="text" disabled style="width: 100%; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #e6e6e6; color: #333;" />
                                </div>
                            </div>
                        </div>

                        <div style="display: flex; align-items: center;">
                            <label style="${labelS}">Tên NCC</label>
                            <input id="ten_dtkd_kov" type="text" disabled style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #e6e6e6; color: #333;" />
                        </div>

                        <div style="display: flex; align-items: center;">
                            <label style="${labelS}">Địa chỉ</label>
                            <div style="flex: 1; display: flex; position: relative; align-items: center;">
                                <input id="diachi_kov" type="text" disabled style="width: 100%; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #e6e6e6; color: #333;" />
                            </div>
                        </div>

                        <div style="display: flex; align-items: center; gap: 15px; width: 100%;">
                            <div style="display: flex; align-items: center; flex: 1;">
                                <label style="${labelS}">Ngày mua</label>
                                <div style="flex: 1; display: flex; position: relative; align-items: center;">
                                    <input id="ngaydonhang_kov" type="text" style="width: 100%; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px;" />
                                </div>
                            </div>
                            <div style="display: flex; align-items: center; flex: 1;">
                                <label style="${labelS}">Ngày nhập</label>
                                <div style="flex: 1; display: flex; position: relative; align-items: center;">
                                    <input id="ngaygiaohang_kov" type="text" style="width: 100%; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px;" />
                                </div>
                            </div>
                        </div>

                        <div style="display: flex; align-items: center; gap: 15px; width: 100%;">
                            <div style="display: flex; align-items: center;flex: 1;">
                                <label style="${labelS}">Ngày thanh toán</label>
                                <div style="flex: 1; display: flex; position: relative; align-items: center;">
                                    <input id="ngaythanhtoan_kov" type="text" style="width: 100%; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px;" />
                                </div>
                            </div>
                            <div style="display: flex; align-items: center;flex: 1;">
                                <label style="${labelS}">Nơi nhập</label>
                                <input id="diadiem_giaohang_kov" type="text" style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #fff;" />
                            </div>
                        </div>

                        <div style="display: flex; align-items: center; gap: 15px; width: 100%;">
                            <div style="display: flex; align-items: center;flex: 1;">
                                <label style="${labelS}">HT Thanh toán</label>
                                <select id="hinhthucthanhtoan_kov" style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #fff;">
                                    <option>Chuyển khoản</option>
                                </select>
                            </div>
                            <div style="display: flex; align-items: center;flex: 1;">
                                <label style="${labelS}">-</label>
                                <select id="md_dieukienthanhtoan_id_kov" style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #fff;">
                                    <option>Thanh toán ngay khi đặt hàng</option>
                                </select>
                            </div>
                        </div>

                        <div style="display: flex; align-items: center;">
                            <label style="${labelS}">Ghi chú</label>
                            <textarea id="mota_kov" type="text" style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; background: #fff;" />
                        </div>

                        <hr style="border: 0; border-top: 1px dashed #ccc; margin: 10px 0 5px 0;" />

                        <div style="display: flex; align-items: center; margin: 0px 0px 0px auto;">
                            <label style="${labelS}">Tổng tiền</label>
                            <input id="tongtien_kov" type="text" value="0" readonly style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; color: #333; text-align: right; font-weight: bold; padding-right: 28px !important;" />
                        </div>

                        <div style="display: flex; align-items: center; margin: 0px 0px 0px auto;">
                            <label style="${labelS}">Giảm giá</label>
                            <input id="giamgia_kov" type="text" value="0" style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; text-align: right; padding-right: 28px !important;" />
                        </div>

                        <div style="display: flex; align-items: center; margin: 0px 0px 0px auto;">
                            <label style="${labelS}">Chi phí</label>
                            <input id="chiphi_kov" type="text" value="0" style="flex: 1; padding: 8px; border: 1px solid #a6a6a6; border-radius: 2px; text-align: right; padding-right: 28px !important;" />
                        </div>

                        <div style="display: flex; align-items: center; margin: 0px 0px 0px auto;">
                            <label style="${labelS}; font-weight: bold; color: #0078d4;">Cần trả</label>
                            <input id="cantra_kov" type="text" value="0" readonly style="flex: 1; padding: 8px; border: 1px solid #0078d4; border-radius: 2px; color: #0078d4; text-align: right; font-weight: bold; padding-right: 28px !important;" />
                        </div>
                    </div>
                </div>
	        </div>
        </div>
    `);

    let input_focus = '', $gridLeft = $(`#${gridId}`);
    $('#dlg_gridSmall').dialog({
        modal: true,
        width: window.innerWidth - 5,
        height: window.innerHeight,
        open: function (event, ui) {
            $('#dlg_gridSmall').css('height', '');
            $('#hinhthucthanhtoan_kov').html($('#gs_hinhthucthanhtoan').html());
            $('#md_dieukienthanhtoan_id_kov').html($('#gs_md_dieukienthanhtoan_id').html());
            $('#md_trangthai_id_kov').html($('#gs_md_trangthai_id').html());
            format_sanpham_donmuahang.create($('#mahang_kiotv'));
            format_datetime($('#ngaydonhang_kov'));
            format_datetime($('#ngaygiaohang_kov'));
            format_datetime($('#ngaythanhtoan_kov'));
            format_nhacungcap.create($('#md_doitackinhdoanh_id_kov'));
            format_number($('#tongtien_kov'), 1);
            format_number($('#giamgia_kov'), 1);
            format_number($('#chiphi_kov'), 1);
            format_number($('#cantra_kov'), 1);
            $('#md_trangthai_id_kov').val('SOANTHAO');
            $gridLeft[0].editRowLoad = function (id) {
                let $inputs = $(`#${id}_sl_dathang, #${id}_gianhap`);
                // 2. Thao tác đồng thời cho cả 2 input
                $inputs.attr('autocomplete', 'off');
                // 3. Sử dụng vòng lặp each() để gán sự kiện focusout cho từng input mà không lo trùng lặp
                $inputs.each(function () {
                    this.style.setProperty('text-align', 'right', 'important');
                    this.style.setProperty('padding-right', '25px', 'important');
                    if (!this.focusoutF) {
                        this.focusoutF = 1;
                        $(this).focusout(function () {
                            $gridLeft[0].capnhatThanhTien(id);
                        });
                    }
                });
                $inputs.first().focus();
            };
            $gridLeft.jqGrid({
                height: window.innerHeight - 195,
                datatype: 'local',
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
                postData: {
                    _search: true
                },
                colModel: [
                    { key: true, fixed: true, label: `md_sanpham_id`, name: 'md_sanpham_id', width: 120, editable: false, hidden: true, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 32 }, formoptions: { label: `c_dongdsdh_id` } },
                    { key: false, fixed: true, label: `Mã hàng`, name: 'ma_sanpham', width: 100, editable: false, editrules: { edithidden: true }, hidden: false, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { dataInit: function (elem) { format_sanpham.create(elem); } }, frozen: false, formoptions: { label: `Mã hàng` } },
                    { key: false, fixed: true, label: `Tên hàng`, name: 'mota_tiengviet', width: 200, editable: false, editrules: { edithidden: true }, hidden: false, align: 'left', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 255 }, frozen: false, formoptions: { label: `Tên hàng` } },
                    { key: false, fixed: true, label: `ĐVT`, name: 'md_donvitinhsanpham_id', width: 70, editable: false, editrules: { edithidden: true }, hidden: false, align: 'center', searchoptions: { sopt: ['bw'] }, editoptions: { maxLength: 255 }, frozen: false, formoptions: { label: `Tên hàng` } },
                    { key: false, fixed: true, label: `SL đặt mua`, name: 'sl_dathang', index: 'dsdh.sl_dathang ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, formatter: vnn_number, unformat: disable_formatter, align: 'right', searchoptions: { sopt: ['en'], dataInit: function (elem) { search_number(elem); } }, editoptions: { defaultValue: '1', dataInit: function (elem) { format_number(elem, 1); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 0, suffix: '' }, formoptions: { label: `SL Đặt Hàng` } },
                    { key: false, fixed: true, label: `Giá mua (VNĐ)`, name: 'gianhap', index: ' dsdh.gianhap ', width: 100, editable: true, editrules: { edithidden: true }, hidden: false, formatter: vnn_number, unformat: disable_formatter, align: 'right', searchoptions: { sopt: ['en'] }, editoptions: { defaultValue: '1', dataInit: function (elem) { format_number(elem, 1); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' }, formoptions: { label: `Giá bán (VNĐ)` } },
                    { key: false, fixed: true, label: `Thành tiền`, name: 'thanhtien', index: '0', width: 100, editable: false, hidden: false, formatter: vnn_number, unformat: disable_formatter, align: 'right', search: false, searchoptions: { sopt: ['disable'] }, editoptions: { dataInit: function (elem) { format_number(elem, 1); } }, frozen: false, formatoptions: { decimalSeparator: vnn_formatnumber()[0], thousandsSeparator: vnn_formatnumber()[1], decimalPlaces: 'auto', suffix: '' }, formoptions: { label: `Thành tiền` } },
                    {
                        key: false, fixed: true, label: `Xóa`, name: 'del', align: 'center', width: 70, formatter: function (cellvalue, options, rowObject) {
                            setTimeout(function () {
                                $(`#btn-del-inline-${options.rowId}`).click(function () {
                                    $gridLeft.jqGrid('delRowData', options.rowId);
                                    $gridLeft[0].capnhatTongtien();
                                });
                            }, 100);
                            return `<button type="button" id="btn-del-inline-${options.rowId}" style="background: none; border: none; cursor: pointer; color: #ff4d4d; font-size: 10px; padding: 0;" title="Xóa dòng này">❌</button>`;
                        }
                    },
                ],
                beforeRequest: function () {

                },
                gridComplete: function () {
                    $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width() - 25);
                },
                loadComplete: function (data) {
                },
                caption: ''
            });
            Logo_Center(CLform_infor.logo, CLform_infor.canhgiua);
            $gridLeft[0].capnhatThanhTien = function (rowid) {
                var sl = Number($(`#${rowid}_sl_dathang`).val()) || 0;
                var gia = Number($(`#${rowid}_gianhap`).val()) || 0;
                var thanhTienMoi = sl * gia;
                $gridLeft.jqGrid('setCell', rowid, 'thanhtien', thanhTienMoi);
                $gridLeft[0].capnhatTongtien();
            };

            $gridLeft[0].capnhatTongtien = function () {
                const datas = $gridLeft.jqGrid('getRowData');
                let tongtien = 0, cantra = 0, giamgia = Number($('#giamgia_kov').val()), chiphi = Number($('#chiphi_kov').val());
                for (let i in datas) {
                    const data = datas[i];
                    let thanhtien = Number(data.thanhtien.replaceAll(vnn_formatnumber()[1], '').replaceAll(',', '.'));
                    tongtien += thanhtien;
                }
                cantra = tongtien - (isNaN(giamgia) ? 0 : giamgia) + (isNaN(chiphi) ? 0 : chiphi);
                $('#tongtien_kov').val(tongtien);
                $('#cantra_kov').val(cantra);
            };

            $('#giamgia_kov, #chiphi_kov').on('change', $gridLeft[0].capnhatTongtien);
        },
        close: function () {
            $(this).dialog('destroy').remove();
        },
        buttons: [
            {
                id: 'btn-ok',
                text: 'Đồng ý',
                click: function () {
                    const details = [];
                    const datas = $gridLeft.jqGrid('getRowData');
                    for (let i in datas) {
                        const data = datas[i];
                        details.push({
                            md_sanpham_id: data.md_sanpham_id,
                            sl: $(`#${data.md_sanpham_id}_sl_dathang`).val(),
                            gia: $(`#${data.md_sanpham_id}_gianhap`).val()
                        });
                    }

                    showLoad();
                    $.post('Controller/JQGridModify/JQGridMD_00_DonMuaHangModify.ashx', {
                        oper: 'addKOV',
                        details: JSON.stringify(details),
                        master: JSON.stringify({
                            md_doitackinhdoanh_id: $('#md_doitackinhdoanh_id_kov').val(),
                            ngaydonhang: $('#ngaydonhang_kov').val(),
                            ngaygiaohang: $('#ngaygiaohang_kov').val(),
                            ngaythanhtoan: $('#ngaythanhtoan_kov').val(),
                            diadiem_giaohang: $('#diadiem_giaohang_kov').val(),
                            hinhthucthanhtoan: $('#hinhthucthanhtoan_kov').val(),
                            md_dieukienthanhtoan_id: $('#md_dieukienthanhtoan_id_kov').val(),
                            giamgia: $('#giamgia_kov').val(),
                            chiphi: $('#chiphi_kov').val(),
                            mota: $('#mota_kov').val()
                        })
                    }, function (rs) {
                        rs = JSON.parse(rs);
                        if (rs.ok) {
                            window[`id_${ma_module}`] = rs.idnew;
                            $(`#${tengrid}`)[0].triggerToolbar();
                            $('#btn-close').click();
                        }
                        else {
                            alert(rs.msg);
                        }
                    }).fail(function (xhr, status, error) {
                        alert("Đã có lỗi xảy ra từ server: " + error);
                    }).always(function () {
                        hideLoad();
                    });
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
//end CA_01_THemmoiDMHHVT


//start CA_01_KetThucDonMuHang
function CA_01_KetThucDonMuHang(tengrid, id_parent, ma_case, Form_infor, Model_infor, load_stt) {
    let masterId = $(`#${tengrid}`).jqGrid('getGridParam', 'selrow');
    let cell = $(`#${tengrid}`).getRowData(masterId);
    if (masterId == null) {
        alert('Chưa chọn đối tượng cần thao tác.');
        return;
    }

    //if (cell.md_trangthai_id != 'HIEULUC') {
    //    alert(`Đơn mua hàng không ở trạng thái "Hiệu lực".`);
    //    return;
    //}

    let CLform_infor = new get_Forminfor(ma_case, Form_infor);
    let ma_module = tengrid.replace('grid', '');
    $('body').append(`
        <div id="dlg_gridSmall" title="${CLform_infor.tieude}">
            <div class="dlg_content">Bạn muốn kết thúc đơn mua hàng này?</div>
        </div>`
    );

    let $dlg_gridSmall = $('#dlg_gridSmall');
    $dlg_gridSmall.dialog({
        modal: true,
        width: CLform_infor.dodai,
        height: CLform_infor.docao,
        open: function (event, ui) {
            Logo_Center(CLform_infor.logo, CLform_infor.canhgiua);
        },
        close: function () {
            $(this).dialog('destroy').remove();
        },
        buttons: [
            {
                id: 'btn-ok',
                text: 'Đồng ý',
                click: function () {
                    $('#btn-ok').button('option', 'disabled', true);
                    $('.dlg_content').prepend('<div class="nhan_loading"></div>');
                    $.post(`Controller/JQGridModify/JQGrid${ma_module}Modify.ashx?oper=${ma_case}&ma_module=${ma_module}`,
                        { id: masterId.toString() }, function (result) {
                            $('.dlg_content').html(result);
                            $('.nhan_loading').remove();

                            if (result.indexOf('error') <= -1) {
                                $(`#${tengrid}`)[0].triggerToolbar();
                                Close_Form($dlg_gridSmall, CLform_infor.dongform);
                            }
                            else {
                                $('#btn-ok').button('option', 'disabled', false);
                            }
                        });
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
//end CA_01_KetThucDonMuHang

//start CA_01_TPNKMG2
function CA_01_TPNKMG2(tengrid, id_parent, ma_case, Form_infor, Model_infor, load_stt) {
    let masterId = $(`#${tengrid}`).jqGrid('getGridParam', 'selrow');
    let cell = $(`#${tengrid}`).getRowData(masterId);
    if (masterId == null) {
        alert('Chưa chọn đối tượng cần thao tác.');
        return;
    }

    if (cell.md_trangthai_id != 'SOANTHAO') {
        alert(`Đơn mua hàng không ở trạng thái "Soạn Thảo".`);
        return;
    }

    let ngayNhap = $(`#${tengrid}`).jqGrid('getCell', masterId, 'ngaygiaohang') + '';
    let diadiem_giaohang = $(`#${tengrid}`).jqGrid('getCell', masterId, 'diadiem_giaohang') + '';
    let CLform_infor = new get_Forminfor(ma_case, Form_infor);
    let ma_module = tengrid.replace('grid', '');
    const styleSpan = `position:relative; top:-4px;`;
    $('body').append(`
        <div id="dlg_gridSmall" title="${CLform_infor.tieude}">
            <div class="dlg_content">
                <div class="result" style="padding-bottom:6px"></div>
                <table style="width:100%">
                    <tr style="height: 20px;">
                        <td>
                            <span style="${styleSpan}">Địa điểm giao hàng</span><br/>
                            <input id="txtDiadiem" style="width:100%" autocomplete="off" type="text" class="FormElement ui-widget-content ui-corner-all" />
                        </td>
                    </tr>
                    <tr style="height: 7px;"></tr>
                    <tr style="height: 20px;">
                        <td>
                            <span style="${styleSpan}">Ngày nhập hàng</span><br/>
                            <input id="txtNgayChuyen" style="width:100%" autocomplete="off" type="text" class="FormElement ui-widget-content ui-corner-all" />
                        </td>
                    </tr>
                    <tr style="height: 7px;"></tr>
                </table>
            </div>
        </div>
    `);

    let $diaDiem = $('#txtDiadiem');
    let $ngayChuyen = $('#txtNgayChuyen');
    let $dlg_gridSmall = $('#dlg_gridSmall');
    $dlg_gridSmall.dialog({
        modal: true,
        width: CLform_infor.dodai,
        height: CLform_infor.docao,
        open: function (event, ui) {
            $diaDiem.val(diadiem_giaohang);
            $ngayChuyen.val(ngayNhap);
            $ngayChuyen.blur();
            format_datetime($ngayChuyen);
            Logo_Center(CLform_infor.logo, CLform_infor.canhgiua);
        },
        close: function () {
            $(this).dialog('destroy').remove();
        },
        buttons: [
            {
                id: 'btn-ok',
                text: 'Đồng ý',
                click: function () {
                    $('#btn-ok').button('option', 'disabled', true);
                    $('.dlg_content').prepend('<div class="nhan_loading"></div>');
                    $.post(`Controller/JQGridModify/JQGrid${ma_module}Modify.ashx?oper=${ma_case}&ma_module=${ma_module}`,
                        {
                            id: masterId.toString(),
                            diadiemgiaohang: $diaDiem.val(),
                            ngaynhap: $ngayChuyen.val()
                        }, function (result) {
                            $('.dlg_content .result').html(result);
                            $('.nhan_loading').remove();
                            if (result.indexOf('error') <= -1) {
                                $(`#${tengrid}`)[0].triggerToolbar();
                                Close_Form($dlg_gridSmall, CLform_infor.dongform);
                            }
                            else {
                                $('#btn-ok').button('option', 'disabled', false);
                            }
                        });
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
//end CA_01_TPNKMG2


//start CA_01_donMuaHangHoaVatTu
function CA_01_donMuaHangHoaVatTu(tengrid, id_parent, ma_case, Form_infor, Model_infor, load_stt) {
    let masterId = $('#' + tengrid).jqGrid('getGridParam', 'selrow');
    let cell = $('#' + tengrid).getRowData(masterId);
    if (masterId == null | masterId == '') {
        alert('Chưa chọn đối tượng cần thao tác.');
        return;
    }

    let CLform_infor = new get_Forminfor(ma_case, Form_infor);
    let ma_module = tengrid.replace('grid', '');
    let lblStyle = `cursor:pointer;user-select:none;`;
    $('body').append(`
        <div id="dlg_gridSmall" title="${CLform_infor.tieude}">
            <div class="dlg_content">
			    <table>
				    <tr>
					    <td><input id="rdoDMHHVT1" type="radio" name="rdoDMHHVT" style="outline:none" value="1" /></td>
					    <td><label style="${lblStyle}" for="rdoDMHHVT1">In đơn mua hàng hóa vật tư (có giá trị)</label></td>
				    </tr>

                    <tr>
					    <td><input id="rdoDMHHVT2" type="radio" name="rdoDMHHVT" style="outline:none" value="2" /></td>
					    <td><label style="${lblStyle}" for="rdoDMHHVT2">In đơn mua hàng hóa vật tư (không có giá trị)</label></td>
				    </tr>

                    <tr style="height: 30px;">
                        <td colspan=2>
                            <p class ="pTitlePublic">Chọn cách in</p>
                            ${getHTMLSelectKieuIn(0)}
                        </td>
                    </tr>
			    </table>
		    <div>
        </div>`
    );

    $('#dlg_gridSmall').dialog({
        modal: true,
        width: CLform_infor.dodai,
        height: CLform_infor.docao,
        open: function (event, ui) {
            $('#rdoDMHHVT1').click();
            $("#kieuInBaoCao").val(3);

            if (inDMHcoGiaTri) {
                $('#rdoDMHHVT1').click();
            }
            else {
                $('#rdoDMHHVT1').parent().parent().remove();
                $('#rdoDMHHVT2').click();
            }
            Logo_Center(CLform_infor.logo, CLform_infor.canhgiua);
        },
        close: function () {
            $(this).dialog('destroy').remove();
        },
        buttons: [
            {
                id: 'btn-ok',
                text: 'Đồng ý',
                click: function () {
                    let sothapphan = $('#sothapphan').val();
                    let sel_val = $(`input:radio[name="rdoDMHHVT"]:checked`).val();
                    let inPDF = $("#kieuInBaoCao").val();
                    if (sel_val == 1) {
                        url = `${url_org_sys}View/Print/${ma_module}/DMHCoGiaTri.aspx?ma_module=${ma_module}` +
                            "&id=" + masterId +
                            "&inPDF=" + inPDF;
                    }
                    else if (sel_val == 2) {
                        url = `${url_org_sys}View/Print/${ma_module}/DMHKhongGiaTri.aspx?ma_module=${ma_module}` +
                            "&id=" + masterId +
                            "&inPDF=" + inPDF;
                    }

                    openPrintDialog(url, "eMan Anco1", []);
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
//end CA_01_donMuaHangHoaVatTu


//start CA_01_TPNKMG
function CA_01_TPNKMG(tengrid, id_parent, ma_case, Form_infor, Model_infor, load_stt) {
    let masterId = $(`#${tengrid}`).jqGrid('getGridParam', 'selrow');
    let cell = $(`#${tengrid}`).getRowData(masterId);
    if (masterId == null) {
        alert('Chưa chọn đối tượng cần thao tác.');
        return;
    }

    if (cell.md_trangthai_id != 'SOANTHAO') {
        alert(`Đơn mua hàng không ở trạng thái "Soạn Thảo".`);
        return;
    }

    let CLform_infor = new get_Forminfor(ma_case, Form_infor);
    let ma_module = tengrid.replace('grid', '');
    $('body').append(`
        <div id="dlg_gridSmall" title="${CLform_infor.tieude}">
            <div class="dlg_content">Bạn muốn xác nhận đơn mua hàng này?</div>
        </div>`
    );

    let $dlg_gridSmall = $('#dlg_gridSmall');
    $dlg_gridSmall.dialog({
        modal: true,
        width: CLform_infor.dodai,
        height: CLform_infor.docao,
        open: function (event, ui) {
            Logo_Center(CLform_infor.logo, CLform_infor.canhgiua);
        },
        close: function () {
            $(this).dialog('destroy').remove();
        },
        buttons: [
            {
                id: 'btn-ok',
                text: 'Đồng ý',
                click: function () {
                    $('#btn-ok').button('option', 'disabled', true);
                    $('.dlg_content').prepend('<div class="nhan_loading"></div>');
                    $.post(`Controller/JQGridModify/JQGrid${ma_module}Modify.ashx?oper=${ma_case}&ma_module=${ma_module}`,
                        { id: masterId.toString() }, function (result) {
                            $('.dlg_content').html(result);
                            $('.nhan_loading').remove();

                            if (result.indexOf('error') <= -1) {
                                $(`#${tengrid}`)[0].triggerToolbar();
                                Close_Form($dlg_gridSmall, CLform_infor.dongform);
                            }
                            else {
                                $('#btn-ok').button('option', 'disabled', false);
                            }
                        });
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
//end CA_01_TPNKMG

function getCellValue(rowId, cellId) {
    var cell = $('#' + rowId + '_' + cellId);
    var val = cell.val();
    return val;
}