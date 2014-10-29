using System;
using NUnit.Framework;
using WebMarco.Utilities.Logging;
using WebMarco.Utilities.Misc;
using BridgeTry.Backend.Core.Model.Entities;
using Vici.CoolStorage;
using System.Collections.Generic;


namespace BridgeTry.Backend.Core.Test {
    [TestFixture]
    public partial class Model
    {
        #region Model
        [Test]
        public void EnsureDatabaseReadWriteWorksCorrectly() {

            DLogger.WriteLog("EnsureDatabaseReadWriteWorksCorrectly");
            TestData entrySaved = TestData.New();
            const string TextToTestDatabaseIoWith = "Text to test";
            entrySaved.TestText = TextToTestDatabaseIoWith;

            const double DoubleToTestDatabaseIoWith = 777d;
            entrySaved.TestDouble = DoubleToTestDatabaseIoWith;

            byte[] ImageToTestDatabaseIoWith = new byte[] { 250, 250, 250, 100, 156, 156, 90, 60, 100 };
            entrySaved.TestBlob = ImageToTestDatabaseIoWith;

            entrySaved.Save();

            var entryRead = TestData.ReadFirst("ID = @ID", "@ID", entrySaved.ID);

            Assert.True(entrySaved.ID == entryRead.ID, string.Format("ID mismatch, was {0} and now is: {1}", entrySaved.ID, entryRead.ID));
            Assert.True(entrySaved.TestText == entryRead.TestText, string.Format("TestText mismatch, was {0} and now is {1}", entrySaved.TestText, entryRead.TestText));
            Assert.True(ComparisonHelper.Equality(entrySaved.TestBlob,entryRead.TestBlob), string.Format("TestBlob mismatch, was {0} and now is {1}", entrySaved.TestBlob, entryRead.TestBlob));
            entrySaved.Delete();
        }



        //[Test]
        //public void Pass() {
        //    DLogger.WriteLog("test1");
        //    Assert.True(true);
        //}

        //[Test]
        //public void Fail() {
        //    Assert.False(true);
        //}

        //[Test]
        //[Ignore("another time")]
        //public void Ignore() {
        //    Assert.True(false);
        //}

        //[Test]
        //public void Inconclusive() {
        //    Assert.Inconclusive("Inconclusive");
        //}
        #endregion
    }
}