using System;
using NUnit.Framework;
using WebMarco.Backend.App.Common;
//using WebMarco.Utilities.Test;
//using WebMarco.Utilities


namespace BridgeTry.Backend.Core.Test {
    [TestFixture]
    public partial class CoreTest {
#if MACOSX
		/// <summary>
		/// Stub in order to execute MonoMac.AppKit.NSApplication.Init ();//!!!Must be very first thing to run!!!
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main (string[] args)
		{

		}
#endif
        [SetUp]
        public void Setup() {
#if MACOSX
			//essential for stuff like NSHomeDirectory etc
			MonoMac.AppKit.NSApplication.Init ();//!!!Must be very first thing to run!!!
#endif
            AppHelper.Data.ConnectDatabase();
        }

        [TearDown]
        public void Tear() { }

    }
}