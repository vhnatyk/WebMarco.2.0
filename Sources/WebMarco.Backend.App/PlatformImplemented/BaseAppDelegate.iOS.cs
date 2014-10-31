using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using WebMarco.Utilities.Logging;
using WebMarco.Backend.App.Common;
using System.Threading;
using MonoTouch.UIKit;


namespace WebMarco.Backend.App.PlatformImplemented.iOS {
    public abstract class BaseAppDelegate : WebMarco.Backend.App.Common.BaseAppDelegate {
        public override void ExecuteOnMainThread(Action action) {
           UIApplication.SharedApplication.InvokeOnMainThread(() => { action.Invoke(); });//TODO: emm... Separate file for these oneliners ? :| 
        }

        public override void ExecuteOnMainThread<T>(Action<T> action, T optionalParameter) {
            UIApplication.SharedApplication.InvokeOnMainThread(() => { action.Invoke(optionalParameter); });
        }

        public override void Quit() {
           ///We can terminate app but it's not desired behavior
           ///So does nothing on iOS
            //UIApplication.SharedApplication.PerformSelector(new Selector("terminateWithSuccess"), null, 0f);
        }
    }
}