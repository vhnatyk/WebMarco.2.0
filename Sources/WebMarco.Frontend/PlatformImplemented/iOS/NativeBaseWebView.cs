
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


    public abstract class NativeBaseWebView : UIWebView, INativeWebView {

        #region Private methods

        #endregion

        protected IBaseWindow parentWindow = null;
        protected BaseWebViewImplementer implementer;


        public NativeBaseWebView(bool bouncingEnabled = false)
            : base()//window.Frame) 
        {
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


        #region INativeWebView

        #region Public Properties

        public virtual Uri ViewUrl {
            get {
                return new Uri(this.Request.Url.ToString());
            }
        }

        #endregion

        public void LoadMarkup(string markup) {
            BaseAppDelegate.Instance.ExecuteOnMainThread(() => {
                base.LoadHtmlString(markup, new NSUrl(string.Empty));//TODO: why url is empty
            });
        }

        public void LoadMarkup(Uri url) {
            NSUrl nsUrl = NSUrl.FromString(url.ToString());
            if(nsUrl == null) {
                nsUrl = NSUrl.FromFilename(url.LocalPath);
            }
            BaseAppDelegate.Instance.ExecuteOnMainThread(() => {
                base.LoadRequest(new NSUrlRequest(nsUrl));
            });
        }

        public object CallFrontend(string script) {
            object result = null;
            BaseAppDelegate.Instance.ExecuteOnMainThread(() => result = this.EvaluateJavascript(script));
            return (result != null) ? result.ToString() : result;
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
                return new Rectangle(this.Frame);
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
            throw new NotImplementedException();
        }

        #endregion
    }
}
