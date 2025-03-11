document.addEventListener("DOMContentLoaded", function () {
			console.log('Penis');
			console.log($('.counter'))
			$('.counter').counterUp({
				delay: 10,
				time: 3000
			});
		})