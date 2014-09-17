using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarco.Frontend.Common;

namespace WebMarco.Frontend.PlatformImplemented.Android {
    public class MainWebView : BaseWebView {
        private Action loadMainViewUrlOnceOnInitialization;

        public MainWebView(IBaseWindow window, BaseWebPage defaultPage)
            : base(window, defaultPage) {

        }

        /// <summary>
        /// Lets mark such methods with some special tag, like:
        /// #ExtraInit #Android #MainWebView
        /// so we would be able to locate them, if necessary, with text search.
        /// </summary>
        protected override void InitializeComponent() {//TODO: why override and where that initialization should really happen, here!?
        
        }
    }
}
