using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using WebMarco.Utilities.Logging;
using WebMarco.Backend.App.Common;
using System.Threading;
using System.Windows.Forms;

namespace WebMarco.Backend.App.PlatformImplemented.Win {
    public abstract class BaseAppDelegate : WebMarco.Backend.App.Common.BaseAppDelegate {
        public override void ExecuteOnMainThread(Action action) {
            WindowsFormsSynchronizationContext.Current.Post(p => { action.Invoke(); }, null);//TODO: emm... Separate file for these oneliners ? :| 
        }

        public override void ExecuteOnMainThread<T>(Action<T> action, T optionalParameter) {
            WindowsFormsSynchronizationContext.Current.Post(p => { action.Invoke((T)p); }, optionalParameter);
        }

        public override void Quit() {
            Application.Exit();
        }
    }
}