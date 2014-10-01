
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMarco.Backend.App.PlatformImplemented.iOS;
using WebMarco.Backend.Bridge.Common;
using WebMarco.Frontend;
using WebMarco.Frontend.Common;

namespace WebMarco.Frontend.PlatformImplemented.iOS {
    public abstract class BaseWindow : UIWindow, IBaseWindow {

        #region Private Fields

        readonly BaseWindowImplementer implementer = new BaseWindowImplementer();

        public IBaseView MainView {
            get {
                return implementer.MainView;
            }
            protected set {
                implementer.MainView = value;
            }
        }

        #endregion

        #region Protected fields and properties



        #endregion

        #region Public Properties

        public IBaseView TopView {
            get {
                return MainView.LoadedViews.TopView;
            }
        }

        public IBaseView TopFullFrameView {
            get {
                throw new NotImplementedException();
            }
        }

        public IBaseWebView TopWebView {
            get {
                throw new NotImplementedException();
            }
        }


        public Rectangle CurrentFrame {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region Constructors and initialization

        public BaseWindow()
            : base() {

        }

        public BaseWindow(Rectangle frame) :
            base(new System.Drawing.RectangleF(
                (float)(frame.TopLeft.X),
                (float)(frame.TopLeft.Y),
                (float)(frame.Width),
                (float)(frame.Height))) {

        }

        #endregion

        #region Frontend/Backend call mechanics

        #region Frontend call mechanics

        public object CallFrontend(string script) {
            return implementer.CallFrontend(script);
        }

        #region Views management



        #endregion

        #endregion

        #region Backend call mechanics

        public CallResult ProcessCallFromFrontend(CallConfig config) {
            return implementer.ProcessCallFromFrontend(config);
        }

        #endregion

        #endregion

        #region Private and Protected methods



        #endregion

        #region Public methods

        public void ExecuteOnMainThread(Action action) {
            InvokeOnMainThread(new NSAction(() => { action.Invoke(); }));
            //or can be 
            //BaseAppDelegate.Instance.ExecuteOnMainThread(action);
        }

        #endregion
        BaseRectangle IBaseWindow.CurrentFrame {
            get { return new iOS.Rectangle(this.Frame);}
        }
    }
}
