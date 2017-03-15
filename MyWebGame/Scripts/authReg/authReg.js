$("document").ready(function () {
    //alert(document.cookie);
    // if(document.cookie.Nick)

    $("#modalAuthReg").on("show.bs.modal", function (e) {        
        if (e.relatedTarget.id == "authHref") {
            $("#authPanel").trigger("click");
        }
        else if (e.relatedTarget.id == "regHref") {
            $("#regPanel").trigger("click");
        }
        else if (e.relatedTarget.id == "nickNameHref") {
            $("#nickNamePanel").trigger("click");
        }
    });
    $("#nickNamePanel").click(function (e) {
        e.preventDefault();        
        $("#resultAuthReg").load("http://localhost:30657/Account/PlayNow");
    });
    $("#authPanel").click(function (e) {
        e.preventDefault();
        $("#resultAuthReg").load("http://localhost:30657/Account/SignIn");
    });
    $("#regPanel").click(function (e) {
        e.preventDefault();
        $("#resultAuthReg").load("http://localhost:30657/Account/Registration");
    });
   
});
