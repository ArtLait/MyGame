$(function () {    
    var network = $.connection.chatHub;
    
     
    $.connection.hub.start().done(function () {
        network.server.checkAuth(window.innerWidth, window.innerHeight);        
    });
    //-------------------For three js--------------------  
    network.client.initialSettings = function (worldSizeX, worldSizeY) {       
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
            
            network.server.moveAndRotate(e.pageX, e.pageY)                
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
            players[i].cube.rotation.z = result[i].Rotation;
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