using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using WebMarco.Backend.Bridge.Common;
using WebMarco.Backend.App.Common;
using WebMarco.Utilities.Logging;

namespace WebMarco.Frontend.Common {
    public class BaseWebViewImplementer : BaseViewImplementer, IBaseWebView {

        /// <summary>
        /// Things that make no sense to be implemented here and 
        /// are here just because of compliance with IBaseWebView, must be declared abstract,
        /// so if they later will be "implemented" in platform implementation
        /// they can be autocreated with "Implement abstract class" feature
        /// and just will be throwing NotImplementedException to ensure they aren't used.
        /// </summary>

        protected IBaseWebView webView;
        protected ViewsHolder loadedViews;

        public BaseWebViewImplementer(IBaseWebView webView) {
            this.webView = webView;
            View = webView;
            loadedViews = new ViewsHolder();
        }

        #region IBaseWebView


        public Uri ViewUrl { get { return (Page == null) ? webView.ViewUrl : Page.Url; } }

        /// <summary>
        /// Returns BaseWebPage descendant, associated with current WebView. 
        /// IMPORTANT!!! : This property has priority over ViewUrl. 
        /// It's here to make crossplatform, where required in this class.
        /// </summary>
        public BaseWebPage Page { get { return webView.Page; } }

        /// <summary>
        /// This property must be implemented in BaseWebView descendant, 
        /// therefore makes no sense here.
        /// </summary>
        public string NameString {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// This property must be implemented in BaseWebView descendant, 
        /// therefore makes no sense here.
        /// </summary>
        public string Markup {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// IMPORTANT!!! : WebView must implement this method itself, otherwise will be an endless loop.
        /// </summary>
        /// <param name="url">URL</param>
        public void LoadMarkup(Uri url) {
            webView.LoadMarkup(url);
        }

        public void LoadMarkup(string markup) {
            throw new NotImplementedException();
        }

        public void LoadMarkup() {
            LoadMarkup(ViewUrl);
        }

        /// <summary>
        /// IMPORTANT!!! : WebView must implement this method itself, otherwise will be an endless loop.
        /// </summary>
        /// <param name="page"></param>
        public void LoadMarkup(BaseWebPage page) {
            webView.LoadMarkup(page);
        }

        /// <summary>
        /// This method is not crossplatform, therefore makes no sense here.
        /// </summary>
        public object CallFrontend(string script) {
            throw new NotImplementedException();
        }

        public virtual CallResult ProcessCallFromFrontend(CallConfig config) {             //dict["name"]
            DLogger.WriteLog("Call from frontend of method name:{0} with arguments:{1}", config.MethodName, config.Params);

            object instance = webView.Page;
            MethodInfo theMethod = null;

            if (Page != null) {//first lets look for the method in the page
                theMethod = Page.GetType().GetMethod(config.MethodName);
            }

            if (theMethod == null) {
                theMethod = webView.GetType().GetMethod(config.MethodName);
                instance = webView;
            }

            if (theMethod == null) {//if the method is not found in current view, then lets search for it in other loaded views
                foreach (var view in LoadedViews.ViewForEachType) {
                    theMethod = view.GetType().GetMethod(config.MethodName);
                    if (theMethod != null) {
                        instance = view;
                        break;
                    }
                }
            }

            if (theMethod == null) {//if the method is not found in loaded views, then lets search for it in parent window
                theMethod = ParentWindow.GetType().GetMethod(config.MethodName);
                instance = ParentWindow;
            }

            if (theMethod == null) {//if the method is not found in parent window, then lets search for it in AppDelegate.Instance
                theMethod = BaseAppDelegate.Instance.GetType().GetMethod(config.MethodName);
                instance = BaseAppDelegate.Instance;
            }

            CallResult result = new CallResult(string.Empty);
            try {
				//BaseAppDelegate.Instance.ExecuteOnMainThread(()=> {
                	result = (CallResult)theMethod.Invoke(instance, new object[] { config.Params });
				//});
            } catch (Exception ex) {
                DLogger.WriteLog(ex);
                try {//try parameter less
                    //BaseAppDelegate.Instance.ExecuteOnMainThread(() => {
						result = (CallResult)theMethod.Invoke(instance, null);
					//});
                } catch (Exception ex2) {
                    DLogger.WriteLog(ex2);
                }
            }

            return result;
        }

        #endregion

        #region BaseViewImplementer

        public override void Load() {
            LoadedViews.TryAddView(webView);
        }

        #endregion

        protected override IBaseView View {
            get {
                return (IBaseView)webView;
            }
            set {
                webView = (IBaseWebView)value;
            }
        }
    }
}