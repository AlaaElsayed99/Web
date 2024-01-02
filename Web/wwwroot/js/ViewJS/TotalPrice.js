// Display total price
function calculateTotal() {
    var quantity = $("#Quantity").val();
    var price = $("#Price").val();
    var totalPrice = quantity * price;
    $("#TotalPrice").val(totalPrice);
}
