$(document).ready(function () {
    function updateTotalPrice(rowIndex) {
        var price = $(`#Items_${rowIndex}__Price`).val();
        var quantity = $(`#Items_${rowIndex}__Quantity`).val();

        var totalPrice = price * quantity;
        $(`#Items_${rowIndex}__TotalPrice`).val(totalPrice);
    }

    $(document).on('change', 'select.form-control', function () {
        var selectedOption = $(this).find('option:selected');
        var price = selectedOption.attr('Price');
        var rowIndex = $(this).closest('tr').attr('id');

        $(`#Items_${rowIndex}__Price`).val(price);
        updateTotalPrice(rowIndex);
    });

    $(document).on('input', 'input[type="number"]', function () {
        var rowIndex = $(this).closest('tr').attr('id');
        updateTotalPrice(rowIndex);
    });

    $(document).on('click', 'a.btn-danger', function () {
        var rowIndex = $(this).closest('tr').attr('id');
        $('#' + rowIndex).remove();
    });
});
