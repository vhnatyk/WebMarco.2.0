using System;
using System.Collections.Generic;
using System.Text;
using WebMarco.Backend.Bridge.Common;

namespace WebMarco.Frontend.Common {
    public interface IBaseWebView : INativeWebView, IBaseView {
        string NameString { get; }
        Uri ViewUrl { get; }
        BaseWebPage Page { get; }
        string Markup { get; }

        void LoadMarkup();
        void LoadMarkup(BaseWebPage page);

        object CallFrontend(string script);
        CallResult ProcessCallFromFrontend(CallConfig config);
    }
}