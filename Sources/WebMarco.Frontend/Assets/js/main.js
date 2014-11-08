/* various common service methods and vars */
/* logging methods */
var console = (function (c) {
    return {
        log: function (v) {
            //c.log('_original_');
            c.log(v);
            var debugDivs = $('#debugInfo');
            var debugDiv = debugDivs[0];
            if (debugDiv !== null) {
                debugDivs.html(v);
            }
        }
    };
}(window.console));

writeLog = function (logText, ex, noFormat) {
    //console.log(logText);

    if (ex) {
        if (logText) {
            logText = "Exception note: \n" + logText + "\nException string: \n" + ex;
        } else {
            logText = ex;
        }
    }

    if (!noFormat) {
        logText = "\n\n******************* JavaScript log message ******************\n\n" + logText + "\n\n------------------- end of JavaScript log message ------------------\n\n";
    }

    //callBackend("WriteFrontendLog", logText, true);
}
/* end of logging methods */

function isEmpty(obj) {
    if (typeof obj == 'undefined' || obj === null || obj === '') return true;
    if (typeof obj == 'number' && isNaN(obj)) return true;
    if (obj instanceof Date && isNaN(Number(obj))) return true;
    return false;
}

function isVisible(elemLocator) {
    try {
        return $(elemLocator).is(":visible");
    } catch (e) { }
    return false;
}

function htmlEncode(value) {
    //create an in-memory div, set it's inner text(which jQuery automatically encodes)
    //then grab the encoded contents back out.  The div never exists on the page.
    return $('<div/>').text(value).html();
}

function htmlDecode(value) {
    return $('<div/>').html(value).text();
}

function Guid() {
    var S4 = function () {
        return ("0000" + Math.floor(
                Math.random() * 0x10000 /* 65536 */
            ).toString(16)).substr(-4);
    };

    return (
            S4() + S4() + '-' +
            S4() + '-' +
            S4() + '-' +
            S4() + '-' +
            S4() + S4() + S4());
}

/* WebMarco Bridge */
/* handle call from backend */
backendCallResultHandler = function (config) {
    try {
        if (!config) {
            return;
        }

        if (config.IsAsync) {
            Ext.Function.defer(function () {
                window[config.MethodName](config.Params);
            }, 10);
        } else {
            return window[config.MethodName](config.Params);
        }
    } catch (e) {
        console.log(e, 'exception in backendCallResultHandler with function name: ' + config.MethodName);
        throw e;
    }
}

backendCallResultHandlerWithJsonConfig = function (configAsJson) {
    backendCallResultHandler(JSON.parse(configAsJson));
}
/* end of handle call from backend */

/* call backend from frontend */
/* backend bridge media prototypes */
CallConfig = function (uid, methodName, params, isAsync) {
    if (isEmpty(uid)) {
        uid = Guid();
    }
    this.UID = uid; //unique ID of call,
    this.MethodName = methodName;
    this.Params = params;
    this.IsAsync = (isAsync) ? true : false;
}
/* end of backend bridge media prototypes */

callBackend = function (name, arguments, isAsync) {
    return callBackendWithConfig(new CallConfig(null, name, arguments, isAsync));
}

callBackendWithConfig = function (callConfig) {
    var result;

    if (isEmpty(callConfig.UID)) {
        callConfig.UID = Guid();
    }

    $.ajax({
        //crossOrigin: true,
        async: callConfig.IsAsync, //true,
        type: 'POST',
        url: 'http://' + serverIP + ':' + serverInstancePort + '/' + serverInstanceUid + '/callbackend?params=' + encodeURIComponent(JSON.stringify(callConfig)),
        // data: callConfig,
        success: function (responseData) {
            console.log('ajax success');
            var callbackConfig = null;
            try {
                callbackConfig = JSON.parse(responseData);
            } catch (e) { }
            if (isEmpty(callbackConfig)) {
                result = responseData;//doesn't contain callback config
            } else {
                console.log('backendCallResultHandler :' + callbackConfig.MethodName);
                result = backendCallResultHandler(callbackConfig);//result, probably is unnecessary here, but can be used if it is
            }
        },
        error: function (request, status, err) {
            var ajaxResult = 'ajax error : ' + err;
            console.log(ajaxResult);
            result = ajaxResult;
        }
    });

    return result;
}
/* end of call backend from frontend */

/* server initialization */
var serverInstanceUid = '898316fb-f8d6-41ea-8ccf-a58090745d9f';
var serverInstancePort = '38701';
var serverIP = '127.0.0.1';

function setServerInstanceUid(val) {
    serverInstanceUid = val;
    console.log('serverInstanceUid = ' + val);
}

reinitializeServerUid = function() {
    console.log("reinitializeServerUid()");
    var result = callBackend("ReinitializeServerUid", null, true);
    console.log('result : ' + result);
}
/* end of server initialization */
/* end of WebMarco Bridge */

/* WebMarco API */
/* template loading */
loadElementFromTemplate = function (itemToLoad, elementId, template) {
    var el = template.import.querySelector('.' + itemToLoad);
    el.id = elementId;
    document.body.appendChild(el.cloneNode(true));
    return $('#' + elementId);
};
/* end of template loading */

setElementWithBackendMethod = function (id, backendMethodName, updateOrAppend) {
    var resultFromBackend = callBackend(backendMethodName);
    var el = $('#' + id);
    if (updateOrAppend) {
        el.append(resultFromBackend);
    } else {
        el.html(resultFromBackend);
    }
}

appendToElementWithBackendMethod = function (id, backendMethodName) {
    setElementWithBackendMethod(id, backendMethodName, true);
}

/* view loading */
loadedViews = {}; //contains true values for views that are already loaded

function loadView(viewName, callbackFunction) {
    callBackend("LoadView", [viewName, callbackFunction]);
}

function loadPage(pageName, callbackFunction) {
    callBackend("LoadPage", [pageName, callbackFunction]);
}
/* end of view loading */
/* end of WebMarco API */
/* end of various common service methods and vars */



/* common methods */
function quit() {
    var result = callBackend('Quit');
}

//--common init----------------------
////load MainView
//loadView("MainView");


//openUrlInSystemBrowser = function (url) {
//    callBackend("OpenUrlInSystemBrowser", url, false);
//    return false;
//}

//function showActivityIndicator() {
//    $("#overlay").fadeIn("100");
//}

//function disableActivityIndicator() {
//    $("#overlay").fadeOut("100");
//}

$(document).ready(function () {
    try {
        reinitializeServerUid();
        console.log("reinitializeServerUid passed ok");
    } catch (e) {
        console.log("exception : " + e.description + "number:" + e.number);
    }
});