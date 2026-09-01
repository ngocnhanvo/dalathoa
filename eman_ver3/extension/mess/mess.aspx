<%@ Page Language="C#" Async="true" EnableSessionState="False" %>
<% 
    var ma_user = Security.all_taikhoan(Context)["ma_user"];
%>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Thông báo nhắc việc</title>
    <link rel="shortcut icon" href="../images/logo/anco_logo.ico" />
    <link href="reportBootstrap.css" rel="stylesheet" />
    <link href="../ckeditor/samples/font-awesome-4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="mess.css" rel="stylesheet" />
    <script src="../../js/Public_script/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="../../js/Public_script/moment.js"></script>   
    <script src="../../js/jqxwidgets/jquery-1.10.2.min.js"></script>
    <script src="../../js/Public_script/bootstrap.min.js"></script>
    <script src="tableToExcel.js"></script>
    <style type="text/css">
        .main > h2 {
            text-align: center;
            margin-bottom: 15px;
            position: relative;
            font-size: 120%;
            color: #000000;
        }
        @media (max-width: 576px) {
            .main {
                padding: 0px 14px 0px 14px;
            }

            img.reportPublic {
                display: none;
            }
        }
    </style>
</head>
<body>
    <div class="loadingPage">
	    <div id="loader"></div>
    </div>

    <div>
        <div>
            <div class="main">
                <h2>THÔNG BÁO - TIN NHẮN - NHẮC VIỆC</h2>
                <!-- Another variation with a button -->
                
		        <div class="input-group">
                    <div class="previewContent" style="display:none"></div>
                    <input type="text" id="searchReport" class="form-control" placeholder="Tiêu đề, nội dung hoặc người gửi">
                    <div class="input-group-append" style="position: relative;">
                        <button onclick="displayPanelSearch()" class="btn btn-secondary btnPanelSearch" type="button">
                            <i class="fa fa-ellipsis-v"></i>
                        </button>
                        <div class="panelSearch hidden">
                            <div class="fa fa-caret-left"></div>
                            <ul>
                                <li>
                                    <span onclick="displayTabsPanelSearch(this.textContent, 's01_01')">Chưa xem</span>
                                </li>
                                <li>
                                    <span onclick="displayTabsPanelSearch(this.textContent, 's01_02')">Đã xem</span>
                                </li>
                                <%--<li>
                                    <span onclick="displayTabsPanelSearch(this.textContent, 's02_01')">Hôm qua</span>
                                </li>
                                <li>
                                    <span onclick="displayTabsPanelSearch(this.textContent, 's02_02')">Hôm nay</span>
                                </li>--%>
                                <li>
                                    <span autoclick onclick="displayTabsPanelSearch(this.textContent, 's03_01')">Từ đầu tháng</span>
                                </li>
                                <li style="position:relative">
                                    <span onclick="displayPanelSearch(null, 'searchDate')">Ngày tùy chọn</span>
                                    <div class="searchDate hidden">
                                        <div class="fa fa-caret-left"></div>
                                        <input type="date" id="timeSTT">
                                        <input type="date" id="timeEND" style="margin-top:5px">
                                        <input class="btnSelectDate" onclick="customDateToSearchClick(this)" type="button" value="Chọn"/>
                                        <span class="btnRefresh" onclick="document.getElementById('timeSTT').value = null, document.getElementById('timeEND').value = null">Làm mới</span>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="input-group-append">
                        <button class="btn btn-secondary" type="button">
                            <i class="fa fa-search"></i>
                        </button>
                    </div>
                </div>
                
                <div class="tabsPanelSearch">

                </div>

                <div class="list">
                    <ul id="myUL" style="list-style-type: none;">
                        <li>
                            <img class="avatarRpt" src="report_image_userMale.png" data-src="${avatar}" onerror="this.src='report_image_userMale.png'" alt="">
                            <div class="noidung">
                                <div class="fa fa-caret-left"></div>
                                <div class="contentA" style="${newReport}">
                                    <div class="title">
                                        <b>${tieude}</b><span>${viewState}</span>
                                    </div>
                                    <div class="content">
                                        <div style="color: #3c3c3c;font-size: 80%;position: relative;top: -5px;display:${displayOrigin};">
                                            Gửi từ "${value_nguoitao}" vào ${ngaytao}
                                        </div>
                                        ${noidung}
                                    </div>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
            
            <script type="text/javascript">
                let keySignalR = window.location.origin.replace(/\//g, '_').replace(/:/g, '_').replace(/\./g, '_')+ 'khainguyen_2';
                var channelB = new BroadcastChannel(keySignalR);
                channelB.onmessage = function (e) {
                    if (e.data.checkNewReport) {
                        alert('Bạn nhận được thông báo mới');
                    }
                };
                
                var dataSeach = {
                    keyword: "",
                    viewState: "",
                    timeFil: "",
                    timeSTT: null,
                    timeEND: null
                };

                let customDateToSearchClick = function (a) {
                    let eleDate1 = $('#timeSTT');
                    let eleDate2 = $('#timeEND');

                    if(!eleDate1.val())
                    {
                        alert('Bạn cần nhập ngày bắt đầu');
                        eleDate1.focus();
                        return;
                    }
                    else if (!eleDate2.val()) {
                        alert('Bạn cần nhập ngày kết thúc');
                        eleDate2.focus();
                        return;
                    }

                    let date1 = moment(new Date(eleDate1.val()));
                    let date2 = moment(new Date(eleDate2.val()));
                    let fmtDMY = 'DD/MM/YYYY';
                    eleDate1.attr('valSav', date1.format(fmtDMY));
                    eleDate2.attr('valSav', date2.format(fmtDMY));

                    let text = `Từ ngày "${date1.format(fmtDMY)}" đến ngày "${date2.format(fmtDMY)}"`;
                    displayTabsPanelSearch(text, 's02_03');
                };

                let excuteFuncWithOption = function(type) {
                    switch (type) {
                        case "s01_01":
                            dataSeach.viewState = 0;
                            break;
                        case "s01_02":
                            dataSeach.viewState = 1;
                            break;
                        case "s02_01":
                            dataSeach.timeFil = "yesterday";
                            break;
                        case "s02_02":
                            dataSeach.timeFil = "today";
                            break;
                        case "s02_03":
                            dataSeach.timeFil = "customDay";
                            dataSeach.timeSTT = $('#timeSTT').attr('valSav');
                            dataSeach.timeEND = $('#timeEND').attr('valSav');
                            break;
                        case "s03_01":
                            dataSeach.timeFil = "thismonth";
                            break;
                        default:
                            break;
                    };
                };

                let displayTabsPanelSearch = function (text, type) {
                    $('.searchDate').addClass('hidden');

                    let tabsPanelSearch = $('.tabsPanelSearch');

                    let loadReportFromServerWithTabs = function () {
                        dataSeach.viewState = "";
                        dataSeach.timeFil = "";
                        dataSeach.timeSTT = null;
                        dataSeach.timeEND = null;
                        tabsPanelSearch.children('.tabDetail').each(function () {
                            let typeClass = $(this).attr('class').replace('tabDetail', '').replace(' ', '');
                            excuteFuncWithOption(typeClass);
                        });
                        loadReportFromServer();
                    };

                    let removeAllTabs = function () {
                        let findTabAll = tabsPanelSearch.children(`.tabDetailAll`);
                        if (!findTabAll.length) {
                            tabsPanelSearch.prepend(`<div class="tabDetailAll">Xóa tìm kiếm</div>`);

                            findTabAll = tabsPanelSearch.children(`.tabDetailAll`);
                            findTabAll.off('click');
                            findTabAll.click(function () {
                                tabsPanelSearch.html('');
                                loadReportFromServerWithTabs();
                            });
                        }
                    };

                    let findTab = tabsPanelSearch.children(`.tabDetail.${type}`);
                    if (findTab.length)
                        findTab.remove();

                    for (let i = 0; i < 99; i++) {
                        let txtI = i < 10 ? '0' + i : i;
                        tabsPanelSearch.children(`.tabDetail.${type.substring(0, 4) + txtI}`).remove();
                    }
                    tabsPanelSearch.append(`<div class="tabDetail ${type}">${text} <div class="fa fa-times"></div></div>`);
                    loadReportFromServerWithTabs();

                    let close = tabsPanelSearch.find(`.tabDetail.${type} > .fa-times`);
                    close.off('click');
                    close.click(function () {
                        $(this).parent().remove();
                        if (tabsPanelSearch.children('.tabDetail').length <= 0) {
                            tabsPanelSearch.html('');
                        }
                        loadReportFromServerWithTabs();
                    });

                    removeAllTabs();
                    displayPanelSearch(false);
                };

                let displayPanelSearch = function (show, ele) {
                    let panelSearch = ele ? $('.' + ele) : $('.panelSearch');
                    if (show == null)
                        panelSearch.hasClass('hidden') ? panelSearch.removeClass('hidden') : panelSearch.addClass('hidden');
                    else
                        show ? $(panelSearch).removeClass('hidden') : $(panelSearch).addClass('hidden');
                };

                var viewMess = function (elem, ma_menu, ma_module, sochungtu, index, id) {
                    elem = $(elem).parent().parent().parent();
                    let contentA = $(elem).children('.contentA');
                    let styleA = contentA.attr('style');
                    if (styleA.length > 0) {
                        contentA.css('border', '');
                        contentA.find('.title > span').html('');
                        $.ajax({
                            url: '../../Controller/JQGridModify/JQGridMD_00_TBModify.ashx?oper=viewMess',
                            method: 'POST',
                            data: { ma_menu: ma_menu, id: id },
                            success: function (rs) { },
                            fail: function (rs) { },
                            complete: function () {
                                contentA.find('.removeAfter').remove();
                            }
                        });
                    }

                    window.parent.postMessage({ moveToMenu: true, ma_menu: ma_menu, ma_module: ma_module, sochungtu: sochungtu, index: index }, '*');
                };

                let get_cookie = function (cname) {
                    let name = cname + "=";
                    var decodedCookie = decodeURIComponent(document.cookie);
                    var ca = decodedCookie.split(';');
                    for (var i = 0; i < ca.length; i++) {
                        var c = ca[i];
                        while (c.charAt(0) == ' ') {
                            c = c.substring(1);
                        }
                        if (c.indexOf(name) == 0) {
                            return c.substring(name.length, c.length);
                        }
                    }
                    return "";
                };

                let delete_cookie = function (name) {
                    document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
                };

                let displayLoading = function (show) {
                    show ? $('.loadingPage').show() : $('.loadingPage').hide();
                }

                let myUL = $('#myUL');
                let format = myUL.html();
                //Call Ajax to get data from server
                let loadReportFromServer = function (keyword) {
                    displayLoading(true);
                    $.ajax({
                        url: '../../Controller/JQGridModify/JQGridMD_00_TBModify.ashx?oper=getJSONMess',
                        method: 'POST',
                        data: dataSeach,
                        success: function (rs) {
                            let maUser = '<%=ma_user%>';
                            let replaceAvariableToValue = function (ii, value, formatRow) {
                                let reg = new RegExp("\\${" + ii + "}", "g");
                                return formatRow.replace(reg, value);
                            };
                            rs = JSON.parse(rs);
                            myUL.html('');
                            for (let i in rs) {
                                let formatRow = format;
                                let row = rs[i];
                                let jsonNN = JSON.parse(row.nguoinhan).filter(function (a) { return a.user == maUser })[0];
                                row.newReport = jsonNN.viewTime ? "" : "border: 2px solid #f00";
                                row.viewState = jsonNN.viewTime ? "" : "";

                                let pvct = $('.previewContent');
                                pvct.html(row.noidung);
                                if (jsonNN.viewTime)
                                    pvct.find('.removeAfter').remove();
                                else
                                    pvct.append(`<br><span class='viewMess removeAfter' onclick="viewMess(this, '', '', '', '', '${row.ad_mess_id}')">Đã xem</span>`);

                                row.noidung = pvct.html();
                                row.displayOrigin = "block";
                                let ngayTao = moment(new Date(row.ngaytao));
                                let DD = ngayTao.format('DD');
                                let MM = ngayTao.format('MM');
                                let YY = ngayTao.format('YYYY');
                                let HH = ngayTao.format('HH');
                                let mm = ngayTao.format('mm');
                                row.ngaytao = `ngày ${DD} tháng ${MM} năm ${YY} lúc ${HH} giờ ${mm} phút`;
                                row.avatar = `../Controller/API_System.ashx?oper=loadImage&code=${row.value_nguoitao}&type=2`;
                                for (let ii in row) {
                                    formatRow = replaceAvariableToValue(ii, row[ii], formatRow);
                                }
                                myUL.append(formatRow);
                            }

                            if (myUL.html().length <= 0) {
                                let formatRow = format;
                                formatRow = replaceAvariableToValue('displayOrigin', 'none', formatRow);
                                formatRow = replaceAvariableToValue('viewState', '', formatRow);
                                formatRow = replaceAvariableToValue('newReport', '', formatRow);
                                formatRow = replaceAvariableToValue('tieude', 'Hệ thống trả lời tự động', formatRow);
                                let contentSystem = '';
                                contentSystem += '<span style="color:red">Bạn chưa có tin nhắn tại thời điểm này</span>, mọi thắc mắc xin liên hệ quản trị để được tư vấn.';
                                formatRow = replaceAvariableToValue('noidung', contentSystem, formatRow);
                                myUL.append(formatRow);
                            }
                            else {
                                $('.avatarRpt').each(() => {
                                    $(this).attr('src', $(this).attr('data-src'));
                                });
                            }
                        },
                        fail: function (rs) {
                            myUL.html(rs);
                        },
                        complete: function () {
                            displayLoading(false);

                            $('a[file]').each(function () {
                                $(this).removeAttr('href');
                                $(this).off('click');
                                $(this).click(function () {
                                    let fName = $(this).text();
                                    displayLoading(true);
                                    let file = $(this).attr('file').replace('../', '');
                                    let id = file.substring(file.lastIndexOf('/') + 1, file.lastIndexOf('.') - 1);
                                    id = id == null | id == '' ? file : id;
                                    let link = '../Controller/PublicFunction/Download.ashx';
                                    window.location.href = `${link}?id=${file}&cookieId=${id}&fileName=${fName}`;
                                    let interval = setInterval(function () {
                                        let cookieVal = get_cookie(id);
                                        let check = cookieVal == null || cookieVal === 'undefined';
                                        if (!check) {
                                            delete_cookie(id);
                                            displayLoading(false);
                                            clearInterval(interval);
                                        }
                                    }, 200);
                                });
                            });

                            $('a[excel]').each(function () {
                                $(this).off('click');
                                $(this).click(function () {
                                    $(this).parents('.content').first().find('.viewMess').click();
                                    let text = $(this).text();
                                    let title = $(this).attr('title');
                                    $(this).text('');
                                    let tbl = $(this).parent().parent().parent().parent();
                                    TableToExcel.convert(tbl[0], {
                                        name: `${title}.xlsx`,
                                        sheet: {
                                            name: "Sheet 1"
                                        }
                                    });

                                    $(this).text(text);
                                });
                            });

                            $('table').each(function () {
                                let ele = $(this);
                                let eleW = ele.outerWidth();
                                ele.attr('widthOld', eleW);
                            });
                            $(window).resize();
                        }
                    });
                };

                $('#searchReport').change(function () {
                    dataSeach.keyword = $(this).val();
                    loadReportFromServer();
                });

                $(window).resize(function () {
                    let windowW = $('#myUL').width() - 50;
                    $('table').each(function () {
                        let ele = $(this);
                        let eleW = Number(ele.attr('widthOld'));

                        if (eleW > windowW) {
                            ele.css('width', '100%');
                        }
                        else {
                            ele.css('width', ele.attr('widthOld') + 'px');
                        }
                    });
                });

                $(document).click(function (e) {
                    let chk = $(e.target).closest("div.panelSearch").length;
                    let chk2 = $(e.target).closest("button.btnPanelSearch").length;
                    if (!chk & !chk2)
                        displayPanelSearch(false);
                });

                $('.panelSearch span[autoclick]').click();
            </script> 
        </div>
    </div>
</body>
</html>