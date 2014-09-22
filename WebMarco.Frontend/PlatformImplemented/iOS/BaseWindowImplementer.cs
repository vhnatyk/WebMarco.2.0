
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMarco.Frontend;
using WebMarco.Frontend.Common;

namespace WebMarco.Frontend.PlatformImplemented.iOS {
    /// <summary>
    /// A place for #if WIN .. #endif code
    /// </summary>
    public class BaseWindowImplementer : WebMarco.Frontend.Common.BaseWindowImplementer {
        #region Private Fields


        #endregion

        #region Protected fields and properties



        #endregion

        #region Public Properties

        IBaseView mainView;
        public override IBaseView MainView {
            get { return mainView; }
            set { mainView = value; }
        }

        #endregion

        #region Constructors and initialization



        #endregion

        #region Frontend/Backend call mechanics

        #region Frontend call mechanics



        #region Views management



        #endregion

        #endregion

        #region Backend call mechanics



        #endregion

        #endregion

        #region Private and Protected methods


        #endregion

        #region Public methods



        #endregion

    }
}
