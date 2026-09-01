//Get data from server to client
let ngayhethong = '', now = '';
function addZero(e) { return 10 > e && (e = "0" + e), e }
function vnn_formatdatetime(e) { var t = addZero(e.getDate()), n = addZero(e.getMonth() + 1), a = e.getFullYear(), i = addZero(e.getHours()), r = addZero(e.getMinutes()), o = addZero(e.getSeconds()), l = ngayhethong, c = ngayhethong.split(" "), s = "", d = ""; c[0].indexOf("yyyy") > -1 ? l = l.replace(/yyyy/g, a) : (a = a.toString().substring(2, 4), l = l.replace(/yy/g, a)); try { d = c[2], d.indexOf("tt") > -1 | d.indexOf("TT") > -1 && (s = addAMPM(i, d), 0 == i ? i = 12 : i > 12 && (i = addZero(i - 12))) } catch (h) { } return l = l.replace(/dd/g, t).replace(/MM/g, n).replace(/hh/g, i).replace(/mm/g, r).replace(/ss/g, o).replace(/tt/, s).replace(/TT/, s) }
function get_now() {
    let now = new Date();
    now = vnn_formatdatetime(now);
    return now;
}
// Nhi-kk ADD STT
function link_report() {
    //window.open("extension/report.aspx", "reportFrame");
    click_loadtrangchu();
}
function checkNewReport() {
    let aCanhBao = $('#a_canhbao');
    $.post('Controller/JQGridModify/JQGridMD_00_TBModify.ashx?oper=getJSONMess', { count: true }, function (rs) {
        rs = Number(rs);
        if (!isNaN(rs)) {
            let val = aCanhBao.html();
            val = (rs > 9 ? '9+' : rs);
            aCanhBao.html(val);
            if (Number(rs) > 0) {
                aCanhBao.attr('numrpt', rs);
                document.title = `${rs} Tin nhắn mới`;
            }
            else {
                if (document.title.lastIndexOf('Tin nhắn mới') > -1)
                    document.title = 'eMan';
                aCanhBao.removeAttr('numrpt');
            }
        }
    });
}
// Nhi-kk ADD END

let sessionbd = new Array(), id_action = new Array(), id_oper = new Array(), reset_column = new Array(), module_select = new Array();
let t = "", max_es = 3600000, min_es = 1000, espera2 = min_es, enable_timer = true, refresco2 = null, module_thuake = 0, loai_module_md = 'JQG', reload_page = null, xhr_menu, xhr_content, title_header = '', title_header_1 = '', title_header_2 = '', title_header_3 = '', loaddautien_page = 0, loadgrid_at = true, systemload = 0, id_new = "0", header_sep = ' <a style="color:rgb(225, 66, 110); font-weight:bold"> → </a> ';

var encodeHTML = function (a) {
    if (a) {
        a = a.replace(/</g, '0ψ0');
        a = a.replace(/>/g, '1Ψ1');
    }
    return a;
};

var decodeHTML = function (a) {
    if (a) {
        a = a.replace(/0ψ0/g, '<');
        a = a.replace(/1Ψ1/g, '>');
    }
    return a;
};

var uuidv4 = function () {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
};

function xemthongtinchitiet() {
    let n = "",
        i = "MD_00_TaiKhoan";
    const styleCHK = `right:4px; width:15px; height:15px; position:relative; top:-10px`;
    const styleDivCHK = `display: flex; flex-direction: row-reverse; width: 20%;`;
    $("#mainpane").append(`
        <div 
            id="dlg_gridSmall"
            style="overflow:hidden"
            align="center"
            title="Thông tin chi tiết của tài khoản"
        >
            <table style="width:100%" id="xemchitiet">
                <tr>
                    <td>
                        <label>Họ tên</label>
                        <br />
                        <input readonly id="xct_hoten" type="text" value="Đang tải..." /></td>
                    <td>
                        <label>Email</label>
                        <br />
                        <input autocomplete="one-time-code" readonly id="xct_thudientu" type="text" value="Đang tải..." />
                    </td>
                    <td>
                        <label>Điện thoại</label>
                        <br />
                        <input readonly id="xct_dienthoai" type="text" value="Đang tải..." />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Mã tài khoản</label>
                        <br />
                        <input readonly id="xct_mauser" type="text" value="Đang tải..." />
                    </td>
                    <td>
                        <label>Chức vụ</label>
                        <br />
                        <div id="xct_vaitro">Đang tải...</div>
                    </td>
                    <td>
                        <label>Bộ phận</label>
                        <br />
                        <input readonly id="xct_phongban" type="text" value="Đang tải..." />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Mật khẩu cũ:</label>
                        <br />
                        <input id="txtMKcu" type="password" name="txtMKcu" autocomplete="off" readonly onfocus="this.removeAttribute('readonly');" />
                    </td>
                    <td>
                        <label>Mật khẩu mới:</label>
                        <br />
                        <input type="password" id="txtMKmoi" autocomplete="off" readonly onfocus="this.removeAttribute('readonly');" name="txtMKmoi" />
                    </td>
                    <td>
                        <label>Xác nhận mật khẩu:</label>
                        <br />
                        <input type="password" id="txtXNMK" name="txtXNMK" readonly onfocus="this.removeAttribute('readonly');" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <label>Thông tin khác</label>
                        <br />
                        <textarea id="xct_mota" style="height:100px" readonly role="textbox" multiline="true" class="FormElement ui-widget-content ui-corner-all"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="user-select:none" colspan="3">
                        <div style="display:flex; flex-direction:row-reverse; margin-top:10px;">
                            <div style="${styleDivCHK}min-width:122px">
                                <label for="vaitro_macdinh">Giữ chức vụ đã chọn</label>
                                <input style="${styleCHK}" id="vaitro_macdinh" type="checkbox" />
                            </div>
                            <div style="${styleDivCHK}min-width:120px;margin-right:15px;">
                                <label for="giaodien_macdinh">Menu và Header tối</label>
                                <input style="${styleCHK}" id="giaodien_macdinh" type="checkbox" />
                            </div>
                            <div style="width: 100%; margin-right: 15px;">
                                <span style="position:relative; top:-18px;">Đường viền của lưới</span><br/>
                                <select id="giaodien_luoi" style="position: relative;top: -13px;width: 113px">
                                    <option value="no-border">Không có</option>
                                    <option value="border-right">Bên phải</option>
                                    <option value="border-bottom">Bên dưới</option>
                                    <option value="border-all">Bao bọc</option>
                                </select>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    `);

    $("#dlg_gridSmall").dialog({
        modal: !0,
        width: window.innerWidth >= 550 ? 550 : window.innerWidth,
        maxWidth: window.innerWidth,
        position: {
            my: "right top-50",
            at: "right bottom",
            of: ".menu-taikhoan"
        },
        open: function () {
            $("#dlg_gridSmall").css({ 'padding': '0', 'overflow': 'auto' });
            Logo_Center("glyphicon glyphicon-user", "");
            $.getJSON("Controller/JQGridModify/JQGrid" + i + "Modify.ashx?oper=getthongtin", function (e) {
                $("#xct_vaitro").empty();
                $("#xct_hoten").val(e.hoten),
                    $("#xct_thudientu").val(e.email),
                    $("#xct_dienthoai").val(e.phone),
                    $("#xct_mauser").val(e.ma_user),
                    $("#xct_vaitro").prepend(e.vaitro),
                    $("#xct_phongban").val(e.ten_phongban),
                    n = e.md_phongban_id;
                const conf = JSON.parse(e.mauBackground);
                if (conf) {
                    $('#giaodien_macdinh').prop('checked', conf.mixmode);
                    if (conf.border)
                        $('#giaodien_luoi').val(conf.border);
                }
            })
                .fail(function (a, b, c, d, e) {
                    const laDangNhap = hienthitrangDangNhap(a.responseText);
                    if (laDangNhap) {
                        $('#btn-close').click();
                        return;
                    }
                });
            $("#xct_vaitro").change(function () {
                $("#xct_phongban").val("Đang tải..."), $.get("Controller/JQGridModify/JQGrid" + i + "Modify.ashx?oper=get_phongban&id=" + $("#xct_vaitro select").val(), function (n) {
                    $("#xct_phongban").val(n)
                })
            });
        },
        close: function () {
            $(this).dialog("destroy").remove()
        },
        buttons: [{
            id: "btn-ok",
            text: "Đồng ý",
            click: function () {
                $("#dlg_gridSmall").prepend('<div class="nhan_loading"></div>'),
                    $("#dlg_gridSmall .ui-state-error").remove(),
                    $("#dlg_gridSmall .nhan-thanhcong").remove();

                $.post("Controller/JQGridModify/JQGrid" + i + "Modify.ashx", {
                    matkhau: "",
                    hoten: $("#xct_hoten").val(),
                    diachi: $("#xct_diachi").val(),
                    phone: $("#xct_dienthoai").val(),
                    md_phongban_id: n,
                    mota: $("#xct_mota").val(),
                    fax: $("#xct_fax").val(),
                    vaitro_macdinh: document.getElementById("vaitro_macdinh").checked,
                    ad_role_id: $("#xct_vaitro select").val(),
                    oldpassword: $('#txtMKcu').val(),
                    newpassword: $('#txtMKmoi').val(),
                    confirm: $('#txtXNMK').val(),
                    giaodien_macdinh: $('#giaodien_macdinh').prop('checked'),
                    giaodien_luoi: $('#giaodien_luoi').val(),
                    oper: "save"
                }, function (n) {
                    const laDangNhap = hienthitrangDangNhap(n);
                    if (!laDangNhap) {
                        $('.nhan-loi').remove();
                        $('.nhan-thanhcong').remove();
                        n = n.split("(##)"), $("#dlg_gridSmall .nhan_loading").remove(), $("#dlg_gridSmall").prepend(n[2]), "true" == n[0] && (reload_page = !0, window.location.href = n[1])
                    }
                })
            }
        }, {
            id: "btn-close",
            text: "Thoát",
            click: function () {
                $(this).dialog("destroy").remove()
            }
        }]
    })
}
//--
function logout() {
    const style = `position: relative; top: 10px;`;
    $("#mainpane").append(`
        <div id="dlg_gridSmall" title="Thoát hệ thống">
            <span style="${style}">Bạn có chắc chắn muốn thoát hệ thống?</a>
        </div>`
    ),
        $("#dlg_gridSmall").dialog({
            modal: !0,
            position: {
                my: "right top-50",
                at: "right bottom",
                of: ".menu-taikhoan"
            },
            open: function () {
                Logo_Center("glyphicon glyphicon-log-out", !0)
            },
            close: function () {
                $(this).dialog("destroy").remove()
            },
            buttons: [
                {
                    id: "btn-ok",
                    text: "Đồng ý",
                    click: function () {
                        reload_page = !0,
                            enable_timer = !1,
                            window.location.href = "Controller/PublicFunction/LogOut.ashx"
                    }
                },
                {
                    id: "btn-close",
                    text: "Thoát",
                    click: function () {
                        $(this).dialog("destroy").remove()
                    }
                }]
        })
}
function thaydoinoidungxoa(n, i) {
    $("#DelTbl_" + n + " table tr > .delmsg").empty(), $("#DelTbl_" + n + " table tr > .delmsg").append(i)
}
//function download
function modify_code(url) { reload_page = "no"; window.location.href = url; setTimeout(function () { reload_page = null }, 1000) }

var winconfigGV = null;
function click_configGV_link(tengrid) {
    let ma_module = tengrid.replace('grid', '');
    let sord = $('#' + tengrid).jqGrid('getGridParam', 'postData').sord;
    let sidx = $('#' + tengrid).jqGrid('getGridParam', 'postData').sidx;
    let filters = $('#' + tengrid).jqGrid('getGridParam', 'postData').filters;
    let id_parent = $('#' + tengrid).jqGrid('getGridParam', 'postData').id;
    if (filters) {
        filters = filters.replace(/'/g, "\\'");
    }
    else {
        filters = "";
    }
    if (!sord) {
        sord = "";
    }
    if (!sidx) {
        sidx = "";
    }
    filters = filters.replace(/&/g, '%26');
    let link_export = 'View/Print/MD_01_Export/XuatDSTK.aspx?ma_module=' + ma_module + '&sord=' + sord + '&sidx=' + sidx + '&filters=' + filters + '&_search=true&id=' + id_parent;

    try { winconfigGV.close(); } catch (r) { console.error(r); }
    winconfigGV = window.open(link_export, 'Chiết Xuất Dữ Liệu', 'width = 500px, height = 500px');
}

function click_configGV(tengrid) {
    var ma_menu = 'mainbody', ma_module = tengrid.replace('grid', '');
    var id_parent = $('#' + tengrid).jqGrid('getGridParam', 'postData').id;
    var dialog = 'dlg_gridSmall' + ma_menu;
    $('body').append(`<div class="${dialog}" align="center" title="Chi tiết dữ liệu đang xem">
	<table class="EditTable" style="width:100%">
		<tr style="height: 40px;">
			<td>
				Tất cả dữ liệu: <b class="ctdldx_dlhc">Đang tải...</b>
			</td>
		</tr>

		<tr style="height: 40px;">
			<td>
				Dữ liệu đang hiển thị: <b class="ctdldx_dldht">Đang tải...</b>
			</td>
		</tr>

		<tr style="height: 40px;">
			<td>
				<a onclick="click_configGV_link('${tengrid}')" class="btn-escedoc">Xuất dữ liệu</a>
			</td>
		</tr>
	</table>
	</div>`);
    dialog = '.' + dialog;
    $(dialog).dialog({
        modal: !0,
        width: 300,
        open: function () {
            $.get('Controller/PublicFunction/ConfigSystem.ashx', { ma_module: ma_module, oper: 'count_module2', id: id_parent }, function (result) {
                $(dialog).find('.ctdldx_dlhc').html(result + ' dòng.');
                var dht = 0;
                $('#' + tengrid).find('tr.ui-widget-content.jqgrow.ui-row-ltr').each(function () {
                    if ($(this).attr('class').indexOf('esc_vnn_') > -1 &
                        $(this).attr('class').indexOf('esc_vnn_vnnnodel_esc') <= -1) {
                        dht++;
                    }
                });
                if (result <= 0)
                    dht = 0;
                $(dialog).find('.ctdldx_dldht').html(dht + ' dòng.');
            });
            Logo_Center("glyphicon glyphicon-cog", true);
        },
        close: function () {
            $(this).dialog("destroy").remove();
        },
        buttons: [{
            id: "btn-close",
            text: "Thoát",
            click: function () {
                $(this).dialog("destroy").remove();
            }
        }]
    })
}

var findnamegrid_basecodegrid = function (a, b) {
    var a_cp = a.replace('grid', '').substring(0, 5);
    var name = "";
    if (a_cp == 'MD_00') {
        name = $('.nav1_li_a_select').text();
    }
    else if (a_cp == 'MD_01') {
        name = $('#jqxTabs' + b + ' .jqx-tabs-title-selected-top.jqx-fill-state-pressed').attr('ten_mod');

    }
    else if (a_cp == 'MD_02') {
        name = $('#jqxTabs' + b + '_capdo1 .jqx-tabs-title-selected-top.jqx-fill-state-pressed').attr('ten_mod');
    }
    return name;
}
var search_jqgrid = function (grid) {
    var ma_menu = $('.nav1_li_a_select').attr('ma_menu');
    var dialog = 'dlg_gridSmall' + grid;

    $('.content-menu-' + ma_menu).append('<div class="' + dialog +
        `" align="center" title="Tìm kiếm ` + findnamegrid_basecodegrid(grid, ma_menu) + `">
	<table class="EditTable" style="width:100%">
	</table>
	</div>`);
    dialog = '.' + dialog;
    $(dialog).dialog({
        modal: !0,
        width: 450,
        height: 550,
        open: function () {
            var tbl = dialog + ' .EditTable';
            var i = 2;
            $('.content-menu-' + ma_menu).append($(this).parent().prev());
            $('.content-menu-' + ma_menu).append($(this).parent());
            var th_header = '.tbl_vnn_' + grid + ' .ui-jqgrid-labels > .ui-state-default.ui-th-column.ui-th-ltr';
            var input_str = '.tbl_vnn_' + grid + ' .ui-search-table .ui-search-input > input';
            var select_str = '.tbl_vnn_' + grid + ' .ui-search-table .ui-search-input > select';
            $(input_str + ',' + select_str).each(function () {
                var th_sr = $(this).parent().parent().parent().parent().parent().parent().attr('style');
                var ok = false;
                if (th_sr == null) {
                    ok = true;
                }
                else if (th_sr.indexOf('display') <= -1) {
                    ok = true;
                }
                if (ok == true) {
                    var kq = '';
                    kq += '<tr class="FormData">';
                    kq += '<td style="padding-bottom: 10px;" class="DataTD">';
                    kq += '<div style="color: #6d41ec;">' + $(th_header + ':nth-child(' + i + ')').text() + '</div>';
                    var id_el = $(this).attr('id') + grid;
                    if ($(this).prop("tagName") == 'INPUT') {
                        kq += '<input id="' + id_el + '" class="FormElement ui-widget-content ui-corner-all" />';
                    }
                    else {
                        kq += '<select id="' + id_el + '" class="FormElement ui-widget-content ui-corner-all">';
                        kq += $(this).html();
                        kq += '</select>';
                    }
                    kq += '</tr>';
                    kq += '</td>';
                    $(tbl).append(kq);
                    var elem_new = $('#' + id_el);
                    elem_new.val($(this).val());
                    if ($(this).hasClass('date')) {
                        search_datetime(elem_new, 1);
                    }
                    else if ($(this).hasClass('number')) {
                        search_number(elem_new, 1, 1);
                    }
                    else if ($(this).prop("tagName") == 'SELECT') {
                        elem_new.chosen(vnn_sel_config['default']);
                    }
                }
                i++;
            });

            Logo_Center("glyphicon glyphicon-search", "", dialog);
        },
        close: function () {
            $(this).dialog("destroy").remove();
        },
        buttons: [{
            id: "btn-ok" + grid,
            text: "Đồng ý",
            click: function () {
                var tbl = dialog + ' .EditTable';
                var input_str = '.tbl_vnn_' + grid + ' .ui-search-table .ui-search-input > input';
                var select_str = '.tbl_vnn_' + grid + ' .ui-search-table .ui-search-input > select';
                $(input_str + ',' + select_str).each(function () {
                    var val_new = $('#' + $(this).attr('id') + grid).val();
                    $(this).val(val_new);
                    chosen_update($(this));
                });
                $('#' + grid)[0].triggerToolbar();
                $(this).dialog("destroy").remove();
            }
        }, {
            id: "btn-close" + grid,
            text: "Thoát",
            click: function () {
                $(this).dialog("destroy").remove();
            }
        }]
    })
}

var set_height_tabs = function (elem, grid) {
    if (ismobile == true) {
        try {
            var height = height_menu_auto(1) - 13;
            elem.jqxTabs({ height: height });
            grid.setGridHeight(height - 92);
            grid.setGridWidth($('.page-content').children().first().width());
            $('.tbl_vnn_' + grid.attr('id') + ' .ui-search-toolbar .ui-search-table .ui-search-input select').each(function () {
                $(this).next().css('width', $(this).parent().width());
            });
        }
        catch (r) {
            console.log(r);
        }
    }
};

var excelToJSON = {
    json: [],
    parseExcel: function (file, callback) {
        excelToJSON.json = [];
        var reader = new FileReader();

        reader.onload = function (e) {
            var data = e.target.result;
            var workbook = XLSX.read(data, {
                type: 'binary'
            });
            let jsonLc = [];
            workbook.SheetNames.forEach(function (sheetName) {
                // Here is your object
                let XL_row_object = XLSX.utils.sheet_to_json(workbook.Sheets[sheetName], { defval: "" });
                jsonLc.push(XL_row_object);
            });
            excelToJSON.json = jsonLc;
            callback();
        };

        reader.onerror = function (ex) {
            console.log(ex);
        };

        reader.readAsBinaryString(file);
    }
};