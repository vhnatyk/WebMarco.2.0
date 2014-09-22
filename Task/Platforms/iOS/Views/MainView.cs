using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using WebMarco.Frontend.PlatformImplemented.iOS;
using WebMarco.Frontend.Common;
using WebMarco.Backend.Bridge.Common;
using System.Globalization;
using WebMarco.Utilities.Library;


namespace BridgeTry.Views {
    class MainView : BridgeTry.Frontend.Common.Views.MainView {
        public MainView(IBaseWindow window)
            : base(window) {

            IsModal = false;
        }

        public override void Load() {
            base.Load();

            //LoadUrl("javascript:console.log('test c# log')");
            //LoadUrl("javascript:setServerInstanceUid('7c9e6679742540de944be07fc1f90ae7')");
            //string jsResult = jsInterface.GetJsValue(this, "2 + 5");
        }

        /// A place to start putting your methods, like we used to in WinForms in the old good days;)

        public CallResult Quit() {
            string textResult = new CallConfig(Guid.NewGuid(), "displayShowcaseInfo", "On iOS you don't have to exit your app! Press \"Home\" button instead. ").ToJsonString();
            return new CallResult(textResult);
        }
    }
}