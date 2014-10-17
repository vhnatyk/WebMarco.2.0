using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using WebMarco.Utilities.Logging;

#if WIN
using CefSharp;
#endif

namespace WebMarco.Frontend.Test {
    [TestFixture]
    public class NUnitTestFixture {
        [Test]
        public void Test() {
#if DEBUG
#if WIN            
            CefSharp.CefErrorCode err = CefSharp.CefErrorCode.Aborted;
            DLogger.WriteLog(err.ToString());
#endif
#endif
        }
    }
}

