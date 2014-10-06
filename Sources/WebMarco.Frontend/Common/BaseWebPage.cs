using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebMarco.Backend.App.Common;

//[assembly: InternalsVisibleToAttribute("BridgeTry.Frontend")] //TODO: make it work via solution wide project import
namespace WebMarco.Frontend.Common {
    public abstract class BaseWebPage {

        #region Private methods

        internal static Uri GetPageUrl(BaseWebPage page) {
            return new Uri(AppHelper.Paths.GetPageAssetPath(page.NameString + ".htm"), UriKind.Absolute);
        }

        #endregion

        private IBaseWebView parentWebView;
        public IBaseWebView ParentWebView {
            get {
                return parentWebView;
            }
            set {
                if (parentWebView == null) { //set only once
                    parentWebView = value;
                } else { 
#if DEBUG
                    throw new MemberAccessException("ParentWebView already set."); 
#endif
                }       
            }
        } // internal set; } //TODO: make it work via solution wide settings project import

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
