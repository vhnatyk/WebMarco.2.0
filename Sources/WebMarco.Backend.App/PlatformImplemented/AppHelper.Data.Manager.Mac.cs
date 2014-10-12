using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using WebMarco.Backend.App.Common;
using System.IO;
using WebMarco.Utilities.Logging;
using WebMarco.Utilities.Paths;

namespace WebMarco.Backend.App.PlatformImplemented.Mac {
    public class Manager : AppHelper.Data.Manager {//TODO: a separate file only for this? hmm...
        public override void RestoreWorkingCopyOfDatabase(string databaseWorkingPath) { //TODO: seems to look like very crossplatform code
            string dataBaseName = Path.GetFileName(databaseWorkingPath);
            string databaseInAssetsPath = PathUtils.PathCombineCrossPlatform(AppHelper.Paths.DatafilesAssetPath, dataBaseName);
			try {
				File.Delete(databaseWorkingPath);
			} catch (Exception ex) {
				DLogger.WriteLog (ex);
			}
			File.Copy(databaseInAssetsPath, databaseWorkingPath);
        }
    }
}