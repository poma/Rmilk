﻿var login = '$$login';
var password = '$$password';

if (window.location.href.indexOf('section.tasks') != -1) {
    var hiddenTabs = ["Sent"];
    var shortcuts = {
	    'с ': 'today ', 
	    'з ': 'tomorrow ', 
	    'пн ': 'monday ', 
	    'вт ': 'tuesday ', 
	    'ср ': 'wednsday ', 
	    'чт ': 'thursday ', 
	    'пт ': 'friday ', 
	    'сб ': 'satudray ', 
	    'вс ': 'sunday '
    };

    $(function () {
	    $('.appfootercontent, .xtoolbox_selector, #appheader, #sort-button, #add-helpicon').hide();
	    $('#content').css('padding-top', '0px');
	    $('#detailsbox').css('padding-top', '10px');
	    $('#statusbox').css({'padding': '0', 'margin': '0', 'height': '', 'position': 'fixed', 'bottom': '-20'});
	    $('body').css('overflow-x', 'hidden');
        $('body').css('overflow-y', 'auto');
	    $('#listtabs ul li').each(function (i, e) {
	        if ($.inArray($(e).text(), hiddenTabs) > -1) 
	    	    $(e).hide();
	    });
	    $('.xtoolbox_button[value="Complete"]').val('  √  ');
	    $('.xtoolbox_button[value="Postpone"]').val('  »  ');
	    STRING_TABLE["INTERFACE_TASKS_BUTTON_POSTPONE"] ="  »  ";
	    STRING_TABLE["INTERFACE_TASKS_BUTTON_COMPLETE"] ="  √  ";
	    $("#add-text").keyup(function() {
		    var s = shortcuts[$('#add-text').val()];
		    if (s != undefined)
			    $('#add-text').val(s);
	    });

	    $(window).resize(function(e) {
            $('#list').width($(window).width() < 505 ? $(window).width() : '');
            $('#statusbox').width($('#list').width());
        });
        $(window).resize();
    });
}

if (window.location.href.indexOf('login') != -1 && ) {
    $(function () {
        $('#username').val('login');
        $('#password').val('password');
        $('#remember').attr('checked','checked');
        $('#loginform').submit();
    });
}

if (window.location.href == 'http://www.rememberthemilk.com' || window.location.href == 'http://www.rememberthemilk.com/') {
    window.location.href = 'http://www.rememberthemilk.com/login/';
}