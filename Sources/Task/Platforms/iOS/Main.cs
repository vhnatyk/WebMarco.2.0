using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using TinyIoC;
using WebMarco.Backend.App.Common;
using WebMarco.Backend.App.PlatformImplemented.iOS;

namespace BridgeTry {
    public class Application {
        // This is the main entry point of the application.
        static void Main(string[] args) {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.

            //Level 0 for IoC registration
            TinyIoCContainer.Current.Register<AppHelper.Data.Manager, Manager>(new Manager());
            AppHelper.Data.ConnectDatabase();//TODO: noo has to be done async - otherwise will kill the app on huge db load due to timeout


            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}