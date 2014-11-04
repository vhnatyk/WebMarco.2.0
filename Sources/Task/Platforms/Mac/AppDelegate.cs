using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using WebMarco.Frontend.PlatformImplemented.Mac.Monobjc;

namespace BridgeTry.Mac.Monobjc
{
	public partial class AppDelegate : WebMarco.Backend.App.PlatformImplemented.Mac.Monobjc.BaseAppDelegate
	{
		MainWindowController mainWindowController;

		public AppDelegate ()
		{
		}

		public new static AppDelegate Instance { get { return (AppDelegate)(NSApplication.SharedApplication.Delegate); } }

		public override void FinishedLaunching (NSObject notification)
		{
			mainWindowController = new MainWindowController ();
			mainWindowController.Window.MakeKeyAndOrderFront (this);
		}
	}
}

