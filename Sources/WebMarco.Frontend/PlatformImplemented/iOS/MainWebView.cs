using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarco.Frontend.Common;

namespace WebMarco.Frontend.PlatformImplemented.iOS {
    public class MainWebView : BaseWebView {

        public MainWebView(IBaseWindow window, BaseWebPage defaultPage)
            : base(window, defaultPage) {

        }

        /// <summary>
        /// Lets mark such methods with some special tag, like:
        /// 
        /// #ExtraInit MainWebView
        /// 
        /// so we would be able to locate them, if necessary, with text search.
        /// </summary>
        protected override void InitializeComponent() {//TODO: why override and where that initialization should really happen, here!?
            base.InitializeComponent();
            //some extra initialization, specific to current platform only
            
            //TODO: Load here?
        }
    }
}
