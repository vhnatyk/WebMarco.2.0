using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMarco.Frontend.Common;


#if MONOBJC
using Monobjc.Foundation;
#else

#endif

namespace WebMarco.Frontend.PlatformImplemented.Mac {
    public class Rectangle : BaseRectangle {
        public Rectangle(System.Drawing.RectangleF iOsRectangle) :
            base(
                new Point((double)(iOsRectangle.Left),
                    (double)(iOsRectangle.Top)),
                (double)(iOsRectangle.Width),
                (double)(iOsRectangle.Height)) {

        }

		#if MONOBJC

		public Rectangle(NSRect monobjcRect) :
		base(
			new Point((double)(monobjcRect.MinX),
				(double)(monobjcRect.MinY)),
			(double)(monobjcRect.Width.value),
			(double)(monobjcRect.Height.value)) {

		}

		public static BaseRectangle GetBaseWithRectangleF(NSRect frame) {
			return (BaseRectangle)(new Rectangle(frame));
		}

		public static NSRect GetRectangleF(BaseRectangle rectangle) {
			return new System.Drawing.RectangleF(
				(float)(rectangle.TopLeft.X),
				(float)(rectangle.TopLeft.Y),
				(float)(rectangle.Width),
				(float)(rectangle.Height));
		}

		#else

		public static System.Drawing.RectangleF GetRectangleF(BaseRectangle rectangle) {
			return new System.Drawing.RectangleF(
				(float)(rectangle.TopLeft.X),
				(float)(rectangle.TopLeft.Y),
				(float)(rectangle.Width),
				(float)(rectangle.Height));
		}

		#endif



		public static BaseRectangle GetBaseWithRectangleF(System.Drawing.RectangleF frame) {
			return (BaseRectangle)(new Rectangle(frame));
		}

		public Rectangle(Point topLeft, double width, double height) :
			base(topLeft,width,height) {

		}

		public Rectangle(IBaseView owningView) : base(owningView){

		}


        public static Rectangle GetWithRectangleF(System.Drawing.RectangleF frame) {
            return new Rectangle(frame);
        }

        public static Rectangle GetWithBaseRectangle(BaseRectangle baseRectangle) {
            return new Rectangle(baseRectangle.TopLeft, baseRectangle.Width, baseRectangle.Height);
        }
			
        public System.Drawing.RectangleF ToRectangleF() {
            return Rectangle.GetRectangleF((BaseRectangle)this);
        }
    }
}