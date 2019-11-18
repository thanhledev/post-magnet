var PrimaryMenu = function () {

    return {

        //main function
        init: function () {
            PrimaryMenu.initMenuStyle();
        },

        initMenuStyle: function() {
            $('a[id="' + selectedMenu + '"]').closest('li.nav-item').addClass('active open');
            $('a[id="' + selectedMenu + '"]').closest('li.nav-item.main-menu-root-item').find('span.arrow').addClass('open');
            $('a[id="' + selectedMenu + '"]').closest('li.nav-item.main-menu-root-item').children('a').append('<span class="selected"></span>');
            $('a[id="' + selectedMenu + '"]').closest('li.nav-item.main-menu-root-item').addClass('active open');
        }
    };

}();

jQuery(document).ready(function () {
    PrimaryMenu.init();
});
