function markValidationErrors() {
            var fields = $(".field-validation-error");
            fields.each(function () {
                if (this.val != "") {
                    var inputField = $(this).prev();
                    inputField.addClass('is-invalid');
                }
            });
        }