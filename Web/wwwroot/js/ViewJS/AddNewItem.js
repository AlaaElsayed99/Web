$('#Add').click(function () {
    $.ajax({
        url: "AddNewItem",
        type: 'POST',
        data: $('form').serialize(),
        content: 'application/json',
        success: function (partialView) {
            console.log('AJAX request successful'); // Log success
            $('#Items').html(partialView);
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
        },
        error: function (xhr, status, error) {
            console.error('AJAX request failed:', error); // Log error
        }
    });
});
