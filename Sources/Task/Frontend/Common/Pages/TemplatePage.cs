using System;
using System.Collections.Generic;
using System.Linq;
using WebMarco.Backend.Bridge.Common;
using WebMarco.Frontend.Common;
using WebMarco.Utilities.Library;

namespace BridgeTry.Frontend.Common.Pages {
    public sealed class TemplatePage : BaseWebPage {

        /// <summary>
        /// Some example method to call from frontend
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public CallResult GetTestDataFromBackend(object parameters) {
            var someValue = (string)parameters;//do your conversion as necessary
            string response = String.Format("Some text {0}", someValue);
            return new CallResult(response); //return data as CallResult
        }

    }
}
