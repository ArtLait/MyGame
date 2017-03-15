$("#formReg").submit(function (e) {

    e.preventDefault();
    if ($(this).valid()) {
        $.post("http://localhost:30657/Account/Registration", $("#formReg").serialize())
            .done(function (data) {
                console.log(data);
                if (data.successful) {
                    $("#resultAuthReg").load("http://localhost:30657/");
                }
                $("#resultAuthReg").html(data);
            });
    };
});