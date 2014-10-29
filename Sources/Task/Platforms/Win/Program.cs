using CefSharp;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using WebMarco.Utilities.Logging;


namespace BridgeTry.Win {
    public static class Program {
        ///<summary>
        ///Application main entry point
        ///</summary>
        [STAThread]
        static void Main() {
            //            try {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Cef.Initialize();

            Application.Run(new MainWindow());

            //            } catch (Exception ex) {
            //                DLogger.WriteLog(ex);
            //#if DEBUG
            //                throw;
            //#endif
            //            }
        }
    }
}
