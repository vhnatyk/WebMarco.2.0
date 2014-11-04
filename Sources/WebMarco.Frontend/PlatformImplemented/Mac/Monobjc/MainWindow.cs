using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TinyIoC;
using WebMarco.Backend.Bridge.Common;
using WebMarco.Backend.App.Common;


#if MONOBJC
using Monobjc;
using Monobjc.Foundation;
#else
using MonoMac.Foundation;
#endif


namespace WebMarco.Frontend.PlatformImplemented.Mac {
    public abstract class MainWindow : BaseWindow {
		Class currentType = Class.Get(typeof(MainWindow));

        public MainWindow(Rectangle frame) :			
			base(frame) { 
			///Initialization, can be after base.OnCreate(bundle); though            
			BaseAppDelegate.Instance.StartServer(ProcessCallFromFrontend); //TODO:  has to be some sort of check to ensure or even enforce that server was started
		}

		#if MONOBJC
		[ObjectiveCMessage("makeKeyAndOrderFront:")]
		public override void MakeKeyAndOrderFront(Id sender)  {
			this.SendMessageSuper (currentType, "makeKeyAndOrderFront:", sender);
		#else
		public override void MakeKeyAndOrderFront(NSObject sender)  {
			base.MakeKeyAndOrderFront(sender);
		#endif

            /// Program entry point is here

            //TODO: Mac - really here??? Nooo
        }
    }
}