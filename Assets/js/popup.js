/***************************/
//@Author: Adrian "yEnS" Mato Gondelle
//@website: www.yensdesign.com
//@email: yensamg@gmail.com
//@license: Feel free to use it, but keep this credits please!					
/***************************/

//SETTING UP OUR POPUP
//0 means disabled; 1 means enabled;
var popupStatus = 0;

//loading popup with jQuery magic!
function loadPopup() {
    //loads popup only if it is disabled
    if (popupStatus == 0) {
        $("#backgroundPopup").css({
            "opacity": "0.7"
        });
        $("#backgroundPopup").fadeIn("slow");
        $("#popupContact").fadeIn("slow");
        popupStatus = 1;
    }
}

//disabling popup with jQuery magic!
function disablePopup() {
    //disables popup only if it is enabled
    if (popupStatus == 1) {
        $("#backgroundPopup").fadeOut("slow");
        $("#popupContact").fadeOut("slow");
        popupStatus = 0;
    }
}

//centering popup
function centerPopup() {
    //request data for centering
    var windowWidth = $(window).width();
    var windowHeight = $(window).height();
    var popupHeight = $("#popupContact").height();
    var popupWidth = $("#popupContact").width();
    //centering
    $("#popupContact").css({
        "position": "absolute",
        "top": windowHeight / 2 - popupHeight / 2,
        "left": windowWidth / 2 - popupWidth / 2
    });
    //only need force for IE6

    $("#backgroundPopup").css({
        "height": windowHeight
    });
}

function centerPopup1() {

    //request data for centering
    $("#backgroundPopup").before("<div id='msg_div'></div>");

    var windowWidth = $(window).width();
    var windowHeight = $(window).height();
    var popupHeight = 230;
    var popupWidth = 350;
    //centering
    $("#msg_div").css({
        "position": "absolute",
        "top": windowHeight / 2 - popupHeight / 2,
        "left": windowWidth / 2 - popupWidth / 2
    });
    //only need force for IE6


    $("#backgroundPopup").css({
        "height": windowHeight
    });
}

function loadPopup1() {
    //loads popup only if it is disabled
    if (popupStatus == 0) {
        $("#backgroundPopup").css({
            "opacity": "0.7"
        });
        $("#backgroundPopup").fadeIn("slow");
        $("#msg_div").fadeIn("slow");
        popupStatus = 1;
    }
}

function disablePopup1() {
    //disables popup only if it is enabled
    if (popupStatus == 1) {
        $("#backgroundPopup").fadeOut("slow");
        $("#msg_div").fadeOut("slow");
        popupStatus = 0;
    }
}

showMessageBox = function (arr_param) {
    var title = arr_param[0];
    var message = arr_param[1];
    var isOkOrYesNo = "true";
    var yesAction = "";
    var no

    try {
        isOkOrYesNo = arr_param[2];
    } catch (e) { }

    try {
        yesAction = arr_param[3];
    } catch (e) { }

    var buttons = "";
    if (isOkOrYesNo == "true") {
        buttons = "<input style='position: absolute; bottom: 5px; left:110px;' class='btnBlue' type='button' value='OK' onclick='disablePopup1();'>";
    } else {
        if (yesAction == null || yesAction == '') {
            yesAction = "window.open('Main.htm?ACTION=MENUEXIT&XID=0', '_self')";
        }
        buttons = "<input style='position: absolute; bottom: 15px; left:180px; width: 80px;' class='btnRed' type='button' value='No' onclick='onExitCancel();'>";
        buttons += "<input style='position: absolute; bottom: 15px; left:90px; width: 80px;' class='btnRed' type='button' value='Yes' onclick=\"" + yesAction + ";\">";
    }


    centerPopup1();
    loadPopup1();

    $("#msg_div")[0].innerHTML = "<h1>" + title + "</h1><div style='padding: 10px;'><p id='contactArea'><div class=\"popupMessage\" >" + message + "</div></p>" + buttons + "</div>";
}


refreshProgressBar = function (progress) {
    centerPopup();
    //load popup
    loadPopup();
    //startTimer();
    $("#popupProgressBar").css({
        background: "-webkit-gradient(linear,left top, right top, color-stop(" + (progress / 100) + ", rgb(234, 231, 16)), color-stop(" + ((progress) / 100) + ", rgb(245,245,245)))"
    }).css({
        background: "-moz-linear-gradient(left , rgba(42,212,42,0.9) " + progress + "%, rgba(211,202,230,0.3) " + (progress + 0.4) + "%)"
    });
}

updateProgressBar = function (arr_param) {
    $("#popupProgressDesc")[0].innerHTML = arr_param[0];
    $("#popupProgressCount")[0].innerHTML = arr_param[1];
}

function startTimer() {
    var timetogo = 100;
    var progress = 1;
    var timer = window.setInterval(function () {
        $("#popupProgressBar").css({
            background: "-webkit-gradient(linear,left top, right top, color-stop(" + progress / 100 + ", rgb(42,212,42)), color-stop(" + ((progress + 0.01) / 100) + ", rgb(211,202,230)))"
        }).css({
            background: "-moz-linear-gradient(left , rgba(42,212,42,0.9) " + progress + "%, rgba(211,202,230,0.3) " + (progress + 0.4) + "%)"
        });
        if (timetogo <= 0) {

            window.clearInterval(timer);
            disablePopup();
        }
        progress++;
        timetogo--;
    }, 100);
}

//*-----------on Cancel click  ------*/
onCancelClick = function () {
    var cfgCancel = {
        name: "OnCancelClick",
        arguments: [""],
        async: false
    };

    var res = window["callCSharp"](cfgCancel);
}

onExitCancel = function () {
    var cfgCancel = {
        name: "OnExitCancel",
        arguments: [""],
        async: false
    };

    disablePopup1();
    var res = window["callCSharp"](cfgCancel);    
}

//CONTROLLING EVENTS IN jQuery
$(document).ready(function () {
    //LOADING POPUP
    //Click the button event!
    $("#showpopup").click(function () {
        //centering with css
        centerPopup();
        //load popup
        loadPopup();
        startTimer();
    });

    //CLOSING POPUP
    //close click event
    $("#popupContactClose").click(function () {
        onCancelClick();
        disablePopup();
    });
    $(document).keypress(function (e) {
        if (e.keyCode == 27 && popupStatus == 1) {
            onCancelClick();
            disablePopup();
        }
    });
});