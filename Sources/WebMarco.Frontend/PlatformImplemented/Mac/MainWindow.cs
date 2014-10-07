using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using WebMarco.Backend.Bridge.Common;
using TinyIoC;
using WebMarco.Backend.App.Common;
using MonoMac.Foundation;


namespace WebMarco.Frontend.PlatformImplemented.Mac {
    public abstract class MainWindow : BaseWindow {

        public MainWindow(Rectangle frame) : base(frame) { 
			///Initialization, can be after base.OnCreate(bundle); though            
			BaseAppDelegate.Instance.StartServer(ProcessCallFromFrontend); //TODO:  has to be some sort of check to ensure or even enforce that server was started
		}

		public override void MakeKeyAndOrderFront(NSObject sender)  {

			base.MakeKeyAndOrderFront (sender);
            /// Program entry point is here

            //TODO: Mac - really here??? Nooo
        }

    }
}