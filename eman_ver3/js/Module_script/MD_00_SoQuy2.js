//Add function at here (don't remove this line, please)

/**
 * Dialog thêm / sửa Sổ quỹ.
 * Hỗ trợ một phiếu thu phân bổ cho nhiều hóa đơn qua c_soquy_hoadon.
 */
function loadDialogThemSuaSoQuy2(tengrid, id_parent, ma_module, ma_case, Form_infor, Model_infor, jsonEdit) {
    const actionEdit = !!(jsonEdit && jsonEdit.master);
    const master = actionEdit ? jsonEdit.master : {};
    let allocations = actionEdit && Array.isArray(jsonEdit.allocations)
        ? jsonEdit.allocations.slice()
        : [];
    let masterId = $('#' + tengrid).jqGrid('getGridParam', 'selrow');
    let CLform_infor;
    let loaiGiaoDich = 'THU';

    if (typeof ma_case !== 'string') {
        loaiGiaoDich = String(ma_case.loai_giaodich || 'THU').toUpperCase();
        CLform_infor = new get_Forminfor(ma_case.old, Form_infor);
        ma_case = ma_case.new;
    } else {
        CLform_infor = new get_Forminfor(ma_case, Form_infor);
        if (actionEdit && master.loai_giaodich) {
            loaiGiaoDich = String(master.loai_giaodich).toUpperCase();
        }
    }

    const laThu = loaiGiaoDich === 'THU';
    const textThuChi = laThu ? 'thu' : 'chi';
    const textNguoi = laThu ? 'nộp' : 'nhận';
    const khoaPhanBo = actionEdit
        && master.nguon_nghiepvu === 'HOADON_BANHANG'
        && !!master.c_hoadonbanhang_id;

    $('body').append(`
        <div id="dlg_gridSmall" title="${CLform_infor.tieude}" style="background:#fff;">
            <div class="dlg_content sq-dialog">
                <style>
                    #dlg_gridSmall .sq-wrap{padding:18px 22px 10px;box-sizing:border-box;font-size:14px}
                    #dlg_gridSmall .sq-row{display:flex;gap:28px;margin-bottom:17px}
                    #dlg_gridSmall .sq-col{flex:1;min-width:0}
                    #dlg_gridSmall .sq-label{display:block;color:#292929;margin-bottom:7px;font-weight:normal}
                    #dlg_gridSmall .sq-input,#dlg_gridSmall .sq-select,#dlg_gridSmall .sq-textarea{width:100%;box-sizing:border-box;border:1px solid #cfd5dc;border-radius:6px;color:#222;outline:none;font-size:14px}
                    #dlg_gridSmall .sq-input:not(.formatnumber),#dlg_gridSmall .sq-select{height:40px;padding:0 12px}
                    #dlg_gridSmall .sq-input.formatnumber{height:40px;padding-right:40px!important}
                    #dlg_gridSmall .sq-textarea{min-height:88px;resize:vertical;padding:10px 12px}
                    #dlg_gridSmall .sq-readonly{background:#f5f7f9;color:#7a8591}
                    #dlg_gridSmall .sq-money{text-align:right;font-size:18px;font-weight:600}
                    #dlg_gridSmall .sq-inline{display:flex;align-items:center;gap:7px}
                    #dlg_gridSmall .sq-create-new{white-space:nowrap;color:#0675e8;cursor:pointer;text-decoration:none}
                    #dlg_gridSmall .sq-allocation{margin-top:4px;border-top:1px solid #e1e5e9;padding-top:14px}
                    #dlg_gridSmall .sq-allocation-head{display:flex;align-items:center;justify-content:space-between;margin-bottom:10px}
                    #dlg_gridSmall .sq-allocation-title{font-size:15px;font-weight:600}
                    #dlg_gridSmall .sq-load-hd{border:1px solid #1473e6;background:#fff;color:#1473e6;border-radius:5px;padding:6px 12px;cursor:pointer}
                    #dlg_gridSmall .sq-table-wrap{overflow:auto;max-height:280px;border:1px solid #d7dce1;border-radius:5px}
                    #dlg_gridSmall .sq-table{width:100%;border-collapse:collapse;min-width:850px}
                    #dlg_gridSmall .sq-table th{background:#f1f3f5;text-align:left;padding:10px;border-bottom:1px solid #d7dce1;white-space:nowrap}
                    #dlg_gridSmall .sq-table td{padding:9px 10px;border-bottom:1px solid #e5e8eb;vertical-align:middle}
                    #dlg_gridSmall .sq-table .num{text-align:right}
                    #dlg_gridSmall .sq-phanbo-input{width:125px;text-align:right;border:1px solid #cfd5dc;border-radius:4px;padding:6px}
                    #dlg_gridSmall .sq-status-ok{color:#16823b;background:#e8f7ed;border-radius:4px;padding:3px 6px;white-space:nowrap}
                    #dlg_gridSmall .sq-status-debt{color:#9b6500;background:#fff4d8;border-radius:4px;padding:3px 6px;white-space:nowrap}
                    #dlg_gridSmall .sq-allocation-total{text-align:right;padding:10px 2px;font-size:14px;line-height:26px}
                    #dlg_gridSmall .sq-allocation-total strong{display:inline-block;min-width:130px;font-size:16px}
                    @media(max-width:850px){#dlg_gridSmall .sq-row{display:block}#dlg_gridSmall .sq-col{margin-bottom:14px}}
                </style>

                <div class="sq-wrap">
                    <div class="sq-row">
                        <div class="sq-col"><label class="sq-label">Mã phiếu</label><input id="sq_ma_phieu" class="sq-input sq-readonly" type="text" readonly placeholder="Tự động" /></div>
                        <div class="sq-col"><label class="sq-label">Thời gian</label><input id="sq_ngay_giaodich" class="sq-input" type="text" /></div>
                    </div>
                    <div class="sq-row">
                        <div class="sq-col"><label class="sq-label">${laThu ? 'Loại thu' : 'Loại chi'}</label><select id="sq_md_loaithuchi_id" class="sq-select"></select></div>
                        <div class="sq-col"><label class="sq-label">Người ${textThuChi}</label><select id="sq_nguoi_thuchi" class="sq-select"></select></div>
                    </div>
                    <div class="sq-row">
                        <div class="sq-col"><label class="sq-label">Đối tượng ${textNguoi}</label><select disabled id="sq_md_loaidtkd_id" class="sq-select"></select></div>
                        <div class="sq-col" style="position:relative">
                            <div class="sq-inline" style="justify-content:space-between"><label class="sq-label">Tên người ${textNguoi}</label><a class="sq-create-new" id="sq_btn_tao_doitac">Tạo mới</a></div>
                            <input id="sq_md_doitackinhdoanh_id" type="hidden" />
                            <input id="sq_nguoi_nop_nhan" class="sq-input" type="text" placeholder="Tìm người ${textNguoi}" autocomplete="off" />
                        </div>
                    </div>
                    <div class="sq-row">
                        <div class="sq-col"><label class="sq-label">Phương thức thanh toán</label><select id="sq_phuongthucthanhtoan" class="sq-select"></select></div>
                        <div class="sq-col"><label class="sq-label">Số tiền</label><input id="sq_sotien" class="sq-input sq-money" inputmode="numeric" type="text" value="0" /></div>
                    </div>
                    <div class="sq-row"><div class="sq-col"><label class="sq-label">Ghi chú</label><textarea id="sq_diengiai" class="sq-textarea" placeholder="Nhập ghi chú"></textarea></div></div>

                    ${laThu ? `
                    <div class="sq-allocation">
                        <div class="sq-allocation-head">
                            <label class="sq-inline" style="margin:0"><input id="sq_phanbo_enabled" type="checkbox" ${allocations.length ? 'checked' : ''} /> <span class="sq-allocation-title">Phân bổ vào hóa đơn</span></label>
                            <button type="button" id="sq_load_hoadon" class="sq-load-hd" ${khoaPhanBo ? 'style="display:none"' : ''}>Nạp hóa đơn còn nợ</button>
                        </div>
                        <div id="sq_allocation_body" style="${allocations.length ? '' : 'display:none'}">
                            <div class="sq-table-wrap">
                                <table class="sq-table">
                                    <thead><tr><th>Mã phiếu</th><th>Thời gian</th><th class="num">Giá trị phiếu</th><th class="num">Đã thu trước</th><th class="num">Tiền thu</th><th>Trạng thái</th></tr></thead>
                                    <tbody id="sq_allocation_rows"></tbody>
                                </table>
                            </div>
                            <div class="sq-allocation-total">
                                <div>Đã phân bổ: <strong id="sq_total_allocated">0</strong></div>
                                <div>Tiền chưa phân bổ: <strong id="sq_unallocated">0</strong></div>
                            </div>
                        </div>
                    </div>` : ''}
                </div>
            </div>
        </div>
    `);

    const $dlg = $('#dlg_gridSmall');

    function money(v) {
        const n = Number(v || 0);
        return n.toLocaleString('en-US', { maximumFractionDigits: 8 });
    }

    function numberValue(v) {
        if (typeof v === 'number') return v;
        return Number(String(v || '0').replace(/,/g, '')) || 0;
    }

    function copyOptions(target, candidates) {
        let html = '';
        for (let i = 0; i < candidates.length; i++) {
            const $source = $(candidates[i]);
            if ($source.length) {
                html = $source.html();
                if (html && $.trim(html).length) break;
            }
        }
        if (html) { $(target).html(html); return true; }
        return false;
    }

    if (typeof nhanviensSoQuy !== 'undefined' && Array.isArray(nhanviensSoQuy)) {
        $('#sq_nguoi_thuchi').html(nhanviensSoQuy.map(user =>
            `<option value="${user.ma_user}">${user.hoten}</option>`).join(''));
        if (!actionEdit && typeof ma_tk !== 'undefined') $('#sq_nguoi_thuchi').val(ma_tk);
    }

    copyOptions('#sq_md_loaithuchi_id', ['#gs_md_loaithuchi_id', '#gs_ten_loaithuchi', '#gs_ma_loaithuchi']);
    copyOptions('#sq_md_loaidtkd_id', ['#gs_md_loaidtkd_id', '#gs_ten_loaidtkd', '#gs_ma_loaidtkd']);

    $('#sq_btn_tao_doitac').on('click', function () {
        openPrintDialog('?menu=MN_01_DTKD&case=CA_00_26102015014357160PM', 'Thêm đối tác', []);
    });

    function renderAllocations() {
        if (!laThu) return;
        const enabled = $('#sq_phanbo_enabled').prop('checked');
        $('#sq_allocation_body').toggle(enabled);
        if (!enabled) return;

        const rows = allocations.map((r, index) => {
            const giaTri = numberValue(r.gia_tri_phieu);
            const daThu = numberValue(r.da_thu_truoc);
            const phanBo = numberValue(r.sotien_phanbo);
            const conLai = Math.max(0, giaTri - daThu - phanBo);
            const status = conLai <= 0 ? 'Đã thanh toán' : 'Còn nợ';
            return `<tr data-index="${index}">
                <td><a href="javascript:void(0)" style="color:#087af5">${r.ma_phieu || ''}</a></td>
                <td>${r.ngay || ''}</td>
                <td class="num">${money(giaTri)}</td>
                <td class="num">${money(daThu)}</td>
                <td class="num"><input class="sq-phanbo-input" data-index="${index}" value="${money(phanBo)}" ${khoaPhanBo ? 'readonly' : ''} /></td>
                <td><span class="${status === 'Đã thanh toán' ? 'sq-status-ok' : 'sq-status-debt'}">${status}</span></td>
            </tr>`;
        }).join('');

        $('#sq_allocation_rows').html(rows || '<tr><td colspan="6" style="text-align:center;color:#7a8591">Chưa có hóa đơn để phân bổ</td></tr>');
        updateAllocationTotals();
    }

    function updateAllocationTotals() {
        let total = 0;
        allocations.forEach(r => total += numberValue(r.sotien_phanbo));
        const soTien = numberValue($('#sq_sotien').val());
        $('#sq_total_allocated').text(money(total));
        $('#sq_unallocated').text(money(Math.max(0, soTien - total)));
    }

    function loadHoaDonPhanBo() {
        const doiTacId = $('#sq_md_doitackinhdoanh_id').val();
        if (!doiTacId) {
            alert('Vui lòng chọn khách hàng trước khi phân bổ hóa đơn.');
            return;
        }

        showLoad();
        $.post(`Controller/JQGridModify/JQGrid${ma_module}Modify.ashx`, {
            oper: 'loadHoaDonPhanBo',
            md_doitackinhdoanh_id: doiTacId,
            c_soquy_id: actionEdit ? (master.c_soquy_id || masterId || '') : ''
        }, function (rs) {
            if (typeof rs === 'string') {
                try { rs = JSON.parse(rs); } catch (e) { rs = null; }
            }
            if (!rs || rs.ok === false) {
                alert((rs && rs.error) || 'Không tải được hóa đơn.');
                return;
            }

            const oldMap = {};
            allocations.forEach(x => oldMap[x.c_hoadonbanhang_id] = numberValue(x.sotien_phanbo));
            allocations = (rs.rows || []).map(x => {
                if (oldMap[x.c_hoadonbanhang_id] != null) x.sotien_phanbo = oldMap[x.c_hoadonbanhang_id];
                return x;
            });
            $('#sq_phanbo_enabled').prop('checked', true);
            renderAllocations();
        }).fail(function (xhr) {
            alert(xhr.responseText || 'Không tải được hóa đơn.');
        }).always(function () { hideLoad(); });
    }

    if (laThu) {
        $('#sq_phanbo_enabled').on('change', renderAllocations);
        $('#sq_load_hoadon').on('click', loadHoaDonPhanBo);
        $('#sq_allocation_rows').on('change blur', '.sq-phanbo-input', function () {
            const index = Number($(this).data('index'));
            let value = numberValue($(this).val());
            const row = allocations[index];
            if (!row) return;
            const max = Math.max(0, numberValue(row.gia_tri_phieu) - numberValue(row.da_thu_truoc));
            value = Math.max(0, Math.min(value, max));
            row.sotien_phanbo = value;
            $(this).val(money(value));
            renderAllocations();
        });
        $('#sq_sotien').on('change blur keyup', updateAllocationTotals);
    }

    function getMasterSoQuy() {
        return {
            loai_giaodich: loaiGiaoDich,
            ngay_giaodich: $('#sq_ngay_giaodich').val(),
            md_loaithuchi_id: $('#sq_md_loaithuchi_id').val(),
            md_doitackinhdoanh_id: $('#sq_md_doitackinhdoanh_id').val(),
            md_loaidtkd_id: $('#sq_md_loaidtkd_id').val(),
            nguoi_nop_nhan: $.trim($('#sq_nguoi_nop_nhan').val()),
            sotien: $('#sq_sotien').val(),
            nguoi_thuchi: $('#sq_nguoi_thuchi').val(),
            phuongthucthanhtoan: $('#sq_phuongthucthanhtoan').val(),
            diengiai: $.trim($('#sq_diengiai').val())
        };
    }

    function getAllocations() {
        if (!laThu || !$('#sq_phanbo_enabled').prop('checked')) return [];
        return allocations
            .map(x => ({
                c_hoadonbanhang_id: x.c_hoadonbanhang_id,
                sotien_phanbo: numberValue(x.sotien_phanbo)
            }))
            .filter(x => x.c_hoadonbanhang_id && x.sotien_phanbo > 0);
    }

    function validateSoQuy(masterData) {
        if (!masterData.ngay_giaodich) return 'Vui lòng chọn thời gian.';
        if (!masterData.md_loaithuchi_id) return `Vui lòng chọn loại ${textThuChi}.`;
        if (!masterData.sotien || numberValue(masterData.sotien) <= 0) return 'Số tiền phải lớn hơn 0.';

        const phanBo = getAllocations();
        const tong = phanBo.reduce((s, x) => s + numberValue(x.sotien_phanbo), 0);
        if (tong > numberValue(masterData.sotien)) return 'Tổng tiền phân bổ không được lớn hơn số tiền phiếu thu.';
        return '';
    }

    function saveSoQuy(inPhieu) {
        const masterData = getMasterSoQuy();
        const error = validateSoQuy(masterData);
        if (error) { alert(error); return; }

        const $btnSave = $('#btn_sq_save');
        const $btnPrint = $('#btn_sq_save_print');
        $btnSave.prop('disabled', true);
        $btnPrint.prop('disabled', true);
        showLoad();

        $.post(`Controller/JQGridModify/JQGrid${ma_module}Modify.ashx?ma_module=${ma_module}`, {
            oper: actionEdit ? 'edit' : 'add',
            id: actionEdit ? (master.c_soquy_id || masterId) : '',
            master: JSON.stringify(masterData),
            allocations: JSON.stringify(getAllocations())
        }, function (result) {
            hideLoad();
            $btnSave.prop('disabled', false);
            $btnPrint.prop('disabled', false);

            const ok = typeof result === 'string' && result.indexOf('true#') === 0;
            if (!ok) {
                alert(typeof result === 'string' ? result.replace(/^false#/, '') : 'Có lỗi xảy ra.');
                return;
            }

            try { $(`#${tengrid}`)[0].triggerToolbar(); }
            catch (e) { $(`#${tengrid}`).trigger('reloadGrid'); }
            $dlg.dialog('destroy').remove();
        }).fail(function (xhr) {
            hideLoad();
            $btnSave.prop('disabled', false);
            $btnPrint.prop('disabled', false);
            alert(xhr.responseText || 'Không thể lưu phiếu.');
        });
    }

    $dlg.dialog({
        modal: true,
        width: Math.min(1200, Math.max(800, window.innerWidth - 120)),
        height: 'auto',
        maxHeight: window.innerHeight - 40,
        open: function () {
            $('#sq_nguoi_nop_nhan')[0].keyfmt = 'ten_dtkd';
            format_khachhang.create($('#sq_nguoi_nop_nhan'));
            format_datetime($('#sq_ngay_giaodich'));
            $('#sq_phuongthucthanhtoan').html($('#gs_hinhthucthanhtoan_kov').html());
            $('#sq_md_loaidtkd_id').html($('#gs_doituongnop').html());
            format_number($('#sq_sotien'), 1);

            if (!actionEdit) {
                $('#sq_ma_phieu').val('');
                if (typeof ma_tk !== 'undefined') $('#sq_nguoi_thuchi').val(ma_tk);
                $('#sq_ngay_giaodich').val(get_now());
                $('#sq_phuongthucthanhtoan').val('Tiền mặt');
            } else {
                $('#sq_ma_phieu').val(master.ma_phieu || '');
                $('#sq_ngay_giaodich').val(master.ngay_giaodich || '');
                $('#sq_md_loaithuchi_id').val(master.md_loaithuchi_id || '');
                $('#sq_md_doitackinhdoanh_id').val(master.md_doitackinhdoanh_id || '');
                $('#sq_md_loaidtkd_id').val(master.md_loaidtkd_id || '');
                $('#sq_nguoi_nop_nhan').val(master.nguoi_nop_nhan || '');
                $('#sq_nguoi_thuchi').val(master.nguoi_thuchi || '');
                $('#sq_diengiai').val(master.diengiai || '');
                $('#sq_sotien').val(master.sotien);
                $('#sq_phuongthucthanhtoan').val(master.phuongthucthanhtoan);
                $('#sq_md_loaithuchi_id').prop('disabled', true);
                khoa_column('sq_sotien');
                khoa_column('sq_nguoi_nop_nhan');
                setTimeout(() => $('#btn_sq_save_print').hide(), 50);
            }

            renderAllocations();
            try { Logo_Center(CLform_infor.logo, CLform_infor.canhgiua); } catch (e) { }

            $('#btn_sq_cancel').prepend('<i class="fa fa-times" style="position:absolute;margin-right:50px"></i>');
            $('#btn_sq_save_print').prepend('<i class="fa fa-print" style="position:absolute;margin-right:60px"></i>');
            $('#btn_sq_save').prepend('<i class="fa fa-save" style="position:absolute;margin-right:50px"></i>');

            const isMobile = window.innerWidth <= 600;
            $('#btn_sq_cancel, #btn_sq_save_print, #btn_sq_save').css({
                display:'inline-flex','align-items':'center','justify-content':'center',gap:isMobile?'4px':'8px',
                'min-width':isMobile?'82px':'105px',flex:isMobile?'1 1 0':'none',height:'42px',
                padding:isMobile?'0 5px':'0 16px','font-size':isMobile?'12px':'14px','font-weight':'600','margin-right':'10px'
            });
            $('#btn_sq_save').css({'border-color':'#087af5'});
        },
        close: function () { $(this).dialog('destroy').remove(); },
        buttons: [
            { id:'btn_sq_cancel', text:'Bỏ qua', click:function(){ $(this).dialog('destroy').remove(); } },
            { id:'btn_sq_save_print', text:'Lưu & In', click:function(){ saveSoQuy(true); } },
            { id:'btn_sq_save', text:'Lưu', click:function(){ saveSoQuy(false); } }
        ]
    });
}

//start CA_01_SuaSoQuyKOV
function CA_01_SuaSoQuyKOV(tengrid, id_parent, ma_case, Form_infor, Model_infor, load_stt) {
    let ma_module = tengrid.replace('grid', '');
    id_parent = $(`#${tengrid}`).jqGrid('getGridParam', 'selrow');
    showLoad();
    $.post(`Controller/JQGridModify/JQGrid${ma_module}Modify.ashx`, { oper:'loadEdit', id:id_parent }, function (rs) {
        try {
            if (typeof rs === 'string') rs = JSON.parse(rs);
            if (rs.error) { alert(rs.error); return; }
            if (!rs.master) { alert('Server không trả dữ liệu phiếu.'); return; }
            loadDialogThemSuaSoQuy2(tengrid,id_parent,ma_module,ma_case,Form_infor,Model_infor,rs);
        } catch (e) {
            console.error(e, rs);
            alert('Không đọc được dữ liệu phiếu từ server.');
        }
    }).fail(function(xhr,status,error){
        alert('Đã có lỗi xảy ra từ server: ' + (xhr.responseText || error));
    }).always(function(){ hideLoad(); });
}
//end CA_01_SuaSoQuyKOV

//start CA_01_ThemSoQuyKOV
function CA_01_ThemSoQuyKOV(tengrid,id_parent,ma_case,Form_infor,Model_infor,load_stt) {
    let ma_module = 'MD_00_SoQuy2';
    $('body').append(`<div id="dlg_chon_loai_soquy" title="Chọn loại phiếu"><div style="display:flex;gap:20px;justify-content:center;align-items:center;padding:25px 10px"><button type="button" id="btn_chon_thu" style="min-width:160px;padding:15px 20px;font-size:16px;cursor:pointer">Phiếu thu</button><button type="button" id="btn_chon_chi" style="min-width:160px;padding:15px 20px;font-size:16px;cursor:pointer">Phiếu chi</button></div></div>`);
    const $dlg = $('#dlg_chon_loai_soquy');
    function moFormSoQuy(loai) {
        $dlg.dialog('destroy').remove();
        loadDialogThemSuaSoQuy2(tengrid,id_parent,ma_module,{old:ma_case,new:ma_case,loai_giaodich:loai},Form_infor,Model_infor);
    }
    $dlg.dialog({modal:true,width:430,height:'auto',resizable:false,close:function(){$(this).dialog('destroy').remove();}});
    $('#btn_chon_thu').on('click',function(){moFormSoQuy('THU');});
    $('#btn_chon_chi').on('click',function(){moFormSoQuy('CHI');});
}
//end CA_01_ThemSoQuyKOV
