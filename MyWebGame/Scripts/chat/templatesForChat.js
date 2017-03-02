function getTime() {
    var date = new Date();
    var hours = date.getHours();
    var minutes = date.getMinutes();
    var timeStr = hours + " " + resources.hours + " " +
        minutes + " " + resources.minutes;
    return timeStr;
}
function createData(name, message) {  
    var data = {
        timeStr : getTime(),        
        name: name, 
        message: message
    };
    return data;
}
function createTemplate(name, message) {
    if (message != '') {
        $("#messageTmpl").tmpl(createData(name, message)).appendTo(".chat");
    }
}
function scrollDown() {
    var heightOfChat = $('.chat').height() + 10;
    $('#panelBodyChat').scrollTop(heightOfChat);
}
function conversionHtmlToText(str) {
   
    var newStr = "";
    for (var i = 0; i < str.length; i ++)
    {
        if (str[i] == "<") newStr += "&lt;"
        else if (str[i] == ">") newStr += "&gt;"
        else {
            newStr += str[i];
        }
    }
    return newStr;
}
function checkSpaces(str) {
    var bool, result = str.search(/ /);
    if (result == -1) bool = true
    else bool = false;
    return bool;
}
