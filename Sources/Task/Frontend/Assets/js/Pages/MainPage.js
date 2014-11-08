function testClick() {
    //var configObject = {
    //    name: "GetViewHtml",
    //    arguments: ["FlightsView"],
    //    async: false
    //};

    //var res = callCSharp(configObject);

    var now = new Date();

    var currentTime = ("00" + now.getMinutes()).substr(-2) + ':' + ("00" + now.getSeconds()).substr(-2) + ':' + ("000" + now.getMilliseconds()).substr(-3);
    console.log("now.toTimeString() : " + currentTime);

    var result = callBackend("GetUiMinimalResponseTime", currentTime);
    now = new Date();
    result += " end time :" + ("00" + now.getMinutes()).substr(-2) + ':' + ("00" + now.getSeconds()).substr(-2) + ':' + ("000" + now.getMilliseconds()).substr(-3);

    console.log('result : ' + result);

    result += "</br>" + callBackend("GetTestStringFromDatabase");

	displayShowcaseInfo(result);
}

/* from inline block */
function setPlatformNotePopupContentHtml(htmlValue) {
    $('div.popup-platform-note').html(htmlValue);
}

function displayShowcaseInfo(textToShow) {
    var demoBoardLocator = 'div.grid1.demo-board';
    if (isVisible(demoBoardLocator)) {
        $(demoBoardLocator).html('<p>' + textToShow + '</p>');
    } else {
        $('#demoShowcaseResultContainer').html(textToShow);
        $.magnificPopup.open({
            items: {
                src: '#small-dialog1'
            },
            type: 'inline',
            preloader: false,
            closeBtnInside: true,
            mainClass: 'my-mfp-zoom-in'//,
            //modal: true
        });
    }
}

$(document).ready(function () {

    //methods that can or need to be defined when DOM is loaded

    //console.log('hello world');

    //console.log("here ok1");
    $('.popup-with-zoom-anim').magnificPopup({
        type: 'inline',
        fixedContentPos: false,
        fixedBgPos: true,
        overflowY: 'auto',
        closeBtnInside: true,
        preloader: false,
        midClick: true,
        removalDelay: 300,
        mainClass: 'my-mfp-zoom-in'
    });

    //console.log("here ok2");     

    //console.log("$(document).ready(function ()...");
    try {        
        console.log("MainPage.js loaded");
    } catch (e) {
        console.log("exception : " + e.description + "number:" + e.number);
    }
});