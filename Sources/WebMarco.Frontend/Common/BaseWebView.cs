using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebMarco.Backend.App.Common;
using WebMarco.Backend.Bridge.Common;
using WebMarco.Utilities.Logging;

///TODO: this #if block can be avoided if .PlatformImplemented 
///namespace would be stripped of platform subnamespace, for example .Android
///but it will require going slightly against either
///a) namespace naming policy - if using subfolder per platform, or
///b) class naming policy - if adding platform specifier to a file name but not to a class name.
///Both are not deadly sins as neither are #if <PLATFORM> blocks I guess:)
namespace WebMarco.Frontend.Common {
    public abstract class BaseWebView :
#if ANDROID
        WebMarco.Frontend.PlatformImplemented.Android.NativeBaseWebView
#elif WIN
        WebMarco.Frontend.PlatformImplemented.Win.NativeBaseWebView
#elif iOS
        WebMarco.Frontend.PlatformImplemented.iOS.NativeBaseWebView 
#elif MACOSX
        WebMarco.Frontend.PlatformImplemented.Mac.NativeBaseWebView 
#else 
  ///
#endif
, IBaseWebView {
        #region IBaseWebView
        public BaseWebView(IBaseWindow window, BaseWebPage defaultPage) :
#if ANDROID
            base(window)    
#elif WIN
            base(defaultPage.Url.ToString())
#elif iOS
            base() 
#elif MACOSX
            base()
#else 
  ///
#endif
 {
            LoadedPages = new Stack<BaseWebPage>();
            Page = defaultPage;
            parentWindow = window;
            implementer = new BaseWebViewImplementer(this);
        }

        #region Public Properties

        public string NameString {
            get { return this.GetType().Name; }
        }

        public string Markup {
            get {
                throw new NotImplementedException();
            }
        }

        public Stack<BaseWebPage> LoadedPages { get; private set; }

        public BaseWebPage Page {
            get {
                return LoadedPages.Peek();
            }
            private set {
                LoadedPages.Push(value);
            }
        }

        #endregion

        public void LoadMarkup() {
            implementer.LoadMarkup();
        }

        public void LoadMarkup(BaseWebPage page) {
            Page = page;
            LoadMarkup(page.Url);
        }

        #region Navigation API

        private static BaseWebPage GetPageByTypeName(string pageTypeName, string assemblyName) {
            return (BaseWebPage)(Activator.CreateInstance(
                       assemblyName,
                       string.Format("{0}.Common.Pages.{1}", assemblyName, pageTypeName),
                       true,
                       System.Reflection.BindingFlags.ExactBinding,
                       null,
                       null,
                       CultureInfo.InvariantCulture,
                       null).Unwrap()
                    );
        }

        private static string taskAssemblyName;
        private static BaseWebPage GetPageFromTaskFrontendAssembly(string pageTypeName) {
            if(string.IsNullOrWhiteSpace(taskAssemblyName)) {
                List<string> assemblyNames = AppDomain.CurrentDomain.GetAssemblies().
                    Where(assembly => assembly.GetName().Name.Contains("Frontend") && !assembly.GetName().Name.Contains("WebMarco")).
                    Select(assembly => assembly.GetName().Name).ToList();
                foreach(var assemblyName in assemblyNames) {
                    try {
                        BaseWebPage page = GetPageByTypeName(pageTypeName, assemblyName);
                        taskAssemblyName = assemblyName;
                        return page;
                    } catch(Exception ex) {
                        DLogger.WriteLog(ex);
                        return null;
                    }
                }
            }
            return GetPageByTypeName(pageTypeName, taskAssemblyName);
        }

        public virtual void LoadPage(string pageTypeName, string callBackMethodName = null) {//TODO: callBackMethodName ignored for now
            BaseWebPage page = GetPageFromTaskFrontendAssembly(pageTypeName);
            page.ParentWebView = this;
            page.Load();
        }

        public void Back() {
            LoadedPages.Pop();
            LoadMarkup(LoadedPages.Pop());
        }

        #endregion

        public CallResult ProcessCallFromFrontend(CallConfig config) {
            return implementer.ProcessCallFromFrontend(config);
        }

        public virtual void Load() {
            implementer.Load(); /*Sort of base.Load if BaseView would be base class for this one, 
            but it's not, but still can be executed in implementer if necessary */
            //a place to add firebug-lite ??? ?
            LoadMarkup();
        }

        #endregion
    }
}
