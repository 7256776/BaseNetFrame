/**
布局设置
**/
var frameStyle = function () {

    // 主题操作事件
    var handleTheme = function () {
        var panel = $('.theme-panel');
        //设置初始整体框架布局 fluid=流布局 
        if ($('body').hasClass('page-boxed') === false) {
            $('.layout-option', panel).val("fluid");
        }
        //设置菜单栏固定状态
        $('.sidebar-option', panel).val("default");
        //设置顶部页面固定状态
        $('.page-header-option', panel).val("fixed");
        //设置底部页面固定状态
        $('.page-footer-option', panel).val("default");
        //设置菜单栏位置(当body设置了从右向左的模式 style="direction: rtl;")
        if ($('.sidebar-pos-option').attr("disabled") === false) {
            $('.sidebar-pos-option', panel).val(App.isRTL() ? 'right' : 'left');
        }

        //重置主题布局
        var resetLayout = function () {
            $("body").
                removeClass("page-boxed").
                removeClass("page-footer-fixed").
                removeClass("page-sidebar-fixed").
                removeClass("page-header-fixed").
                removeClass("page-sidebar-reversed");

            $('.page-header > .page-header-inner').removeClass("container");

            if ($('.page-container').parent(".container").size() === 1) {
                //$('.page-container').insertAfter('body > .clearfix');
                $('.page-container').insertAfter('.layoutBody > .clearfix');
            }

            if ($('.page-footer > .container').size() === 1) {
                $('.page-footer').html($('.page-footer > .container').html());
            } else if ($('.page-footer').parent(".container").size() === 1) {
                $('.page-footer').insertAfter('.page-container');
                $('.scroll-to-top').insertAfter('.page-footer');
            }

            $(".top-menu > .navbar-nav > li.dropdown").removeClass("dropdown-dark");

            //$('body > .container').remove();
            $('.layoutBody > .container').remove();
        };

        var lastSelectedLayout = '';

        //设置主题布局
        var setLayout = function () {

            //整体框架布局
            var layoutOption = $('.layout-option', panel).val();
            //菜单栏固定状态
            var sidebarOption = $('.sidebar-option', panel).val();
            //顶部页面固定状态
            var headerOption = $('.page-header-option', panel).val();
            //底部页面固定状态
            var footerOption = $('.page-footer-option', panel).val();
            //菜单栏位置
            var sidebarPosOption = $('.sidebar-pos-option', panel).val();
            //
            var sidebarStyleOption = $('.sidebar-style-option', panel).val();
            //菜单栏显示模式
            var sidebarMenuOption = $('.sidebar-menu-option', panel).val();
            //消息框背景颜色
            var headerTopDropdownStyle = $('.page-header-top-dropdown-style-option', panel).val();

            if (sidebarOption == "fixed" && headerOption == "default") {
                alert('菜单栏为固定状态时,顶部页面必须同时设置为固定状态!');
                $('.page-header-option', panel).val("fixed");
                $('.sidebar-option', panel).val("fixed");
                sidebarOption = 'fixed';
                headerOption = 'fixed';
            }
            //重置主题布局
            resetLayout();
            //设置框布局
            if (layoutOption === "boxed") {
                $("body").addClass("page-boxed");

                // 设置顶部页面
                $('.page-header > .page-header-inner').addClass("container");
                //var cont = $('body > .clearfix').after('<div class="container"></div>');
                var cont = $('.layoutBody > .clearfix').after('<div class="container"></div>');

                // 设置内容页面
                //$('.page-container').appendTo('body > .container');
                $('.page-container').appendTo('.layoutBody > .container');

                // 设置底部页面
                if (footerOption === 'fixed') {
                    $('.page-footer').html('<div class="container">' + $('.page-footer').html() + '</div>');
                } else {
                    //$('.page-footer').appendTo('body > .container');
                    $('.page-footer').appendTo('.layoutBody > .container');
                }
            }

            if (lastSelectedLayout != layoutOption) {
                //页面整体布局发生变化后重置页面已经注册的布局事件
                App.runResizeHandlers();
            }
            lastSelectedLayout = layoutOption;

            //设置顶部页面
            if (headerOption === 'fixed') {
                $("body").addClass("page-header-fixed");
                $(".page-header").removeClass("navbar-static-top").addClass("navbar-fixed-top");
            } else {
                $("body").removeClass("page-header-fixed");
                $(".page-header").removeClass("navbar-fixed-top").addClass("navbar-static-top");
            }

            //设置菜单栏 (page-full-width是设置在body的样式,表示主内容区域全屏撑满不显示菜单栏)
            if ($('body').hasClass('page-full-width') === false) {
                //菜单栏固定状态
                if (sidebarOption === 'fixed') {
                    $("body").addClass("page-sidebar-fixed");
                    $("page-sidebar-menu").addClass("page-sidebar-menu-fixed");
                    $("page-sidebar-menu").removeClass("page-sidebar-menu-default");
                    Layout.initFixedSidebarHoverEffect();
                } else {
                    $("body").removeClass("page-sidebar-fixed");
                    $("page-sidebar-menu").addClass("page-sidebar-menu-default");
                    $("page-sidebar-menu").removeClass("page-sidebar-menu-fixed");
                    $('.page-sidebar-menu').unbind('mouseenter').unbind('mouseleave');
                }
            }

            // 消息框背景颜色
            if (headerTopDropdownStyle === 'dark') {
                $(".top-menu > .navbar-nav > li.dropdown").addClass("dropdown-dark");
            } else {
                $(".top-menu > .navbar-nav > li.dropdown").removeClass("dropdown-dark");
            }

            //设置底部页面
            if (footerOption === 'fixed') {
                $("body").addClass("page-footer-fixed");
            } else {
                $("body").removeClass("page-footer-fixed");
            }

            //菜单栏样式没发现有冇用
            if (sidebarStyleOption === 'compact') {
                $(".page-sidebar-menu").addClass("page-sidebar-menu-compact");
            } else {
                $(".page-sidebar-menu").removeClass("page-sidebar-menu-compact");
            }

            //菜单栏显示模式
            if (sidebarMenuOption === 'hover') {
                if (sidebarOption == 'fixed') {
                    $('.sidebar-menu-option', panel).val("accordion");
                    alert("悬停菜单栏与固定菜单栏模式不兼容。选择默认的菜单栏模式");
                } else {
                    $(".page-sidebar-menu").addClass("page-sidebar-menu-hover-submenu");
                }
            } else {
                $(".page-sidebar-menu").removeClass("page-sidebar-menu-hover-submenu");
            }

            //菜单栏显示位置
            if (App.isRTL()) {
                if (sidebarPosOption === 'left') {
                    $("body").addClass("page-sidebar-reversed");
                    $('#frontend-link').tooltip('destroy').tooltip({
                        placement: 'right'
                    });
                } else {
                    $("body").removeClass("page-sidebar-reversed");
                    $('#frontend-link').tooltip('destroy').tooltip({
                        placement: 'left'
                    });
                }
            } else {
                if (sidebarPosOption === 'right') {
                    $("body").addClass("page-sidebar-reversed");
                    $('#frontend-link').tooltip('destroy').tooltip({
                        placement: 'left'
                    });
                } else {
                    $("body").removeClass("page-sidebar-reversed");
                    $('#frontend-link').tooltip('destroy').tooltip({
                        placement: 'right'
                    });
                }
            }

            Layout.fixContentHeight(); // 修复内容高度      
            Layout.initFixedSidebar(); // 重新启动菜单栏

            setCookiesFrame();
        };

        var setCookiesFrame = function () {
            var frameSideLayout = {};
            //整体框架布局
            frameSideLayout.layoutOption = $('.layout-option', panel).val();
            //菜单栏固定状态
            frameSideLayout.sidebarOption = $('.sidebar-option', panel).val();
            //顶部页面固定状态
            frameSideLayout.headerOption = $('.page-header-option', panel).val();
            //底部页面固定状态
            frameSideLayout.footerOption = $('.page-footer-option', panel).val();
            //菜单栏位置
            frameSideLayout.sidebarPosOption = $('.sidebar-pos-option', panel).val();
            //菜单栏显示模式
            frameSideLayout.sidebarMenuOption = $('.sidebar-menu-option', panel).val();
            //消息框背景颜色
            frameSideLayout.headerTopDropdownStyle = $('.page-header-top-dropdown-style-option', panel).val();
            //主题圆角或直角的div风格
            frameSideLayout.layoutStyleOption = $('.layout-style-option', panel).val();
            //主题颜色风格
            frameSideLayout.frameColor = $('#hdFrameColor').val();

            Cookies.set('FrameSideLayout', frameSideLayout, { expires: 365 });
        }

        // 设置主题颜色
        var setColor = function (color) {
            var color_ = (App.isRTL() ? color + '-rtl' : color); 
            $('#style_color').attr("href", Layout.getLayoutCssPath() + 'themes/' + color_ + ".min.css");
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
            setCookiesFrame();
        };

        //注册主题颜色选择框点击事件
        $('.theme-colors > li', panel).click(function () {
            var color = $(this).attr("data-theme");
            setColor(color);
            $('ul > li', panel).removeClass("active");
            $(this).addClass("active");
            //此处是判断选择的是深色还是浅色主题来设置页面顶部按钮的颜色(该按钮目前没使用)
            if (color === 'dark') {
                $('.page-actions .btn').removeClass('red-haze').addClass('btn-default btn-transparent');
            } else {
                $('.page-actions .btn').removeClass('btn-default btn-transparent').addClass('red-haze');
            }
        });

        // 设置主题圆角或直角的div风格
        var setThemeStyle = function (style) {
            var file = (style === 'rounded' ? 'components-rounded' : 'components');
            file = (App.isRTL() ? file + '-rtl' : file);
            //
            $('#style_components').attr("href", App.getGlobalCssPath() + file + ".min.css");
            //
            $('.layout-style-option', panel).val(style);
            //
            setCookiesFrame();
        };

        // 注册主题圆角或直角的div风格点击事件
        $('.theme-panel .layout-style-option').change(function () {
            setThemeStyle($(this).val());
        });

        //初始主题设置窗体的默认值

        //初始整体框架布局
        if ($("body").hasClass("page-boxed")) {
            $('.layout-option', panel).val("boxed");
        }
        //初始菜单栏固定状态
        if ($("body").hasClass("page-sidebar-fixed")) {
            $('.sidebar-option', panel).val("fixed");
        }
        //初始顶部页面固定状态
        if ($("body").hasClass("page-header-fixed")) {
            $('.page-header-option', panel).val("fixed");
        }
        //初始底部页面固定状态
        if ($("body").hasClass("page-footer-fixed")) {
            $('.page-footer-option', panel).val("default");
        }
        //初始菜单栏位置
        if ($("body").hasClass("page-sidebar-reversed")) {
            $('.sidebar-pos-option', panel).val("right");
        }
        //该设置似乎不存在
        if ($(".page-sidebar-menu").hasClass("page-sidebar-menu-light")) {
            $('.sidebar-style-option', panel).val("light");
        }
        //初始菜单栏显示模式
        if ($(".page-sidebar-menu").hasClass("page-sidebar-menu-hover-submenu")) {
            $('.sidebar-menu-option', panel).val("hover");
        }
        //菜单栏固定状态
        var sidebarOption = $('.sidebar-option', panel).val();
        //顶部页面固定状态
        var headerOption = $('.page-header-option', panel).val();
        //底部页面固定状态
        var footerOption = $('.page-footer-option', panel).val();
        //菜单栏位置
        var sidebarPosOption = $('.sidebar-pos-option', panel).val();
        var sidebarStyleOption = $('.sidebar-style-option', panel).val();
        //菜单栏显示模式
        var sidebarMenuOption = $('.sidebar-menu-option', panel).val();
        //注册主题设置窗体组件的事件
        $('.layout-option, .page-header-top-dropdown-style-option, .page-header-option, .sidebar-option, .page-footer-option, .sidebar-pos-option, .sidebar-style-option, .sidebar-menu-option', panel).change(setLayout);

        //初始根据cookic获取主题布局
        (function () {
            var frameSideLayout = Cookies.getJSON('FrameSideLayout') || {};

            //整体框架布局
            if (frameSideLayout.layoutOption) {
                $('.layout-option', panel).val(frameSideLayout.layoutOption);
            }
            //菜单栏固定状态
            if (frameSideLayout.sidebarOption) {
                $('.sidebar-option', panel).val(frameSideLayout.sidebarOption);
            }
            //顶部页面固定状态
            if (frameSideLayout.headerOption) {
                $('.page-header-option', panel).val(frameSideLayout.headerOption);
            }
            //底部页面固定状态
            if (frameSideLayout.footerOption) {
                $('.page-footer-option', panel).val(frameSideLayout.footerOption);
            }
            //菜单栏位置
            if (frameSideLayout.sidebarPosOption) {
                $('.sidebar-pos-option', panel).val(frameSideLayout.sidebarPosOption);
            }
            //菜单栏显示模式
            if (frameSideLayout.sidebarMenuOption) {
                $('.sidebar-menu-option', panel).val(frameSideLayout.sidebarMenuOption);
            }
            //消息框背景颜色
            if (frameSideLayout.headerTopDropdownStyle) {
                $('.page-header-top-dropdown-style-option', panel).val(frameSideLayout.headerTopDropdownStyle);
            }
            //主题圆角或直角的div风格
            if (frameSideLayout.headerTopDropdownStyle) {
                setThemeStyle(frameSideLayout.layoutStyleOption);
            }
            //主题颜色风格
            if (frameSideLayout.headerTopDropdownStyle) {
                setColor(frameSideLayout.frameColor);
            }
            setLayout();
        })();

    };

    return {

        //main function to initiate the theme
        init: function () {
            handleTheme();
        }
    };

}();

if (App.isAngularJsApp() === false) {
    jQuery(document).ready(function() {    
        frameStyle.init(); // init metronic core componets
    });
}