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
using Android.OS;

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
        public string EvaluateScript(NativeBaseWebView webView, string expression) {
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

    public abstract class NativeBaseWebView : WebView, INativeWebView {
        SynchronousFrontendScriptInterface scriptInterface;
        #region Private methods

        private static Uri GetViewUrl(IBaseWebView view) {
            return new Uri(AppHelper.Paths.GetPageAssetPath(view.NameString + ".htm"), UriKind.Absolute);
        }

        #endregion

        protected IBaseWindow parentWindow = null;
        protected BaseWebViewImplementer implementer;

        public NativeBaseWebView(IBaseWindow window)
            : base((Context)window) {
            scriptInterface = new SynchronousFrontendScriptInterface();
            AddJavascriptInterface((Java.Lang.Object)scriptInterface, scriptInterface.Name);
#if DEBUG
            try {
                bool isEmulator = Build.Hardware.Contains("goldfish") || Build.Hardware.Contains("vbox86");
                if(!isEmulator) {
                    bool isSupportedSdk = Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat;
                    if(isSupportedSdk) {
                        //WebView.SetWebContentsDebuggingEnabled(true);//TODO:throws anyway on emulator
                    }
                }
            } catch(System.Exception ex) {
                DLogger.WriteLog(ex);
                throw new NotSupportedException("Remote debugging can't be enabled. Please check SDK minimum supported version.", ex);
            }
#endif
        }
        #region Hide base properties











        #endregion

        /// <summary>
        /// Some basic common initialization can be done here
        /// </summary>
        protected virtual void InitializeComponent() {

            ///Your extra stuff here?

        }

        #region INativeWebView

        #region Public properties

        public virtual Uri ViewUrl {
            get {
                return new Uri(this.Url);
            }
        }

        #endregion

        public void LoadMarkup(string markup) {
            base.LoadData(markup, MediaTypeNames.Text.Html, UTF8Encoding.UTF8.WebName);
        }

        public void LoadMarkup(Uri url) {
            base.LoadUrl(url.ToString());
        }

        public object CallFrontend(string script) {
            object result = null;
            BaseAppDelegate.Instance.ExecuteOnMainThread(() => result = scriptInterface.EvaluateScript(this, script));
            return (result != null) ? result.ToString() : result;
        }

        #endregion

        #region INativeView
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

        public void Load() {
            throw new NotImplementedException();
        }

        public BaseRectangle CurrentFrame {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
