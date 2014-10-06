using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarco.Frontend.Common;
using WebMarco.Frontend.PlatformImplemented.Win;
using BridgeTry.Frontend.Common.Views;

namespace BridgeTry.Views {
    /// <summary>
    /// Windows specific stuff for MainView class
    /// </summary>
    class MainView : BridgeTry.Frontend.Common.Views.MainView {
        public MainView(IBaseWindow window)
            : base(window) {
            InitializeComponent();//TODO: has it to be standard thing of a base class?
        }
    }
}
