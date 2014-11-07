using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarco.Backend.App.Common;
using WebMarco.Backend.Bridge.Common;

///TODO: this #if block can be avoided if .PlatformImplemented 
///namespace would be stripped of platform subnamespace, for example .Android
///but it will require going slightly against either
///a) namespace naming policy - if using subfolder per platform, or
///b) class naming policy - if adding platform specifier to a file name but not to a class name.
///Both are not deadly sins as neither are #if <PLATFORM> blocks I guess:)
namespace WebMarco.Frontend.Common {
    public abstract class BaseWebView :
#if ANDROID
        WebMarco.Frontend.PlatformImplemented.Android.NativeBaseWebView
#elif WIN
        WebMarco.Frontend.PlatformImplemented.Win.NativeBaseWebView
#elif iOS
        WebMarco.Frontend.PlatformImplemented.iOS.NativeBaseWebView 
#elif MACOSX
        WebMarco.Frontend.PlatformImplemented.Mac.NativeBaseWebView 
#else 
  ///
#endif
, IBaseWebView {

        #region IBaseWebView
        public BaseWebView(IBaseWindow window, BaseWebPage defaultPage) :
#if ANDROID
            base(window)    
#elif WIN
            base(defaultPage.Url.ToString())
#elif iOS
    WebMarco.Frontend.PlatformImplemented.iOS.NativeBaseWebView 
#elif MACOSX
    WebMarco.Frontend.PlatformImplemented.Mac.NativeBaseWebView 
#else 
  ///
#endif
            

       
        {

            Page = defaultPage;
            parentWindow = window;
            implementer = new BaseWebViewImplementer(this);
        }

        #region Public Properties

        public string NameString {
            get { return this.GetType().Name; }
        }

        public string Markup {
            get {
                throw new NotImplementedException();
            }
        }



        public BaseWebPage Page { get; private set; }

        #endregion

        public void LoadMarkup() {
            implementer.LoadMarkup();
        }

        public void LoadMarkup(BaseWebPage page) {
            Page = page;
            LoadMarkup(page.Url);
        }

        public CallResult ProcessCallFromFrontend(CallConfig config) {
            return implementer.ProcessCallFromFrontend(config);
        }

        public virtual void Load() {
            implementer.Load(); /*Sort of base.Load if BaseView would be base class for this one, 
            but it's not, but still can be executed in implementer if necessary */
            //a place to add firebug-lite ??? ?
            LoadMarkup();
        }

        #endregion
    }
}
