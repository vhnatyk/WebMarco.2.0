using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMarco.Backend.Bridge.Common;

namespace WebMarco.Frontend.Common {
    public interface IBaseWindow {
        IBaseView MainView { get; }

        #region Protected fields and properties



        #endregion

        #region Public Properties
        IBaseView TopView { get; }
        IBaseView TopFullFrameView { get; }
        IBaseWebView TopWebView { get; }
        BaseRectangle CurrentFrame { get; }
        #endregion

        #region Constructors and initialization



        #endregion

        #region Frontend/Backend call mechanics

        #region Frontend call mechanics

        object CallFrontend(string script);

        #region Views management



        #endregion

        #endregion

        #region Backend call mechanics

        CallResult ProcessCallFromFrontend(CallConfig config);

        #endregion

        #endregion

        #region Private and Protected methods

        void ExecuteOnMainThread(Action action);

        #endregion

        #region Public methods



        #endregion
    }
}
