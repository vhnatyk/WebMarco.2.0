using Android.Content;
using Android.Webkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mime;
using WebMarco.Frontend.Common;
using WebMarco.Frontend;
using Java.Util.Concurrent;
using WebMarco.Utilities.Logging;
using Java.Lang;
using Java.Interop;
using WebMarco.Backend.Bridge.Common;
using WebMarco.Backend.App.Common;

namespace WebMarco.Frontend.PlatformImplemented.Android {

    /**
     * Provides an interface for getting synchronous javascript calls
     * @author btate
     *
     */
    class SynchronousFrontendScriptInterface : Java.Lang.Object {

        /** The javascript interface name for adding to web view. */
        private string name = "WebMarcoScriptInterface";

        /** Countdown latch used to wait for result. */
        private CountDownLatch latch = null;

        /** Return value to wait for. */
        private string returnValue;

        /**
        * Base Constructor.
        */
        public SynchronousFrontendScriptInterface() {

        }

        /// <summary>
        /// Evaluates the expression and returns the value.
        /// </summary>
        /// <param name="webView"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public string EvaluateScript(BaseWebView webView, string expression) {
            latch = new CountDownLatch(1);
            string code = "javascript:window." + Name + ".setValue((function(){try{return " + expression
            + "+\"\";}catch(js_eval_err){console.log('js_eval_err : ' + js_eval_err); return '';}})());";
            webView.LoadUrl(code);

            try {
                // Set a 1 second timeout in case there's an error
                latch.Await(1, TimeUnit.Seconds);
                return returnValue;
            } catch (System.Exception ex) {
                DLogger.WriteLog(ex);
            }
            return null;
        }

        /// <summary>
        /// Receives the value from the javascript.
        /// </summary>
        /// <param name="value"></param>
        [Export("setValue")]
        public void SetValue(Java.Lang.String value) {
            returnValue = value.ToString();
            try { latch.CountDown(); } catch (System.Exception ex) {
                DLogger.WriteLog(ex);
            }
        }

        public string Name {
            get {
                return name;
            }
        }
    }

    public abstract class BaseWebView : WebView, IBaseWebView {
        SynchronousFrontendScriptInterface scriptInterface;
        #region Private methods

        private static Uri GetViewUrl(IBaseWebView view) {
            return new Uri(AppHelper.Paths.GetPageAssetPath(view.NameString + ".htm"), UriKind.Absolute);
        }

        #endregion

        private IBaseWindow parentWindow = null;

        private BaseWebViewImplementer implementer;

        public BaseWebView(IBaseWindow window, BaseWebPage defaultPage = null)
            : base((Context)window) {
            parentWindow = window;
            Page = defaultPage;
            implementer = new BaseWebViewImplementer(this);


            scriptInterface = new SynchronousFrontendScriptInterface();
            AddJavascriptInterface((Java.Lang.Object)scriptInterface, scriptInterface.Name);
        }
        #region Hide base properties











        #endregion

        /// <summary>
        /// Some basic common initialization can be done here
        /// </summary>
        protected virtual void InitializeComponent() {

            ///Your extra stuff here?

        }


        #region IBaseWebView

        #region Public Properties

        public string NameString {
            get { return this.GetType().Name; }
        }

        public virtual Uri ViewUrl {
            get {
                return (Page == null)? new Uri(this.Url) : Page.Url;
            }
        }

        public string Markup {
            get {
                throw new NotImplementedException();
            }
        }

        public BaseWebPage Page { get; private set; }

        #endregion

        public void LoadMarkup(string markup) {
            base.LoadData(markup, MediaTypeNames.Text.Html, UTF8Encoding.UTF8.WebName);
        }

        public void LoadMarkup(Uri url) {
            base.LoadUrl(url.ToString());
        }

        public void LoadMarkup() {
            implementer.LoadMarkup();
        }

        public void LoadMarkup(BaseWebPage page) {
            Page = page;
            LoadMarkup(page.Url);
        }

        public object CallFrontend(string script) {
            object result = null;
            BaseAppDelegate.Instance.ExecuteOnMainThread(() => result = scriptInterface.EvaluateScript(this, script));
            return (result != null) ? result.ToString() : result;
        }

        public CallResult ProcessCallFromFrontend(CallConfig config) {
            return implementer.ProcessCallFromFrontend(config);
        }

        #endregion
        #region IBaseView
        public Point TopLeft {
            get {
                return implementer.TopLeft;
            }
            set {
                implementer.TopLeft = value;
            }
        }

        public Point Center {
            get {
                return implementer.Center;
            }
            set {
                implementer.Center = value;
            }
        }

        public new double Width {
            get {
                return implementer.Width;
            }
            set {
                implementer.Width = value;
            }
        }

        public new double Height {
            get {
                return implementer.Height;
            }
            set {
                implementer.Height = value;
            }
        }


        public bool IsFullFrame {
            get {
                return implementer.IsFullFrame;
            }
            set {
                implementer.IsFullFrame = value;
            }
        }



        public bool IsModal { get; protected set; }

        public bool Visible {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public void Show() {
            throw new NotImplementedException();
        }

        public void Hide() {
            throw new NotImplementedException();
        }

        public ViewsHolder LoadedViews {
            get { return implementer.LoadedViews; }
        }

        public void AddSubview(IBaseView view) {
            throw new NotImplementedException();
        }

        public IBaseWindow ParentWindow {
            get { return parentWindow; }
        }

        public virtual void Load() {
            implementer.Load();
            LoadMarkup();
        }

        #endregion
    }
}
