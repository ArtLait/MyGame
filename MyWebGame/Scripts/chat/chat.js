$(function () {

    $('#loginBlock').show();
    // Ссылка на автоматиsчески-сгенерированный прокси хаба
    var chat = $.connection.chatHub;    

    // Объявление функции, которая хаб вызывает при получении сообщений
    chat.client.addMessage = function (name, message) {
        // Добавление сообщений на веб-страницу
        createTemplate(name, message);
        scrollDown();
    };

    // Функция, вызываемая при подключении нового пользователя
    chat.client.onConnected = function (id, userName, allUsers) {    
        $('#loginBlock').hide();
        $('#chatBody').show();
        // установка в скрытых полях имени и id текущего пользователя
        $('#hdId').val(id);
        $('#username').val(userName);
        $('#header').html('<h3>' + resources.welcome + ", " + conversionHtmlToText(userName) + '</h3>');
        scrollDown();

        // Добавление всех пользователей
        for (i = 0; i < allUsers.length; i++) {

            AddUser(allUsers[i].ConnectionId, allUsers[i].Name);
        }
    }

    // Добавляем нового пользователя
    chat.client.onNewUserConnected = function (id, name) {

        AddUser(id, name);
    }

    // Удаляем пользователя
    chat.client.onUserDisconnected = function (id, userName) {

        $('#' + id).remove();
    }
    // Открываем соединение
    $.connection.hub.start().done(function () {


        $('#sendmessage').click(function () {

            // Вызываем у хаба метод Send
            chat.server.send($('#username').val(), $('#message').val());
            $('#message').val('');
        });
        $('#message').keydown(function (e) {
            if (e.keyCode == 13) {
                $("#sendmessage").trigger("click");
            }
        });

        // обработка логина
        $("#btnLogin").click(function () {

            var name = $("#txtUserName").val();
            if (name.length > 0) {
                if (name.length < 16) {
                    if (checkSpaces(name)) {
                        chat.server.connect(name);
                    }
                    else {
                        alert(resources.errorEntryNickNameWithoutSpaces)
                    }
                }
                else {
                    alert(resources.nameIsTooLength)
                }
            }
            else {
                alert(resources.entryNameError);
            }
        });
        $("#txtUserName").keydown(function (e) {
            if (e.keyCode == 13) {
                $("#btnLogin").trigger("click");
            }
        })
    });
});
// Кодирование тегов
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
//Добавление нового пользователя
function AddUser(id, name) {

    var userId = $('#hdId').val();

    if (userId != id) {

        $("#chatusers").append('<p id="' + id + '"><b>' + name + '</b></p>');
    }
}