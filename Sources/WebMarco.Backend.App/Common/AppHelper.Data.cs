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

#elif iOS
using Vici.CoolStorage.Xamarin.iOS.Sqlite;
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

            public static bool ExistsMainDb {
                get { return File.Exists(MainDatabaseFileInWorkingPath); }
            }

            public static void ConnectDatabase() {
                ConnectDatabase(MainDatabaseFileInWorkingPath);
            }

            public static void ConnectDatabase(string databaseWorkingCopyPath) {
				try {
                    if (!FileExists(databaseWorkingCopyPath)) {
                        Manager.Instance.RestoreWorkingCopyOfDatabase(databaseWorkingCopyPath);
                    }

                    CSConfig.SetDB(new CSDataProviderSqlite("Data Source=" + databaseWorkingCopyPath));

                    DLogger.WriteLog(string.Format("Connected to Db: " + databaseWorkingCopyPath));

                    return;
                } catch (Exception ex) {
                    DLogger.WriteLog(ex);
					throw new Exception(string.Format("Can't connect to database at \"{0}\"", databaseWorkingCopyPath), ex);
                }
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

		public virtual void RestoreWorkingCopyOfDatabase(string databaseWorkingPath) {
			string dataBaseName = Path.GetFileName(databaseWorkingPath);
			string databaseInAssetsPath = PathUtils.PathCombineCrossPlatform(AppHelper.Paths.DatafilesAssetPath, dataBaseName);
			try {
				File.Delete(databaseWorkingPath);
			} catch (Exception ex) {
				DLogger.WriteLog (ex);
			}
			File.Copy(databaseInAssetsPath, databaseWorkingPath);
		}

                public virtual void RestoreWorkingCopyOfMainDatabase() {
					RestoreWorkingCopyOfDatabase(MainDatabaseFileInWorkingPath);
                }
            }
        }
        #endregion
    }
}
