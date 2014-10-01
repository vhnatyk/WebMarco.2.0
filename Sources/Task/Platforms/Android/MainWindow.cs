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
using WebMarco.Frontend.PlatformImplemented.Android;
using Android.Webkit;
using BridgeTry.Views;
using WebMarco.Backend.App.PlatformImplemented;
using TinyIoC;
using WebMarco.Backend.App.Common;
using WebMarco.Backend.App.PlatformImplemented.Android;



namespace BridgeTry {
    [Activity(Label = "BridgeTry.Android", MainLauncher = true, Icon = "@drawable/icon")]
    class MainWindow : WebMarco.Frontend.PlatformImplemented.Android.MainWindow {
        protected override void OnCreate(Bundle bundle) {
            TinyIoCContainer.Current.Register<WebMarco.Backend.App.Common.BaseAppDelegate, AppDelegate>(AppDelegate.Instance);
            TinyIoCContainer.Current.Register<AppHelper.Data.Manager, Manager>(new Manager((Context)this));
            AppHelper.Data.ConnectDatabase();
            base.OnCreate(bundle);

            /// Program entry point is here
            


            MainView = new MainView(this);
            SetContentView((View)MainView);
            MainView.Load();
        }
    }
}