//Codes for index page top menu
$(document).ready(function () {
   
    $('.page_resizer').click(function () {
        $('.tutorial_menu_wrap, .body_wrapper, .menu_new_style').toggleClass('active');
        $('.hide_on_resized').toggleClass('active');
    });
    $('.custom_checkbox_lang').click(function () {
        $('.custom_checkbox_lang').toggleClass('active');
    });

    //jQury index index
    $('.trigger').click(function () {
        $('.trigger_element').slideToggle("slow");
    });

    //Admin Menu toggling.
    $('.cntrl_toggling_btn').click(function () {
        $('.ad_menu_wrapper').toggleClass('active activeSmDevices');
        $('.admin_right').toggleClass('active');
    });

    //Blog toggling
    $('.toggling_menu_btn').click(function() {
        $('.blg_left').toggleClass('active');
        $('.blg_right').toggleClass('active');
    })
});

$(document).ready(function () {

    var floatingDiv = $('.under_nav');
    var tutorialMenuList = $('.tutorial_menu_list');//ul
    var floatingDivPosition = floatingDiv.position();
    var logInSignUpFormWrapper = $('.logIn_signUp_form_wrapper');

    //$(window).scroll(function () {
    //    var scrollBarPosition = $(window).scrollTop();

    //    if (scrollBarPosition > floatingDivPosition.top) {
    //        floatingDiv.addClass('floatingDiv');
    //        logInSignUpFormWrapper.css('color', 'black');
    //        tutorialMenuList.addClass('fixed_top_menu');
    //    } else {
    //        floatingDiv.removeClass('floatingDiv');
    //        tutorialMenuList.removeClass('fixed_top_menu');
    //    }
    //});

    $('#toogle_button').click(function () {
        $('#toggle_sm_top_menu').toggleClass('show');
    });

    //var overlay = $('.overlay');

    //overlay.css('display', 'none');
    //$('#toogle_button').click(function () {
    //    $('.overlay').toggleClass('show');
    //});
    //overlay.click(function () {
    //    overlay.toggleClass('show');
    //    $('#toggle_sm_top_menu').toggleClass('show');
    //});

    //$('.top_menu_for_sm_devices li a').click(function() {
    //    $('#toggle_sm_top_menu').toggleClass('show');
    //});

    $(window).dblclick(function () {
        $('#toggle_sm_top_menu').toggleClass('show');
    });

    $('#open_menu').click(function () {
        $('#overlays').addClass('overlay');
        $('.tutorial_menu_wrap').css({
            'margin-left': 0
        });
        $('.dashboard').css({
            'margin-left': 0
        });
    });

    function screenEightHundredPixels() {
        if ($(window).width() <= 800) {
            return true;
        }
        return false;
    }

    if (screenEightHundredPixels()) {
        $('.logIn_signUp_form_wrapper').css('width', '100%');

        $('#overlays').click(function (e) {
            if (e.target.id == "searchBoxId" || e.target.className == "custom_checkbox_lang" || e.target.className == "custom_checkbox_lang active") {
                
            } else if ($(this).hasClass('overlay')) {
                $('#overlays').removeClass('overlay');
                $('.tutorial_menu_wrap').css({
                    'margin-left': -240
                });
                $('.dashboard').css({
                    'margin-left': -240
                });
            }
            
        });
    }

    $('.top_menu li').on({
        'click': function () {
            $(this).siblings().removeClass('active');
            $(this).addClass('active');
        }
    });



    //COPY TO CLIPBOARD for html
    $('.copy_button').click(function () {
        var text = $(".copy_codes").text();
        var tempElement = $("<input />").val(text).appendTo('body').select();
        document.execCommand("copy");
        tempElement.remove();
        alert("Text Copied");
    });

    //Compiler or editors Codes
    $("#run_button").click(function () {
        var x = $('.editor_codes').val();
        $("#editor_result").html(x);
    });

    $('.toggle_editor_color').click(function () {
        $('body').toggleClass('t_b_color');
        $(this).toggleClass('active1');
        $('.editor_codes').toggleClass('light');
        $('#editor_result, #sql_editor_result').toggleClass('light');
    })

    //Ending Here

    //Super admin menu

    //High Lighting Active Page
    $('[href]').each(function () {
        if (this.href == window.location.href) {
            $(this).addClass('active');
        }
    });
});