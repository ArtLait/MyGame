var network, name;
network = $.connection.chatHub;

name = $("#nickNameForm").find(".input-auth-reg").val();
$("#nickNameForm").submit(function (event) {

    name = $("#nickNameForm").find(".input-auth-reg").val();
    event.preventDefault();

    $.post("http://localhost:30657/Account/PlayNow", $("#nickNameForm").serialize())
    .done(function (data) {
        if (data.successful) {

            $.connection.hub.start().done(function () {
                network.server.connect(name);
                $("#hello").show();
                $("#nowConnected").show();
                $("#userName").html(name);
                messageHandler(name, network);
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