using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMarco.Backend.Bridge.Common;
using WebMarco.Backend.App.Common;

namespace WebMarco.Frontend.Common {
    public class BaseWindowImplementer : IBaseWindow {
        public IBaseView MainView { get; set; }

        public virtual IBaseView TopView {
            get {
                return MainView.LoadedViews.TopView;
            }
        }
        public virtual IBaseView TopFullFrameView {
            get {
                throw new NotImplementedException();
            }
        }
        public virtual IBaseWebView TopWebView {
            get {
                IBaseView[] mainViewLoadedViewsItemsToArray = MainView.LoadedViews.Items.ToArray();
                for (int index = MainView.LoadedViews.Items.Count - 1; index >= 0; index--) {
                    try {
                        return (IBaseWebView)mainViewLoadedViewsItemsToArray[index];
                    } catch { }
                }

                return null;
            }
        }

        public virtual object CallFrontend(string script) {
            return TopWebView.CallFrontend(script);
        }
        public virtual CallResult ProcessCallFromFrontend(CallConfig config) {
            return TopWebView.ProcessCallFromFrontend(config);
        }
        public virtual void ExecuteOnMainThread(Action action) {
            BaseAppDelegate.Instance.ExecuteOnMainThread(action);
        }


        public BaseRectangle CurrentFrame {
            get { throw new NotImplementedException(); }
        }
    }
}
