let keySignalR = window.location.origin.replace(/\//g, '_').replace(/:/g, '_').replace(/\./g, '_');
let mainhub = $.connection.mainhub;
mainhub.vnnObj = { };
mainhub.channelB = new BroadcastChannel(keySignalR);

let retrySignalR = function (type, func) {
    let text = '';
    switch (type) {
        case 0: text = 'Không kết nối được server.'; break;
        case 1: text = 'Không gửi được dữ liệu.'; break;
        case 2: text = 'xử lý vượt quá 120s.'; break;
        default: text = 'unknown';
    }

    mainhub.client[func]({ end: true, mess: 'Lỗi, hãy thử lại!!!, ' + text });
}

let checkConnectSignalR = function (func, data) {

    if (mainhub.vnnObj[func].timeI >= 5) {
        mainhub.vnnObj[func].timeI = 0;
        retrySignalR(0, func);
    }
    else {
        mainhub.vnnObj[func].timeI += 1;
        setTimeout(function () { SignalrExec(func, data); }, 1000);
    }
}

let clearTimeoutSignalR = function (func) {

    let idtimeout = mainhub.vnnObj[func].timeout;
    if (idtimeout != null) {
        clearTimeout(Number(idtimeout));
        mainhub.vnnObj[func].timeout = null;
    }
}

let SignalrExec = function (func, data) {
    
    if (!mainhub.vnnObj.hasOwnProperty(func)) {
        mainhub.vnnObj = { func: null };
        mainhub.vnnObj[func] = {
            timeout: null,
            timeI: 0
        };
    }

    if ($.connection.hub.state !== 1)
    {
        console.error('Không kết nối được signalR, thử lại...');
        $.connection.hub.start()
            .done(() => {
                checkConnectSignalR(func, data);
            }).fail(() => {
                checkConnectSignalR(func, data);
            });
    }
    else {
        console.log('Gửi dữ liệu tới ' + func + '...');
        mainhub.vnnObj[func].timeout = setTimeout(function () {
            $.connection.hub.stop();
            retrySignalR(2, func);
        }, 120000);

        mainhub.invoke(func, data)
            .fail(function () {
                clearTimeoutSignalR(func);
                retrySignalR(1, func);
            })
            .done(function () {
                
            });
    }
}

mainhub.client["ChinhSuaMauExcel"] = function (data) {
    clearTimeoutSignalR("ChinhSuaMauExcel");

    if (data.end)
    {
        $('.nhan_loading0').remove();
        $('#btn-close').button('option', 'disabled', false);
        $('.ui-dialog-titlebar-close').button('option', 'disabled', false);
        data.mess = data.mess.replace('ExcelTemp', 'ExcelTemp boldLarge');

        let iconBack = '<span class="glyphicon glyphicon-arrow-left" style="left: -5px;position: relative;top: 2px;"></span>';
        let buttonBack = '<a style="font-size: 120% !important;color: #6617ff;cursor: pointer;padding: 5px 10px 5px 10px;background-color: #fff982;" id="backQLMExcel">' + iconBack + 'Quay lại</a>';

        data.mess += '<div style="text-align:center">' + buttonBack + '</div>';
        $('.xuLyThaoTacExcel').append(data.mess);

        $('#backQLMExcel').off('click');
        $('#backQLMExcel').click(function () {
            $('.huongDanThaoTacExcel').show();
            $('.xuLyThaoTacExcel').hide();
            $('#btn-ok').button('option', 'disabled', false);
            $('.taiLenMauExcel').next().val('').change();
        });
    }
    else
        $('.xuLyThaoTacExcel').append(data.mess);

    $('#dlg_gridSmallgridMD_00_QuanLyMauInExcel').parent().dialogCenter()
};

mainhub.client["sendReportToClient"] = function (data) {
    if (data.end == true) {
        let thongbao = JSON.parse(data.mess);
        let nguoinhans = JSON.parse(thongbao.nguoinhan);
        let checkExist = nguoinhans.filter(function (a) { return a.user == ma_tk }).length > 0;
        if (checkExist) {
            checkNewReport();
            mainhub.channelB.postMessage({ checkNewReport: true });
        }
    }
};

mainhub.client["taiLaiHDLH"] = function (data) {
    if (data.end == true) {
        let tt = JSON.parse(data.mess);
        $(`.esc_vnn_${tt.lienket}`).css('color', '');
        $(`.esc_vnn_${tt.md_taptin_id}`).css('color', '');
    }
};

mainhub.channelB.onmessage = function (e) {
    if (e.data.checkNewReport) {
        checkNewReport();
    }
    else if (e.data.stopHub) {
        $.connection.hub.stop();
    }
};

window.addEventListener('focus', function () {
    if ($.connection) {
        if ($.connection.hub) {
            if ($.connection.hub.state !== 1) {
                mainhub.channelB.postMessage({ stopHub: true });
                $.connection.hub.start();
            }
        }
    }
});



