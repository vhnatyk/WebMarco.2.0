
using MonoTouch.UIKit;
using System;
using System.Collections.Generic;
using System.Text;
using WebMarco.Frontend.Common;

namespace WebMarco.Frontend.PlatformImplemented.iOS {
    public class BaseViewImplementer : WebMarco.Frontend.Common.BaseViewImplementer {

        protected override IBaseView View { get; set; }

        private readonly BaseView baseView = null;

        public BaseViewImplementer(IBaseView view = null) {
            this.View = view;
            baseView = (BaseView)view;
        }

        #region IBaseView

        public new Point TopLeft {
            get {
                return baseView.CurrentFrame.TopLeft;
            }
            set {
                throw new NotImplementedException();
            }
        }

        Point Center {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        double Width {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        double Height {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }


        bool IsFullFrame {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        bool IsModal {
            get { throw new NotImplementedException(); }
        }

        bool Visible {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        BaseRectangle CurrentFrame {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public override void Load() {
            throw new NotImplementedException();
        }

        void Show() {
            throw new NotImplementedException();
        }

        void Hide() {
            throw new NotImplementedException();
        }

        void AddSubview(IBaseView view) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
