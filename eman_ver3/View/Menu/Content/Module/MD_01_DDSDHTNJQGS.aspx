<%@ Page Language="C#" %>
<%
    string ma_module = "MD_01_DDSDHTNJQGS";
    

    string[] get_records = VNN_Config.get_records();
    string[] get_STTaID = VNN_Config.get_IDParent_STTLoad2(ma_module, 1);
    string[] colModel = VNN_Config.get_colModel(Context, ma_module);
    string[][] modifyForm = VNN_Config.get_ModifyFormInfor2(ma_module, 1);
%>
<style type="text/css">
    .ui-jqgrid tr.footrow-ltr td {
        border-right-width: 0px;
        border-bottom: 0px;
        border-top: 0px;
    }

    .ui-jqgrid .ui-jqgrid-ftable {
        width: 100% !important;
    }

    .ui-jqgrid-sdiv .ui-jqgrid-hbox {
        padding-right: 0px;
    }

    .md01-footer-mobile,
    .md01-footer-search-mobile {
        display: none;
    }

    @media (max-width: 600px) {
        .md01-footer-desktop,
        .md01-footer-search-desktop {
            display: none !important;
        }

        .md01-footer-wrap {
            align-items: center !important;
            padding: 5px 3px 3px !important;
            gap: 6px;
        }

        .md01-footer-search-mobile {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            width: 30px;
            height: 30px;
            padding: 0;
            flex: 0 0 30px;
            border: 1px solid #aaa;
            border-radius: 2px;
            background: #fff;
            color: #333;
            cursor: pointer;
            box-sizing: border-box;
        }

        .md01-footer-search-mobile i {
            font-size: 14px;
        }

        .md01-footer-mobile {
            display: grid;
            grid-template-columns: 62px minmax(72px, 1fr) 90px minmax(62px, .9fr);
            column-gap: 6px;
            row-gap: 6px;
            align-items: center;
            width: 100%;
            min-width: 0;
            margin-left: auto;
            box-sizing: border-box;
            font-variant-numeric: tabular-nums;
        }

        .md01-footer-mobile-label {
            color: #666;
            text-align: right;
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .md01-footer-mobile-value {
            min-width: 0;
            text-align: right;
            font-weight: 600;
            white-space: nowrap;
        }

        .md01-footer-mobile-value.is-total {
            font-weight: 700;
        }

        .md01-footer-mobile-value.is-payable {
            color: #d9534f;
            font-weight: 700;
        }
    }
</style>

<table id='grid<%=ma_module %>'></table>
<div id='pagergrid<%=ma_module %>'></div>


<script type="text/javascript">
    //bien load grid
    var input_focus1=null, load_grid1 = 0;
    var rownum1 = Number(<%=get_records[0] %>);
    //Cac bien can truyen vao truoc khi load chuc nang
    //--bien bat buoc
    var load_stt1 = <%=get_STTaID[0] %>;
    var id_parent1 = <%=get_STTaID[1] %>;
    var tengrid1 = 'grid<%=ma_module %>';
    var Form_infor1 = '<%=VNN_VariablePublic.Form_infor %>';
    var Model_infor1 = '<%=VNN_VariablePublic.Model_infor %>' ;
    //--#bien bat buoc
    //#Cac bien can truyen vao truoc khi load chuc nang
    //Load chức năng
    <%=VNN_Config.get_NavFunc2(Context, ma_module, 1) %>
    //#Load chức năng
</script>


<script type="text/javascript">
<%=VNN_JQGridver2.get_layout_face() %>

    function isMobileFooter1() {
        return window.matchMedia && window.matchMedia('(max-width: 600px)').matches;
    }

    function resizeFooterGrid1() {
        if (!$grid1 || !$grid1.length) return;

        let footerHeight = isMobileFooter1() ? 82 : 75;
        $grid1[0].hFooter = footerHeight;
        $grid1.jqGrid('setGridHeight', Math.max(80, getHeightGrid(1) - footerHeight));
    }

    var createFooter1 = function (elem) {
        let $grid = $(elem);
        const $gridPR = $('#' + tengrid0);
        const $rowPR = $gridPR.getRowData(id_parent1);
        let tongTien = $rowPR.tongtienhang_kov;
        let giamGia = $rowPR.giamgia_kov;
        let phuThu = $rowPR.phuthu;
        let tongCuoi = $rowPR.khachcantra_kov;
        let daThanhToan = $rowPR.dathanhtoan;
        let conNo = $rowPR.conno;
        let $footerTable = $grid.closest('.ui-jqgrid-bdiv').next('.ui-jqgrid-sdiv').find('.ui-jqgrid-ftable');
        let $firstRow = $footerTable.find('tr.footrow');

        $footerTable.find('.custom-footer-row').remove();
        const $cols = $firstRow.find(`td[aria-describedby!="${$grid.attr('id')}_thanhtien"]`);
        $cols.hide();
        const $col = $firstRow.find(`td[aria-describedby="${$grid.attr('id')}_thanhtien"]`);
        $col.attr('colspan', $cols.length).css('text-align', 'right');
        $col.html(`
            <div class="md01-footer-wrap" style="display:flex; align-items:flex-start; justify-content:space-between; width:100%; padding:8px 4px 4px; box-sizing:border-box; font-family:inherit; color:#000;">
                <div class="md01-footer-search-desktop" style="padding-top:3px; flex:0 0 auto;">
                    <input type="button" id="btn_donhanglq" onclick="open_DanhSachHangHoaLienQuan()" value="Tìm kiếm nâng cao" />
                </div>

                <button type="button" class="md01-footer-search-mobile" title="Tìm kiếm nâng cao" aria-label="Tìm kiếm nâng cao" onclick="open_DanhSachHangHoaLienQuan()">
                    <i class="fa fa-search-plus" aria-hidden="true"></i>
                </button>

                <div class="md01-footer-desktop" style="display:grid; grid-template-columns:105px 125px 115px 125px; column-gap:18px; row-gap:8px; align-items:center; margin-left:auto; font-variant-numeric:tabular-nums;">
                    <div style="text-align:right; color:#555; white-space:nowrap;">Tổng tiền:</div>
                    <div style="text-align:right; font-weight:600; white-space:nowrap;">${tongTien}</div>
                    <div style="text-align:right; color:#555; white-space:nowrap;">Giảm giá:</div>
                    <div style="text-align:right; font-weight:600; white-space:nowrap;">${giamGia}</div>

                    <div style="text-align:right; color:#555; white-space:nowrap;">Phụ thu:</div>
                    <div style="text-align:right; font-weight:600; white-space:nowrap;">${phuThu}</div>
                    <div style="text-align:right; color:#555; white-space:nowrap;">Khách cần trả:</div>
                    <div style="text-align:right; font-weight:700; color:#d9534f; font-size:1.08em; white-space:nowrap;">${tongCuoi}</div>

                    <div style="text-align:right; color:#555; white-space:nowrap;">Đã trả:</div>
                    <div style="text-align:right; font-weight:600; white-space:nowrap;">${daThanhToan}</div>
                    <div style="text-align:right; color:#555; white-space:nowrap;">Còn lại:</div>
                    <div style="text-align:right; font-weight:700; white-space:nowrap;">${conNo}</div>
                </div>

                <div class="md01-footer-mobile">
                    <span class="md01-footer-mobile-label">Tổng tiền:</span>
                    <strong class="md01-footer-mobile-value is-total">${tongTien}</strong>
                    <span class="md01-footer-mobile-label">Giảm giá:</span>
                    <strong class="md01-footer-mobile-value">${giamGia}</strong>

                    <span class="md01-footer-mobile-label">Phụ thu:</span>
                    <strong class="md01-footer-mobile-value">${phuThu}</strong>
                    <span class="md01-footer-mobile-label">Khách cần trả:</span>
                    <strong class="md01-footer-mobile-value is-payable">${tongCuoi}</strong>

                    <span class="md01-footer-mobile-label">Đã trả:</span>
                    <strong class="md01-footer-mobile-value">${daThanhToan}</strong>
                    <span class="md01-footer-mobile-label">Còn lại:</span>
                    <strong class="md01-footer-mobile-value is-total">${conNo}</strong>
                </div>
            </div>
        `);

        resizeFooterGrid1();
    }

    let $grid1 = $(`#${tengrid1}`);
    $grid1[0].hFooter = 75;
    $grid1.jqGrid({
        url: 'Controller/JqGrid/JQGrid<%=ma_module%>Load.ashx?ma_module=<%=ma_module%>&ma_menu='+$('#input_idmenu').val(),
        editurl: 'Controller/JQGridModify/JQGrid<%=ma_module%>Modify.ashx?ma_module=<%=ma_module%>&ma_menu='+$('#input_idmenu').val(),
        height: getHeightGrid(1) - $grid1[0].hFooter,
        datatype: 'json',
        autowidth: true,
        shrinkToFit: true,
        rownumbers: true,
        viewrecords: true,
        search: true,
        scroll: false,
        rowNum: rownum1,
        multiselect: <%=get_STTaID[3] %>,
        multiboxonly: <%=get_STTaID[3] %>,
        rowList: <%=get_records[1] %>,
        pager: '#pager' + tengrid1,
        footerrow: true,
        onSelectRow: function (ids) {
			 //checkbox customize
			 checkbox_JQgrid(tengrid1, 0);
            var value_header = '', cell = $grid1.getRowData(ids);
            if (id_parent1 != null) { 
                    value_header = <%=get_STTaID[1].Replace("id_","header_") %>;
                    header_<%=ma_module %> = value_header + header_sep + <%=get_STTaID[2] %>;
            }
            else {
                header_<%=ma_module %> = value_header + <%=get_STTaID[2] %>;
            }
            set_headerJQG(tengrid1, header_<%=ma_module %>);
            // public id da chon
            if(ids != null & ids !='' & ids !='0'){
            	 id_<%=ma_module %> = ids;
                module_select[1] = 1;
            }
            load_detail(load_grid1, 2, 1);
        },
        colModel: [
            <%=colModel[0] %>
        ],
        loadBeforeSend: function (xhr) {
            jqgridXHR[tengrid1] = xhr;
        },
        beforeRequest: function() {
            //giữ focus
            input_focus1 = $('input:focus');
            //giữ filter start
            if (id_oper[load_stt1] == null)
                $grid1.jqGrid('getGridParam', 'postData').id_sel = id_<%=ma_module %>;
            $grid1.jqGrid('getGridParam', 'postData').module_select = module_select[0];
            set_Filter(load_grid1, tengrid1, '<%=ma_module %>');
            $grid1.jqGrid('getGridParam', 'postData').id = id_parent1
        },
        ondblClickRow: function(){
            <%=get_STTaID[4] %>;
        },
        gridComplete: function () {
            $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width());
            if(load_grid1 == 0){taonut_header1(tengrid1)};
        },
        loadComplete: function (data) {
            //chia màu chẵn lẻ
            var top_rowid = $grid1.find('tr:nth-child(2)').attr('id');
            phanmauchogrid(this);
            if(id_<%=ma_module %> != null & id_<%=ma_module %> != '0')
                $grid1.jqGrid('setSelection', id_<%=ma_module %>);
            module_select[1] = 1;
            var b = $grid1.jqGrid ('getGridParam', 'selrow');
            if (b == null | b == '' | b=='0') {
                if(top_rowid != null & top_rowid != 0) {
                    $grid1.jqGrid('setSelection', top_rowid);
                } else {
                    load_detail(load_grid1, 2, 1);
                    module_select[1] = 2;
                }
            }
            else {
                if(id_new == '0' | id_new == null)
                { }
                else
                { $grid1.jqGrid('setSelection', id_new); id_new = '0'; }
                Focus_Selection(tengrid1);
            }
            //làm mới bộ lọc
			dem_stt();
            clearSearchOptions(tengrid1);
            //giữ filter end
            if (load_grid1 == 0)
            {
                fix_disableSelect(tengrid1);
                set_ValueFilter(tengrid1, filterVal_<%=ma_module %>, sord_<%=ma_module %>, sidx_<%=ma_module %>);
                load_grid1 = 1;
            }
            else
            {
                filterVal_<%=ma_module %> = get_ValueFilter(tengrid1);
            }
			 //checkbox customize
			 checkbox_JQgrid(tengrid1, 1);
            //giữ focus end
            input_focus1.focus();
            createFooter1(this);
        },
        caption: ''
    });


    $grid1.jqGrid('navGrid', '#pager'+tengrid1,
    {
        <%=VNN_Config.get_navGrid(Context, ma_module) %>
    },{
    //edit
        beforeShowForm: function (formid) {
            <%=modifyForm[0][0] %>
            <%=modifyForm[0][1] %>
            <%=modifyForm[0][2] %>
            <%=modifyForm[0][3] %>
        },
        afterShowForm: function (formid) {
            <%=colModel[1]%>
            <%=colModel[5] %>
            <%=colModel[6] %>
            Change_Value1(Avariable1(),'edit');
        },
        beforeSubmit: function (postdata, formid) {
            postdata.id_parent = id_parent1;
            formid.prepend('<div class="nhan_loading">&nbsp;</div>');
            return [true, ''];
        },
        afterSubmit: function (response, formid) {
            $('.nhan_loading').remove();
            if(thongbaokhimodify(response.responseText,this.id) == false)
            {
                return [false, response.responseText.split('#')[1]];
            }
            else
            {
                <%=colModel[5] %>
                <%=modifyForm[0][4] %>
                add_edit_del_complete1('edit'); loadclick(tengrid1,'edit',load_stt1);
                return [false, ''];
            }
        }
    },{
    //add
        beforeShowForm: function (formid) {
            <%=modifyForm[1][0] %>
            <%=modifyForm[1][1] %>
            <%=modifyForm[1][2] %>
            <%=modifyForm[1][3] %>
        },
        afterShowForm: function (formid) {
            <%=colModel[2]%>
            <%=colModel[3] %>
            <%=colModel[5] %>
            <%=colModel[7] %>
            Change_Value1(Avariable1(),'add');
            countRows(tengrid1, 'sapxep');
        },
        beforeSubmit: function (postdata, formid) {
            postdata.id_parent = id_parent1;
            formid.prepend('<div class="nhan_loading">&nbsp;</div>');
            return [true, ''];
        },
        afterSubmit: function (response) {
            $('.nhan_loading').remove();
            if(thongbaokhimodify(response.responseText,this.id) == false)
            {
                return [false, response.responseText.split('#')[1]];
            }
            else
            {
                <%=colModel[4] %>
                <%=colModel[5] %>
                <%=modifyForm[1][4] %>
                id_new = response.responseText.split('#')[2]; countRows(tengrid1, 'sapxep');
                add_edit_del_complete1('add'); loadclick(tengrid1,null,load_stt1);
                return [false, ''];
            }
        }
    },{
    //del
        beforeShowForm: function (formid) {
            <%=modifyForm[2][0] %>
            <%=modifyForm[2][1] %>
            <%=modifyForm[2][2] %>
            <%=modifyForm[2][3] %>
        },
        afterShowForm: function (formid) {
        },
        beforeSubmit: function (postdata, formid) {
            postdata.id_parent = id_parent1;
            $('#DelTbl_'+tengrid1).prepend('<div class="nhan_loading">&nbsp;</div>');
            return [true, ''];
        },
        afterSubmit: function (response) {
            $('.nhan_loading').remove();
            if(thongbaokhimodify(response.responseText,this.id) == false)
            {
                return [false, response.responseText.split('#')[1]];
            }
            else
            {
                <%=modifyForm[2][4] %>
                add_edit_del_complete1('del'); loadclick(tengrid1, null,load_stt1);
                return [false,''];
            }
        }
    }, {
    //search
        beforeShowForm: function (formid) {
            formid.closest('div.ui-jqdialog').dialogCenter();
        }
    }, {
    //view
        beforeShowForm: function (formid) {
            <%=modifyForm[3][0] %>
            <%=modifyForm[3][1] %>
            <%=modifyForm[3][2] %>
            <%=modifyForm[3][3] %>
        },
        afterShowForm: function (formid) {
        },
    },{
    //refesh
    });
    $grid1.jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
    jQuery('#pager'+ tengrid1 +'_left table').css('display','none');
    $grid1.jqGrid('setFrozenColumns');
    

    //Start Ham ho tro them cho Grid (sẽ tự update nếu module chính update)
    function Avariable1() {
        var column_array = [];
        return column_array;
    }
    //--
    function Change_Value1(column_array, action)
    {
        for(var i in column_array) {
            $('#' + column_array[i]).change(function() {
                 action_grid1(action, $(this).attr('id'));
            });
        }
        action_grid1(action, null);
    }
    //--
    function action_grid1(action, column) {
        let ngaychuyen = $('#' + tengrid0).jqGrid('getCell', id_MD_00_DSDHTCJQGS, 'hangiaohang_po');
        if(action == 'add') {
            dem_stt();
            $('#han_giaohang').val(ngaychuyen);
        }
        else if(action == 'edit') {
        
        }


    }
    //--
    function add_edit_del_complete1(action) {
        if(action = 'add') {
        }
        else if(action = 'edit') {
        }
        else if(action = 'del') {
        }
    }
	//--
	function dem_stt() {
		try {
            var top_rowid = $grid1.find('tr:nth-child(2)').attr('id');
            var count = $grid1.getGridParam('records'), stt = 10;
			if(top_rowid != null & top_rowid != 0) {
				stt = (1 + count) * 10
			}
			$('#sothutu').val(stt);
		}
		catch(r) {
		}
	}
    //#End Ham ho tro them cho Grid (sẽ tự update nếu module chính update)
</script>