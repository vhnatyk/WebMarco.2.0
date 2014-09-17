using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using WebMarco.Backend.Bridge.Common;
using TinyIoC;
using WebMarco.Backend.App.Common;


namespace WebMarco.Frontend.PlatformImplemented.Win {
    public abstract class MainWindow : BaseWindow {

        protected override void OnLoad(EventArgs e) {
            ///Initialization, can be after base.OnCreate(bundle); though            
            BaseAppDelegate.Instance.StartServer(ProcessCallFromFrontend); //TODO:  has to be some sort of check to ensure or even enforce that server was started

            base.OnLoad(e);
            /// Program entry point is here


            ///Warning!!! - even OnLoad can occur multiple times hypothetically in some scenarios 
        }

    }
}