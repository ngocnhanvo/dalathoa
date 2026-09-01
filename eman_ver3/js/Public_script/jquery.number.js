/*!function(e){"use strict";function t(e,t){if(this.createTextRange){var a=this.createTextRange();a.collapse(!0),a.moveStart("character",e),a.moveEnd("character",t-e),a.select()}else this.setSelectionRange&&(this.focus(),this.setSelectionRange(e,t))}function a(e){var t=this.value.length;if(e="start"==e.toLowerCase()?"Start":"End",document.selection){var a,i,n,l=document.selection.createRange();return a=l.duplicate(),a.expand("textedit"),a.setEndPoint("EndToEnd",l),i=a.text.length-l.text.length,n=i+l.text.length,"Start"==e?i:n}return"undefined"!=typeof this["selection"+e]&&(t=this["selection"+e]),t}var i=0,n={codes:{46:127,188:44,109:45,190:46,191:47,192:96,220:92,222:39,221:93,219:91,173:45,187:61,186:59,189:45,110:46},shifts:{96:"~",49:"!",50:"@",51:"#",52:"$",53:"%",54:"^",55:"&",56:"*",57:"(",48:")",45:"_",61:"+",91:"{",93:"}",92:"|",59:":",39:'"',44:"<",46:">",47:"?"}};e.fn.number=function(l,s,r,u){u="undefined"==typeof u?",":u,r="undefined"==typeof r?".":r,s="undefined"==typeof s?0:s;var h="\\u"+("0000"+r.charCodeAt(0).toString(16)).slice(-4),o=new RegExp("[^"+h+"0-9]","g"),c=new RegExp(h,"g");return l===!0?this.is("input:text")?this.on({"keydown.format":function(l){i=1;var h=e(this),o=h.data("numFormat"),c=l.keyCode?l.keyCode:l.which,v="",d=a.apply(this,["start"]),p=a.apply(this,["end"]),f="",g=!1;if(n.codes.hasOwnProperty(c)&&(c=n.codes[c]),!l.shiftKey&&c>=65&&90>=c?c+=32:!l.shiftKey&&c>=69&&105>=c?c-=48:l.shiftKey&&n.shifts.hasOwnProperty(c)&&(v=n.shifts[c]),""==v&&(v=String.fromCharCode(c)),8!=c&&45!=c&&127!=c&&v!=r&&!v.match(/[0-9]/)){var m=l.keyCode?l.keyCode:l.which;if(46==m||8==m||127==m||9==m||27==m||13==m||(65==m||82==m||80==m||83==m||70==m||72==m||66==m||74==m||84==m||90==m||61==m||173==m||48==m)&&(l.ctrlKey||l.metaKey)===!0||(86==m||67==m||88==m)&&(l.ctrlKey||l.metaKey)===!0||m>=35&&39>=m||m>=112&&123>=m)return;return l.preventDefault(),!1}if(0==d&&p==this.value.length?8==c?(d=p=1,this.value="",o.init=s>0?-1:0,o.c=s>0?-(s+1):0,t.apply(this,[0,0])):v==r?(d=p=1,this.value="0"+r+new Array(s+1).join("0"),o.init=s>0?1:0,o.c=s>0?-(s+1):0):45==c?(d=p=2,this.value="-0"+r+new Array(s+1).join("0"),o.init=s>0?1:0,o.c=s>0?-(s+1):0,t.apply(this,[2,2])):(o.init=s>0?-1:0,o.c=s>0?-s:0):o.c=p-this.value.length,o.isPartialSelection=d==p?!1:!0,s>0&&v==r&&d==this.value.length-s-1)o.c++,o.init=Math.max(0,o.init),l.preventDefault(),g=this.value.length+o.c;else if(45!=c||0==d&&0!=this.value.indexOf("-"))if(v==r)o.init=Math.max(0,o.init),l.preventDefault();else if(s>0&&127==c&&d==this.value.length-s-1)l.preventDefault();else if(s>0&&8==c&&d==this.value.length-s)l.preventDefault(),o.c--,g=this.value.length+o.c;else if(s>0&&127==c&&d>this.value.length-s-1){if(""===this.value)return;"0"!=this.value.slice(d,d+1)&&(f=this.value.slice(0,d)+"0"+this.value.slice(d+1),h.val(f)),l.preventDefault(),g=this.value.length+o.c}else if(s>0&&8==c&&d>this.value.length-s){if(""===this.value)return;"0"!=this.value.slice(d-1,d)&&(f=this.value.slice(0,d-1)+"0"+this.value.slice(d),h.val(f)),l.preventDefault(),o.c--,g=this.value.length+o.c}else 127==c&&this.value.slice(d,d+1)==u?l.preventDefault():8==c&&this.value.slice(d-1,d)==u?(l.preventDefault(),o.c--,g=this.value.length+o.c):s>0&&d==p&&this.value.length>s+1&&d>this.value.length-s-1&&isFinite(+v)&&!l.metaKey&&!l.ctrlKey&&!l.altKey&&1===v.length&&(f=p===this.value.length?this.value.slice(0,d-1):this.value.slice(0,d)+this.value.slice(d+1),this.value=f,g=d);else l.preventDefault();g!==!1&&t.apply(this,[g,g]),h.data("numFormat",o)},"keyup.format":function(n){i=1;var l,r=e(this),u=r.data("numFormat"),h=n.keyCode?n.keyCode:n.which,o=a.apply(this,["start"]),c=a.apply(this,["end"]);0!==o||0!==c||189!==h&&109!==h||(r.val("-"+r.val()),o=1,u.c=1-this.value.length,u.init=1,r.data("numFormat",u),l=this.value.length+u.c,t.apply(this,[l,l])),""===this.value||(48>h||h>57)&&(96>h||h>105)&&8!==h&&46!==h&&110!==h||(r.val(r.val()),s>0&&(u.init<1?(o=this.value.length-s-(u.init<0?1:0),u.c=o-this.value.length,u.init=1,r.data("numFormat",u)):o>this.value.length-s&&8!=h&&(u.c++,r.data("numFormat",u))),46!=h||u.isPartialSelection||(u.c++,r.data("numFormat",u)),l=this.value.length+u.c,t.apply(this,[l,l]))},"paste.format":function(t){var a=e(this),i=t.originalEvent,n=null;return window.clipboardData&&window.clipboardData.getData?n=window.clipboardData.getData("Text"):i.clipboardData&&i.clipboardData.getData&&(n=i.clipboardData.getData("text/plain")),a.val(n),t.preventDefault(),!1}}).each(function(){var t=e(this).data("numFormat",{c:-(s+1),decimals:s,thousands_sep:u,dec_point:r,regex_dec_num:o,regex_dec:c,init:this.value.indexOf(".")?!0:!1});""!==this.value&&t.val(t.val())}):this.each(function(){var t=e(this),a=+t.text().replace(o,"").replace(c,".");t.number(isFinite(a)?+a:0,s,r,u)}):this.text(e.number.apply(window,arguments))};var l=null,s=null;e.isPlainObject(e.valHooks.text)?(e.isFunction(e.valHooks.text.get)&&(l=e.valHooks.text.get),e.isFunction(e.valHooks.text.set)&&(s=e.valHooks.text.set)):e.valHooks.text={},e.valHooks.text.get=function(t){var a,i=e(t),n=i.data("numFormat");return n?""===t.value?"":(a=+t.value.replace(n.regex_dec_num,"").replace(n.regex_dec,"."),(0===t.value.indexOf("-")?"-":"")+(isFinite(a)?a:0)):e.isFunction(l)?l(t):void 0},e.valHooks.text.set=function(t,a){var i=e(t),n=i.data("numFormat");if(n){var l=e.number(a,n.decimals,n.dec_point,n.thousands_sep);return e.isFunction(s)?s(t,l):t.value=l}return e.isFunction(s)?s(t,a):void 0},e.number=function(e,t,a,n){n="undefined"==typeof n?"1000"!==new Number(1e3).toLocaleString()?new Number(1e3).toLocaleString().charAt(1):"":n,a="undefined"==typeof a?new Number(.1).toLocaleString().charAt(1):a,t=isFinite(+t)?Math.abs(t):0;var l="\\u"+("0000"+a.charCodeAt(0).toString(16)).slice(-4),s="\\u"+("0000"+n.charCodeAt(0).toString(16)).slice(-4),r=e;if(0==i){if(","==n)r=e.replace(new RegExp(n,"g"),"");else for(;r.indexOf(n)>-1;)r=r.replace(".","");e=r}else i=0;e=(e+"").replace(".",a).replace(new RegExp(s,"g"),"").replace(new RegExp(l,"g"),".").replace(new RegExp("[^0-9+-Ee.]","g"),"");var u=isFinite(+e)?+e:0,h="",o=function(e,t){return""+ +(Math.round(e+"e+"+t)+"e-"+t)};return h=(t?o(u,t):""+Math.round(u)).split("."),h[0].length>3&&(h[0]=h[0].replace(/\B(?=(?:\d{3})+(?!\d))/g,n)),(h[1]||"").length<t&&(h[1]=h[1]||"",h[1]+=new Array(t-h[1].length+1).join("0")),h.join(a)}}(jQuery);*/
/**
 * jQuery number plug-in 2.1.5
 * Copyright 2012, Digital Fusion
 * Licensed under the MIT license.
 * http://opensource.teamdf.com/license/
 *
 * A jQuery plugin which implements a permutation of phpjs.org's number_format to provide
 * simple number formatting, insertion, and as-you-type masking of a number.
 *
 * @author	Sam Sehnert
 * @docs	http://www.teamdf.com/web/jquery-number-format-redux/196/
 */
(function ($) {

    "use strict";

	/**
	 * Method for selecting a range of characters in an input/textarea.
	 *
	 * @param int rangeStart			: Where we want the selection to start.
	 * @param int rangeEnd				: Where we want the selection to end.
	 *
	 * @return void;
	 */
    var vnn_number = 0, vnn_firstload = 0;
    function setSelectionRange(rangeStart, rangeEnd) {
        // Check which way we need to define the text range.
        if (this.createTextRange) {
            var range = this.createTextRange();
            range.collapse(true);
            range.moveStart('character', rangeStart);
            range.moveEnd('character', rangeEnd - rangeStart);
            range.select();
        }

        // Alternate setSelectionRange method for supporting browsers.
        else if (this.setSelectionRange) {
            this.focus();
            this.setSelectionRange(rangeStart, rangeEnd);
        }
    }

	/**
	 * Get the selection position for the given part.
	 *
	 * @param string part			: Options, 'Start' or 'End'. The selection position to get.
	 *
	 * @return int : The index position of the selection part.
	 */
    function getSelection(part) {
        var pos = this.value.length;

        // Work out the selection part.
        part = (part.toLowerCase() == 'start' ? 'Start' : 'End');

        if (document.selection) {
            // The current selection
            var range = document.selection.createRange(), stored_range, selectionStart, selectionEnd;
            // We'll use this as a 'dummy'
            stored_range = range.duplicate();
            // Select all text
            //stored_range.moveToElementText( this );
            stored_range.expand('textedit');
            // Now move 'dummy' end point to end point of original range
            stored_range.setEndPoint('EndToEnd', range);
            // Now we can calculate start and end points
            selectionStart = stored_range.text.length - range.text.length;
            selectionEnd = selectionStart + range.text.length;
            return part == 'Start' ? selectionStart : selectionEnd;
        }

        else if (typeof (this['selection' + part]) != "undefined") {
            pos = this['selection' + part];
        }
        return pos;
    }

	/**
	 * Substitutions for keydown keycodes.
	 * Allows conversion from e.which to ascii characters.
	 */
    var _keydown = {
        codes: {
            46: 127,
            188: 44,
            109: 45,
            190: 46,
            191: 47,
            192: 96,
            220: 92,
            222: 39,
            221: 93,
            219: 91,
            173: 45,
            187: 61, //IE Key codes
            186: 59, //IE Key codes
            189: 45, //IE Key codes
            110: 46  //IE Key codes
        },
        shifts: {
            96: "~",
            49: "!",
            50: "@",
            51: "#",
            52: "$",
            53: "%",
            54: "^",
            55: "&",
            56: "*",
            57: "(",
            48: ")",
            45: "_",
            61: "+",
            91: "{",
            93: "}",
            92: "|",
            59: ":",
            39: "\"",
            44: "<",
            46: ">",
            47: "?"
        }
    };

	/**
	 * jQuery number formatter plugin. This will allow you to format numbers on an element.
	 *
	 * @params proxied for format_number method.
	 *
	 * @return : The jQuery collection the method was called with.
	 */
    $.fn.number = function (number, decimals, dec_point, thousands_sep) {
        // Enter the default thousands separator, and the decimal placeholder.
        thousands_sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep;
        dec_point = (typeof dec_point === 'undefined') ? '.' : dec_point;
        decimals = (typeof decimals === 'undefined') ? 0 : decimals;

        // Work out the unicode character for the decimal placeholder.
        var u_dec = ('\\u' + ('0000' + (dec_point.charCodeAt(0).toString(16))).slice(-4)),
            regex_dec_num = new RegExp('[^' + u_dec + '0-9]', 'g'),
            regex_dec = new RegExp(u_dec, 'g');

        // If we've specified to take the number from the target element,
        // we loop over the collection, and get the number.
        if (number === true) {
            // If this element is a number, then we add a keyup
            if (this.is('input:text')) {
                vnn_firstload = 1;
                // Return the jquery collection.
                return this.on({

					/**
					 * Handles keyup events, re-formatting numbers.
					 *
					 * Uses 'data' object to keep track of important information.
					 *
					 * data.c
					 * This variable keeps track of where the caret *should* be. It works out the position as
					 * the number of characters from the end of the string. E.g., '1^,234.56' where ^ denotes the caret,
					 * would be index -7 (e.g., 7 characters from the end of the string). At the end of both the key down
					 * and key up events, we'll re-position the caret to wherever data.c tells us the cursor should be.
					 * This gives us a mechanism for incrementing the cursor position when we come across decimals, commas
					 * etc. This figure typically doesn't increment for each keypress when to the left of the decimal,
					 * but does when to the right of the decimal.
					 *
					 * @param object e			: the keyup event object.s
					 *
					 * @return void;
					 */
                    'keydown.format': function (e) {
                        vnn_number = 1;
                        // Define variables used in the code below.
                        var $this = $(this),
                            data = $this.data('numFormat'),
                            code = (e.keyCode ? e.keyCode : e.which),
                            chara = '', //unescape(e.originalEvent.keyIdentifier.replace('U+','%u')),
                            start = getSelection.apply(this, ['start']),
                            end = getSelection.apply(this, ['end']),
                            val = '',
                            setPos = false;

                        // Webkit (Chrome & Safari) on windows screws up the keyIdentifier detection
                        // for numpad characters. I've disabled this for now, because while keyCode munging
                        // below is hackish and ugly, it actually works cross browser & platform.

                        //						if( typeof e.originalEvent.keyIdentifier !== 'undefined' )
                        //						{
                        //							chara = unescape(e.originalEvent.keyIdentifier.replace('U+','%u'));
                        //						}
                        //						else
                        //						{
                        if (_keydown.codes.hasOwnProperty(code)) {
                            code = _keydown.codes[code];
                        }
                        if (!e.shiftKey && (code >= 65 && code <= 90)) {
                            code += 32;
                        } else if (!e.shiftKey && (code >= 69 && code <= 105)) {
                            code -= 48;
                        } else if (e.shiftKey && _keydown.shifts.hasOwnProperty(code)) {
                            //get shifted keyCode value
                            chara = _keydown.shifts[code];
                        }

                        if (chara == '') chara = String.fromCharCode(code);
                        //						}




                        // Stop executing if the user didn't type a number key, a decimal character, backspace, or delete.
                        if (code != 8 && code != 45 && code != 127 && chara != dec_point && !chara.match(/[0-9]/)) {
                            // We need the original keycode now...
                            var key = (e.keyCode ? e.keyCode : e.which);
                            if ( // Allow control keys to go through... (delete, backspace, tab, enter, escape etc)
                                key == 46 || key == 8 || key == 127 || key == 9 || key == 27 || key == 13 ||
                                // Allow: Ctrl+A, Ctrl+R, Ctrl+P, Ctrl+S, Ctrl+F, Ctrl+H, Ctrl+B, Ctrl+J, Ctrl+T, Ctrl+Z, Ctrl++, Ctrl+-, Ctrl+0
                                ((key == 65 || key == 82 || key == 80 || key == 83 || key == 70 || key == 72 || key == 66 || key == 74 || key == 84 || key == 90 || key == 61 || key == 173 || key == 48) && (e.ctrlKey || e.metaKey) === true) ||
                                // Allow: Ctrl+V, Ctrl+C, Ctrl+X
                                ((key == 86 || key == 67 || key == 88) && (e.ctrlKey || e.metaKey) === true) ||
                                // Allow: home, end, left, right
                                ((key >= 35 && key <= 39)) ||
                                // Allow: F1-F12
                                ((key >= 112 && key <= 123)) ||
                                // Allow: alt A, alt S, alt D
                                ((key == 65 || key == 68 || key == 83) && (e.altKey) === true)
                            ) {
                                return;
                            }

                            // But prevent all other keys.
                            e.preventDefault();
                            return false;
                        }

                        // The whole lot has been selected, or if the field is empty...
                        if (start == 0 && end == this.value.length) //|| $this.val() == 0 )
                        {
                            if (code == 8)		// Backspace
                            {
                                // Blank out the field, but only if the data object has already been instantiated.
                                start = end = 1;
                                this.value = '';

                                // Reset the cursor position.
                                data.init = (decimals > 0 ? -1 : 0);
                                data.c = (decimals > 0 ? -(decimals + 1) : 0);
                                setSelectionRange.apply(this, [0, 0]);
                            }
                            else if (chara == dec_point) {
                                start = end = 1;
                                this.value = '0' + dec_point + (new Array(decimals + 1).join('0'));

                                // Reset the cursor position.
                                data.init = (decimals > 0 ? 1 : 0);
                                data.c = (decimals > 0 ? -(decimals + 1) : 0);
                            }
                            else if (code == 45)	// Negative sign
                            {
                                start = end = 2;
                                this.value = '-0' + dec_point + (new Array(decimals + 1).join('0'));

                                // Reset the cursor position.
                                data.init = (decimals > 0 ? 1 : 0);
                                data.c = (decimals > 0 ? -(decimals + 1) : 0);

                                setSelectionRange.apply(this, [2, 2]);
                            }
                            else {
                                // Reset the cursor position.
                                data.init = (decimals > 0 ? -1 : 0);
                                data.c = (decimals > 0 ? -(decimals) : 0);
                            }
                        }

                        // Otherwise, we need to reset the caret position
                        // based on the users selection.
                        else {
                            data.c = end - this.value.length;
                        }

                        // Track if partial selection was used
                        data.isPartialSelection = start == end ? false : true;

                        // If the start position is before the decimal point,
                        // and the user has typed a decimal point, we need to move the caret
                        // past the decimal place.
                        if (decimals > 0 && chara == dec_point && start == this.value.length - decimals - 1) {
                            data.c++;
                            data.init = Math.max(0, data.init);
                            e.preventDefault();

                            // Set the selection position.
                            setPos = this.value.length + data.c;
                        }

                        // Ignore negative sign unless at beginning of number (and it's not already present)
                        else if (code == 45 && (start != 0 || this.value.indexOf('-') == 0)) {
                            e.preventDefault();
                        }

                        // If the user is just typing the decimal place,
                        // we simply ignore it.
                        else if (chara == dec_point) {
                            data.init = Math.max(0, data.init);
                            e.preventDefault();
                        }

                        // If hitting the delete key, and the cursor is before a decimal place,
                        // we simply move the cursor to the other side of the decimal place.
                        else if (decimals > 0 && code == 127 && start == this.value.length - decimals - 1) {
                            // Just prevent default but don't actually move the caret here because it's done in the keyup event
                            e.preventDefault();
                        }

                        // If hitting the backspace key, and the cursor is behind a decimal place,
                        // we simply move the cursor to the other side of the decimal place.
                        else if (decimals > 0 && code == 8 && start == this.value.length - decimals) {
                            e.preventDefault();
                            data.c--;

                            // Set the selection position.
                            setPos = this.value.length + data.c;
                        }

                        // If hitting the delete key, and the cursor is to the right of the decimal
                        // we replace the character after the caret with a 0.
                        else if (decimals > 0 && code == 127 && start > this.value.length - decimals - 1) {
                            if (this.value === '') return;

                            // If the character following is not already a 0,
                            // replace it with one.
                            if (this.value.slice(start, start + 1) != '0') {
                                val = this.value.slice(0, start) + '0' + this.value.slice(start + 1);
                                // The regex replacement below removes negative sign from numbers...
                                // not sure why they're necessary here when none of the other cases use them
                                //$this.val(val.replace(regex_dec_num,'').replace(regex_dec,dec_point));
                                $this.val(val);
                            }

                            e.preventDefault();

                            // Set the selection position.
                            setPos = this.value.length + data.c;
                        }

                        // If hitting the backspace key, and the cursor is to the right of the decimal
                        // (but not directly to the right) we replace the character preceding the
                        // caret with a 0.
                        else if (decimals > 0 && code == 8 && start > this.value.length - decimals) {
                            if (this.value === '') return;

                            // If the character preceding is not already a 0,
                            // replace it with one.
                            if (this.value.slice(start - 1, start) != '0') {
                                val = this.value.slice(0, start - 1) + '0' + this.value.slice(start);
                                // The regex replacement below removes negative sign from numbers...
                                // not sure why they're necessary here when none of the other cases use them
                                //$this.val(val.replace(regex_dec_num,'').replace(regex_dec,dec_point));
                                $this.val(val);
                            }

                            e.preventDefault();
                            data.c--;

                            // Set the selection position.
                            setPos = this.value.length + data.c;
                        }

                        // If the delete key was pressed, and the character immediately
                        // after the caret is a thousands_separator character, simply
                        // step over it.
                        else if (code == 127 && this.value.slice(start, start + 1) == thousands_sep) {
                            // Just prevent default but don't actually move the caret here because it's done in the keyup event
                            e.preventDefault();
                        }

                        // If the backspace key was pressed, and the character immediately
                        // before the caret is a thousands_separator character, simply
                        // step over it.
                        else if (code == 8 && this.value.slice(start - 1, start) == thousands_sep) {
                            e.preventDefault();
                            data.c--;

                            // Set the selection position.
                            setPos = this.value.length + data.c;
                        }

                        // If the caret is to the right of the decimal place, and the user is entering a
                        // number, remove the following character before putting in the new one.
                        else if (
                            decimals > 0 &&
                            start == end &&
                            this.value.length > decimals + 1 &&
                            start > this.value.length - decimals - 1 && isFinite(+chara) &&
                            !e.metaKey && !e.ctrlKey && !e.altKey && chara.length === 1
                        ) {
                            // If the character preceding is not already a 0,
                            // replace it with one.
                            if (end === this.value.length) {
                                val = this.value.slice(0, start - 1);
                            }
                            else {
                                val = this.value.slice(0, start) + this.value.slice(start + 1);
                            }

                            // Reset the position.
                            this.value = val;
                            setPos = start;
                        }

                        // If we need to re-position the characters.
                        if (setPos !== false) {
                            //console.log('Setpos keydown: ', setPos );
                            setSelectionRange.apply(this, [setPos, setPos]);
                        }

                        // Store the data on the element.
                        $this.data('numFormat', data);

                    },

					/**
					 * Handles keyup events, re-formatting numbers.
					 *
					 * @param object e			: the keyup event object.s
					 *
					 * @return void;
					 */
                    'keyup.format': function (e) {
                        vnn_number = 1;
                        // Store these variables for use below.
                        var $this = $(this),
                            data = $this.data('numFormat'),
                            code = (e.keyCode ? e.keyCode : e.which),
                            start = getSelection.apply(this, ['start']),
                            end = getSelection.apply(this, ['end']),
                            setPos;


                        // Check for negative characters being entered at the start of the string.
                        // If there's any kind of selection, just ignore the input.
                        if (start === 0 && end === 0 && (code === 189 || code === 109)) {
                            $this.val('-' + $this.val());
                            start = 1;
                            data.c = 1 - this.value.length;
                            data.init = 1;

                            $this.data('numFormat', data);

                            setPos = this.value.length + data.c;
                            setSelectionRange.apply(this, [setPos, setPos]);
                        }

                        // Stop executing if the user didn't type a number key, a decimal, or a comma.
                        if (this.value === '' || (code < 48 || code > 57) && (code < 96 || code > 105) && code !== 8 && code !== 46 && code !== 110) return;

                        // Re-format the textarea.
                        $this.val($this.val());

                        if (decimals > 0) {
                            // If we haven't marked this item as 'initialized'
                            // then do so now. It means we should place the caret just
                            // before the decimal. This will never be un-initialized before
                            // the decimal character itself is entered.
                            if (data.init < 1) {
                                start = this.value.length - decimals - (data.init < 0 ? 1 : 0);
                                data.c = start - this.value.length;
                                data.init = 1;


                                $this.data('numFormat', data);
                            }

                            // Increase the cursor position if the caret is to the right
                            // of the decimal place, and the character pressed isn't the backspace key.
                            else if (start > this.value.length - decimals && code != 8) {
                                data.c++;

                                // Store the data, now that it's changed.
                                $this.data('numFormat', data);
                            }
                        }

                        // Move caret to the right after delete key pressed
                        if (code == 46 && !data.isPartialSelection) {
                            data.c++;

                            // Store the data, now that it's changed.
                            $this.data('numFormat', data);
                        }

                        //console.log( 'Setting pos: ', start, decimals, this.value.length + data.c, this.value.length, data.c );

                        // Set the selection position.
                        setPos = this.value.length + data.c;
                        setSelectionRange.apply(this, [setPos, setPos]);
                    },

					/**
					 * Reformat when pasting into the field.
					 *
					 * @param object e 		: jQuery event object.
					 *
					 * @return false : prevent default action.
					 */
                    'paste.format': function (e) {
                        // Defint $this. It's used twice!.
                        var $this = $(this),
                            original = e.originalEvent,
                            val = null;

                        // Get the text content stream.
                        if (window.clipboardData && window.clipboardData.getData) { // IE
                            val = window.clipboardData.getData('Text');
                        } else if (original.clipboardData && original.clipboardData.getData) {
                            val = original.clipboardData.getData('text/plain');
                        }

                        // Do the reformat operation.
                        $this.val(val);

                        // Stop the actual content from being pasted.
                        e.preventDefault();
                        return false;
                    }

                })

                    // Loop each element (which isn't blank) and do the format.
                    .each(function () {

                        var $this = $(this).data('numFormat', {
                            c: -(decimals + 1),
                            decimals: decimals,
                            thousands_sep: thousands_sep,
                            dec_point: dec_point,
                            regex_dec_num: regex_dec_num,
                            regex_dec: regex_dec,
                            init: this.value.indexOf('.') ? true : false
                        });

                        // Return if the element is empty.
                        if (this.value == '' | this.value == null) return;
                        // Otherwise... format!!
                        $this.val($this.val());
                    });
            }
            else {
                // return the collection.
                return this.each(function () {
                    var $this = $(this), num = +$this.text().replace(regex_dec_num, '').replace(regex_dec, '.');
                    $this.number(!isFinite(num) ? 0 : +num, decimals, dec_point, thousands_sep);
                });
            }
        }

        // Add this number to the element as text.
        return this.text($.number.apply(window, arguments));
    };

    //
    // Create .val() hooks to get and set formatted numbers in inputs.
    //

    // We check if any hooks already exist, and cache
    // them in case we need to re-use them later on.
    var origHookGet = null, origHookSet = null;

    // Check if a text valHook already exists.

    if ($.isPlainObject($.valHooks.text)) {
        // Preserve the original valhook function
        // we'll call this for values we're not
        // explicitly handling.
        if ($.isFunction($.valHooks.text.get)) origHookGet = $.valHooks.text.get;
        if ($.isFunction($.valHooks.text.set)) origHookSet = $.valHooks.text.set;
    }
    else {
        // Define an object for the new valhook.
        $.valHooks.text = {};
    }

	/**
	* Define the valHook to return normalised field data against an input
	* which has been tagged by the number formatter.
	*
	* @param object el			: The raw DOM element that we're getting the value from.
	*
	* @return mixed : Returns the value that was written to the element as a
	*				  javascript number, or undefined to let jQuery handle it normally.
	*/
    $.valHooks.text.get = function (el) {

        // Get the element, and its data.
        var $this = $(el), num, negative,
            data = $this.data('numFormat');

        // Does this element have our data field?
        if (!data) {
            // Check if the valhook function already existed
            if ($.isFunction(origHookGet)) {
                // There was, so go ahead and call it
                return origHookGet(el);
            }
            else {
                // No previous function, return undefined to have jQuery
                // take care of retrieving the value
                return undefined;
            }
        }
        else {
            // Remove formatting, and return as number.
            if (el.value === '') return '';


            // Convert to a number.
            num = +(el.value
                .replace(data.regex_dec_num, '')
                .replace(data.regex_dec, '.'));

            // If we've got a finite number, return it.
            // Otherwise, simply return 0.
            // Return as a string... thats what we're
            // used to with .val()
            return (el.value.indexOf('-') === 0 ? '-' : '') + (isFinite(num) ? num : 0);
        }
    };

	/**
	* A valhook which formats a number when run against an input
	* which has been tagged by the number formatter.
	*
	* @param object el		: The raw DOM element (input element).
	* @param float			: The number to set into the value field.
	*
	* @return mixed : Returns the value that was written to the element,
	*				  or undefined to let jQuery handle it normally.
	*/
    $.valHooks.text.set = function (el, val) {
        // Get the element, and its data.
        var $this = $(el),
            data = $this.data('numFormat');

        // Does this element have our data field?
        if (!data) {

            // Check if the valhook function already exists
            if ($.isFunction(origHookSet)) {
                // There was, so go ahead and call it
                return origHookSet(el, val);
            }
            else {
                // No previous function, return undefined to have jQuery
                // take care of retrieving the value
                return undefined;
            }
        }
        else {
            let isNum = val != null & val != '';
            if (isNum == 0)
                return;

            var num = $.number(val, data.decimals, data.dec_point, data.thousands_sep);
            // Make sure empties are set with correct signs.
            //			if(val.indexOf('-') === 0 && +num === 0)
            //			{
            //				num = '-'+num;
            //			}
            return $.isFunction(origHookSet) ? origHookSet(el, num) : el.value = num;
        }
    };

	/**
	 * The (modified) excellent number formatting method from PHPJS.org.
	 * http://phpjs.org/functions/number_format/
	 *
	 * @modified by Sam Sehnert (teamdf.com)
	 *	- don't redefine dec_point, thousands_sep... just overwrite with defaults.
	 *	- don't redefine decimals, just overwrite as numeric.
	 *	- Generate regex for normalizing pre-formatted numbers.
	 *
	 * @param float number			: The number you wish to format, or TRUE to use the text contents
	 *								  of the element as the number. Please note that this won't work for
	 *								  elements which have child nodes with text content.
	 * @param int decimals			: The number of decimal places that should be displayed. Defaults to 0.
	 * @param string dec_point		: The character to use as a decimal point. Defaults to '.'.
	 * @param string thousands_sep	: The character to use as a thousands separator. Defaults to ','.
	 *
	 * @return string : The formatted number as a string.
	 */
    $.number = function (number, decimals, dec_point, thousands_sep) {

        // Set the default values here, instead so we can use them in the replace below.
        thousands_sep = (typeof thousands_sep === 'undefined') ? (new Number(1000).toLocaleString() !== '1000' ? new Number(1000).toLocaleString().charAt(1) : '') : thousands_sep;
        dec_point = (typeof dec_point === 'undefined') ? new Number(0.1).toLocaleString().charAt(1) : dec_point;
        decimals = !isFinite(+decimals) ? 0 : Math.abs(decimals);

        // Work out the unicode representation for the decimal place and thousand sep.
        var u_dec = ('\\u' + ('0000' + (dec_point.charCodeAt(0).toString(16))).slice(-4));
        var u_sep = ('\\u' + ('0000' + (thousands_sep.charCodeAt(0).toString(16))).slice(-4));
        // Fix the number, so that it's an actual number.
        var t_s_nb = number;
        if (vnn_number == 0) {
            if (thousands_sep == ',')
                t_s_nb = number.replace(new RegExp(thousands_sep, 'g'), '');
            else {
                if (vnn_firstload == 2) {
                    while (t_s_nb.indexOf(thousands_sep) > -1) {
                        t_s_nb = t_s_nb.replace('.', '');
                    };
                }
                else {
                    vnn_firstload = 2;
                }
            }
            number = t_s_nb;
        }
        else
            vnn_number = 0;


        number = (number + '')
            .replace('\.', dec_point) // because the number if passed in as a float (having . as decimal point per definition) we need to replace this with the passed in decimal point character
            .replace(new RegExp(u_sep, 'g'), '')
            .replace(new RegExp(u_dec, 'g'), '.')
            .replace(new RegExp('[^0-9+\-Ee.]', 'g'), '');

        var n = !isFinite(+number) ? 0 : +number,
            s = '',
            toFixedFix = function (n, decimals) {
                return '' + (+(Math.round(n + 'e+' + decimals) + 'e-' + decimals));
            };

        // Fix for IE parseFloat(0.55).toFixed(0) = 0;
        s = (decimals ? toFixedFix(n, decimals) : '' + Math.round(n)).split('.');
        if (s[0].length > 3) {
            s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, thousands_sep);
        }
        if ((s[1] || '').length < decimals) {
            s[1] = s[1] || '';
            s[1] += new Array(decimals - s[1].length + 1).join('0');
        }

        return s.join(dec_point);
    }

})(jQuery);

//js/Public_script/jquery.numeric.min.js
(function (factory) { if (typeof define === 'function' && define.amd) { define(['jquery'], factory); } else { factory(window.jQuery); } }(function ($) { $.fn.numeric = function (config, callback) { if (typeof config === "boolean") { config = { decimal: config, negative: true, decimalPlaces: -1 } } config = config || {}; if (typeof config.negative == "undefined") { config.negative = true } var decimal = config.decimal === false ? "" : config.decimal || "."; var negative = config.negative === true ? true : false; var decimalPlaces = typeof config.decimalPlaces == "undefined" ? -1 : config.decimalPlaces; callback = typeof callback == "function" ? callback : function () { }; return this.data("numeric.decimal", decimal).data("numeric.negative", negative).data("numeric.callback", callback).data("numeric.decimalPlaces", decimalPlaces).keypress($.fn.numeric.keypress).keyup($.fn.numeric.keyup).blur($.fn.numeric.blur) }; $.fn.numeric.keypress = function (e) { var decimal = $.data(this, "numeric.decimal"); var negative = $.data(this, "numeric.negative"); var decimalPlaces = $.data(this, "numeric.decimalPlaces"); var key = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0; if (key == 13 && this.nodeName.toLowerCase() == "input") { return true } else if (key == 13) { return false } var allow = false; if (e.ctrlKey && key == 97 || e.ctrlKey && key == 65) { return true } if (e.ctrlKey && key == 120 || e.ctrlKey && key == 88) { return true } if (e.ctrlKey && key == 99 || e.ctrlKey && key == 67) { return true } if (e.ctrlKey && key == 122 || e.ctrlKey && key == 90) { return true } if (e.ctrlKey && key == 118 || e.ctrlKey && key == 86 || e.shiftKey && key == 45) { return true } if (key < 48 || key > 57) { var value = $(this).val(); if ($.inArray("-", value.split("")) !== 0 && negative && key == 45 && (value.length === 0 || parseInt($.fn.getSelectionStart(this), 10) === 0)) { return true } if (decimal && key == decimal.charCodeAt(0) && $.inArray(decimal, value.split("")) != -1) { allow = false } if (key != 8 && key != 9 && key != 13 && key != 35 && key != 36 && key != 37 && key != 39 && key != 46) { allow = false } else { if (typeof e.charCode != "undefined") { if (e.keyCode == e.which && e.which !== 0) { allow = true; if (e.which == 46) { allow = false } } else if (e.keyCode !== 0 && e.charCode === 0 && e.which === 0) { allow = true } } } if (decimal && key == decimal.charCodeAt(0)) { if ($.inArray(decimal, value.split("")) == -1) { allow = true } else { allow = false } } } else { allow = true; if (decimal && decimalPlaces > 0) { var dot = $.inArray(decimal, $(this).val().split("")); if (dot >= 0 && $(this).val().length > dot + decimalPlaces) { allow = false } } } return allow }; $.fn.numeric.keyup = function (e) { var val = $(this).val(); if (val && val.length > 0) { var carat = $.fn.getSelectionStart(this); var selectionEnd = $.fn.getSelectionEnd(this); var decimal = $.data(this, "numeric.decimal"); var negative = $.data(this, "numeric.negative"); var decimalPlaces = $.data(this, "numeric.decimalPlaces"); if (decimal !== "" && decimal !== null) { var dot = $.inArray(decimal, val.split("")); if (dot === 0) { this.value = "0" + val; carat++; selectionEnd++ } if (dot == 1 && val.charAt(0) == "-") { this.value = "-0" + val.substring(1); carat++; selectionEnd++ } val = this.value } var validChars = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, "-", decimal]; var length = val.length; for (var i = length - 1; i >= 0; i--) { var ch = val.charAt(i); if (i !== 0 && ch == "-") { val = val.substring(0, i) + val.substring(i + 1) } else if (i === 0 && !negative && ch == "-") { val = val.substring(1) } var validChar = false; for (var j = 0; j < validChars.length; j++) { if (ch == validChars[j]) { validChar = true; break } } if (!validChar || ch == " ") { val = val.substring(0, i) + val.substring(i + 1) } } var firstDecimal = $.inArray(decimal, val.split("")); if (firstDecimal > 0) { for (var k = length - 1; k > firstDecimal; k--) { var chch = val.charAt(k); if (chch == decimal) { val = val.substring(0, k) + val.substring(k + 1) } } } if (decimal && decimalPlaces > 0) { var dot = $.inArray(decimal, val.split("")); if (dot >= 0) { val = val.substring(0, dot + decimalPlaces + 1); selectionEnd = Math.min(val.length, selectionEnd) } } this.value = val; $.fn.setSelection(this, [carat, selectionEnd]) } }; $.fn.numeric.blur = function () { var decimal = $.data(this, "numeric.decimal"); var callback = $.data(this, "numeric.callback"); var negative = $.data(this, "numeric.negative"); var val = this.value; if (val !== "") { var re = new RegExp("^" + (negative ? "-?" : "") + "\\d+$|^" + (negative ? "-?" : "") + "\\d*" + decimal + "\\d+$"); if (!re.exec(val)) { callback.apply(this) } } }; $.fn.removeNumeric = function () { return this.data("numeric.decimal", null).data("numeric.negative", null).data("numeric.callback", null).data("numeric.decimalPlaces", null).unbind("keypress", $.fn.numeric.keypress).unbind("keyup", $.fn.numeric.keyup).unbind("blur", $.fn.numeric.blur) }; $.fn.getSelectionStart = function (o) { if (o.type === "number") { return undefined } else if (o.createTextRange && document.selection) { var r = document.selection.createRange().duplicate(); r.moveEnd("character", o.value.length); if (r.text == "") return o.value.length; return Math.max(0, o.value.lastIndexOf(r.text)) } else { try { return o.selectionStart } catch (e) { return 0 } } }; $.fn.getSelectionEnd = function (o) { if (o.type === "number") { return undefined } else if (o.createTextRange && document.selection) { var r = document.selection.createRange().duplicate(); r.moveStart("character", -o.value.length); return r.text.length } else return o.selectionEnd }; $.fn.setSelection = function (o, p) { if (typeof p == "number") { p = [p, p] } if (p && p.constructor == Array && p.length == 2) { if (o.type === "number") { o.focus() } else if (o.createTextRange) { var r = o.createTextRange(); r.collapse(true); r.moveStart("character", p[0]); r.moveEnd("character", p[1] - p[0]); r.select() } else { o.focus(); try { if (o.setSelectionRange) { o.setSelectionRange(p[0], p[1]) } } catch (e) { } } } } }));
