/* File Created: March 18, 2013 
 as an example of what and how can be done.
*/

//methods that can be defined before DOM is loaded, mostly should be placed in some main.js common file
onButtonClick = function () {
    $('#divWelcome').hide(); //.show() doesn't always work
    $('#divUnlock').show();//.hide() doesn't always work
    $('.closeButtonContainer').show();
};

onImageClick = function () {
    var configObject = {
        name: "OnImageClick",
        arguments: [],
        async: false
    };
    var res = callBackendWithConfig(configObject);
    return false;
};

onLinkClick = function (url) {
    window.open(url, '_self');
};

showElement = function (el) {//overloaded
    //var el = $('#id')[0];
    el.style['display'] = 'block';//not quite good since display css property can have various values and gets overwritten
}

hideElement = function (id) {
    var el = $('#'+id)[0];
    el.style['display'] = 'none';
}

//vars that can be defined before DOM is loaded
var elLoadsFirstTime = true;


//when this event fires - the DOM is already loaded
$(document).ready(function () {
    var el = $('div');//abstract element
    el.load(function () {
        if (el.is(":visible")) {
            if (!elLoadsFirstTime) {
                //do something
            }
        }
        elLoadsFirstTime = false;
    });

    //methods that can or need to be defined when DOM is loaded
    getWelcomeMessage = function () {        
        var configObject = {
            name: "GetWelcomeMessage",
            arguments: [],
            async: false
        };
        var res = callBackendWithConfig(configObject);
        var greetingContent = res.response[0]; //response received
        var welcomeNotice = $('.welcomeNotice')[0];
        welcomeNotice.innerHTML = greetingContent;
        var otherParameter = res.response[1];
        if (otherParameter == "true") {
            //do something
        }
    }

    //this needs to be launched only when DOM is loaded
    getWelcomeMessage();
});

