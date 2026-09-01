<%@ Page Language="C#" %>

<%
    string ma_module = "MD_02_CAS";


    string[] get_records = VNN_Config.get_records();
    string[] get_STTaID = VNN_Config.get_IDParent_STTLoad2(ma_module, 2);
    string[] colModel = VNN_Config.get_colModel(Context, ma_module);
    string[][] modifyForm = VNN_Config.get_ModifyFormInfor2(ma_module, 2);
%>


<table id='grid<%=ma_module %>'></table>
<div id='pagergrid<%=ma_module %>'></div>


<script type="text/javascript">
    //bien load grid
    var input_focus2 = null, load_grid2 = 0;
    var rownum2 = Number(<%=get_records[0] %>);
    //Cac bien can truyen vao truoc khi load chuc nang
    //--bien bat buoc
    var load_stt2 = <%=get_STTaID[0] %>;
    var id_parent2 = <%=get_STTaID[1] %>;
    var tengrid2 = 'grid<%=ma_module %>';
    var Form_infor2 = '<%=VNN_VariablePublic.Form_infor %>';
    var Model_infor2 = '<%=VNN_VariablePublic.Model_infor %>';
    //--#bien bat buoc
    //#Cac bien can truyen vao truoc khi load chuc nang
    //Load chức năng
    <%=VNN_Config.get_NavFunc2(Context, ma_module, 2) %>
    //#Load chức năng
</script>


<script type="text/javascript">
<%=VNN_JQGridver2.get_layout_face() %>
    jQuery('#' + tengrid2).jqGrid({
        url: 'Controller/JqGrid/JQGrid<%=ma_module%>Load.ashx?ma_module=<%=ma_module%>&ma_menu=' + $('#input_idmenu').val(),
        editurl: 'Controller/JQGridModify/JQGrid<%=ma_module%>Modify.ashx?ma_module=<%=ma_module%>&ma_menu=' + $('#input_idmenu').val(),
        height: getHeightGrid(2),
        datatype: 'json',
        autowidth: true,
        shrinkToFit: true,
        rownumbers: true,
        viewrecords: true,
        search: true,
        scroll: false,
        rowNum: rownum2,
        multiselect: <%=get_STTaID[3] %>,
        multiboxonly: <%=get_STTaID[3] %>,
        rowList: <%=get_records[1] %>,
        pager: '#pager' + tengrid2,
        onSelectRow: function (ids) {
            //checkbox customize
            checkbox_JQgrid(tengrid2, 0);
            var value_header = '', cell = $('#' + tengrid2).getRowData(ids);
            if (id_parent2 != null) {
                value_header = <%=get_STTaID[1].Replace("id_","header_") %>;
                header_<%=ma_module %> = value_header + header_sep + <%=get_STTaID[2] %>;
            }
            else {
                header_<%=ma_module %> = value_header + <%=get_STTaID[2] %>;
            }
            set_headerJQG(tengrid2, header_<%=ma_module %>);
            // public id da chon
            if (ids != null & ids != '' & ids != '0') {
                id_<%=ma_module %> = ids;
                module_select[2] = 1;
            }
        },
        colModel: [
            <%=colModel[0] %>
        ],
        loadBeforeSend: function (xhr) {
            try { jqgridXHR[tengrid2].abort(); } catch { }
            jqgridXHR[tengrid2] = xhr;
        },
        beforeRequest: function () {
            //giữ focus
            input_focus2 = $('input:focus').attr('class');
            //giữ filter start
            if (id_oper[load_stt2] == null)
                $('#' + tengrid2).jqGrid('getGridParam', 'postData').id_sel = id_<%=ma_module %>;
            $('#' + tengrid2).jqGrid('getGridParam', 'postData').module_select = module_select[1];
            set_Filter(load_grid2, tengrid2, '<%=ma_module %>');
            $('#' + tengrid2).jqGrid('getGridParam', 'postData').id = id_parent2
        },
        ondblClickRow: function () {
            <%=get_STTaID[4] %>;
        },
        gridComplete: function () {
            $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width());
            if (load_grid2 == 0) { taonut_header2(tengrid2) };
        },
        loadComplete: function (data) {
            //chia màu chẵn lẻ
            var top_rowid = $('#' + tengrid2 + ' tr:nth-child(2)').attr('id');
            phanmauchogrid(this);
            if (id_<%=ma_module %> != null & id_<%=ma_module %> != '0')
                $('#' + tengrid2).jqGrid('setSelection', id_<%=ma_module %>);
            module_select[2] = 1;
            var b = $('#' + tengrid2).jqGrid('getGridParam', 'selrow');
            if (b == null | b == '' | b == '0') {
                if (top_rowid != null & top_rowid != 0) {
                    $('#' + tengrid2).jqGrid('setSelection', top_rowid);
                } else {
                    module_select[2] = 2;
                }
            }
            else {
                if (id_new == '0' | id_new == null) { }
                else { resetSelection(tengrid2); clickSelection(tengrid2, id_new); id_new = '0'; }
                Focus_Selection(tengrid2);
            }
            //làm mới bộ lọc
            clearSearchOptions(tengrid2);
            //giữ filter end
            if (load_grid2 == 0) {
                fix_disableSelect(tengrid2);
                set_ValueFilter(tengrid2, filterVal_<%=ma_module %>, sord_<%=ma_module %>, sidx_<%=ma_module %>);
                load_grid2 = 1;
            }
            else {
                filterVal_<%=ma_module %> = get_ValueFilter(tengrid2);
            }
            //checkbox customize
            checkbox_JQgrid(tengrid2, 1);
            //giữ focus end
            $('.' + input_focus2).focus();
        },
        caption: ''
    });


    jQuery('#' + tengrid2).jqGrid('navGrid', '#pager' + tengrid2,
        {
        <%=VNN_Config.get_navGrid(Context, ma_module) %>
        }, {
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
            Change_Value2(Avariable2(), 'edit');
        },
        beforeSubmit: function (postdata, formid) {
            postdata.id_parent2 = id_parent2;
            formid.prepend('<div class="nhan_loading">&nbsp;</div>');
            return [true, ''];
        },
        afterSubmit: function (response, formid) {
            $('.nhan_loading').remove();
            if (thongbaokhimodify(response.responseText, this.id) == false) {
                return [false, response.responseText.split('#')[1]];
            }
            else {
                <%=colModel[5] %>
                <%=modifyForm[0][4] %>
                add_edit_del_complete2('edit'); loadclick(tengrid2, 'edit', load_stt2);
                return [false, ''];
            }
        }
    }, {
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
            Change_Value2(Avariable2(), 'add');
            countRows(tengrid2, 'sapxep');
        },
        beforeSubmit: function (postdata, formid) {
            postdata.id_parent2 = id_parent2;
            formid.prepend('<div class="nhan_loading">&nbsp;</div>');
            return [true, ''];
        },
        afterSubmit: function (response) {
            $('.nhan_loading').remove();
            if (thongbaokhimodify(response.responseText, this.id) == false) {
                return [false, response.responseText.split('#')[1]];
            }
            else {
                <%=colModel[4] %>
                <%=colModel[5] %>
                <%=modifyForm[1][4] %>
                id_new = response.responseText.split('#')[2]; countRows(tengrid2, 'sapxep');
                add_edit_del_complete2('add'); loadclick(tengrid2, null, load_stt2);
                return [false, ''];
            }
        }
    }, {
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
            postdata.id_parent = id_parent2;
            $('#DelTbl_' + tengrid2).prepend('<div class="nhan_loading">&nbsp;</div>');
            return [true, ''];
        },
        afterSubmit: function (response) {
            $('.nhan_loading').remove();
            if (thongbaokhimodify(response.responseText, this.id) == false) {
                return [false, response.responseText.split('#')[1]];
            }
            else {
                <%=modifyForm[2][4] %>
                add_edit_del_complete2('del'); loadclick(tengrid2, null, load_stt2);
                return [false, ''];
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
    }, {
        //refesh
    });
    jQuery('#' + tengrid2).jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
    jQuery('#pager' + tengrid2 + '_left table').css('display', 'none');
    jQuery('#' + tengrid2).jqGrid('setFrozenColumns');

    //Start Ham ho tro them cho Grid (sẽ tự update nếu module chính update)
    function Avariable2() { //khai bien o ham nay
        var column_array = ['thuake'];
        return column_array;
    }
    //--
    function Change_Value2(column_array, action) {
        for (var i in column_array) {
            $('#' + column_array[i]).change(function () {
                action_grid2(action, $(this).attr('id'));
            });
        }
        action_grid2(action, null);
    }
    //--
    function ma_case_focusout() {
        $('#hamxuly').val($('#ma_case').val() + '(tengrid, id_parent, ma_case, Form_infor, Model_infor, load_stt)');
    }
    function action_grid2(action, column) {
        var ma_case = document.getElementById("ma_case");
        ma_case.removeEventListener("focusout", ma_case_focusout);
        if (action == 'add') {
            ma_case.addEventListener("focusout", ma_case_focusout);
        }

        if (action == 'edit') {
            if ($('#thuake').val() != '' & $('#thuake').val() != null) {
                khoa_field(tengrid2);
                mokhoa_column('thuake');
                mokhoa_column('sapxep');
                mokhoa_column('mota');
                mokhoa_column('updatehd');
                mokhoa_column('hoatdong');
                mokhoa_column('id_parent');
            }
            else {
                mokhoa_field(tengrid2);
                khoa_column('ma_case');
            }

            if (module_thuake == 1) {
                khoa_field(tengrid2);
                mokhoa_column('hoatdong');
            }
        }
        else {
            if ($('#thuake').val() != '' & $('#thuake').val() != null) {
                khoa_field(tengrid2);
                mokhoa_column('ma_case');
                mokhoa_column('thuake');
                mokhoa_column('sapxep');
                mokhoa_column('mota');
                mokhoa_column('id_parent');
            }
            else {
                mokhoa_field(tengrid2);
                khoa_column('updatehd');
                khoa_column('hoatdong');
            }

            if (module_thuake == 1) {
                khoa_field(tengrid2);
            }
        }
    }
    //--
    function add_edit_del_complete2(action) {
        if (action = 'add') {
        }
        else if (action = 'edit') {
        }
        else if (action = 'del') {
        }
    }
    //#End Ham ho tro them cho Grid (sẽ tự update nếu module chính update)
</script>
