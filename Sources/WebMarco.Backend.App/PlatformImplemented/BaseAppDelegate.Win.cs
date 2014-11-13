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
            ((Form)MainWindow).Invoke(action);//TODO: emm... Separate file for these oneliners ? :| 
        }

        public override void ExecuteOnMainThread<T>(Action<T> action, T optionalParameter) {
            ((Form)MainWindow).Invoke(action, new object[] { optionalParameter });
        }

        public override void Quit() {
            Application.Exit();
        }
    }
}