$(function () {
    
    var network = $.connection.chatHub;        

    $.connection.hub.start().done(function () {
        var name = network.server.checkAuth();

        $('#sendmessage').click(function () {
            // Вызываем у хаба метод Send
            alert($("#message").val());
            network.server.send(name, $('#message').val());
            $('#message').val('');
        });
        $('#message').keydown(function (e) {
            if (e.keyCode == 13) {
                $("#sendmessage").trigger("click");
            }
        });
    });

    network.client.addMessage = function (name, message) {
        // Добавление сообщений на веб-страницу        
        createTemplate(name, message);
        scrollDown();
    };

    network.client.onConnected = function (id, userName, allUsers) {

        $("#hdId").val(id);
        $("#hello").show();
        $("#nowConnected").show();
        $("#userName").html(userName);
        //For chat
        $('#loginBlock').hide();
        $('#chatBody').show();
        // установка в скрытых полях имени и id текущего пользователя
        $('#hdId').val(id);
        $('#username').val(userName);
        $('#header').html('<h3>' + resources.welcome + ", " + conversionHtmlToText(userName) + '</h3>');
        scrollDown();
        
        alert("onconnected");
        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }
    };
    network.client.onNewUserConnected = function (id, name) {
       
        AddUser(id, name);
    }

    network.client.onUserDisconnected = function (id, name) {

        $('#' + id).remove();

        $("#networkResult").append('<p class="user-disconnected">' + 'now User ' + '<b>' + name + ' </b>' + ' is disconnected' + '</p>');
        $("#networkResult p.another-user b").each( function (index) {
            if ($(this).html() == name)
                $(this).parent().remove();
            $("#deletedUser").html(name);       
                
        });
    }
    function htmlEncode(value) {
        var encodedValue = $('<div />').text(value).html();
        return encodedValue;
    }
    function AddUser(id, name) {

        //For chat
        var userId = $('#hdId').val();

        if (userId != id) {

            $("#chatusers").append('<p id="' + id + '"><b>' + name + '</b></p>');
        }
        //For users        
        var userId = $('#hdId').val();        

        if (userId != id) {

            $("#networkResult").prepend('<p class="another-user" id="' + id + '"><b>' + name + '</b></p>');
        }
    }

});