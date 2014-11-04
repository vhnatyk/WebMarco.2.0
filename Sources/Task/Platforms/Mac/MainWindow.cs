
using System;
using System.Collections.Generic;
using System.Linq;
using BridgeTry.Mac.Monobjc.Views;
using TinyIoC;
using WebMarco.Backend.App.Common;

using WebMarco.Frontend.Common;

#if MONOBJC
using Monobjc.Foundation;
using Monobjc.AppKit;
using WebMarco.Backend.App.PlatformImplemented.Mac.Monobjc;
using WebMarco.Frontend.PlatformImplemented.Mac.Monobjc;
#else
using MonoMac.Foundation;
using MonoMac.AppKit;
using WebMarco.Backend.App.PlatformImplemented.Mac.Xamarin;
using WebMarco.Frontend.PlatformImplemented.Mac.Xamarin;
#endif

namespace BridgeTry.Mac.Monobjc {
	public partial class MainWindow : WebMarco.Frontend.PlatformImplemented.Mac.Monobjc.MainWindow
	{
		#region Constructors

		public MainWindow(Rectangle frame) : base(frame){ }

		#endregion

		public override void MakeKeyAndOrderFront(NSObject sender) {
			///Is not necessary
			//TinyIoCContainer.Current.Register<WebMarco.Backend.App.Common.BaseAppDelegate, AppDelegate>((AppDelegate)(AppDelegate.Instance));
			//TinyIoCContainer.Current.Register<AppHelper.Data.Manager, Manager>(new Manager());
			//AppHelper.Data.ConnectDatabase();//TODO: noo has to be done async - otherwise will kill the app on huge db load due to timeout
			MainView = new MainView(this);

			Point center = new Rectangle (NSScreen.MainScreen.Frame).Center;
			MainView.CurrentFrame = Rectangle.GetBaseWithRectangleF(Frame);//TODO: must come from settings, like last position and size etc

			ContentView = (NSView)MainView;
			base.MakeKeyAndOrderFront(sender);

			MainView.Load(); //TODO: or here

			/// Program entry point is here




		}
	}
}
