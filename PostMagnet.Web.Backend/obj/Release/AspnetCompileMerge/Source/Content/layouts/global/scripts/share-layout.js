var SharedLayout = function() {

    return {
        
		initClickOnMainMenu: function() {
			jQuery('.nav-leaf').click(function() {
				jQuery('ul[name=main-menu] li').removeClass('active').removeClass('open'); //remove the active & open class of all current <li> tags
				jQuery('ul[name=main-menu]').find('span.selected, span.arrow open').remove(); //remove all <span> with class like "selected" or "arrow open"
				
				jQuery(this).append('<span class=\'selected\'></span>');
				jQuery(this).parents('li').addClass('active open');
				jQuery(this).parents('li').last().find('>:first-child').append('<span class=\'selected\'></span>');
			});
		},
		
        init: function() {

			this.initClickOnMainMenu();
        }
    };
	
}();

if (App.isAngularJsApp() === false) { 
    jQuery(document).ready(function() {
        SharedLayout.init(); // init metronic core componets
    });
}