using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebMarco.Backend.App.Common;
using WebMarco.Backend.Bridge.Common;

//[assembly: InternalsVisibleToAttribute("RideReady.Frontend")] //TODO: make it work via solution wide project import
namespace WebMarco.Frontend.Common {
    public abstract class BaseWebPage {

        #region Private methods

        internal static Uri GetPageUrl(BaseWebPage page) {
            return new Uri(AppHelper.Paths.GetPageAssetPath(page.NameString + ".htm"), UriKind.Absolute);
        }

        #endregion


        protected BaseWebPage() { 
        
        }

        public BaseWebPage(IBaseWebView parentWebView = null) {            
            ParentWebView = parentWebView;
        }

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

        #region Wrappers of methods of BaseWebView

        public virtual void Load() {
            ParentWebView.LoadMarkup(this);
        }

        /// <summary>
        /// This method is here for convenient calls instead of ParentWebView.CallFrontend
        /// </summary>
        /// <param name="script">JavaScript script to execute.</param>
        /// <returns>result of call from frontend</returns>
        public object CallFrontend(string script) {
            return ParentWebView.CallFrontend(script);
        }

        #endregion
    }
}
