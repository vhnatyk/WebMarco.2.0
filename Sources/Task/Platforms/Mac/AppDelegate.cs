using System;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using WebMarco.Frontend.PlatformImplemented.Mac;
using TinyIoC;
using WebMarco.Backend.App.Common;

namespace BridgeTry.Mac
{
	public partial class AppDelegate : WebMarco.Backend.App.PlatformImplemented.Mac.BaseAppDelegate
	{
		MainWindowController mainWindowController;

		public AppDelegate ()
		{
		}

		public new static AppDelegate Instance { get { return (AppDelegate)(NSApplication.SharedApplication.Delegate); } }

		public override void FinishedLaunching (NSObject notification)
		{
			TinyIoCContainer.Current.Register<BaseAppDelegate, AppDelegate>(AppDelegate.Instance);
			mainWindowController = new MainWindowController ();
			mainWindowController.Window.MakeKeyAndOrderFront (this);
		}
	}
}

