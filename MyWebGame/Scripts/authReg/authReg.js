$(document).ready(function () {  

    $("#modalAuthReg").on("show.bs.modal", function (e) {        
        if (e.relatedTarget.id == "authHref") {
            $("#authPanel").trigger("click");
        }
        else if (e.relatedTarget.id == "regHref") {
            $("#regPanel").trigger("click");
        }
        else if (e.relatedTarget.id == "nickNameHref") {
            $("#nickNamePanel").trigger("click");
        }
    });
    $("#nickNamePanel").click(function (e) {
        e.preventDefault();        
        $("#resultAuthReg").load("http://localhost:30657/Account/PlayNow");
    });
    $("#authPanel").click(function (e) {
        e.preventDefault();
        $("#resultAuthReg").load("http://localhost:30657/Account/SignIn");
    });
    $("#regPanel").click(function (e) {
        e.preventDefault();
        $("#resultAuthReg").load("http://localhost:30657/Account/Registration");
    });
    var network, name;
    network = $.connection.chatHub;

    name = $("#nickNameForm").find(".input-auth-reg").val();

    $("#resultAuthReg").on("submit", "#nickNameForm", function (event) {               

        name = $("#nickNameForm").find(".input-auth-reg").val();
        event.preventDefault();

        $.post("http://localhost:30657/Account/PlayNow", $("#nickNameForm").serialize())
        .done(function (data) {
            if (data.successful) {

                $(".container-with-weight").css("display", "block");
                $.connection.hub.start().done(function () {                   
                    network.server.connect(name)
                    $("#hello").show();
                    $("#nowConnected").show();
                    $("#userName").html(name);                    
                });

                $("#nickNameHref").hide();
                $(".close-auth-reg").trigger("click");
                $("#chatBody").removeClass("hidden");
                $("#nickNamePanel").hide();
            }
            else {
                $("#resultAuthReg").html(data);
            }         

        });
    });
    $("#resultAuthReg").on("click", "#forgot-password", function (e) {        
        e.preventDefault();
        $("#resultAuthReg").load("http://localhost:30657/Account/OnlyEmail");
    });
    $("#resultAuthReg").on("click", "#onlyEmail", function (e) {
        e.preventDefault();
        $("#resultAuthReg").load("http://localhost:30657/Account/WaitingForConfirm");
    });
    $("#resultAuthReg").on("submit", "#formAuth", function (event) {
        name = $("#formAuth").find(".input-auth-reg").val();
        event.preventDefault();

        $.post("http://localhost:30657/Account/SignIn", $("#formAuth").serialize())
        .done(function (data) {


            if (data.responceMessage == "successful") {
                
                location.href = "http://localhost:30657/";
            }
            else if (data.responceMessage == "emailNotConfirmed") {
                $("#resultAuthReg").load("http://localhost:30657/Account/PleaseConfirmedEmail");
            }
            $("#resultAuthReg").html(data);
        });
    });
    $("#resultAuthReg").on("submit", "#formReg", function (e) {

        e.preventDefault();
        if ($(this).valid()) {
            $.post("http://localhost:30657/Account/Registration", $("#formReg").serialize())
                .done(function (data) {                    
                    if (data.successful) {
                        $("#resultAuthReg").load("http://localhost:30657/");
                    }
                    $("#resultAuthReg").html(data);
                });
        };
    });
});
