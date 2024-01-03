$(document).on('input', 'input[type="number"]', function () {
   
    calculateAndDisplayTotalPrice();
    
});

function calculateAndDisplayTotalPrice() {
    var total = 0;
    $('.total-price').each(function () {
        total += parseFloat($(this).val() || 0);
    });

    $('#totalprice').text('Total Price: ' + total.toFixed(2));
}
