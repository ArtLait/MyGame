$(function () {
    var network = $.connection.chatHub;
    network.client.onConnected = function (id, userName, allUsers) {

        $("#hdId").val(id);
        alert("onconnected");
        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }
    };
    network.client.onNewUserConnected = function (id, name) {
       console.log("addNewUser");
        AddUser(id, name);
    }
    function AddUser(id, name) {
        
        var userId = $('#hdId').val();

        console.log(userId == id);        

        if (userId != id) {

            $("#networkResult").append('<p id="' + id + '"><b>' + name + '</b></p>');
        }
    }

});