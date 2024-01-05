function Remove(itemId) {

    $.ajax({
        url: `Invoice/RemoveItem?itemId=${itemId}`,
        type: 'POST',
        data: $('form').serialize(),
        //content: 'application/json',
        success: function (partialView) {
            console.log('AJAX request successful');

            $('#Items').html(partialView);
            calculateAndDisplayTotalPrice();
            $("form").removeData("validator");
            $("form").removeData("unobtrusiveValidation");
            $.validator.unobtrusive.parse("form");
        },
        error: function (xhr, status, error) {
            console.error('AJAX request failed:', error);
        }
    });
}
