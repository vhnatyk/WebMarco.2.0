using System;
using System.Drawing;
using MonoMac.Foundation;
using MonoMac.AppKit;
using MonoMac.ObjCRuntime;
using TinyIoC;
using WebMarco.Backend.App.Common;
using WebMarco.Backend.App.PlatformImplemented.Mac;

namespace BridgeTry.Mac
{
	class MainClass
	{
		static void Main (string[] args)
		{
			NSApplication.Init ();//!!!Must be very first thing to run!!!
			#if DEBUG
			NSApplication.CheckForIllegalCrossThreadCalls = false;//causes wrong exceptions alluring that NSApplication.SharedApplication.InvokeOnMainThread doesnt work as expected

			//WebInspector
			NSUserDefaults userDefaults = NSUserDefaults.StandardUserDefaults;
			NSMutableDictionary appDefaults = new NSMutableDictionary ();
			appDefaults.SetValueForKey (NSObject.FromObject (true), new NSString ("WebKitDeveloperExtras"));
			userDefaults.RegisterDefaults (appDefaults);
			userDefaults.Synchronize ();
			#endif

			TinyIoCContainer.Current.Register<AppHelper.Data.Manager, Manager>(new Manager());
			AppHelper.Data.ConnectDatabase();//TODO: noo has to be done async - otherwise will kill the app on huge db load due to timeout


			NSApplication.Main (args);
		}
	}
}

