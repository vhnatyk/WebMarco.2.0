
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Text;
using WebMarco.Frontend.Common;
using MonoMac.AppKit;

namespace WebMarco.Frontend.PlatformImplemented.Mac {
    public class BaseView : NSView, IBaseView {

        private IBaseWindow parentWindow = null;
        public BaseView(IBaseWindow window)
            : base() {
            parentWindow = window;
            implementer = TinyIoC.TinyIoCContainer.Current.Resolve<BaseViewImplementer>();//TODO: ???
        }

        readonly BaseViewImplementer implementer;

        public Point TopLeft {
            get {
                return new Point((double)(this.Frame.Left), (double)(this.Frame.Top));
            }
            set {
                throw new NotImplementedException();
            }
        }

        public Point Center {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public double Width {
            get {
                return (double)(this.Frame.Width);
            }
            set {
                throw new NotImplementedException();
            }
        }

        public double Height {
            get {
                return (double)(this.Frame.Height);
            }
            set {
                throw new NotImplementedException();
            }
        }

        public bool IsFullFrame {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public BaseRectangle CurrentFrame {
            get {
                return Rectangle.GetBaseWithRectangleF(Frame);
            }
            set {
                Frame = Rectangle.GetRectangleF(value);
            }
        }

        public IBaseWindow ParentWindow {
            get { throw new NotImplementedException(); }
        }

        public ViewsHolder LoadedViews {
            get {
                return implementer.LoadedViews;
            }
        }

        public bool IsModal {
            get { throw new NotImplementedException(); }
        }

        public bool Visible {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public void Load() {
            //TODO: oh well...
            ///good place for some basic load routines like 
            ///adding this to parent window loaded views list etc, I guess
        }


        public void Show() {
            throw new NotImplementedException();
        }

        public void Hide() {
            throw new NotImplementedException();
        }

        public void AddSubview(IBaseView view) {
            throw new NotImplementedException();
        }
    }
}
