
using System;
using System.Collections.Generic;
using System.Linq;
using MonoMac.Foundation;
using MonoMac.AppKit;
using WebMarco.Frontend.PlatformImplemented.Mac.Monobjc;
using WebMarco.Frontend.Common;

namespace BridgeTry.Mac.Monobjc
{
	public partial class MainWindowController : MonoMac.AppKit.NSWindowController
	{
		#region Constructors

		// Called when created from unmanaged code
		public MainWindowController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}
		
		// Called when created directly from a XIB file
		[Export ("initWithCoder:")]
		public MainWindowController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}
		
//		// Call to load from the XIB/NIB file
//		public MainWindowController () : base ("MainWindow")
//		{
//			Initialize ();
//		}

		public MainWindowController () : base () {
			Initialize ();
		}
		
		// Shared initialization code
		void Initialize () {
			//does center itself in MakeKeyAndFront to replicate iOS behaviour
			//Point centerOfScreen = new Rectangle(NSScreen.MainScreen.Frame).Center;
			var rectangle = new Rectangle (new Point (0, 0), 800, 700);//TODO: must come from config, like last position etc
			Window = new BridgeTry.Mac.Monobjc.MainWindow (rectangle);
			//Window.CurrentFrame.Center = centerOfScreen;//TODO:Make it work and cross platform
			//Window.SetFrameOrigin (new System.Drawing.PointF ((float)(centerOfScreen.X), (float)(centerOfScreen.Y)));
			Window.StyleMask = NSWindowStyle.Miniaturizable | NSWindowStyle.Resizable | NSWindowStyle.Titled;
			Window.Center ();
			base.Window = (NSWindow)Window;
		}

		#endregion

		//strongly typed window accessor
		public new BridgeTry.Mac.Monobjc.MainWindow Window { get; private set; }
	}
}

