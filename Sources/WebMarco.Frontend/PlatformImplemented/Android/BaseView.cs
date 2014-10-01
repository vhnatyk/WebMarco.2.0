using Android.Content;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Text;
using WebMarco.Frontend.Common;

namespace WebMarco.Frontend.PlatformImplemented.Android {
    public class BaseView : View, IBaseView {

        private IBaseWindow parentWindow = null;
        public BaseView(IBaseWindow window)
            : base((Context)window) {
            parentWindow = window;
            implementer = TinyIoC.TinyIoCContainer.Current.Resolve<BaseViewImplementer>();//TODO: ???
        }

        readonly BaseViewImplementer implementer;

        public Point TopLeft {
            get {
                return new Point((double)this.Left, (double)this.Top);
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

        public new double Width {
            get {
                return (double)this.Width;
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new double Height {
            get {
                return (double)this.Height;
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
            throw new NotImplementedException();
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
