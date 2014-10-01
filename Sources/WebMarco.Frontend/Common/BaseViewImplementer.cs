using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMarco.Frontend.Common {
    public abstract class BaseViewImplementer : IBaseView {

        protected abstract IBaseView View { get; set; }
        
        #region IBaseView

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

        public double Width {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public double Height {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public IBaseWindow ParentWindow {
            get { return LoadedViews.TopView.ParentWindow; }
        }

        private ViewsHolder loadedViews;
        public ViewsHolder LoadedViews {
            get {
                if (loadedViews == null) {
                    loadedViews = new ViewsHolder();
                }
                return loadedViews;
            }
        }

        public virtual bool IsFullFrame { get; set; }
 
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

        public BaseRectangle CurrentFrame {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }
        
        public abstract void Load();

        public void Show() {
            throw new NotImplementedException();
        }

        public void Hide() {
            throw new NotImplementedException();
        }

        public void AddSubview(IBaseView view) {
            throw new NotImplementedException();
        }

        #endregion
    }
}
