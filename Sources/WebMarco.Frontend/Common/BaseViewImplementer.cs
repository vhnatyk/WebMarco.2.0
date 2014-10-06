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
				return View.CurrentFrame.TopLeft;//!!!View.TopLeft will cause loop!
			}
			set {
				View.CurrentFrame.TopLeft = value;
			}
        }

        public Point Center {
			get {
				return View.CurrentFrame.Center;//!!!View.Center will cause loop!
			}
			set {
				View.CurrentFrame.Center = value;
			}
        }

        public double Width {
            get {
				return View.CurrentFrame.Width;//!!!View.Width will cause loop!
            }
            set {
				View.CurrentFrame.Width = value;
            }
        }

        public double Height {
			get {
				return View.CurrentFrame.Height;//!!!View.Height will cause loop!
			}
			set {
				View.CurrentFrame.Height = value;
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
