$(document).ready(function () {

    $('#productionDate').datepicker({
        dateFormat: 'dd-M-y'
    });

    $('#productionDateUpdate').datepicker({
        dateFormat: 'dd-M-y'
    });

    $('#productionDateCopy').datepicker({
        dateFormat: 'dd-M-y'
    });

    $('#editOpenModal').on('show.bs.modal', function (e) {
        var invoker = $(e.relatedTarget);
        var row = invoker[0].parentElement.parentElement;
        var columns = row.childNodes;
        var leftColumn = columns[1]; // column with image
        var rightColumn = columns[3]; // column with data
        document.getElementById('imageUpdate').src = leftColumn.childNodes[1].src;
        var table = rightColumn.childNodes[1];

        document.getElementById('idUpdate').value = getId(invoker[0].id);
        document.getElementById('modelUpdate').value = table.rows[0].cells[1].innerHTML;
        document.getElementById('productionDateUpdate').value = table.rows[1].cells[1].innerHTML;
        document.getElementById('colourUpdate').value = table.rows[2].cells[1].innerHTML;
        document.getElementById('wheelsDiameterUpdate').value = table.rows[3].cells[1].innerHTML;
        document.getElementById('motorPowerUpdate').value = table.rows[4].cells[1].innerHTML;
        document.getElementById('batteryManufacturerUpdate').value = table.rows[5].cells[1].innerHTML;
        document.getElementById('amperesUpdate').value = table.rows[6].cells[1].innerHTML;
        document.getElementById('weightUpdate').value = table.rows[7].cells[1].innerHTML;
        document.getElementById('maxSpeedUpdate').value = table.rows[8].cells[1].innerHTML;
        document.getElementById('possibleMileageUpdate').value = table.rows[9].cells[1].innerHTML;
        document.getElementById('motherboardUpdate').value = table.rows[10].cells[1].innerHTML;
        document.getElementById('applicationUpdate').value = table.rows[11].cells[1].innerHTML;
    });

    $('#copyOpenModal').on('show.bs.modal', function (e) {
        var invoker = $(e.relatedTarget);
        var row = invoker[0].parentElement.parentElement;
        var columns = row.childNodes;
        var leftColumn = columns[1]; // column with image
        var rightColumn = columns[3]; // column with data
        document.getElementById('imageCopy').src = leftColumn.childNodes[1].src;
        var table = rightColumn.childNodes[1];

        document.getElementById('idCopy').value = getId(invoker[0].id);
        document.getElementById('modelCopy').value = table.rows[0].cells[1].innerHTML;
        document.getElementById('productionDateCopy').value = table.rows[1].cells[1].innerHTML;
        document.getElementById('colourCopy').value = table.rows[2].cells[1].innerHTML;
        document.getElementById('wheelsDiameterCopy').value = table.rows[3].cells[1].innerHTML;
        document.getElementById('motorPowerCopy').value = table.rows[4].cells[1].innerHTML;
        document.getElementById('batteryManufacturerCopy').value = table.rows[5].cells[1].innerHTML;
        document.getElementById('amperesCopy').value = table.rows[6].cells[1].innerHTML;
        document.getElementById('weightCopy').value = table.rows[7].cells[1].innerHTML;
        document.getElementById('maxSpeedCopy').value = table.rows[8].cells[1].innerHTML;
        document.getElementById('possibleMileageCopy').value = table.rows[9].cells[1].innerHTML;
        document.getElementById('motherboardCopy').value = table.rows[10].cells[1].innerHTML;
        document.getElementById('applicationCopy').value = table.rows[11].cells[1].innerHTML;
    });

    $("#uploadUpdateImage").change(function () {
        var th = $(this);
        document.getElementById('imageUpdate').style.display = 'none';
    });

    $("#uploadCopyImage").change(function () {
        var th = $(this);
        document.getElementById('imageCopy').style.display = 'none';
    });

    $("#addProduct").click(function () {
        var model = $('#model').val();
        var productionDate = $('#productionDate').val();
        var colour = $('#colour').val();
        var wheelsDiameter = $('#wheelsDiameter').val();
        var motorPower = $('#motorPower').val();
        var batteryManufacturer = $('#batteryManufacturer').val();
        var amperes = $('#amperes').val();
        var weight = $('#weight').val();
        var maxSpeed = $('#maxSpeed').val();
        var possibleMileage = $('#possibleMileage').val();
        var motherboard = $('#motherboard').val();
        var application = $('#application').val();
        var image = $('#image')[0].files[0];
        var formData = new FormData();
        formData.append("Model", model);
        formData.append("ProductionDate", productionDate);
        formData.append("Colour", colour);
        formData.append("WheelsDiameter", wheelsDiameter);
        formData.append("MotorPower", motorPower);
        formData.append("BatteryManufacturer", batteryManufacturer);
        formData.append("Amperes", amperes);
        formData.append("Weight", weight);
        formData.append("MaxSpeed", maxSpeed);
        formData.append("PossibleMileage", possibleMileage);
        formData.append("Motherboard", motherboard);
        formData.append("Application", application);
        formData.append("ImageUrl", image);

        $.ajax({
            type: "POST",
            url: "/Product/Add",
            dataType: 'json',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                if (data['result'] == "success") {
                    window.location.reload();
                }
                /*
                var html = '<div class="row" style="border-bottom: 1px dashed #808080; padding-bottom:1%; margin-top:3%;" id="product_' + result['Id'] + '@product.Id">';
                html += '<div class="col-md-4">';
                html += '<img src="' + result['ImageUrl'] + '" alt="Image" width="400" height="400" />';
                html += '<button type="button" style="margin-top:3%;" class="btn btn-info btn-lg" data-toggle="modal" id="update_' + result['Id'] + '" data-target="#editOpenModal">Редактировать</button>';
                html += '<button type="button" style="margin-left:2%; margin-top:3%;" class="btn btn-info btn-lg" data-toggle="modal" name="deleteProduct" id="delete_' + result['Id'] + '">Удалить</button>';
                html += '</div>';
                html += '<div class="col-md-8">';
                html += '<table class="table table-hover">';
                html += '<tbody>'
                html += '<tr><td>Модель</td><td>' + model + '</td></tr>';
                html += '<tr><td>Дата производства</td><td>' + productionDate + '</td></tr>';
                html += '<tr><td>Цвет</td><td>' + colour + '</td></tr>';
                html += '<tr><td>Диаметр колес</td ><td>' + wheelsDiameter + '</td></tr>';
                html += '<tr><td>Мощность мотора</td><td>' + motorPower + '</td></tr>';
                html += '<tr><td>Производитель АКБ</td><td>' + batteryManufacturer + '</td></tr>';
                html += '<tr><td>Амперы</td><td>' + amperes + '</td></tr>';
                html += '<tr><td>Вес устройства</td><td>' + weight + '</td></tr>';
                html += '<tr><td>Максимальная скорость</td><td>' + maxSpeed + '</td></tr>';
                html += '<tr><td>Пробег на одном заряде</td><td>' + possibleMileage + '</td></tr>';
                html += '<tr><td>Материнская плата</td><td>' + motherboard + '</td></tr>';
                html += '<tr><td>Приложение</td><td>' + application + '</td></tr>';
                html += '</tbody>';
                html += '</table>';
                html += '</div>';
                html += '</div>';
                document.getElementById("products").innerHTML += html;
                */
            }
        });
    });


    $("#editProduct").click(function () {
        var id = $('#idUpdate').val();
        var model = $('#modelUpdate').val();
        var productionDate = $('#productionDateUpdate').val();
        var colour = $('#colourUpdate').val();
        var wheelsDiameter = $('#wheelsDiameterUpdate').val();
        var motorPower = $('#motorPowerUpdate').val();
        var batteryManufacturer = $('#batteryManufacturerUpdate').val();
        var amperes = $('#amperesUpdate').val();
        var weight = $('#weightUpdate').val();
        var maxSpeed = $('#maxSpeedUpdate').val();
        var possibleMileage = $('#possibleMileageUpdate').val();
        var motherboard = $('#motherboardUpdate').val();
        var application = $('#applicationUpdate').val();
        var image;
        if (document.getElementById('imageUpdate').style.display == 'none') {
            image = $('#uploadUpdateImage')[0].files[0];
        }
        else {
            image = document.getElementById('imageUpdate').src;
        }
        var formData = new FormData();
        formData.append("Id", id);
        formData.append("Model", model);
        formData.append("ProductionDate", productionDate);
        formData.append("Colour", colour);
        formData.append("WheelsDiameter", wheelsDiameter);
        formData.append("MotorPower", motorPower);
        formData.append("BatteryManufacturer", batteryManufacturer);
        formData.append("Amperes", amperes);
        formData.append("Weight", weight);
        formData.append("MaxSpeed", maxSpeed);
        formData.append("PossibleMileage", possibleMileage);
        formData.append("Motherboard", motherboard);
        formData.append("Application", application);
        formData.append("ImageUrl", image);

        $.ajax({
            type: "POST",
            url: "/Product/Update",
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

    $("#copyProduct").click(function () {
        var model = $('#modelCopy').val();
        var productionDate = $('#productionDateCopy').val();
        var colour = $('#colourCopy').val();
        var wheelsDiameter = $('#wheelsDiameterCopy').val();
        var motorPower = $('#motorPowerCopy').val();
        var batteryManufacturer = $('#batteryManufacturerCopy').val();
        var amperes = $('#amperesCopy').val();
        var weight = $('#weightCopy').val();
        var maxSpeed = $('#maxSpeedCopy').val();
        var possibleMileage = $('#possibleMileageCopy').val();
        var motherboard = $('#motherboardCopy').val();
        var application = $('#applicationCopy').val();
        var image;
        if (document.getElementById('imageCopy').style.display == 'none') {
            image = $('#uploadCopyImage')[0].files[0];
        }
        else {
            image = document.getElementById('imageCopy').src;
        }
        var formData = new FormData();
        formData.append("Model", model);
        formData.append("ProductionDate", productionDate);
        formData.append("Colour", colour);
        formData.append("WheelsDiameter", wheelsDiameter);
        formData.append("MotorPower", motorPower);
        formData.append("BatteryManufacturer", batteryManufacturer);
        formData.append("Amperes", amperes);
        formData.append("Weight", weight);
        formData.append("MaxSpeed", maxSpeed);
        formData.append("PossibleMileage", possibleMileage);
        formData.append("Motherboard", motherboard);
        formData.append("Application", application);
        formData.append("ImageUrl", image);

        $.ajax({
            type: "POST",
            url: "/Product/Copy",
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

    $('#addProductOpenModal').on('hidden.bs.modal', function () {
        document.getElementById('model').value = "";
        document.getElementById('productionDate').value = "";
        document.getElementById('colour').value = "";
        document.getElementById('wheelsDiameter').value = "";
        document.getElementById('motorPower').value = "";
        document.getElementById('batteryManufacturer').value = "";
        document.getElementById('amperes').value = "";
        document.getElementById('weight').value = "";
        document.getElementById('maxSpeed').value = "";
        document.getElementById('possibleMileage').value = "";
        document.getElementById('motherboard').value = "";
        document.getElementById('application').value = "";
        document.getElementById('image').value = "";
    });

    $('button[name="deleteProduct"]').click(function (event) {
        var invoker = event.target;
        var id = getId(invoker.id);
        var formData = new FormData();
        formData.append("productId", id);

        $.ajax({
            type: "POST",
            url: "/Product/Delete",
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
});