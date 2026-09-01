/*
 * jquery.dragScroll v1.0.0
 * author 735126858@qq.com
 * https://github.com/YuTingtao/dragScroll.js
 */
; (function (factory) {
    if (typeof define === 'function' && define.amd) {
        define(['jquery'], factory);
    } else if (typeof exports !== 'undefined') {
        module.exports = factory(require('jquery'));
    } else {
        factory(jQuery);
    }
}(function ($) {
    var methods = {
        init: function (options) {
            var defaults = {
                direction: null,
                throttleTime: 50,  // 节流时间
                onStart: function () { },
                onMove: function () { },
                onEnd: function () { }
            };

            var opt = $.extend({}, defaults, options);

            return this.each(function () {
                let $this = $(this);

                let width = 0;
                $this.children('*').each(function () {
                    width += $(this).outerWidth(true);
                });
                let limit = Math.ceil(width - $this.outerWidth(true));

                let left0, top0, x0, y0, flag = false;
                if (/(Android|Adr|iPhone|iPad|iPod|iOS|Phone|SymbianOS)/i.test(navigator.userAgent)) {
                    $this.off('touchstart');
                    $this.on('touchstart', function (e) {
                        e = e.originalEvent.targetTouches[0];
                        flag = true;
                        x0 = e.clientX;
                        y0 = e.clientY;
                    });

                    $this.off('touchmove');
                    $this.on('touchmove', function (e) {
                        if (!flag)
                            return;
                        e.stopPropagation();
                        e = e.originalEvent.targetTouches[0];

                        let moveX = 8;
                        if (e.clientX > x0) {
                            x0 = e.clientX;
                            let idx = parseInt($this.css("marginLeft").replace('px', ''));
                            if (idx < 0)
                                $this.css('margin-left', idx + moveX);
                        }
                        else if (e.clientX < x0) {
                            x0 = e.clientX;
                            let idx = parseInt($this.css("marginLeft").replace('px', ''));
                            if (Math.abs(idx) <= limit)
                                $this.css('margin-left', idx - moveX);
                            console.log(Math.abs(idx), limit);
                        }
                    });

                    $this.off('touchend');
                    $this.on('touchend', function (e) {
                        flag = false;
                    });
                }
                else {
                    $this.off('mousedown');
                    $this.on('mousedown', function (e) {
                        flag = true;
                        x0 = e.clientX;
                        y0 = e.clientY;
                    });

                    $this.off('mousemove');
                    $this.on('mousemove', function (e) {
                        if (!flag)
                            return;
                        e.preventDefault();

                        let moveX = 4;
                        if (e.clientX > x0) {
                            x0 = e.clientX;
                            let idx = parseInt($this.css("marginLeft").replace('px', ''));
                            if (idx < 0)
                                $this.css('margin-left', idx + moveX);
                        }
                        else if (e.clientX < x0) {
                            x0 = e.clientX;
                            let idx = parseInt($this.css("marginLeft").replace('px', ''));
                            if (Math.abs(idx) <= limit)
                                $this.css('margin-left', idx - moveX);
                            console.log(Math.abs(idx), limit);
                        }
                    });

                    $this.off('mouseup mouseleave');
                    $this.on('mouseup mouseleave', function () {
                        flag = false;
                    });
                }
            });
        },
        destroy: function () {
            return $(this).each(function () {
                var $this = $(this);
                $this.off('mousedown mousemove mouseup mouseleave');
                $this.off('touchstart touchmove touchend');
            });
        }
    };

    $.fn.dragScroll = function (method) {
        if (methods[method]) {
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        } else if (typeof method === 'object' || !method) {
            return methods.init.apply(this, arguments);
        } else {
            $.error('method ' + method + ' does not exist on jquery.dragScroll.js');
        }
    }
}));
