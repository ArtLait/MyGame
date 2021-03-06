﻿$(function () {
    var network = $.connection.chatHub;
    var centerMap = { x: 0, y: 0 };
     
    $.connection.hub.start().done(function () {
        network.server.checkAuth();                      
    });
    //-------------------For three js--------------------  
    network.client.initialSettings = function (worldSizeX, worldSizeY, someFood) {
           
        var someFoodUser = JSON.parse(someFood);
        for (var i = 0; i < someFoodUser.length; i++) {

            someFoodArray.push(createSomeFood(someFoodUser[i].PosX, someFoodUser[i].PosY, someFoodUser[i].Size, someFoodUser[i].Size, someFoodUser[i].Color));
        }
        someFoodArray[0].testName = "Artem";
        console.log(someFoodArray[0]);
        console.log(someFoodArray);
        centerMap.x = window.innerWidth / 2;
        centerMap.y = window.innerHeight / 2;
        plane.scale.set(worldSizeX, worldSizeY, 1);
    }
    
    network.client.addMoreMembers = function (sizeX, sizeY, users) {

        var users = JSON.parse(users);
        for (var i = players.length; i < users.length; i++) {
            var cube = createRectangle(players, users[i].PosX,
                users[i].PosY, users[i].SizeX, users[i].SizeY,
                users[i].Color);
            players.push({
                cube: cube
            });
        };
        //mousemove
        $("body").click(function (e) {
            var dirX = e.pageX - centerMap.x;
            var dirY = centerMap.y - e.pageY;
            network.server.moveAndRotate(dirX, dirY)
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
        for (var i = 0; i < players.length; i ++) {         
            players[i].cube.position.x = result[i].PosX;
            players[i].cube.position.y = result[i].PosY;
            camera.position.x = result[i].PosX;
            camera.position.y = result[i].PosY;
            players[i].cube.material.rotation = result[i].Rotation;
        }
        
        render();
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