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
using Xamarin.Android.NUnitLite;
using System.Reflection;
using TinyIoC;
using WebMarco.Backend.App.Common;
using WebMarco.Backend.App.PlatformImplemented.Android;
using WebMarco.Utilities.Test;

namespace BridgeTry.Backend.Core.Test.PlatformImplemented.Android {
    [Activity(Label = "BridgeTry.Backend.Core.Test", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : TestSuiteActivity {
        protected override void OnCreate(Bundle bundle) {
            // tests can be inside the main assembly
            AddTest(Assembly.GetExecutingAssembly());
            AddTest(Assembly.GetAssembly(typeof(EncryptorDecryptorTest))); 
            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);

            #region Register classes to TinyIoC container here
            TinyIoCContainer.Current.Register<AppHelper.Data.Manager, Manager>(new Manager((Context)this));
            #endregion
        }
    }
}