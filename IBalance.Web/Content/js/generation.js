$(document).ready(function () {
    $("#generate").click(function () {
        var selectProducts = document.getElementById("productsCombobox");
        var selectedProduct = selectProducts.options[selectProducts.selectedIndex];
        var productId = selectedProduct.id;
        var selectConterparties = document.getElementById("counterpartiesCombobox");
        var selectedCounterparty = selectConterparties.options[selectConterparties.selectedIndex];
        var counterpartyId = selectedCounterparty.id;
        var codesNumber = $('#codesNumber').val();
        var consignmentNumber = $('#consignmentNumber').val();

        var formData = new FormData();
        formData.append("ProductId", getId(productId));
        formData.append("CounterpartyId", getId(counterpartyId));
        formData.append("CodesNumber", codesNumber);
        formData.append("ConsignmentNumber", consignmentNumber);

        $.ajax({
            type: "POST",
            url: "/Generation/Generate",
            dataType: 'json',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                var serialKeys = data['result'];
                $('#codeList').empty();
                var html = "";
                html += '<div style="display:none;" id="consignmentNumberToDownload">' + data['consignmentNumber'] + '</div>'
                html += '<table class="table">';
                html += '<tbody>';
                for (var i = 0; i < serialKeys.length; i++) {

                    html += '<tr>';
                    html += '<td>' + (i+1) + '</td>';
                    html += '<td>' + serialKeys[i] + '</td>';
                    html += '</tr>';

                }
                html += '</tbody>';
                html += '</table>';
                $('#codeList').append(html);

                var downloadButton = document.getElementById("download");
                downloadButton.style.display = "block";
            }
        });
    });
});