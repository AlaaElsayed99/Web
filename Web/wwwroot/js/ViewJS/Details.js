$(document).ready(function () {
    var totalPriceCell = $('#totalPriceCell');
    var itemPrices = $('.itemRow .itemPrice');

    var totalPrice = 0;
    itemPrices.each(function () {
        totalPrice += parseFloat($(this).text());
    });

    totalPriceCell.text(totalPrice.toFixed(2));
});

//////////
function printTable() {
    var printContents = document.getElementById('printTable').outerHTML;
    var originalContents = document.body.innerHTML;
    document.body.innerHTML = printContents;
    window.print();
    document.body.innerHTML = originalContents;
}
/////////////////
// JavaScript to calculate and display the total price
document.addEventListener('DOMContentLoaded', function () {
    var totalPriceCell = document.getElementById('totalPriceCell');
    var items = document.querySelectorAll('#itemsTable tbody tr:not(#totalPriceRow)');

    var totalPrice = Array.from(items).reduce(function (acc, itemRow) {
        var itemPrice = parseFloat(itemRow.querySelector('td:nth-child(4)').innerText);
        var itemQuantity = parseInt(itemRow.querySelector('td:nth-child(3)').innerText);
        return acc + (itemPrice * itemQuantity);
    }, 0);

    totalPriceCell.innerText = totalPrice.toFixed(2);
});