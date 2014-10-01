using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using WebMarco.Backend.Bridge.Common;
using TinyIoC;
using WebMarco.Backend.App.Common;


namespace WebMarco.Frontend.PlatformImplemented.iOS {
    public abstract class MainWindow : BaseWindow {

        public MainWindow(Rectangle frame) : base(frame) { }

        public override void MakeKeyAndVisible()  {
            ///Initialization, can be after base.OnCreate(bundle); though            
            BaseAppDelegate.Instance.StartServer(ProcessCallFromFrontend); //TODO:  has to be some sort of check to ensure or even enforce that server was started

            base.MakeKeyAndVisible();
            /// Program entry point is here

            //TODO: iOS - really here??? Nooo
        }

    }
}