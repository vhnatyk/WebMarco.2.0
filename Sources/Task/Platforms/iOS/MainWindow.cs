using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BridgeTry.Views;
using TinyIoC;
using WebMarco.Backend.App.Common;
using WebMarco.Backend.App.PlatformImplemented.iOS;
using WebMarco.Frontend.PlatformImplemented.iOS;
using MonoTouch.UIKit;

namespace BridgeTry {
    class MainWindow : WebMarco.Frontend.PlatformImplemented.iOS.MainWindow {

        public MainWindow(Rectangle frame) : base(frame) { }

        public override void MakeKeyAndVisible() {
            ///Is not necessary
            //TinyIoCContainer.Current.Register<WebMarco.Backend.App.Common.BaseAppDelegate, AppDelegate>((AppDelegate)(AppDelegate.Instance));
            //TinyIoCContainer.Current.Register<AppHelper.Data.Manager, Manager>(new Manager());
            //AppHelper.Data.ConnectDatabase();//TODO: noo has to be done async - otherwise will kill the app on huge db load due to timeout
            MainView = new MainView(this);
            MainView.CurrentFrame = Rectangle.GetWithRectangleF(UIScreen.MainScreen.Bounds);
            //SetContentView((View)MainView);
            UIViewController rootViewController = new UIViewController();
            rootViewController.View = (UIView)MainView;
            //MainView.Load(); //TODO: or here
            RootViewController = rootViewController;
            base.MakeKeyAndVisible();

            MainView.Load(); //TODO: or here

            /// Program entry point is here




        }
    }
}