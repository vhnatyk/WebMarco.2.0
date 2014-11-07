using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebMarco.Utilities.Logging;
using WebMarco.Backend.App.Common;

namespace BridgeTry {
    /// <summary>
    /// The term "Task" was used because other more suitable words like "App" and "Solution" or "Project"
    /// are so heavily overloaded that it's almost impossible to use them to identify your 
    /// Crossplatform Application Solution, so the term "Task" as it turns out  - is second to none, unfortunately.
    /// But perhaps some other good and more meaningful words can be on some native English speaking guy's mind out there;)
    /// The code in the region "Singleton realization" should be copypasted to your AppDelegate of your new so called Task.
    /// So this file may be called a template or an example.
    /// </summary>
    public class AppDelegate : WebMarco.Backend.App.PlatformImplemented.Win.BaseAppDelegate {

        #region Singleton realization
        /// <summary>
        /// This region should be copypasted to your AppDelegate class.
        /// </summary>

        private static readonly Lazy<AppDelegate> lazy =
            new Lazy<AppDelegate>(() => new AppDelegate());

        public new static AppDelegate Instance { get { return lazy.Value; } }

        private AppDelegate() {
            DLogger.Initialize(AppHelper.Paths.WorkingRootFolder);

            ///Some additional early initialization routines can be done here if needed.
        }
        #endregion

    }
}
