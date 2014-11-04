using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using WebMarco.Utilities.Logging;
using WebMarco.Backend.App.Common;
using System.Threading;
using Monobjc.AppKit;



namespace WebMarco.Backend.App.PlatformImplemented.Mac.Monobjc {
    public abstract class BaseAppDelegate : WebMarco.Backend.App.Common.BaseAppDelegate {
        public override void ExecuteOnMainThread(Action action) {
           	NSApplication.SharedApplication.Invoke(action);//TODO: emm... Separate file for these oneliners ? :| 
        }

        public override void ExecuteOnMainThread<T>(Action<T> action, T optionalParameter) {
			NSApplication.SharedApplication.Invoke (action, optionalParameter);
        }

        public override void Quit() {
			ExecuteOnMainThread (() => {
				NSApplication.SharedApplication.Terminate (this);//TODO: exit on Mac? hmm
			});
        }
    }
}