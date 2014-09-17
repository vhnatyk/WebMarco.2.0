using Android.Webkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WebMarco.Backend.Bridge.Common;
using WebMarco.Frontend;
using WebMarco.Frontend.Common;
using WebMarco.Utilities.Logging;

namespace WebMarco.Frontend.PlatformImplemented.Android {
    /// <summary>
    /// Such design scheme of WebMarco.Frontend.<Platform>.PlatformImplemented.<BaseObject>Implementer 
    /// and WebMarco.Frontend.Common.<BaseObject>Implementer classes with same name replaces 
    /// the #if PLATFORM1 ... #elif PLATFORM2 .. #elif ... #endif preprocessor directives
    /// that, according to experiense achieved in previous projects - these tend to be unmanagable.
    /// With this approach common frontend code goes to WebMarco.Frontend.Common.<BaseObject>Implementer
    /// and the code that is speciffic for a platform, for exaple Android, - if it was in #if ANDROID ... #endif
    /// then it goes to WebMarco.Frontend.PlatformImplemented.Android
    /// </summary>
    public class BaseWebViewImplementer : WebMarco.Frontend.Common.BaseWebViewImplementer {
        public BaseWebViewImplementer(IBaseWebView webView)
            : base(webView) {

        }
    }
}
