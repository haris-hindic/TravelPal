(function ( $ ) {
	'use strict';
	$( window ).on(
		'elementor/frontend/init',
		function () {
			qodefAddonsElementor.init();
		}
	);

	var qodefAddonsElementor = {
		init: function () {
			var isEditMode = Boolean( elementorFrontend.isEditMode() );

			if ( isEditMode ) {
				for ( var key in qodefAddonsCore.shortcodes ) {
					for ( var keyChild in qodefAddonsCore.shortcodes[key] ) {
						qodefAddonsElementor.reInitShortcode(
							key,
							keyChild
						);
					}
				}
			}
		},
		reInitShortcode: function ( key, keyChild ) {
			elementorFrontend.hooks.addAction(
				'frontend/element_ready/' + key + '.default',
				function ( e ) {
					// Check if object doesn't exist and print the module where is the error
					if ( typeof qodefAddonsCore.shortcodes[key][keyChild] === 'undefined' ) {
						console.log( keyChild );
					} else if ( typeof qodefAddonsCore.shortcodes[key][keyChild].initSlider === 'function' && e.find( '.qodef-qi-swiper-container' ).length ) {
						var $sliders = e.find( '.qodef-qi-swiper-container' );
						if ( $sliders.length ) {
							$sliders.each(
								function () {
									qodefAddonsCore.shortcodes[key][keyChild].initSlider( $( this ) );
								}
							);
						}
					} else if ( typeof qodefAddonsCore.shortcodes[key][keyChild].initItem === 'function' && e.find( '.qodef-shortcode' ).length ) {
						qodefAddonsCore.shortcodes[key][keyChild].initItem( e.find( '.qodef-shortcode' ) );
					} else {
						qodefAddonsCore.shortcodes[key][keyChild].init();
					}
				}
			);
		},
	};

})( jQuery );
