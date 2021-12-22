(function ($) {
	"use strict";

	$( window ).on(
		'elementor/frontend/init',
		function () {
			// shortcodes
			qodefElementorShortcodes.init();

			// section extension
			qodefElementorSection.init();
			elementorSection.init();
		}
	);

	// shortcodes
	var qodefElementorShortcodes = {
		init           : function () {
			var isEditMode = Boolean( elementorFrontend.isEditMode() );

			if (isEditMode) {
				for (var key in qodefCore.shortcodes) {
					for (var keyChild in qodefCore.shortcodes[key]) {
						qodefElementorShortcodes.reInitShortcode( key, keyChild );
					}
				}
			}
		},
		reInitShortcode: function (key, keyChild) {
			elementorFrontend.hooks.addAction(
				'frontend/element_ready/' + key + '.default',
				function (e) {

					// check if object doesn't exist and print the module where is the error
					if (typeof qodefCore.shortcodes[key][keyChild] === 'undefined') {
						console.log( keyChild );
					} else if (typeof qodefCore.shortcodes[key][keyChild].initItem === 'function' && e.find( '.qodef-shortcode' ).length) {
						qodefCore.shortcodes[key][keyChild].initItem( e.find( '.qodef-shortcode' ) );
					} else {
						qodefCore.shortcodes[key][keyChild].init();
					}
				}
			);
		}
	};
	
	var qodefElementorSection = {
		init: function () {
			$(window).on('elementor/frontend/init', function () {
				elementorFrontend.hooks.addAction('frontend/element_ready/section', elementorSection.init);
			});
		}
	};
	
	var elementorSection = {
		init: function ($scope) {
			var $target = $scope,
				isEditMode = Boolean(elementorFrontend.isEditMode()),
				settings = [],
				sectionData = {};
			
			//generate parallax settings
			if (isEditMode && typeof $scope !== 'undefined') {
				
				// generate options when in admin
				var editorElements = window.elementor.elements,
					sectionId = $target.data('id');
				
				$.each(editorElements.models, function (index, object) {
					if (sectionId === object.id) {
						sectionData = object.attributes.settings.attributes;
					}
				});
				
				//parallax options
				if (typeof sectionData.qodef_parallax_type !== 'undefined') {
					settings['enable_parallax'] = sectionData.qodef_parallax_type;
				}
				
				if (typeof sectionData.qodef_parallax_image !== 'undefined' && sectionData.qodef_parallax_image['url']) {
					settings['parallax_image_url'] = sectionData.qodef_parallax_image['url'];
				}
				
				//generate output backend
				if (typeof $target !== 'undefined') {
					elementorSection.generateOutput($target, settings);
				}
			} else {
				
				// generate options when in frontend using global js variable
				var sectionHandlerData = qodefElementorGlobal.vars.elementorSectionHandler;
				
				$.each(sectionHandlerData, function (index, properties) {
					
					properties.forEach(function (property) {
						
						if (typeof property['parallax_type'] !== 'undefined' && property['parallax_type'] === 'parallax') {
							
							$target = $('[data-id="' + index + '"]');
							settings['parallax_type'] = property['parallax_type'];
							settings['parallax_image_url'] = property['parallax_image']['url'];
							
							if (typeof settings['parallax_image_url'] !== 'undefined') {
								settings['enable_parallax'] = 'parallax';
							}
						}
						
						//generate output frontend
						if (typeof $target !== 'undefined') {
							elementorSection.generateOutput($target, settings);
							
							settings = [];
						}
					});
				});
			}
		},
		generateOutput: function ($target, settings) {
			
			if (typeof settings['enable_parallax'] !== 'undefined' && settings['enable_parallax'] === 'parallax' && typeof settings['parallax_image_url'] !== 'undefined') {
				
				$('.qodef-parallax-row-holder', $target).remove();
				$target.removeClass('qodef-parallax qodef--parallax-row');
				
				var $layout = null;
				
				$target.addClass('qodef-parallax qodef--parallax-row');
				
				$layout = $('<div class="qodef-parallax-row-holder"><div class="qodef-parallax-img-holder"><div class="qodef-parallax-img-wrapper"><img class="qodef-parallax-img" src="' + settings['parallax_image_url'] + '" alt="Parallax Image"></div></div></div>').prependTo($target);
				
				// wait for image src to be loaded
				var newImg = new Image;
				newImg.onload = function () {
					$target.find('img.qodef-parallax-img').attr('src', this.src);
					qodefCore.qodefParallaxBackground.init();
				};
				newImg.src = settings['parallax_image_url'];
			}
		}
	};
	
})(jQuery);
