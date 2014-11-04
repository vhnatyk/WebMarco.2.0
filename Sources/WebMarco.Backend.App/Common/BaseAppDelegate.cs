using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMarco.Backend;
using WebMarco.Backend.Bridge.Common;
using WebMarco.Utilities.Logging;
using Monobjc.Foundation;



namespace WebMarco.Backend.App.Common {
#if iOS
    public abstract partial class BaseAppDelegate : MonoTouch.UIKit.UIApplicationDelegate
#elif MACOSX
	#if MONOBJC
	public abstract partial class BaseAppDelegate : NSObject //, Monobjc.AppKit.INSApplicationDelegate
	#else
	public abstract partial class BaseAppDelegate : MonoMac.AppKit.NSApplicationDelegate
	#endif
#else
    public abstract class BaseAppDelegate 
#endif    
    {
        public object MainWindow { get; private set; }

        public static BaseAppDelegate Instance {
            get {
#if iOS
                return (BaseAppDelegate)(MonoTouch.UIKit.UIApplication.SharedApplication.Delegate);
#elif MACOSX
				#if MONOBJC
				return (BaseAppDelegate)(Monobjc.AppKit.NSApplication.SharedApplication.Delegate);
				#else
				return (BaseAppDelegate)(MonoMac.AppKit.NSApplication.SharedApplication.Delegate);
				#endif
#else
                return TinyIoC.TinyIoCContainer.Current.Resolve<BaseAppDelegate>();
#endif
            }
        }

        public virtual void StartServer(Func<CallConfig, CallResult> requestHandleCallback) {
            MicroServer.Instance.Start(null, null, null, requestHandleCallback);
        }

        public virtual CallResult ReinitializeServerUid(CallConfig config) {
            CallResult result = null;
            string newUid = Guid.NewGuid().ToString();
            MicroServer.Instance.ReinitializeInstanceUid(newUid);

            ///-------------------------------------------------
            ///now we have to let know frontend of the change of 
            ///server instance UID that happened 
            ///-------------------------------------------------
            //CallFrontend(string.Format("setServerInstanceUid('{0}');",newUid); //alternative 1

            ///'native' way via CallResult - alternative 2
            string frontendCallbackConfig = CallConfig.NewCallConfig("setServerInstanceUid", newUid).ToJsonString();
            result = new CallResult(frontendCallbackConfig);
            return result;
        }

        public abstract void ExecuteOnMainThread(Action action);
        public abstract void ExecuteOnMainThread<T>(Action<T> action, T optionalParameter);
        public abstract void Quit();

        public virtual void OpenUrlInSystemBrowser(string url) {
            try {
                System.Diagnostics.Process.Start(url);
            } catch (Exception ex) {
                DLogger.WriteLog(ex);
#if WIN
                System.Diagnostics.Process.Start("IEXPLORE.EXE", url);
#endif
            }
        }
    }
}
