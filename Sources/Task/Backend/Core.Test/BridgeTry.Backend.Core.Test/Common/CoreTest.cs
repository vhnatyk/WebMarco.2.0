using System;
using NUnit.Framework;
using WebMarco.Backend.App.Common;



namespace BridgeTry.Backend.Core.Test {
    [TestFixture]
    public partial class CoreTest {

        [SetUp]
        public void Setup() {
            AppHelper.Data.ConnectDatabase();
        }

        [TearDown]
        public void Tear() { }

    }
}