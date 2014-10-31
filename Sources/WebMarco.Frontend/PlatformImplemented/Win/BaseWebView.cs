
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


    public abstract class BaseWebView : ChromiumWebBrowser, IBaseWebView {

        #region Private methods

        #endregion

        private IBaseWindow parentWindow = null;

        private BaseWebViewImplementer implementer;

        //public BaseWebView(IBaseWindow window)
        //    : base("about:blank") {
        //    parentWindow = window;
        //    implementer = new BaseWebViewImplementer(this);
        //}

        public BaseWebView(IBaseWindow window, BaseWebPage defaultPage)
            : base(defaultPage.Url.ToString()) {
            Page = defaultPage;
            parentWindow = window;
            implementer = new BaseWebViewImplementer(this);
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


        #region IBaseWebView

        #region Public Properties

        public string NameString {
            get { return this.GetType().Name; }
        }

        public virtual Uri ViewUrl {
            get {
                return (Page == null)? new Uri(this.Address) : Page.Url;
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
            base.LoadHtml(markup, string.Empty);//TODO: why url is empty
        }

        public void LoadMarkup(Uri url) {
            base.Load(url.ToString()); //TODO: sort of not working on load in Window!??
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
            BaseAppDelegate.Instance.ExecuteOnMainThread(() => result = this.EvaluateScript(script));
            return (result != null) ? result.ToString() : result;
        }

        public CallResult ProcessCallFromFrontend(CallConfig config) {
            return implementer.ProcessCallFromFrontend(config);
        }

        #endregion

        #region IBaseView

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

        public virtual void Load() {
            implementer.Load(); /*Sort of base.Load if BaseView would be base class for this one, 
            but it's not, but still can be executed in implementer if necessary */
            //a place to add firebug-lite ??? ?
            LoadMarkup();
        }

        #endregion


        public BaseRectangle CurrentFrame {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
    }
}
