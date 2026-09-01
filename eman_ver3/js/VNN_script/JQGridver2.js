function check_click_rutgon(t, a) {
	var i = "unclick_div" + a,
	e = "click_div" + a;
	$(t).hasClass(e) ? ($(t).removeClass(e), $(t).removeClass("glyphicon-plus"), $(t).addClass(i), $(t).hasClass("glyphicon-minus") || $(t).addClass("glyphicon-minus")) : ($(t).removeClass(i), $(t).removeClass("glyphicon-minus"), $(t).addClass(e), $(t).hasClass("glyphicon-plus") || $(t).addClass("glyphicon-plus"))
}
function check_click_rutgon2(t, a) {
	var i = "unclick_div" + a,
	e = "click_div" + a;
	$(t).removeClass(e),
	$(t).hasClass(i) || $(t).addClass(i),
	$(t).removeClass("glyphicon-plus"),
	$(t).hasClass("glyphicon-minus") || $(t).addClass("glyphicon-minus")
}
function check_click_rutgon3(t) {
	for (var a = 0; 3 > a; a++) {
		var i = "unclick_div" + a,
		e = "click_div" + a;
		$(t + a).hasClass(e) && ($(t + a).removeClass(e), $(t + a).addClass(i), $(t + a).removeClass("glyphicon-plus"), $(t + a).hasClass("glyphicon-minus") || $(t + a).addClass("glyphicon-minus"))
	}
}
function click_rutgon_div(t) {
	var a = $("#div_getdt_2").outerHeight();
	$("#div_getdt_1").outerHeight(),
	$("#div_getdt_0").outerHeight();
	0 == t ? $(".btn_check_div0").hasClass("click_div0") ? 5 >= a ? layout_vnn.sizePane("north", "50%") : (layout_vnn.sizePane("north", "33%"), layout_vnn.sizePane("south", "33%")) : (layout_vnn.sizePane("north", "4%"), a > 5 && layout_vnn.sizePane("south", "49%"), setTimeout(function () {
		check_click_rutgon2($(".btn_check_div1"), 1),
		check_click_rutgon2($(".btn_check_div2"), 2)
	},
	100)) : 1 == t ? $(".btn_check_div1").hasClass("click_div1") ? 5 >= a ? layout_vnn.sizePane("north", "49%") : layout_vnn.sizePane("north", "33%") : 5 >= a ? layout_vnn.sizePane("north", "95%") : 27 >= a ? layout_vnn.sizePane("north", "92%") : layout_vnn.sizePane("north", "63%") : 2 == t && ($(".btn_check_div2").hasClass("click_div2") ? (layout_vnn.sizePane("north", "33%"), layout_vnn.sizePane("south", "33%")) : (layout_vnn.sizePane("south", "4%"), layout_vnn.sizePane("north", "49%"), setTimeout(function () {
		check_click_rutgon2($(".btn_check_div0"), 0),
		check_click_rutgon2($(".btn_check_div1"), 1)
	},
	100))),
	check_click_rutgon($(".btn_check_div" + t), t)
}
function load_detail(t, a, i, e, n) {
	var s = ".ui-layout-center > .ui-tabs-nav > .ui-state-active",
	l = ".ui-layout-south > .ui-tabs-nav > .ui-state-active",
	r = "";
	if (1 == a ? $(s).each(function () {
		return null == $(this).attr("style") ? (r = $(this).attr("aria-controls").replace("tabs_", ""), !1) : $(this).attr("style").indexOf("display: none;") <= -1 ? (r = $(this).attr("aria-controls").replace("tabs_", ""), !1) : void 0
	}) : 2 == a ? $(l).each(function () {
		return null == $(this).attr("style") ? (r = $(this).attr("aria-controls").replace("tabs_", ""), !1) : $(this).attr("style").indexOf("display: none;") <= -1 ? (r = $(this).attr("aria-controls").replace("tabs_", ""), !1) : void 0
	}) : (r = a, display_hidden(n, e, r)), 0 == t) {
		if (1 == a) {
			var o = ".ui-layout-north > .ui-tabs-nav > .ui-state-active";
			display_hidden(0, $(o).attr("sel_mod"), r)
		} else if (2 == a) {
			var o = ".ui-layout-center > .ui-tabs-nav > .ui-state-active";
			display_hidden(1, $(o).attr("sel_mod"), r)
		}
		if ("" != r) {
			1 == n && $("div.modcha_" + e).html(""),
			$("#tabs_" + r).html('<div class="nhan_loading0"></div>');
			try {
				xhr_menu.abort()
			} catch(c) {}
			enable_timer = !1,
			xhr_menu = $.get("View/Menu/Content/Module/" + r + ".aspx", function (t) {
				$("#tabs_" + r).html(t),
				2 != n && check_click_rutgon3(".btn_check_div"),
				enable_timer = !0
			})
		}
	} else try {
		if (0 == i) {
			var u = $("#" + tengrid0).jqGrid("getGridParam", "selrow");
			if (id_parent1 != u | 1 == load_grid0) {
				switch (id_parent1 = u, load_grid0) {
				case "3":
					$("#" + tengrid1).trigger("reloadGrid");
					break;
				default:
					$("#" + tengrid1)[0].triggerToolbar()
				}
				load_grid0 = 2
			}
		} else if (1 == i) {
			var u = $("#" + tengrid1).jqGrid("getGridParam", "selrow");
			id_parent2 != u | 1 == load_grid1 && (id_parent2 = u, load_grid1 = 2, $("#" + tengrid2)[0].triggerToolbar())
		}
	} catch(c) {}
}
function display_hidden(t, a) {
	var i = ".ui-layout-center > .ui-tabs-nav > ",
	e = ".ui-layout-south > .ui-tabs-nav > ",
	n = ".ui-layout-center > ",
	s = ".ui-layout-south > ";
	if (0 == t) {
		var l = i + ".ui-state-default",
		r = i + ".modcha_" + a,
		o = n + ".ui-tabs-panel",
		c = n + ".modcha_" + a;
		$(l).hide(),
		$(o).hide(),
		$(r).show(),
		$(c).show(),
		$(r).each(function () {
			if ($(this).hasClass("ui-state-active")) {
				var t = $("#" + $(this).attr("aria-controls"));
				return $(t).insertAfter(".ul_mod_1"),
				!1
			}
		}),
		null == $(r).first().attr("class") && (layout_vnn.sizePane("south", "1%"), layout_vnn.sizePane("north", "99%"))
	} else if (1 == t) {
		var u = e + ".ui-state-default",
		h = e + ".modcha_" + a,
		d = s + ".ui-tabs-panel",
		_ = s + ".modcha_" + a;
		$(u).hide(),
		$(d).hide(),
		$(h).show(),
		$(_).show();
		var v = 0,
		y = null,
		g = null;
		$(h).each(function (t) {
			return $(this).hasClass("ui-state-active") ? (g = $("#" + $(this).attr("aria-controls")), $(g).insertAfter(".ul_mod_2"), !1) : void(0 == t ? (y = $(this), g = $("#" + $(this).attr("aria-controls")), v++) : v++)
		}),
		v >= $(h).length && null != y & null != g && ($(y).addClass("ui-state-active"), $(g).insertAfter(".ul_mod_2")),
		null == $(h).first().attr("class") ? (layout_vnn.sizePane("north", "49%"), layout_vnn.sizePane("south", "1%")) : (layout_vnn.sizePane("north", "35%"), layout_vnn.sizePane("south", "33%"))
	}
}