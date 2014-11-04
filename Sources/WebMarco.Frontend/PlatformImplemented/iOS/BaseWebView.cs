
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mime;
using WebMarco.Frontend.Common;
using WebMarco.Frontend;

using WebMarco.Utilities.Logging;
using WebMarco.Backend.Bridge.Common;
using WebMarco.Backend.App.Common;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using WebMarco.Utilities.Paths;

namespace WebMarco.Frontend.PlatformImplemented.iOS {


    public abstract class BaseWebView : UIWebView, IBaseWebView {

        #region Private methods

        #endregion

        private IBaseWindow parentWindow = null;

        private BaseWebViewImplementer implementer;


        public BaseWebView(IBaseWindow window, BaseWebPage defaultPage, bool bouncingEnabled = false)
            : base()//window.Frame) 
        {
            Page = defaultPage;
            parentWindow = window;
            implementer = new BaseWebViewImplementer(this);
            ScrollView.Bounces = bouncingEnabled;
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
                return (Page == null)? new Uri(this.Request.Url.ToString()) : Page.Url;
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
            BaseAppDelegate.Instance.ExecuteOnMainThread(() => {
                base.LoadHtmlString(markup, new NSUrl(string.Empty));//TODO: why url is empty
            });
        }

        public void LoadMarkup(Uri url) {
            NSUrl nsUrl = NSUrl.FromString(url.ToString());
            if (nsUrl == null) {
               nsUrl = NSUrl.FromFilename(url.LocalPath);    
            }
            BaseAppDelegate.Instance.ExecuteOnMainThread(() => {
                base.LoadRequest(new NSUrlRequest(nsUrl));
            });
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
            BaseAppDelegate.Instance.ExecuteOnMainThread(() => result = this.EvaluateJavascript(script));
            return (result != null) ? result.ToString() : result;
        }

        public CallResult ProcessCallFromFrontend(CallConfig config) {
            return implementer.ProcessCallFromFrontend(config);
        }

        #endregion

        #region IBaseView

        public Point TopLeft {
            get {
                return new Point(Frame.X, Frame.Y);
            }
            set {
                Frame = new System.Drawing.RectangleF((float)(value.X), (float)(value.Y), Frame.Width, Frame.Height);
            }
        }

        public new Point Center {
            get {
                return implementer.Center;
            }
            set {
                implementer.Center = value;
            }
        }

        public double Width {
            get {
                return implementer.Width;
            }
            set {
                implementer.Width = value;
            }
        }

        public double Height {
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

        public BaseRectangle CurrentFrame {
            get {
                return new Rectangle(this);
            }
            set {
                Frame = Rectangle.GetRectangleF(value);
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
            implementer.Load(); /*Sort of base.Load if BaseView would be base class for this one, 
            but it's not, but still can be executed in implementer if necessary */
            LoadMarkup();
        }

        #endregion
    }
}
