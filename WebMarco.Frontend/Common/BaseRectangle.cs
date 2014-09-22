using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMarco.Frontend.Common {
    public class BaseRectangle {

        public BaseRectangle(Point topLeft, double width, double height, IBaseView owningView = null) {
            this.owningView = owningView;
            TopLeft = topLeft;
            Width = width;
            Height = height;
        }

        public static BaseRectangle GetZeroRectangle(IBaseView owningView = null) {
            return new BaseRectangle(new Point(0, 0), 0, 0, owningView);
        }

        private double width;
        public double Width {
            get {
                return width;
            }
            set {
                width = value;
            }
        }

        private double height;
        public double Height {
            get {
                return height;
            }
            set {
                height = value;
            }
        }

        public Point TopLeft { get; set; }
        public Point BottomRight { get; set; }
        public Point Center { get; set; }

        private IBaseView owningView;
        public IBaseView OwningView {
            get {
                return owningView;
            }
        }
    }
}