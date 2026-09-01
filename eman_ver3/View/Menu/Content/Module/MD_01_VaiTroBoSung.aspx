<%@ Page Language="C#" %>
<%
    string ma_module = "MD_01_VaiTroBoSung";
    

    string[] get_records = VNN_Config.get_records();
    string[] get_STTaID = VNN_Config.get_IDParent_STTLoad2(ma_module, 1);
    string[] colModel = VNN_Config.get_colModel(Context, ma_module);
    string[][] modifyForm = VNN_Config.get_ModifyFormInfor2(ma_module, 1);
%>


<table id='grid<%=ma_module %>'></table>
<div id='pagergrid<%=ma_module %>'></div>


<script type="text/javascript">
    //bien load grid
    var input_focus1 = null, load_grid1 = 0;
    var rownum1 = Number(<%=get_records[0] %>);
    //Cac bien can truyen vao truoc khi load chuc nang
    //--bien bat buoc
    var load_stt1 = <%=get_STTaID[0] %>;
    var id_parent1 = <%=get_STTaID[1] %>;
    var tengrid1 = 'grid<%=ma_module %>';
    var Form_infor1 = '<%=VNN_VariablePublic.Form_infor %>';
    var Model_infor1 = '<%=VNN_VariablePublic.Model_infor %>';
    //--#bien bat buoc
    //#Cac bien can truyen vao truoc khi load chuc nang
    //Load chức năng
    <%=VNN_Config.get_NavFunc2(Context, ma_module, 1) %>
    //#Load chức năng
</script>


<script type="text/javascript">
<%=VNN_JQGridver2.get_layout_face() %>
    jQuery('#' + tengrid1).jqGrid({
        url: 'Controller/JqGrid/JQGrid<%=ma_module%>Load.ashx?ma_module=<%=ma_module%>&ma_menu=' + $('#input_idmenu').val(),
        editurl: 'Controller/JQGridModify/JQGrid<%=ma_module%>Modify.ashx?ma_module=<%=ma_module%>&ma_menu=' + $('#input_idmenu').val(),
        height: getHeightGrid(1),
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
        onSelectRow: function (ids) {
            //checkbox customize
            checkbox_JQgrid(tengrid1, 0);
            var value_header = '', cell = $('#' + tengrid1).getRowData(ids);
            if (id_parent1 != null) {
                value_header = <%=get_STTaID[1].Replace("id_","header_") %>;
                header_<%=ma_module %> = value_header + header_sep + <%=get_STTaID[2] %>;
            }
            else {
                header_<%=ma_module %> = value_header + <%=get_STTaID[2] %>;
            }
            set_headerJQG(tengrid1, header_<%=ma_module %>);
            // public id da chon
            if (ids != null & ids != '' & ids != '0') {
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
        beforeRequest: function () {
            //giữ focus
            input_focus1 = $('input:focus').attr('class');
            //giữ filter start
            if (id_oper[load_stt1] == null)
                $('#' + tengrid1).jqGrid('getGridParam', 'postData').id_sel = id_<%=ma_module %>;
            $('#' + tengrid1).jqGrid('getGridParam', 'postData').module_select = module_select[0];
            set_Filter(load_grid1, tengrid1, '<%=ma_module %>');
            $('#' + tengrid1).jqGrid('getGridParam', 'postData').id = id_parent1
        },
        ondblClickRow: function () {
            <%=get_STTaID[4] %>;
        },
        gridComplete: function () {
            $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width());
            if (load_grid1 == 0) { taonut_header1(tengrid1) };
        },
        loadComplete: function (data) {
            //chia màu chẵn lẻ
            var top_rowid = $('#' + tengrid1 + ' tr:nth-child(2)').attr('id');
            phanmauchogrid(this);
            if (id_<%=ma_module %> != null & id_<%=ma_module %> != '0')
                $('#' + tengrid1).jqGrid('setSelection', id_<%=ma_module %>);
            module_select[1] = 1;
            var b = $('#' + tengrid1).jqGrid('getGridParam', 'selrow');
            if (b == null | b == '' | b == '0') {
                if (top_rowid != null & top_rowid != 0) {
                    $('#' + tengrid1).jqGrid('setSelection', top_rowid);
                } else {
                    load_detail(load_grid1, 2, 1);
                    module_select[1] = 2;
                }
            }
            else {
                if (id_new == '0' | id_new == null) {	/*$('#'+tengrid1).jqGrid('setSelection', id_<%=ma_module %>);*/ }
                else
    { resetSelection(tengrid1); clickSelection(tengrid1, id_new); id_new = '0'; }
    Focus_Selection(tengrid1);
            }
    //làm mới bộ lọc
    clearSearchOptions(tengrid1);
    //giữ filter end
    if (load_grid1 == 0) {
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
            $('.'+input_focus1).focus();
        },
        caption: ''
    });


    jQuery('#'+tengrid1).jqGrid('navGrid', '#pager'+tengrid1,
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
    }, {
        //refesh
    });
    jQuery('#' + tengrid1).jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true });
    jQuery('#pager' + tengrid1 + '_left table').css('display', 'none');
    jQuery('#' + tengrid1).jqGrid('setFrozenColumns');


    //Start Ham ho tro them cho Grid (sẽ tự update nếu module chính update)
    function Avariable1() {
        var column_array = [];
        return column_array;
    }
    //--
    function Change_Value1(column_array, action) {
        for (var i in column_array) {
            $('#' + column_array[i]).change(function () {
                action_grid1(action, $(this).attr('id'));
            });
        }
        action_grid1(action, null);
    }
    //--
    function action_grid1(action, column) {
        if (action == 'add') {


        }
        else if (action == 'edit') {


        }
    }
    //--
    function add_edit_del_complete1(action) {
        if (action == 'add') {
        }
        else if (action == 'edit') {
        }
        else if (action == 'del') {
        }
    }
    //#End Ham ho tro them cho Grid (sẽ tự update nếu module chính update)
</script>
