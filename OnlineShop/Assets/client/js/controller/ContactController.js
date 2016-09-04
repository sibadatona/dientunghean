(function ($) {
	$.fn.contact = (function () {

		var init = function () {
			$('#btn_click1').off("click").on("click", function () {
				alert('a');
			});
		}
	})();



	$(document).ready(function () {
		cart.callAlrt();
	});
})(jQuery);