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