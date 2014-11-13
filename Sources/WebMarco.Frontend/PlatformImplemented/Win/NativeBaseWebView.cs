
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
using System.Windows.Forms;
using CefSharp.WinForms;


namespace WebMarco.Frontend.PlatformImplemented.Win {


    public abstract class NativeBaseWebView : ChromiumWebBrowser, INativeWebView {

        #region Private methods

        #endregion

        protected IBaseWindow parentWindow = null;
        protected BaseWebViewImplementer implementer;

        public NativeBaseWebView(string address)
            : base(address) {

        }

        #region Hide base properties

        protected new System.Drawing.Point Location {
            get {
                return base.Location;
            }

            set {
                base.Location = value;
            }
        }

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
                return new Uri(this.Address);
            }
        }

        #endregion

        public void LoadMarkup(string markup) {
            base.LoadHtml(markup, string.Empty);//TODO: why url is empty
        }

        public void LoadMarkup(Uri url) {
            base.Load(url.ToString()); //TODO: sort of not working on load in Window!??
        }

        public object CallFrontend(string script) {
            object result = null;
            BaseAppDelegate.Instance.ExecuteOnMainThread(() => { 
                result = base.EvaluateScript(script, new TimeSpan(0, 0, 3)); });//3 seonds timeout
            return (result != null) ? result.ToString() : result;
        }

        #endregion

        #region INativeView

        public Point TopLeft {
            get {
                return new Point(Location.X, Location.Y);
            }
            set {
                Location = new System.Drawing.Point((int)(value.X), (int)(value.Y));
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

        /// <summary>
        /// These props and methods duplicate inherited stuff, so 
        /// don't have to be implemented in order to comply with IBaseView 
        /// </summary>
        /*
                public new bool Visible {
                    get {
                        return base.Visible;
                    }
                    set {
                        base.Visible = value;
                    }
                }

                public new void Show() {
                    base.Show();
                }

                public new void Hide() {
                    base.Hide();
                }
         */

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
