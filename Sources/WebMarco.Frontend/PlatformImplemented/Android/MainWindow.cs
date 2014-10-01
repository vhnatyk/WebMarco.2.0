using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Webkit;
using WebMarco.Backend.Bridge.Common;
using TinyIoC;
using WebMarco.Backend.App.PlatformImplemented.Android;



namespace WebMarco.Frontend.PlatformImplemented.Android {
    //[Activity(Label = "BridgeTry.Android", MainLauncher = true, Icon = "@drawable/icon")] //
    public abstract class MainWindow : BaseWindow {
        protected override void OnCreate(Bundle bundle) {
            ///Initialization, can be after base.OnCreate(bundle); though
            BaseAppDelegate.Instance.StartServer(ProcessCallFromFrontend);

            base.OnCreate(bundle);

            /// Program entry point is here
            

            ///This has to be in the implementation, 
            ///because there may be request to load other types of views first.
            //MainView = new MainView(this);
            //SetContentView((View)MainView);
            //MainView.Load();
        }
    }
}