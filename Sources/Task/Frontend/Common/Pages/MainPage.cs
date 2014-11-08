using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using BridgeTry.Backend.Core.Model.Entities;
using WebMarco.Backend.Bridge.Common;
using WebMarco.Frontend.Common;
using WebMarco.Utilities.Library;

namespace BridgeTry.Frontend.Common.Pages {
    public sealed class MainPage : BaseWebPage {

        ///Ya can put all ya methods here like in good all days of WinForms... :)

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
