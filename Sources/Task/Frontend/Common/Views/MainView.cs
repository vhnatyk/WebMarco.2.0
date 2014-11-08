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
        /// 
    }
}