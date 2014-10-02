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

$(document).ready(function () {
    //methods that can or need to be defined when DOM is loaded
    //console.log("$(document).ready(function ()...");
    try {
        reinitializeServerUid();
        console.log("here ok3");
    } catch (e) {
        console.log("exception : " + e.description + "number:" + e.number);
    }
});

//function mainView_Loaded() {
//    var configObject = {
//        name: "MainView_Loaded",
//        arguments: [],
//        async: false
//    };
//    var res = callBackendWithConfig(configObject);
//}