$.validator.setDefaults({
	highlight: function (element) {
		$(element).closest('.form-group').addClass('has-error');
	},
	unhighlight: function (element) {
	    $formGroup = $(element).closest('.form-group');
	    if(!$formGroup.hasClass('custom-error-validation')) {
	        $formGroup.removeClass('has-error');
	    }
	},
});
