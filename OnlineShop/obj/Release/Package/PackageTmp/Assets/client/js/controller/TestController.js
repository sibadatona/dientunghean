(function ($) {

	$.fn.clinicalEtaskList = (function () {

		var temp1 = 'temp1';
		var temp2 = 'temp2';

		function function1() {
			alert("function1");
		}

		var init = function () {
			alert("init");
		}

		function function2(a, b) {
			alert(a + b);
		}

		function function3() {
			alert(temp1);
		}
	})();

	//$(document).ready(function () {
	//	clinicalEtaskList.init();
	//});

})(jQuery);