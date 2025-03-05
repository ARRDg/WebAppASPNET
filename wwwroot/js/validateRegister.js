$("#form-register").validate({
    rules: {
        Name: {
            required: true,
            maxlength: 50,
            minlength: 2
        },
        Email: {
            required: true,
            email: true
        },
        Password: {
            required: true,
            minlength: 8
        },
        AgreeToTerms: {
            required: true
        }
    },
    messages: {
        Name: {
            required: "Please enter your name",
            maxlength: "Name cannot exceed 50 characters",
            minlength: "Name must be at least 2 characters"
        },
        Email: {
            required: "Please enter your email",
            email: "Specify the email in the format name@domain.com"
        },
        Password: {
            required: "Please enter a password",
            minlength: "Password must be at least 8 characters"
        },
        AgreeToTerms: "You must agree to the terms"
    },
    errorClass: "text-danger",
    errorElement: "span",
    errorPlacement: function (error, element) {
        if (element.attr("name") === "AgreeToTerms") {
            error.insertAfter(element.closest('.form-check'));
        } else {
            error.insertAfter(element);
        }
    },
    highlight: function (element, errorClass) {
        $(element).closest('.formAuth').addClass('formAuth--error');
    },
    unhighlight: function (element, errorClass) {
        const $form = $(element).closest('.formAuth');
        if ($form.find('.text-danger:visible').length === 0) {
            $form.removeClass('formAuth--error');
        }
    }
});