var network, name;
network = $.connection.chatHub;
alert("Why, you don't work");
$("#forgot-password").click(function (e) {
    alert("forgot-password");
    e.preventDefault();
    $("#resultAuthReg").load("http://localhost:30657/Account/OnlyEmail");
});
$("#formAuth").submit(function (event) {    
    name = $("#formAuth").find(".input-auth-reg").val();
    event.preventDefault();

    $.post("http://localhost:30657/Account/SignIn", $("#formAuth").serialize())
    .done(function (data) {
        

        if (data.responceMessage == "successful") {

            alert("successful");
            location.href = "http://localhost:30657/";
        }
        else if (data.responceMessage == "emailNotConfirmed") {
            $("#resultAuthReg").load("http://localhost:30657/Account/PleaseConfirmedEmail");
        }
        $("#resultAuthReg").html(data);
    });    
});
