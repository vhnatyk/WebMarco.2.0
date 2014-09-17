using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMarco.Frontend.Common {
    public abstract class BaseViewImplementer : IBaseView {
        //public virtual Point TopLeft { get; set; }
        //public virtual Point Center { get; set; }
        //public virtual double Width { get; set; }
        //public virtual double Height { get; set; }

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

        #region Common crossplatform code

        private ViewsHolder loadedViews;
        public ViewsHolder LoadedViews {
            get {
                if (loadedViews == null) {
                    loadedViews = new ViewsHolder();
                }
                return loadedViews;
            }
        }

        #endregion

        public virtual bool IsFullFrame { get; set; }


        public abstract void Load();


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

        public void Show() {
            throw new NotImplementedException();
        }

        public void Hide() {
            throw new NotImplementedException();
        }

        public void AddSubview(IBaseView view) {
            throw new NotImplementedException();
        }


        public IBaseWindow ParentWindow {
            get { return LoadedViews.TopView.ParentWindow; }
        }
    }
}
