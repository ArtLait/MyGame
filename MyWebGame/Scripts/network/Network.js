$(function () {    
    var network = $.connection.chatHub;        
    
    $.connection.hub.start().done(function () {
        network.server.checkAuth();
       // network.server.eventHandlerTestArtem();
    });
    //For three js
    network.client.updateWorld = function (x, y, z) {
        console.log(x, y, z);
        cube.position.x = x;
        cube.position.y = y;
        cube.position.z = z;
        render();
    }
    $("body").keydown(function (e) {
            if (e.target.id != "message") {
                //network.server.EventHundler(e.keyCode);
                network.server.mooved(e.keyCode);               
            }
    
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
    network.client.testConsoleLog = function (testMessage) {
        console.log(testMessage);
    }
    network.client.eventHundler = function (result) {
        console.log(result);
    }
    network.client.testClient = function (message) {
        console.log(message);
    }
    network.client.takeUserName = function (name) {
        messageHandler(name, network);
    }
    network.client.onNewUserConnected = function (id, name) {
       
        AddUser(id, name);
    }
    network.client.hundler = function (x, y, z) {

    }

    network.client.onUserDisconnected = function (id, name, allUsers) {

        $('#' + id).remove();

        $("#disconnectedUsers").append('<p class="user-disconnected">' + 'now User ' + '<b>' + name + ' </b>' + ' is disconnected' + '</p>');
        $("#networkResult").empty();
        $("#chatusers").empty();
        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }        
        //$("#networkResult p.another-user b").each( function (index) {
        //    if ($(this).html() == name)
        //        $(this).parent().remove();                                 
        //});
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