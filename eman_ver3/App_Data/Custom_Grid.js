//Start
function ${ma_editstyle}(elem) { 
    var id_elem = $(elem).attr('id'); 
    
    $(elem).parent().append('<span onclick="timkiem_${ma_editstyle}(this)" ' + 
        'class="span_format_lenhsx glyphicon glyphicon-search" style="position: absolute;margin: 1px 0 0 -21px;background-color: rgb(248, 249, 243);padding: 3.5px;cursor: pointer;border-left: 1px solid rgba(204, 204, 204, 0.63)" />'); 

    $(elem).keypress(function (e) {
        if (e.which == '13') {
            $(elem).next().click();
        }
    });
}

function timkiem_${ma_editstyle}(id_elem, type) {
    // Grid
    var load_sp = 0; 
    $('body').append(`<div id="dlg_gridSmal_2" style="margin: 4px 0 0 0; background-color: rgba(165, 121, 255, 0.07);" title="${ten_editstyle}">
        <table id="grid${ma_module}_cp"></table>
        <div id="pagergrid${ma_module}_cp"></div> 
        </div>`); 

    var where_ex = ''; 
    console.log($(id_elem).parent().parent().parent().parent().attr('id'));

    //-- Table ID - TblGrid_gridMD_00_DSHangHoaVG
    if ($(id_elem).parent().parent().parent().parent().attr('id') == 'TblGrid_gridMD_00_Khuon') { 
       where_ex = ' and vt.son != 1 '; 
    }

    id_elem = $(id_elem).prev();
    $('#dlg_gridSmal_2').dialog({ 
        modal: true, 
        dialogClass: "dialog_index", 
        width: 650, 
        height: window.innerHeight - 10, 
        open: function (event, ui) { 
        jQuery('#grid${ma_module}_cp').jqGrid({ 
        url: 'Controller/JqGrid/JQGrid${ma_module}Load.ashx?ma_module=${ma_module}&ma_menu=${ma_menu}&id=null&id_sel=&module_select=1', 
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
        multiselect: false, 
        multiboxonly: false, 
        rowList: [10, 50, 100, 1000], 
        pager: '#pagergrid${ma_module}_cp', 
        onSelectRow: function (ids) { 
           if (ids != '<a style="color:red">Not data (404)</a>') { 
               cell = $('#grid${ma_module}_cp').getRowData(ids); 
               //set cell value demo 
               if (id_elem.prop('disabled') != true) { 
                   id_elem.val(cell['ma_vattu']); } 
               $('#mota_tiengviet').val(cell['mota_tiengviet']);   
               //set cell type select value demo 
               $('#md_donvitinhsanpham_id option').each(function(index, val) { 
                   if (cell['md_donvitinhsanpham_id'] == $(this).text()) { 
                       $(this).attr('selected', true); 
                       $('#md_donvitinhsanpham_id').val($(this).val()); 
                   } 
                   else { 
                       $(this).removeAttr('selected'); 
                   } 
               }); 
           } 
        }, 
        //demo 
        colModel: [ 
            ${colModel}
        ], 
        beforeRequest: function () { 
            //giữ focus 
            $('#grid${ma_module}_cp').jqGrid('getGridParam', 'postData').where_ex = where_ex; 
            if (id_elem.val() != '' & load_sp == 0) { 
               $('#grid${ma_module}_cp').jqGrid('getGridParam', 'postData').filters = '{"groupOp":"AND","rules":[{"field":" vt.ma_vattu ","op":"bw","data":"' + id_elem.val() + '"}]}'; 
           } 
           input_focus = $('input:focus').attr('class'); 
        }, 
        ondblClickRow: function () { 
            $('#dlg_gridSmal_2').dialog('destroy').remove(); 
        }, 
        gridComplete: function () { 
            $(this).jqGrid('setGridWidth', $(this).parent().parent().parent().parent().parent().width()); 
        }, 
        loadComplete: function (data) { 
            var top_rowid = $('#grid${ma_module}_cp tr:nth-child(2)').attr('id'); 
            var countrow = jQuery("#grid${ma_module}_cp").jqGrid('getGridParam', 'records'); 
            if (load_sp == 0) { 
                $('.gs_ma_vattu.gs_grid${ma_module}_cp').val(id_elem.val()); 
                $('#grid${ma_module}_cp').jqGrid('setSelection', id_elem.val()); 
               load_sp = 1; 
               if (top_rowid.indexOf('<a style="color:red">') <= -1 & type == 1 & countrow == 1) { 
                  $('#dlg_gridSmal_2').dialog('destroy').remove(); 
               } 
            } 
            Focus_Selection('grid${ma_module}_cp'); 
            //giữ focus end 
            $('.' + input_focus).focus(); 
        }, 
        caption: ' ' 
            }); 
            jQuery('#grid${ma_module}_cp').jqGrid('filterToolbar', { searchOnEnter: false, stringResult: true }); 
            Logo_Center("glyphicon glyphicon - search", true, 'dlg_gridSmal_2'); 
        }, 
        close: function () { 
            $(this).dialog('destroy').remove(); 
        }, 
        buttons: 
            [{ 
               id: 'btn-ok_', 
               text: 'OK', 
               click: function () { 
                   $(this).dialog('destroy').remove(); 
               } 
           }, 
           { 
               id: 'btn-close_', 
               text: 'Cancel', 
               click: function () { 
                   $(this).dialog("destroy").remove(); 
               } 
           }] 
      }); 
} 
//End