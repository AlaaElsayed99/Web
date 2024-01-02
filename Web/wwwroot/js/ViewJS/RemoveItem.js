
// Make an AJAX call to delete the item
// Replace this with your actual AJAX call to the DeleteItem action
// For simplicity, I'm assuming you're using jQuery for AJAX here
function Remove(itemId) {
    $.ajax({
        url: `RemoveItem?itemId=${itemId}`,
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
}