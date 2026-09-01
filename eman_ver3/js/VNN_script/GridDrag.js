var move = false;
var x = 0, y = 0, x1 = 0, y1 = 0;
var nhandiv = 0;
var nhanmouse = 0;
var indiv = 0;
var ten_indiv = '';
var indiv_dachon = 0, indiv_chuachon = 0;

function chaytudong(tengrid) {

    if (xchay2 < xchay)
    { $('#gview_' + tengrid + ' .ui-jqgrid-bdiv').scrollLeft($('#gview_' + tengrid + ' .ui-jqgrid-bdiv').scrollLeft() - (xchay - xchay2)); }
    else if (xchay2 > xchay)
    { $('#gview_' + tengrid + ' .ui-jqgrid-bdiv').scrollLeft($('#gview_' + tengrid + ' .ui-jqgrid-bdiv').scrollLeft() + (xchay2 - xchay)); }


    if (ychay2 < ychay) {
        $('#gview_' + tengrid + ' .ui-jqgrid-bdiv').scrollTop($('#gview_' + tengrid + ' .ui-jqgrid-bdiv').scrollTop() - (ychay - ychay2));
    }
    else if (ychay2 > ychay) {
        $('#gview_' + tengrid + ' .ui-jqgrid-bdiv').scrollTop($('#gview_' + tengrid + ' .ui-jqgrid-bdiv').scrollTop() + (ychay2 - ychay));
    }

    xoaselection();

    if (lanchay < 7) {
        lanchay += 1;
        setTimeout("chaytudong('" + tengrid + "')", 10 * lanchay);
    }
}

function xoaselection() {
    if (window.getSelection) {
        if (window.getSelection().empty) {  // Chrome
            window.getSelection().empty(); window.getSelection().removeAllRanges();
        } else if (window.getSelection().removeAllRanges) {  // Firefox
            window.getSelection().removeAllRanges();
        }
    } else if (document.selection) {  // IE?
        document.selection.empty();
    }
}

function click_drag(tengrid) {
    $('#gview_' + tengrid + ' .ui-jqgrid-bdiv').mousedown(function (e) {
        if (e.button == 2) {
            chon_rightclick = 1;
        }
        if (!$('#mainpane').hasClass('nhan_thanhtruot')) {
            i_dem_kt = 0;
            huydem = false;
            indiv = 1;
            ten_indiv = tengrid;
            move = true;
            xchay2 = x1 = e.clientX;
            ychay2 = y1 = e.clientY;

            if (indiv < 1) {
                return true;
            }
            else if (indiv == 1) {
                return false;
            }
        }
    });

    $('#mainpane').mouseup(function (e) {

        if (i_dem_kt <= 2 & nhanmouse == 1) {

            xchay = e.clientX;
            ychay = e.clientY;
            lanchay = 0;
            if (indiv == 1)
            { setTimeout("chaytudong('" + tengrid + "')", 20); }
        }

        $('body').css("cursor", "default");

        indiv = 0; ten_indiv = '';
        indiv_chon = 0; indiv_chuachon = 0;
        huydem = true;
        i_dem_kt = 0;
        move = false;
        nhanmouse = 0;
        $('#nhan-che').remove();
    });

    document.onmousemove = null;
    document.onmousemove = handleMouse;
}

function handleMouse(e) {
    // Verify that x and y already have some value
    // Scroll window by difference between current and previous positions

    if (move == true & ten_indiv != '') {
        if (nhanmouse == 0 & (x != x1 | y != y1)) {
            nhanmouse = 1;
            demlientuc();
            $('body').css("cursor", "all-scroll");
            try {
                if (indiv_dachon <= 0 & indiv_chuachon <= 0)
                    $('#mainpane').prepend('<div id="nhan-che" ></div>');
            } catch (er) { }
        }

        if ((indiv_chuachon > 0 | indiv_dachon > 0) & nhanmouse == 1) {
            $('#nhan_image_themuser').css('left', x + 3);
            $('#nhan_image_themuser').css('top', y + 3);
        }
        //thay doi vi tri theo chuot
        if (indiv == 1 & nhanmouse == 1) {

            if (x < x1)
            { $('#gview_' + ten_indiv + ' .ui-jqgrid-bdiv').scrollLeft($('#gview_' + ten_indiv + ' .ui-jqgrid-bdiv').scrollLeft() - (x - x1)); }
            else if (x > x1)
            { $('#gview_' + ten_indiv + ' .ui-jqgrid-bdiv').scrollLeft($('#gview_' + ten_indiv + ' .ui-jqgrid-bdiv').scrollLeft() + (x1 - x)); }


            if (y < y1) {
                $('#gview_' + ten_indiv + ' .ui-jqgrid-bdiv').scrollTop($('#gview_' + ten_indiv + ' .ui-jqgrid-bdiv').scrollTop() - (y - y1));
            }
            else if (y > y1) {
                $('#gview_' + ten_indiv + ' .ui-jqgrid-bdiv').scrollTop($('#gview_' + ten_indiv + ' .ui-jqgrid-bdiv').scrollTop() + (y1 - y));
            }
        }

        xoaselection();
        x1 = x;
        y1 = y;
    }
    // Store current position
    x = e.clientX;
    y = e.clientY;
}

