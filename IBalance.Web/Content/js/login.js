$(document).ready(function () {
    $("#enter").click(function () {
        var login = $('#login').val();
        var password = $('#password').val();
        var formData = new FormData();
        formData.append("Login", login);
        formData.append("Password", password);

        $.ajax({
            type: "POST",
            url: "/Account/Login",
            dataType: 'json',
            data: formData,
            processData: false,
            contentType: false,
            success: function (data) {
                console.log("boo");
                if (data['result'] == "invalid login") {
                    window.location.reload();
                }
                else if (data['result'] == "redirect") {
                    window.location = data["url"];
                }
            }
        });
    });
});