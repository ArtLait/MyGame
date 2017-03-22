$(function () {
    var network = $.connection.chatHub;
    var centerMap = { x: 0, y: 0 };

    $.connection.hub.start().done(function () {        
        network.server.checkAuth();
    });
    //-------------------For three js--------------------  
    network.client.initialSettings = function (worldSizeX, worldSizeY, someFood, currentClientConnectionId) {
        connectionId = currentClientConnectionId;
        var someFoodUser = JSON.parse(someFood);
        someFoodUser.forEach(function (item, i) {
            var food = createSomeFood(item.PosX, item.PosY,
                    item.Size, item.Size, item.Color); 
            someFoodCollection[item.Id] = food;
                               
        });                
        centerMap.x = window.innerWidth / 2;
        centerMap.y = window.innerHeight / 2;
        plane.scale.set(worldSizeX, worldSizeY, 1);
    }

    network.client.addMoreMembers = function (sizeX, sizeY, users) {
        var users = JSON.parse(users);      
        users.forEach(function (item, i) {
            if (players[item.ConnectionId] == undefined) {
                var cube = createRectangle(item.PosX,
                item.PosY, item.SizeX, item.SizeY,
                item.Color);
                players[item.ConnectionId] = cube;
            }
        });        

        //mousemove
        $("body").click(function (e) {
            if (e.target.id != "message") {
                var dirX = e.pageX - centerMap.x;
                var dirY = centerMap.y - e.pageY;
                network.server.moveAndRotate(dirX, dirY);
            }
        });

        $("body").keydown(function (e) {
            if (e.target.id != "message") {

                network.server.moovedDown(e.keyCode);
            }
        });
        $("body").keyup(function (e) {
            if (e.target.id != "message") {

                network.server.moovedUp(e.keyCode);
            }
        });
    }

    network.client.setPositions = function (data) {
        var result = JSON.parse(data);
        result.forEach(function (item, i) {
            if (item.ConnectionId == connectionId) {
                camera.position.x = item.PosX;
                camera.position.y = item.PosY;
            }
            players[item.ConnectionId].position.x = item.PosX;
            players[item.ConnectionId].position.y = item.PosY;
            players[item.ConnectionId].material.rotation = item.Rotation;
        });

        render();
    }
    network.client.clashWithFood = function (deletedFood, newPositionFood) {

        var idFood = someFoodCollection[JSON.parse(deletedFood).Id];
        scene.remove(idFood);
        var newPositionFood = JSON.parse(newPositionFood);
        var food = createSomeFood(newPositionFood.PosX, newPositionFood.PosY,
                    newPositionFood.Size, newPositionFood.Size, newPositionFood.Color);
        someFoodCollection[newPositionFood.Id] = food;
    }
    network.client.clashWithPlayer = function (idPlayer, newPositionPlayer) {
                
        scene.remove(idPlayer);
        var newPositionPlayer = JSON.parse(newPositionPlayer);
        var player = createRectangle(newPositionPlayer.PosX, newPositionPlayer.PosY,
                    newPositionPlayer.Size, newPositionPlayer.Size, newPositionPlayer.Color);
        someFoodCollection[newPositionPlayer.Id] = player;
    }
   
    network.client.newWeight = function (weight, id) {
        console.log(id);
        $("#weightUser").empty(weight)
        $("#weightUser").append(weight);
        players[id].scale.x += weight * 10;
        players[id].scale.y += weight * 10;
    }

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
        //--------------------For three JS           
        scene.remove(players[id]);
        //--------------------For chat--------------------
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