$(function () {    
    var network = $.connection.chatHub;
    var players = [];
    
    $.connection.hub.start().done(function () {
        network.server.checkAuth();       
    });
    //-------------------For three js--------------------

    network.client.initialCreate = function (sizeX, sizeY, newCoord, name) {
        //var newCoord = JSON.parse(newCoord);        
        //players.push(name);
        //createRectangle(players, newCoord.x, newCoord.y);
    }
    $("body").click(function (event) {
        var playerBridge = {
            name: "Artem"
            };
        players.push(playerBridge);
        createRectangle(players, 40, 40);
    });
    
    network.client.setPositions = function (data) {   
        var result = JSON.parse(data),
            name;        
        for (var i = 0; i < 1; i ++) {
            name = result[i].userName;
            cube.position.x = result[i].Monster.PosX;
            cube.position.y = result[i].Monster.PosY;
            cube.position.z = result[i].Monster.PosZ;
        }
        render();
    }
  
    //$("body").keydown(function (e) {        
    //    if (e.target.id != "message") {            
            
    //            network.server.mooved(e.keyCode);               
    //        }    
    //    });
    //---------------------For chat-----------------------
    network.client.addMessage = function (name, message) {                
        createTemplate(name, message);
        scrollDown();
    };

    network.client.onConnected = function (id, userName, allUsers) {

        //---------------- For chat ----------------
        $('#loginBlock').hide();
        $('#chatBody').show();
        //For other
        $("#hdId").val(id);
        $("#hello").show();
        $("#nowConnected").show();
        $("#userName").html(userName);
        //---------------- For users ----------------
        $('#hdId').val(id);
        $('#username').val(userName);
        $('#header').html('<h3>' + resources.welcome + ", " + conversionHtmlToText(userName) + '</h3>');
        scrollDown();
        
        alert("onconnected");
        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }
    }; 

    network.client.takeUserName = function (name) {
        messageHandler(name, network);
    }
    network.client.onNewUserConnected = function (id, name) {
       
        AddUser(id, name);
    }

    network.client.onUserDisconnected = function (id, name, allUsers) {

        $('#' + id).remove();

        $("#disconnectedUsers").append('<p class="user-disconnected">' + 'now User ' + '<b>' + name + ' </b>' + ' is disconnected' + '</p>');
        $("#networkResult").empty();
        $("#chatusers").empty();
        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }        
    }
    function htmlEncode(value) {
        var encodedValue = $('<div />').text(value).html();
        return encodedValue;
    }
    function AddUser(id, name) {

        //---------------- For chat ----------------
        var userId = $('#hdId').val();

        if (userId != id) {

            $("#chatusers").append('<p id="' + id + '"><b>' + name + '</b></p>');
        }
        //---------------- For users ----------------      
        var userId = $('#hdId').val();        

        if (userId != id) {

            $("#networkResult").prepend('<p class="another-user" id="' + id + '"><b>' + name + '</b></p>');
        }
    }

});