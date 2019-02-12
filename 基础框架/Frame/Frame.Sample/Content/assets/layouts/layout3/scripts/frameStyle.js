/**
Demo script to handle the theme demo
**/

var frameStyle = function () {

    // Handle Theme Settings
    var handleTheme = function () {

        var panel = $('.theme-panel');

        //整体框架布局
        if ($('.page-head > .container-fluid').size() === 1) {
            $('.theme-setting-layout', panel).val("fluid");
        } else {
            $('.theme-setting-layout', panel).val("boxed");
        }
        //消息框背景颜色
        if ($('.top-menu li.dropdown.dropdown-dark').size() > 0) {
            $('.theme-setting-top-menu-style', panel).val("dark");
        } else {
            $('.theme-setting-top-menu-style', panel).val("light");
        }
        //固定顶部页面
        if ($('body').hasClass("page-header-top-fixed")) {
            $('.theme-setting-top-menu-mode', panel).val("fixed");
        } else {
            $('.theme-setting-top-menu-mode', panel).val("not-fixed");
        }
        //菜单背景色
        if ($('.hor-menu.hor-menu-light').size() > 0) {
            $('.theme-setting-mega-menu-style', panel).val("light");
        } else {
            $('.theme-setting-mega-menu-style', panel).val("dark");
        }
        //固定顶部菜单
        if ($('body').hasClass("page-header-menu-fixed")) {
            $('.theme-setting-mega-menu-mode', panel).val("fixed");
        } else {
            $('.theme-setting-mega-menu-mode', panel).val("not-fixed");
        }

        //重置布局
        var resetLayout = function () {
            $("body").
            removeClass("page-header-top-fixed").
            removeClass("page-header-menu-fixed");

            $('.page-header-top > .container-fluid').removeClass("container-fluid").addClass('container');
            $('.page-header-menu > .container-fluid').removeClass("container-fluid").addClass('container');
            $('.page-head > .container-fluid').removeClass("container-fluid").addClass('container');
            $('.page-content > .container-fluid').removeClass("container-fluid").addClass('container');
            $('.page-prefooter > .container-fluid').removeClass("container-fluid").addClass('container');
            $('.page-footer > .container-fluid').removeClass("container-fluid").addClass('container');              
        };

        var setLayout = function () {
            //整体框架布局
            var layoutMode = $('.theme-setting-layout', panel).val();
            //消息框背景颜色
            var headerTopMenuStyle = $('.theme-setting-top-menu-style', panel).val();
            //固定顶部页面
            var headerTopMenuMode = $('.theme-setting-top-menu-mode', panel).val();
            //菜单背景色
            var headerMegaMenuStyle = $('.theme-setting-mega-menu-style', panel).val();
            //固定顶部菜单
            var headerMegaMenuMode = $('.theme-setting-mega-menu-mode', panel).val();
            
            resetLayout(); // reset layout to default state
             //整体框架布局
            if (layoutMode === "fluid") {
                $('.page-header-top > .container').removeClass("container").addClass('container-fluid');
                $('.page-header-menu > .container').removeClass("container").addClass('container-fluid');
                $('.page-head > .container').removeClass("container").addClass('container-fluid');
                $('.page-content > .container').removeClass("container").addClass('container-fluid');
                $('.page-prefooter > .container').removeClass("container").addClass('container-fluid');
                $('.page-footer > .container').removeClass("container").addClass('container-fluid');

                //App.runResizeHandlers();
            }
            //消息框背景颜色
            if (headerTopMenuStyle === 'dark') {
                $(".top-menu > .navbar-nav > li.dropdown").addClass("dropdown-dark");
            } else {
                $(".top-menu > .navbar-nav > li.dropdown").removeClass("dropdown-dark");
            }
              //固定顶部页面
            if (headerTopMenuMode === 'fixed') {
                $("body").addClass("page-header-top-fixed");
            } else {
                $("body").removeClass("page-header-top-fixed");
            }
            //菜单背景色?
            if (headerMegaMenuStyle === 'light') {
                $(".hor-menu").addClass("hor-menu-light");
            } else {
                $(".hor-menu").removeClass("hor-menu-light");
            }
             //固定顶部菜单
            if (headerMegaMenuMode === 'fixed') {
                $("body").addClass("page-header-menu-fixed");
            } else {
                $("body").removeClass("page-header-menu-fixed");
            }      
            //
            setCookiesFrame();
        };

        var setCookiesFrame = function () {
            var frameTopLayout = {};
            //整体框架布局
            frameTopLayout.layoutMode = $('.theme-setting-layout', panel).val();
            //消息框背景颜色
            frameTopLayout.headerTopMenuStyle = $('.theme-setting-top-menu-style', panel).val();
            //固定顶部页面
            frameTopLayout.headerTopMenuMode = $('.theme-setting-top-menu-mode', panel).val();
            //菜单背景色?
            frameTopLayout.headerMegaMenuStyle = $('.theme-setting-mega-menu-style', panel).val();
            //固定顶部菜单
            frameTopLayout.headerMegaMenuMode = $('.theme-setting-mega-menu-mode', panel).val();
            //主题圆角或直角的div风格
            frameTopLayout.layoutStyleOption = $('.theme-setting-style', panel).val();
            //主题颜色风格
            frameTopLayout.frameColor = $('#hdFrameColor').val();

            Cookies.set('FrameTopLayout', frameTopLayout, { expires: 365 });
        }

        // 设置主题颜色
        var setColor = function (color) {
            var color_ = (App.isRTL() ? color + '-rtl' : color);
            $('#style_color').attr("href", Layout.getLayoutCssPath() + 'themes/' + color_ + ".mini.css");
           
            //
            $('#hdFrameColor').val(color);
            //设置logo的颜色
            if (color == "light") {
                $(".frame-logo-page").removeClass("frame-logo-light").addClass("frame-logo-drak");
                $(".frame-logo-title").removeClass("frame-logo-title-light").addClass("frame-logo-title-drak");
            } else {
                $(".frame-logo-page").removeClass("frame-logo-drak").addClass("frame-logo-light");
                $(".frame-logo-title").removeClass("frame-logo-title-drak").addClass("frame-logo-title-light");
            }
            //
            var lis = $(".theme-color");
            $.each($('.theme-color'), function (i, o) {
                $(this).removeClass('active');
                var c = $(this).attr("data-theme");
                if (c == color) {
                    $(this).addClass("active");
                }
            });
            //
            setCookiesFrame();
        };
        //注册主题颜色选择框点击事件
        $('.theme-colors > li', panel).click(function () {
            var color = $(this).attr("data-theme");
            setColor(color);
            $('.theme-colors > li', panel).removeClass("active");
            $(this).addClass("active");
        });

        // 设置主题圆角或直角的div风格
        var setThemeStyle = function (style) {
            var file = (style === 'rounded' ? 'components-rounded' : 'components');
            file = (App.isRTL() ? file + '-rtl' : file);

            $('#style_components').attr("href", App.getGlobalCssPath() + file + ".min.css");
            //
            $('.theme-setting-style', panel).val(style);
            //
            setCookiesFrame();
        };

        // 注册主题圆角或直角的div风格点击事件
        $('.theme-panel .theme-setting-style').change(function () {
            setThemeStyle($(this).val());
        });

        $('.theme-setting-top-menu-mode', panel).change(function(){
            var headerTopMenuMode = $('.theme-setting-top-menu-mode', panel).val();
            var headerMegaMenuMode = $('.theme-setting-mega-menu-mode', panel).val();            

            if (headerMegaMenuMode === "fixed") {
                alert("最上面的页面和导航菜单不能同时固定.");
                $('.theme-setting-mega-menu-mode', panel).val("not-fixed");   
                headerTopMenuMode = 'not-fixed';
            }                
        });

        $('.theme-setting-mega-menu-mode', panel).change(function(){
            var headerTopMenuMode = $('.theme-setting-top-menu-mode', panel).val();
            var headerMegaMenuMode = $('.theme-setting-mega-menu-mode', panel).val();            

            if (headerTopMenuMode === "fixed") {
                alert("最上面的页面和导航菜单不能同时固定.");
                $('.theme-setting-top-menu-mode', panel).val("not-fixed");   
                headerTopMenuMode = 'not-fixed';
            }                
        });

        $('.theme-setting', panel).change(setLayout);

        //初始根据cookic获取主题布局
        (function () {
            var frameTopLayout = Cookies.getJSON('FrameTopLayout') || {};
            //整体框架布局
            if (frameTopLayout.layoutMode) {
                $('.theme-setting-layout', panel).val(frameTopLayout.layoutMode);
            }
            //消息框背景颜色
            if (frameTopLayout.headerTopMenuStyle) {
                $('.theme-setting-top-menu-style', panel).val(frameTopLayout.headerTopMenuStyle);
            }
            //固定顶部页面
            if (frameTopLayout.headerTopMenuMode) {
                $('.theme-setting-top-menu-mode', panel).val(frameTopLayout.headerTopMenuMode);
            }
            //菜单背景色
            if (frameTopLayout.headerMegaMenuStyle) {
                $('.theme-setting-mega-menu-style', panel).val(frameTopLayout.headerMegaMenuStyle);
            }
            //固定顶部菜单
            if (frameTopLayout.headerMegaMenuMode) {
                $('.theme-setting-mega-menu-mode', panel).val(frameTopLayout.headerMegaMenuMode);
            }
            //主题圆角或直角的div风格
            if (frameTopLayout.layoutStyleOption) {
                setThemeStyle(frameTopLayout.layoutStyleOption);
            }
            //主题颜色风格
            if (frameTopLayout.frameColor) {
                setColor(frameTopLayout.frameColor);
            }
            setLayout();
        })();
    };

    

    return {

        //main function to initiate the theme
        init: function() {
            // handles style customer tool
            handleTheme();
        }
    };

}();

if (App.isAngularJsApp() === false) {
    jQuery(document).ready(function() {   
        frameStyle.init();
    });
} 