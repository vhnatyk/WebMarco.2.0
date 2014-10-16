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
using BridgeTry.Backend.Core.Test;
using WebMarco.Utilities.Test;

namespace TestRunner {
    [Activity(Label = "TestRunner", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : TestSuiteActivity {
        protected override void OnCreate(Bundle bundle) {
            // tests can be inside the main assembly
            //AddTest(Assembly.GetExecutingAssembly());

            // or in any reference assemblies
            // AddTest (typeof (Your.Library.TestClass).Assembly);

            AddTest(typeof(CoreTest).Assembly);
            AddTest(typeof(EncryptorDecryptorTest).Assembly);            

            CoreTest.Initialize(this);

            // Once you called base.OnCreate(), you cannot add more assemblies.
            base.OnCreate(bundle);
        }
    }
}

