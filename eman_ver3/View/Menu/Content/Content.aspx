<table class="cl_table_noidung" valign="top" >
    <!-- cong cu them sua xoa mac dinh -->
    <tr class="cl_tr_thanhcongcu">
        <td colspan="6" valign="top" >
            <div style="float:right; height: 25px" align="right">
                <div class="cl_div_chuathanhcongcu">
                </div>   
            </div>
        </td>
    </tr>
    <!-- #cong cu them sua xoa mac dinh -->

    <!-- load nut module theo menu -->
    <tr>
        <td colspan="6" class="cl_td_module" valign="left">
        </td>
    </tr>
    <!-- #load nut module theo menu-->

     <!-- load noi dung theo module -->
    <tr>
        <td colspan="6" class="cl_td_noidungmodule" valign="top">

        </td>
    </tr>
    <!-- #load noi dung theo module -->
</table>

<script type="text/javascript">
    var loaddautien_content = 0;
    //$('.cl_table_noidung').height($('.noidung').height());

    $('.cl_td_module').addClass('loading');
    enable_timer = false;

    $.ajax({
        type: "GET",
        url: "Controller/JQGridModify/JQGridMD_01_MDZModify.ashx?oper=loadModule&ma_menu=" + $('#input_idmenu').val() + "&url_menu=" + $('#input_urlmenu').val() +
            "&ten_menu=" + $('#input_tenmenu1').val(),
        success: function (msg) {
            $('.cl_td_module').removeClass('loading');
            $('.cl_td_module').html(msg);

            $('.cl_td_urlmenu > div').empty();
            $('.cl_td_noidungmodule').html('<div class="div_noidung_menu loading"></div>');

            if ($("#input_nhanback").val('false'))
                $('.module_spanselect').click();

            var ghinho_module = $('#input_ghinhomodule').val().split('#');
            for (var i = ghinho_module.length - 1; i >= 0; i--) {
                var doidaichuoichinh = ghinho_module.length;

                var doidaichuoibo = ghinho_module[i].length;

                var chuoighinho = ghinho_module[i];
                $('#input_ghinhomodule').val(Remove($('#input_ghinhomodule').val(), doidaichuoichinh - doidaichuoibo - 1, doidaichuoichinh));
                if (i <= 1) {
                    $("#input_nhanback").val('false');
                    if (i == 1)
                        $('#' + chuoighinho).click();
                }
                else {
                    $("#input_nhanback").val('true');
                    $('#' + chuoighinho).click();
                }

                if (i == 0) {
                    loaddautien_content = 1;
                }
            }
        }
    });

    function loadnoidung(page, e) {
        if ($("#input_nhanback").val() == 'false') {
            $('.div_noidung_menu').addClass('loading');
            try { xhr_content.abort(); } catch (r) { }
            enable_timer = false;
            xhr_content = $.ajax({
                type: "GET",
                url: page,
                data: {
                    isLinkTransfer,
                    menu: searchParams.get('menu'),
                    module: searchParams.get('module'),
                    hideModules: searchParams.get('hideModules')
                },
                success: function (msg) {
                    dataToShareScreen.md0 = e;
                    dataToShareScreen.md1 = '';
                    dataToShareScreen.md2 = '';
                    update_dataToShareScreen();

                    $('.div_noidung_menu').removeClass('loading');
                    $('.div_noidung_menu').html(msg);
                    enable_timer = true;
                }
            });
       }
    }

    function loadModule(e, d, c, q) {
        let divClTdUrlmenu = $('.cl_td_urlmenu > div');
        // tao hieu ung loading
        $('.border_div_modulelv1').removeAttr('style');
        var congthem = -12;
        var margin = 2;
        // Số càng cao độ cao càng giảm
        var cap0_0 = 223, td0_0 = '32px', margin_nd0_0 = '-13px 0 0 8px'; //if (Browser == 'Firefox') { cap0_0 = 224 + 4; margin0_0 = '-64px 0 0 0'; margin_nd0_0 = '-13px 0 0 8px'; }
        var cap0_1 = 215, td0_1 = '32px', margin_nd0_1 = '-21px 0 0 8px'; //if (Browser == 'Firefox') { cap0_1 = 216 + 4; margin0_1 = '-64px 0 0 0'; margin_nd0_1 = '-21px 0 0 8px'; }
        var cap1_0 = 249, td1_0 = '57px', margin_nd1_0 = '-13px 0 0 8px'; //if (Browser == 'Firefox') { cap1_0 = 240 + 4; margin1_0 = '-56px 0 0 0'; margin_nd1_0 = '-13px 0 0 8px'; }
        var cap1_1 = 223, td1_1 = '32px', margin_nd1_1 = '-13px 0 0 8px'; //if (Browser == 'Firefox') { cap1_1 = 216 + 4; margin1_1 = '-64px 0 0 0'; margin_nd1_1 = '-13px 0 0 8px'; }
        var cap2_0 = 249, td2_0 = '57px', margin_nd2_0 = '-13px 0 0 8px'; //if (Browser == 'Firefox') { cap2_0 = 240 + 4; margin2_0 = '-56px 0 0 0'; margin_nd2_0 = '-13px 0 0 8px'; }

        $('.cl_td_noidungmodule').html('<div class="div_noidung_menu loading"></div>');

        // gán giá trị cho sự kiện trở về trang trước
        if (loaddautien_content > 0) {
            $('#input_trovetrangtruoc').val($('#input_trovetrangtruoc').val() + '&' + 'span_' + e);
        }

        // Neu nhap vao module thu 0
        if (c == 0) {
            // xoa cac hieu ung cua module
            $('.div_modulelv0 span').removeClass('module_spanselectbefore');
            $('.div_modulelv0 span').removeClass('module_spanselect');

            $('.div_modulelv1 span').removeClass('module_spanselectbefore');
            $('.div_modulelv1 span').removeClass('module_spanselect');

            $('.div_modulelv2 span').removeClass('module_spanselectbefore');
            $('.div_modulelv2 span').removeClass('module_spanselect');

            $('.div_modulelv2 span').removeClass('chuanhan');
            $('.div_modulelv2 span').addClass('chuanhan');

            // load nhung mod thuoc mod da chon
            $('.danhan_' + e).removeClass('chuanhan');
            $('.div_modulelv0').removeAttr('style');
            $('.div_module_background').removeAttr('style');

            $('.div_modulelv2').addClass('display_none');
            if ($('.danhan_' + e).length > 0) {
                $('.danhan_' + e + ':last').addClass('chuanhan');
                $('#input_docaogrid').val(cap0_0 + Number(congthem));
            }
            else {
                $('#input_docaogrid').val(cap0_1 + Number(congthem));
            }
            // lay ten mod cua mod dau tien
            $('#input_tenmodule0').val(q);
        }
        // Neu nhap vao module thu 1
        else if (c == 1) {
            $('.span_unselected').removeAttr('style');
            // to den nhung module 0 da tung chon
            $('.module_spanselect').addClass('module_spanselectbefore');
            
            $('.module_spanselectbefore').removeClass('module_spanselect');
            $('.div_modulelv1 span').removeClass('module_spanselectbefore');
            $('.div_modulelv1 span').removeClass('module_spanselect');

            $('.div_modulelv2 span').removeClass('module_spanselectbefore');
            $('.div_modulelv2 span').removeClass('module_spanselect');

            $('.div_modulelv2 span').removeClass('chuanhan');
            $('.div_modulelv2 span').addClass('chuanhan');

            //load nhung mod thuoc mod da chon
            
            $('.danhan_' + e).removeClass('chuanhan');
            if ($('.danhan_' + e).length > 0) {
                $('.danhan_' + e + ':last').addClass('chuanhan');
                $('.div_modulelv2').removeClass('display_none');
                $('#input_docaogrid').val(cap1_0 + Number(congthem));
            }
            else {
                $('.div_modulelv2').addClass('display_none');
                $('#input_docaogrid').val(cap1_1 + Number(congthem));
            }

            // lay ten mod cua mod thu 2
            $('#input_tenmodule1').val(q);

            divClTdUrlmenu.html(
                '<span class="cl_span_urlmenu">' +
                    ' <span>' + $('#input_tenmenu0').val() + '</span>' +
                    ' <span class="span_muiten">&rsaquo;&rsaquo;</span>' +
                    ' <span>' + $('#input_tenmenu1').val() + '</span>' +
                    ' <span class="span_muiten">&rsaquo;&rsaquo;</span>' +
                    ' <span>' + $('#input_tenmodule0').val() + '</span>' +
                    ' <span class="span_muiten">&rarr;</span>' +
                    ' <span>' + $('#input_tenmodule1').val() + '</span>' +
                '</span>');
            divClTdUrlmenu.attr('title', divClTdUrlmenu.text());
        }
        // Neu nhap vao module thu 2
        else if (c == 2) {
            $('.module_spanselect').addClass('module_spanselectbefore');
            $('.module_spanselectbefore').removeClass('module_spanselect');

            $('.div_modulelv2 span').removeClass('module_spanselectbefore');
            $('.div_modulelv2 span').removeClass('module_spanselect');

            // lay ten mod cua mod thu 3
            $('#input_tenmodule2').val(q);

            divClTdUrlmenu.html(
                '<span class="cl_span_urlmenu">' +
                    ' <span>' + $('#input_tenmenu0').val() + '</span>' +
                    ' <span class="span_muiten">&rsaquo;&rsaquo;</span>' +
                    ' <span>' + $('#input_tenmenu1').val() + '</span>' +
                    ' <span class="span_muiten">&rsaquo;&rsaquo;</span>' +
                    ' <span>' + $('#input_tenmodule0').val() + '</span>' +
                    ' <span class="span_muiten">&rarr;</span> ' +
                    ' <span>' + $('#input_tenmodule1').val() + '</span>' +
                    ' <span class="span_muiten">&rarr;</span> ' +
                    ' <span>' + $('#input_tenmodule2').val() + '</span>' +
                '</span>');
            divClTdUrlmenu.attr('title', divClTdUrlmenu.text());
            $('#input_docaogrid').val(cap2_0 + Number(congthem));
        }

        loadnoidung(d, e);

		$('.span_' + e).addClass('module_spanselect');
    }

    //tieu de cua grid
    function loadtieude(e) {
        $('.ui-jqgrid-title').html(e);
    }

    
</script>