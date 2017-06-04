$(document).ready(function () {
    $('#searchByDate').datepicker({
        dateFormat: 'dd-M-y'
    });

    $("#filterParameter").change(function () {
        filterType = this.options[this.selectedIndex].value;
        if (filterType == "searchByDate") {
            document.getElementById('searchByDate').style.display = "block";
            document.getElementById('searchByCounterparty').style.display = "none";
            document.getElementById('searchByConsignmentNumber').style.display = "none";
        }
        else if (filterType == "searchByCounterparty") {
            document.getElementById('searchByDate').style.display = "none";
            document.getElementById('searchByCounterparty').style.display = "block";
            document.getElementById('searchByConsignmentNumber').style.display = "none";
        }
        else if (filterType == "searchByConsignmentNumber") {
            document.getElementById('searchByDate').style.display = "none"; 
            document.getElementById('searchByCounterparty').style.display = "none";
            document.getElementById('searchByConsignmentNumber').style.display = "block";
        }
    });

    $("#downloadSelected").click(function () {
        var formData = new FormData();
        var consignmentIdArray = [];
        var checkedCheckboxes = getCheckedBoxes("selected");
        for (var i = 0; i < checkedCheckboxes.length; i++) {
            consignmentIdArray.push(getId(checkedCheckboxes[i].parentNode.parentNode.id));
        }
        for (var i = 0; i < consignmentIdArray.length; i++) {
            formData.append('idList', consignmentIdArray[i]);
        }
        $.ajax({
            type: "POST",
            url: "/Consignment/FormatSelected",
            dataType: 'json',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                window.location.replace(data['redirect']);
            }
        });
    });

    $("#deleteAll").click(function () {
        $.ajax({
            type: "POST",
            url: "/Consignment/DeleteAll",
            dataType: 'json',
            processData: false,
            contentType: false,
            success: function (data) {
                if (data['result'] == "success") {
                    window.location.reload();
                }
            }
        });
    });

    $("#deleteSelected").click(function () {
        var formData = new FormData();
        var consignmentIdArray = [];
        var checkedCheckboxes = getCheckedBoxes("selected");
        for (var i = 0; i < checkedCheckboxes.length; i++) {
            consignmentIdArray.push(getId(checkedCheckboxes[i].parentNode.parentNode.id));
        }
        for (var i = 0; i < consignmentIdArray.length; i++) {
            formData.append('idList', consignmentIdArray[i]);
        }

        $.ajax({
            type: "POST",
            url: "/Consignment/DeleteSelected",
            dataType: 'json',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                if (data['result'] == "success") {
                    window.location.reload();
                }
            }
        });
    });

    $("#search").click(function () {
        var filter = document.getElementById("filterParameter");
        filterType = filter.options[filter.selectedIndex].value;
        var key = "";
        var value = "";
        if (filterType == "searchByDate") {
            key = "searchByDate";
            value = document.getElementById("searchByDate").value;
        }
        else if (filterType == "searchByCounterparty") {
            key = "searchByCounterparty";
            var searchByCounterparty = document.getElementById("searchByCounterparty");
            value = searchByCounterparty.options[searchByCounterparty.selectedIndex].value;
        }
        else if (filterType == "searchByConsignmentNumber") {
            key = "searchByConsignmentNumber";
            var searchByConsignmentNumber = document.getElementById("searchByConsignmentNumber");
            value = searchByConsignmentNumber.options[searchByConsignmentNumber.selectedIndex].value;
        }

        window.location.replace("/Consignment/Filter?FilterParameter=" + key + "&FilterValue=" + value);
    });
});