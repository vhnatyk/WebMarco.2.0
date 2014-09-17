using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vici.CoolStorage;
using WebMarco.Utilities.Logging;
using WebMarco.Utilities.Paths;
using System.IO;
#if ANDROID
using Vici.CoolStorage.Xamarin.Android.Sqlite;
#elif WIN

#endif


namespace WebMarco.Backend.App.Common {
    public static partial class AppHelper {
        #region Data
        public static class Data {

            private const string MainDatabaseName = "master.db"; //TODO: whoa:(  - from config!!! 
            public static string MainDatabaseFileInAssets {
                get {
                    return PathUtils.PathCombineCrossPlatform(AppHelper.Paths.DatafilesAssetPath, MainDatabaseName);
                }
            }

            public static string MainDatabaseFileInWorkingPath {
                get {
                    return PathUtils.PathCombineCrossPlatform(AppHelper.Paths.DatafilesWorkingPath, MainDatabaseName);
                }
            }

            public static void ConnectDatabase() {
                ConnectDb(MainDatabaseFileInWorkingPath);
            }

            public static void ConnectDb(string databaseWorkingCopyPath) {
//                try {
                    if (!FileExists(databaseWorkingCopyPath)) {
                        Manager.Instance.RestoreWorkingCopyOfDatabase(databaseWorkingCopyPath);
                    }

                    CSConfig.SetDB(new CSDataProviderSqlite("Data Source=" + databaseWorkingCopyPath));

                    DLogger.WriteLog(string.Format("Connected to Db: " + databaseWorkingCopyPath));

                    return;
                //} catch (Exception ex) {
                //    DLogger.WriteLog(ex);
                //    throw new Exception(string.Format("Cant connect to database at \"{0}\"", databasePath), ex);
                //}
            }
            private static bool FileExists(string filePath) {
                return File.Exists(filePath);
            }

            public abstract class Manager {
                public static Manager Instance {
                    get {
                        return TinyIoC.TinyIoCContainer.Current.Resolve<Manager>();
                    }
                }

                public abstract void RestoreWorkingCopyOfDatabase(string databasePathInAssets);
                public virtual void RestoreWorkingCopyOfMainDatabase() {
                    RestoreWorkingCopyOfDatabase(MainDatabaseFileInAssets);
                }
            }
        }
        #endregion
    }
}