
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WebMarco.Frontend.Common;

namespace WebMarco.Frontend.PlatformImplemented.Win {
    public class BaseView : UserControl, IBaseView {

        private IBaseWindow parentWindow = null;
        public BaseView(IBaseWindow window)
            : base() {
            parentWindow = window;
            implementer = TinyIoC.TinyIoCContainer.Current.Resolve<BaseViewImplementer>();//TODO: ???
            base.Load += (sender, args) => {
                this.Load();
            };
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

        //public bool Visible {
        //    get {
        //        throw new NotImplementedException();
        //    }
        //    set {
        //        throw new NotImplementedException();
        //    }
        //}

        public new void Load() {
            //TODO: oh well...
            ///good place for some basic load routines like 
            ///adding this to parent window loaded views list etc, I guess
        }


        //public void Show() {
        //    throw new NotImplementedException();
        //}

        //public void Hide() {
        //    throw new NotImplementedException();
        //}

        public void AddSubview(IBaseView view) {
            throw new NotImplementedException();
        }
    }
}
