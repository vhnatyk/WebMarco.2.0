using System;
using NUnit.Framework;
using WebMarco.Backend.App.Common;
using TinyIoC;
#if MACOSX
using WebMarco.Backend.App.PlatformImplemented.Mac;
#elif WIN
using WebMarco.Backend.App.PlatformImplemented.Win;
#elif iOS
using WebMarco.Backend.App.PlatformImplemented.iOS;
#elif ANDROID
using WebMarco.Backend.App.PlatformImplemented.Android;
#else

#endif

namespace BridgeTry.Backend.Core.Test {
    [TestFixture]
    public partial class CoreTest {
#if MACOSX
		/// <summary>
		/// Stub in order to execute MonoMac.AppKit.NSApplication.Init ();
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main (string[] args)
		{

		}

		private static bool isNSApplicationInitRunOnce = false;
#endif
        [SetUp]
        public void Setup() {
#if MACOSX
			if(!isNSApplicationInitRunOnce)	{
				//essential for stuff like NSHomeDirectory etc
				MonoMac.AppKit.NSApplication.Init ();//!!!Must be very first thing to run!!!
				isNSApplicationInitRunOnce = true;
			}
#endif
			TinyIoCContainer.Current.Register<AppHelper.Data.Manager, Manager>(new Manager());
			AppHelper.Data.Manager.Instance.RestoreWorkingCopyOfMainDatabase ();//reset main database 
            AppHelper.Data.ConnectDatabase();
        }

        [TearDown]
        public void Tear() { }

    }
}