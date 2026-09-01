var format_image = {
    create: function () {
        let t = null;
        $(e).click(function (e) { t = e }),
            $(e).click(function () {
                $("body").append(`
                    <div id="dlg_gridSmal_2"title="Tìm hình ảnh">
                        <table>
                            <tr>
                                <td>
                                    <input id="rdiClass"type="radio"name="rdiStatus"value="img_Class" />
                                    <label for="rdiClass">Hìn ảnh trong Class</label>
                                    <input id="rdiHinhAnh" type="radio" name="rdiStatus" value="img_Content" />
                                    <label for="rdiHinhAnh">Hìn ảnh trong Content</label>
                                    <input id="rdiAweSome" type="radio" name="rdiStatus" value="img_Content" />
                                    <label for="rdiAweSome">Hìn ảnh trong Content</label>
                                </td>
                            </tr>
                            <tr>
                                <td id="td_content"><div class="nhan_loading"></div></td>
                            </tr>
                        </table>
                    </div>`),

                    $("#dlg_gridSmal_2").dialog({
                        modal: !0,
                        dialogClass: "dialog_index",
                        width: window.innerWidth / 3,
                        height: window.innerHeight - 3,
                        open: function () {
                            $("input:radio[name=rdiStatus]").click(function () {
                                $.get(`Controller/PublicFunction/LoadImage.ashx?oper=load_img&check=${$(this).val()}&elem=${$(e).attr("id")}`,
                                    function (t) {
                                        $("#td_content").empty(),
                                            $("#td_content").prepend(t);
                                        var n = $(".nhan_divclass");
                                        $(n).each(function () {
                                            if ($(e).val().indexOf("/") < 0)
                                                try {
                                                    $(this).find("span").attr("class").replace("nhan_spanclass ", "") == $(e).val() && $(this).find("span").css("background", "#FCFC6F")
                                                }
                                                catch (t) { }
                                            else
                                                try {
                                                    $(this).find("img").attr("src").indexOf($(e).val()) > -1 && $(this).find("img").css("background", "#FCFC6F")
                                                } catch (t) {

                                                }
                                            $(this).click(function () {
                                                n.find("span").removeAttr("style"), n.find("img").removeAttr("style"),
                                                    $(this).find("span").css("background", "#FCFC6F"), $(this).find("img").css("background", "#FCFC6F")
                                            });

                                            $(this).dblclick(function () { $("#dlg_gridSmal_2").dialog("destroy").remove() })
                                        })
                                    })
                            }),
                                $(e).val().indexOf("/") < 0 ?
                                    $("#rdiClass").click() :
                                    "fa" == $(e).val().substring(0, 2) ? $("#rdiHinhAnh").click() : $("#rdiAweSome").click()
                        },
                        close: function () { $(this).dialog("destroy").remove() }
                    })
            }), $(e).addClass("format_vnn formatimage")
    }
}