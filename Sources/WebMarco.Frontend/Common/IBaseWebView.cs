using System;
using System.Collections.Generic;
using System.Text;
using WebMarco.Backend.Bridge.Common;

namespace WebMarco.Frontend.Common {
    public interface IBaseWebView : INativeWebView, IBaseView {
        string NameString { get; }
        Stack<BaseWebPage> LoadedPages { get; }
        BaseWebPage Page { get; }
        string Markup { get; }

        void LoadMarkup();
        void LoadMarkup(BaseWebPage page);

        void LoadPage(string pageTypeName, string callBackMethodName = null);
        void Back();

        CallResult ProcessCallFromFrontend(CallConfig config);
    }
}