using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using WebMarco.Frontend.PlatformImplemented.iOS;
using WebMarco.Utilities.Logging;
using WebMarco.Backend.App.Common;

namespace BridgeTry {
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : WebMarco.Backend.App.PlatformImplemented.iOS.BaseAppDelegate {
        
        #region Singleton realization
        ///// <summary>
        ///// This region should be copypasted to your AppDelegate class.
        ///// </summary>

        //private static readonly Lazy<AppDelegate> lazy =
        //    new Lazy<AppDelegate>(() => new AppDelegate());

        public new static AppDelegate Instance { get { return (AppDelegate)(UIApplication.SharedApplication.Delegate); } }//lazy.Value; } }

        //private AppDelegate() {
        //    DLogger.Initialize(AppHelper.Paths.WorkingRootFolder);

        //    ///Some additional early initialization routines can be done here if needed.
        //}
        #endregion


        // class-level declarations
        UIWindow window;

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options) {
            // create a new window instance based on the screen size
            window = new MainWindow(new Rectangle(UIScreen.MainScreen.Bounds));

            // If you have defined a view, add it here:
            // window.RootViewController  = navigationController;


            // make the window visible
            window.MakeKeyAndVisible();

            return true;
        }
    }
}