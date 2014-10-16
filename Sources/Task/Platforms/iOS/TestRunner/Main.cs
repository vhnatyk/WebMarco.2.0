using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TinyIoC;
using WebMarco.Backend.App.PlatformImplemented.iOS;
using WebMarco.Backend.App.Common;
using WebMarco.Utilities.Logging;

namespace BridgeTry.Backend.Core.Test.iOS {
    public class Application {
        // This is the main entry point of the application.
        static void Main(string[] args) {
            //do it asap
            DLogger.Initialize(AppHelper.Paths.WorkingRootFolder);

            CoreTest.Initialize();

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}