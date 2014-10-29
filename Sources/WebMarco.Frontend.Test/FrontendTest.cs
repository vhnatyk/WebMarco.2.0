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
    public class FrontendTest { //TODO: now for Win only add to other platforms
        [Test]
        public void Test() {
#if DEBUG
#if WIN          
            //ensures CEF is referenced as expected
            CefErrorCode err = CefErrorCode.Aborted;
            DLogger.WriteLog(err.ToString());
#endif
#endif
        }
    }
}

