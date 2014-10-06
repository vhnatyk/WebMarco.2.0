using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMarco.Frontend.Common {
    public class BaseRectangle {

		public BaseRectangle(Point topLeft, double width, double height) {
			TopLeft = topLeft;
			Width = width;
			Height = height;
        }

		public BaseRectangle(IBaseView owningView) {
			if (owningView == null) {
				throw new ArgumentNullException ("owningView", "Owning view cannot be null in this constructor.");
			}
			OwningView = owningView;
		}

		private BaseRectangle(){
		
		}
			
        public static BaseRectangle GetZeroRectangle() {
            return new BaseRectangle(new Point(0, 0), 0, 0);
        }

        private double width;
        public double Width {
            get {
				return (owningView == null) ? width : owningView.CurrentFrame.Width;//!!!owningView.Width will cause loop
            }
            set {
				if (owningView == null) {
					width = value;
				} else {
					BaseRectangle newRectangle = new BaseRectangle ();
					newRectangle.width = value;
					newRectangle.height = Height;
					newRectangle.topLeft = TopLeft;
					owningView.CurrentFrame = newRectangle;
				}
            }
        }

        private double height;
        public double Height {
			get {
				return (owningView == null) ? height : owningView.CurrentFrame.Height;//!!!owningView.Height will cause loop
			}
			set {
				if (owningView == null) {
					height = value;
				} else {
					BaseRectangle newRectangle = new BaseRectangle ();
					newRectangle.Width = Width;
					newRectangle.Height = value;
					newRectangle.TopLeft = TopLeft;
					owningView.CurrentFrame = newRectangle;
				}
			}
        }

		private Point topLeft;
        public Point TopLeft {
			get {
				return (owningView == null) ? topLeft : owningView.TopLeft;
			}
			set {
				if (owningView == null) {
					topLeft = value;
				} else {
					owningView.TopLeft = value;
				}                
			}
		}

        public Point BottomRight { 
			get {
				double x = TopLeft.X + Width;
				double y = TopLeft.Y + Height;

				return new Point (x, y);
			}
			set {
				double x = value.X - BottomRight.X;
				double y = value.Y - BottomRight.Y;
				TopLeft.X += x;
				TopLeft.Y += y;
			}
		}

		public Point Center { 
			get {
				double x = TopLeft.X + Width / 2;
				double y = TopLeft.Y + Height / 2;

				return new Point (x, y);
			}
			set {
				double x = value.X - Center.X;
				double y = value.Y - Center.Y;
				TopLeft.X += x;
				TopLeft.Y += y;
			}
		}

        private IBaseView owningView;
        public IBaseView OwningView {
            get {
                return owningView;
            }
			private set { 
				owningView = value;
			}
        }
    }
}