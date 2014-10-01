using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebMarco.Backend.App.Common;

[assembly: InternalsVisibleToAttribute("BridgeTry.Frontend")]
namespace WebMarco.Frontend.Common {
    public abstract class BaseWebPage {

        #region Private methods

        internal static Uri GetPageUrl(BaseWebPage page) {
            return new Uri(AppHelper.Paths.GetPageAssetPath(page.NameString + ".htm"), UriKind.Absolute);
        }

        #endregion
        
        public IBaseWebView ParentWebView { get; internal set; }

        public string NameString {
            get { return this.GetType().Name; }
        }

        public virtual Uri Url {
            get {
                return GetPageUrl(this);
            }
        }

        public virtual void Load() {
            ParentWebView.LoadMarkup();
        }
    }
}
