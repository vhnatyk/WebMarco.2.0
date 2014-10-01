using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WebMarco.Utilities.Logging;
using WebMarco.Backend.App.Common;
using WebMarco.Backend.Bridge.Common;

namespace WebMarco.Backend.App.PlatformImplemented.Android {
    public abstract class BaseAppDelegate : WebMarco.Backend.App.Common.BaseAppDelegate {

        public override void ExecuteOnMainThread(Action action) {
            Application.SynchronizationContext.Post(p => { action.Invoke(); }, null);
        }

        public override void ExecuteOnMainThread<T>(Action<T> action, T optionalParameter) {
            Application.SynchronizationContext.Post(p => { action.Invoke((T)p); }, optionalParameter);
        }

        public override void Quit() {
           ///We can terminate app but it's not desired behaviour
           ///So does nothing on Android

           
        }
    }
}