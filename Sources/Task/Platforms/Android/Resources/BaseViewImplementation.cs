using Android;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Text;
using WebMarco.Frontend.Common;

namespace BridgeTry.Android.PlatformImplemented {
    public class ViewImplementation : View, IBaseView {

        public ViewImplementation(global::Android.Content.Context context)
            : base(context) { 
        
        }

        public Point TopLeft {
            get {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public new double Height {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public ViewsHolder LoadedViews {
            get {
                throw new NotImplementedException();
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

        protected BaseViewInterfaceImplementer Implementer {
            get { return tinyIoc.re }
        }
    }
}
