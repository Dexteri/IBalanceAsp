$(document).ready(function () {
    $('#addCounterpartyOpenModal').on('hidden.bs.modal', function () {
        document.getElementById('name').value = "";
        document.getElementById('email').value = "";
        document.getElementById('city').value = "";
        document.getElementById('type').value = "ООО";
    });

    $('#addPhone').click(function () {
        var html = '<div class="input-group"><br />';
        html += '<input name="addedPhone" type="text" placeholder="+7(050)123-45-67" value="+7" />';
        html += '<span class="input-group-btn">';
        html += '<button name="deletePhone" type="button" class="btn btn-danger btn-xs" style="margin-left:10px;"><span class="glyphicon glyphicon-minus"></span></button>';
        html += '</span>';
        html += '</div>';
        $('#phones').append(html);
    });

    $('#phones').on('click', 'button[name="deletePhone"]', function () {
        var phone = this.parentNode.parentNode;
        phone.parentNode.removeChild(phone);
    });

    $('#phonesUpdate').on('click', 'button[name="deletePhone"]', function () {
        var phone = this.parentNode.parentNode;
        phone.parentNode.removeChild(phone);
    });

    $("#addCounterparty").click(function () {
        var validData = true;
        var name = $('#name').val();
        var email = $('#email').val();
        var phonesList = document.getElementsByName('addedPhone');
        var phones = [];
        for (var i = 0; i < phonesList.length; i++) {
            if (phonesList[i].value != "+7") {
                phones.push(phonesList[i].value);
            }
        }
        var select = document.getElementById("type");
        var selectedType = select.options[select.selectedIndex].text;
        if (selectedType === "ООО") {
            selectedType = "LLC";
        }
        else if (selectedType === "ЧП") {
            selectedType = "IP";
        }
        else if (selectedType === "Физ. лицо") {
            selectedType = "Natural";
        }
        var city = $('#city').val();

        var formData = new FormData();
        formData.append("Name", name);
        formData.append("Email", email);
        for (var i = 0; i < phones.length; i++) {
            if (validatePhone(phones[i])) {
                formData.append('Phones', phones[i]);
            }
            else {
                validData = false;
                break;
            }
        }
        formData.append("Type", selectedType);
        formData.append("City", city);

        if (validData) {
            $.ajax({
                type: "POST",
                url: "/Counterparty/Add",
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
        }
        else {
            alert("Номер телефона должен быть в формате +7(999)999-99-99");
        }
    });

    $('#editCounterpartyOpenModal').on('show.bs.modal', function (e) {
        var invoker = $(e.relatedTarget);
        var nodes = invoker[0].parentNode.parentNode;
        document.getElementById('idUpdate').value = getId(invoker[0].id);
        document.getElementById('nameUpdate').value = nodes.querySelectorAll('[name=name]')[0].innerHTML;
        document.getElementById('emailUpdate').value = nodes.querySelectorAll('[name=email]')[0].innerHTML;
        var phones = nodes.querySelectorAll('[name=phone]');
        if (phones.length > 0) {
            for (var i = 0; i < phones.length; i++) {
                if (i == 0) {
                    var html = '<div class="input-group">';
                    html += '<input name="phoneUpdate" type="text" placeholder="+7(050)123-45-67" value="' + phones[i].innerHTML + '"/>';
                    html += ' <span class="input-group-btn">';
                    html += '<button id="addPhoneEdit" type="button" class="btn btn-success btn-xs" style="margin-left:10px;"><span class="glyphicon glyphicon-plus"></span></button>';
                    html += '</span>'
                    html += '</div>'
                    $('#phonesUpdate').append(html);
                }
                else {
                    var html = '<div class="input-group"><br />';
                    html += '<input name="phoneUpdate" type="text" placeholder="+7(050)123-45-67" value="' + phones[i].innerHTML + '"/>';
                    html += '<span class="input-group-btn">';
                    html += '<button name="deletePhone" type="button" class="btn btn-danger btn-xs" style="margin-left:10px;"><span class="glyphicon glyphicon-minus"></span></button>';
                    html += '</span>';
                    html += '</div>';
                    $('#phonesUpdate').append(html);
                }
            }
        }
        else {
            var html = '<div class="input-group">';
            html += '<input name="phoneUpdate" type="text" placeholder="+7(050)123-45-67" value="+7"/>';
            html += ' <span class="input-group-btn">';
            html += '<button id="addPhoneEdit" type="button" class="btn btn-success btn-xs" style="margin-left:10px;"><span class="glyphicon glyphicon-plus"></span></button>';
            html += '</span>'
            html += '</div>'
            $('#phonesUpdate').append(html);
        }

        document.getElementById('cityUpdate').value = nodes.querySelectorAll('[name=city]')[0].innerHTML;
        document.getElementById('typeUpdate').value = nodes.querySelectorAll('[name=type]')[0].innerHTML;
    });

    $('#phonesUpdate').on('click', '#addPhoneEdit', function () {
        var html = '<div class="input-group"><br />';
        html += '<input name="phoneUpdate" type="text" placeholder="+7(050)123-45-67" value="+7" />';
        html += '<span class="input-group-btn">';
        html += '<button name="deletePhone" type="button" class="btn btn-danger btn-xs" style="margin-left:10px;"><span class="glyphicon glyphicon-minus"></span></button>';
        html += '</span>';
        html += '</div>';
        $('#phonesUpdate').append(html);
    });

    $('#editCounterpartyOpenModal').on('hidden.bs.modal', function () {
        var phonesUpdate = document.getElementById("phonesUpdate");
        while (phonesUpdate.firstChild) {
            phonesUpdate.removeChild(phonesUpdate.firstChild);
        }
    });

    $("#editCounterparty").click(function () {
        var validData = true;
        var id = $('#idUpdate').val();
        var name = $('#nameUpdate').val();
        var email = $('#emailUpdate').val();
        var city = $('#cityUpdate').val();
        var phonesUpdate = document.getElementById('phonesUpdate');
        var phonesList = phonesUpdate.querySelectorAll('[name=phoneUpdate]');
        var phones = [];
        for (var i = 0; i < phonesList.length; i++) {
            if (phonesList[i].value != "+7") {
                phones.push(phonesList[i].value);
            }
        }
        var select = document.getElementById("typeUpdate");
        var selectedType = select.options[select.selectedIndex].text;
        if (selectedType === "ООО") {
            selectedType = "LLC";
        }
        else if (selectedType === "ЧП") {
            selectedType = "IP";
        }
        else if (selectedType === "Физ. лицо") {
            selectedType = "Natural";
        }
        var formData = new FormData();
        formData.append("Id", id);
        formData.append("Name", name);
        formData.append("Email", email);
        for (var i = 0; i < phones.length; i++) {
            if (validatePhone(phones[i])) {
                formData.append('Phones', phones[i]);
            }
            else {
                validData = false;
                break;
            }
        }
        formData.append("Type", selectedType);
        formData.append("City", city);
        if (validData) {
            $.ajax({
                type: "POST",
                url: "/Counterparty/Update",
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
        }
        else {
            alert("Номер телефона должен быть в формате +7(999)999-99-99");
        }
    });


    $('button[name="deleteCounterparty"]').click(function (event) {
        var invoker = event.target;
        var id = getId(invoker.id);
        var formData = new FormData();
        formData.append("counterpartyId", id);

        $.ajax({
            type: "POST",
            url: "/Counterparty/Delete",
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