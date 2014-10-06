using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using WebMarco.Backend.Bridge.Common;
using WebMarco.Utilities.Library;

using System.Globalization;
///TODO: this #if block can be avoided if .PlatformImplemented 
///namespace would be stripped of platform subnamespace, for example .Android
///but it will require going slightly against either
///a) namespace naming policy - if using subfolder per platform, or
///b) class naming policy - if adding platform specifier to a file name but not to a class name.
///Both are not deadly sins as neither are #if <PLATFORM> blocks I guess:)
#if ANDROID
using WebMarco.Frontend.PlatformImplemented.Android;
#elif WIN
using WebMarco.Frontend.PlatformImplemented.Win;
#elif iOS
using WebMarco.Frontend.PlatformImplemented.iOS;
#elif MACOSX
using WebMarco.Frontend.PlatformImplemented.Mac;
#else 
  ///
#endif
using WebMarco.Frontend.Common;
using BridgeTry.Backend.Core.Model.Entities;
using BridgeTry.Frontend.Common.Pages;


namespace BridgeTry.Frontend.Common.Views {
    public abstract class MainView : MainWebView {
        public MainView(IBaseWindow window)
            : base(window, new MainPage()) {
                Page.ParentWebView = this;
        }

        /// OKay - this file contains crossplatform code, but...
        /// The question is - "Is it Frontend or is it Backend.Core.Logic ??? :)
 
        ///Ya can put all ya methods here like in good all days of WinForms dawn... :)

        public CallResult GetUiMinimalResponseTime(object parameters) { //Frontend but, kinda...
            var par = (string)parameters;

            const string timeFormat = "mm\\:ss\\:fff";
            var startTime = TimeSpan.ParseExact(par, timeFormat, CultureInfo.InvariantCulture);
            var stopDate = DateTime.Now;
            TimeSpan stopTime = new TimeSpan(0, 0, stopDate.Minute, stopDate.Second, stopDate.Millisecond);
            string response = stopTime.Subtract(startTime).TotalMilliseconds.ToString();
            response = string.Format(String.Format("success click : {{0:{0}}} : {{1:{0}}} </br> request to backend took {{2}}ms", timeFormat), startTime, stopTime, response);

            return new CallResult(response);
        }

        public CallResult GetTestStringFromDatabase() { //Backend, but... :)
            StopWatchTimer timer = new StopWatchTimer();
            timer.Start("Test database");

            ///Create test values in database

            timer.StartNewLap("Save to database");
            ///This is backend
            //TODO: move this to backend?
            TestData testDataValue = TestData.New();
            const string TextToTestDatabaseIoWith = "Hello from database!;)";
            testDataValue.TestText = TextToTestDatabaseIoWith;

            testDataValue.Save();
            timer.StartNewLap("Read data from database");
            var entryRead = TestData.ReadFirst("ID = @ID", "@ID", testDataValue.ID);
            ///end of Backend code ???
            timer.Stop();

            var response = string.Format("</br>Text from database: \"{0}\", </br> timer log: </br> {1}", entryRead.TestText, timer.ToString().Replace("\n", "</br>"));

            testDataValue.Delete();

            return new CallResult(response);
        }
    }
}