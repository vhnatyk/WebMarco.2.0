
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMarco.Backend.App.PlatformImplemented.Mac;
using WebMarco.Backend.Bridge.Common;
using WebMarco.Frontend;
using WebMarco.Frontend.Common;
using MonoMac.AppKit;
using MonoMac.Foundation;

namespace WebMarco.Frontend.PlatformImplemented.Mac {
    public abstract class BaseWindow : NSWindow, IBaseWindow {

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
            get { 
				return Rectangle.GetWithRectangleF(Frame);
			}
        }

        #endregion

        #region Constructors and initialization

        public BaseWindow()
            : base() {
			InitializeComponent();
        }

        public BaseWindow(Rectangle frame) :
            base(new System.Drawing.RectangleF(
                (float)(frame.TopLeft.X),
                (float)(frame.TopLeft.Y),
                (float)(frame.Width),
				(float)(frame.Height)), 
			NSWindowStyle.Resizable, 
			NSBackingStore.Buffered, 
			false) {
			InitializeComponent();
        }

		void InitializeComponent(){
			//TODO:a place for default style? Must it be in config?

			//StyleMask = NSWindowStyle.Closable | NSWindowStyle.Miniaturizable | NSWindowStyle.Resizable | NSWindowStyle.DocModal;
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
            get { return new Rectangle(this.Frame);}
        }
    }
}
