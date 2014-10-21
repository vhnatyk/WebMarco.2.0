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

    var result = callBackend("GetUiMinimalResponseTime", currentTime, false);
    now = new Date();
    result += " end time :" + ("00" + now.getMinutes()).substr(-2) + ':' + ("00" + now.getSeconds()).substr(-2) + ':' + ("000" + now.getMilliseconds()).substr(-3);

    console.log('result : ' + result);

    result += "</br>" + callBackend("GetTestStringFromDatabase", null, false);

    //$("#resp1").html(result);
	displayShowcaseInfo(result);
}

/* from inline block */
function setPlatformNotePopupContentHtml(htmlValue) {
    $('div.popup-platform-note').html(htmlValue);
}

function displayShowcaseInfo(textToShow) {
    var demoBoardLocator = 'div.pricing-grid.demo-board';
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

    var console = (function (c) {
        return {
            log: function (v) {
                c.log('_original_');
                c.log(v);
                var debugDivs = $('#debugInfo');
                var debugDiv = debugDivs[0];
                if (debugDiv !== null) {
                    debugDivs.html(v);
                }
            }
        };
    }(window.console));
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
        reinitializeServerUid();
        console.log("reinitializeServerUid passed ok3");
    } catch (e) {
        console.log("exception : " + e.description + "number:" + e.number);
    }
});

//"View loaded" backend event handler method call template
//function mainView_Loaded() {
//    var configObject = {
//        name: "MainView_Loaded",
//        arguments: [],
//        async: false
//    };
//    var res = callBackendWithConfig(configObject);
//}