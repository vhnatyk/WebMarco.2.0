﻿using System;
using NUnit.Framework;
using WebMarco.Backend.App.Common;
using TinyIoC;

#if MACOSX
#if MONOBJC
using WebMarco.Backend.App.PlatformImplemented.Mac.Monobjc;

#else
using WebMarco.Backend.App.PlatformImplemented.Mac.Xamarin;
#endif
#elif WIN
using WebMarco.Backend.App.PlatformImplemented.Win;

#elif iOS
using WebMarco.Backend.App.PlatformImplemented.iOS;

#elif ANDROID
using Android.Content;
using WebMarco.Backend.App.PlatformImplemented.Android;

#else

#endif

namespace BridgeTry.Backend.Core.Test
{
#if MACOSX || WIN
	[SetUpFixture] //NUnit 
#elif ANDROID || iOS
    [TestFixture]
#endif

    public partial class CoreTest
	{
		#if MACOSX
		/// <summary>
		/// Stub in order to initialize Objective-C runtime
		/// </summary>
		/// <param name="args">The command-line arguments.</param>
		static void Main (string[] args)
		{

		}

		#endif
		[SetUp]
		public void Setup ()
		{

#if ANDROID || iOS
            //Initialize(); //runs from runner
#else
			Initialize ();
#endif
		}

		[TearDown]
		public void Tear ()
		{//runs once per namespace or assembly after the tests
        
		}

		/// <summary>
		/// Crossplatform unified Core.Test initialization
		/// </summary>
		/// <param name="context"></param>
		public static void Initialize (object context = null)
		{
#if MACOSX
			#if MONOBJC
				#region --- Monobjc Generated Code ---
				//
				// DO NOT ALTER OR REMOVE
				//
				Monobjc.ObjectiveCRuntime.LoadFramework ("Foundation");
				#endregion

				Monobjc.ObjectiveCRuntime.Initialize ();
			#else
				//essential for stuff like NSHomeDirectory etc
				MonoMac.AppKit.NSApplication.Init ();//!!!Must be very first thing to run!!!
			#endif
#endif

#if ANDROID            
            Manager manager = new Manager((Context)context);
#else
			Manager manager = new Manager ();
#endif
			TinyIoCContainer.Current.Register<AppHelper.Data.Manager, Manager> (manager);
			AppHelper.Data.Manager.Instance.RestoreWorkingCopyOfMainDatabase ();//reset main database 
			AppHelper.Data.ConnectDatabase ();
		}
	}
}