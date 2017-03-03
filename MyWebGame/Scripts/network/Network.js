
$(function () {
    var hub = $.connection.hub.start();
    var network = $.connection.chatHub;    
    
    hub.done(function () {
        name = document.cookie.nickName;
        network.server.connect("justStarted");        
    });

    network.client.onConnected = function (id, userName, allUsers) {

        $("#hdId").val(id);
        $("#hello").show();
        $("#nowConnected").show();
        $("#userName").html(userName);
        alert("onconnected");
        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }
    };
    network.client.onNewUserConnected = function (id, name) {
       
        AddUser(id, name);
    }

    network.client.onUserDisconnected = function (id, name) {
        console.log(document.getElementById("networkResult"));
        console.log(name);
        console.log($("#networkResult p.another-user b").html());
        $("#networkResult").append('<p class="user-disconnected">' + 'now User ' + '<b>' + name + ' </b>' + ' is disconnected' + '</p>');
        $("#networkResult p.another-user b").each( function (index) {
            if ($(this).html() == name)
                $(this).parent().remove();
            $("#deletedUser").html(name);       
                
        });
    }
    function AddUser(id, name) {
        
        var userId = $('#hdId').val();        

        if (userId != id) {

            $("#networkResult").prepend('<p class="another-user" id="' + id + '"><b>' + name + '</b></p>');
        }
    }

});