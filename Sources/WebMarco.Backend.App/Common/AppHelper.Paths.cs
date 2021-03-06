﻿using System;
using System.Collections.Generic;
using System.IO;
using WebMarco.Utilities.Logging;
using WebMarco.Utilities.Paths;

#if MACOSX
using MonoMac;
using MonoMac.Foundation;
using MonoMac.ObjCRuntime;
using System.Runtime.InteropServices;
#endif

namespace WebMarco.Backend.App.Common {
    /// <summary>
    /// Due to its inclusion of #if <PLATFORM> .. #endif blocks - this class has to be abstract
    /// and has to have it's implementation in PlatformImplemented subnamespace, but due to oneliner or simple nature of the 
    /// platform-specific code blocks - let's avoid implementing of a class-per-line mess:) Sort of a hack...
    /// </summary>

	public static partial class AppHelper {
        #region Paths

		#if MACOSX
		[DllImport(Constants.FoundationLibrary)]
		public static extern IntPtr NSHomeDirectory();

		/// <summary>
		/// Gets the container directory - sandboxed root.
		/// </summary>
		/// <value>The container directory.</value>
		public static string ContainerDirectory {
			get {
				return ((NSString)Runtime.GetNSObject (NSHomeDirectory ())).ToString ();
			}
		}
		#endif

        public static class Paths {
            #region Folders

            #region Assets

            private static string assetRootFolder;
            public static string AssetRootFolder {
                get {
                    if (assetRootFolder == null) {
#if MACOSX
						assetRootFolder = PathUtils.PathCombineCrossPlatform(AppDomain.CurrentDomain.BaseDirectory, "Assets");//TODO: seems unnecessary
#elif ANDROID
                        assetRootFolder = PathUtils.PathCombineCrossPlatform("/android_asset/", string.Empty);
#elif iOS
                    	assetRootFolder = PathUtils.PathCombineCrossPlatform(Environment.CurrentDirectory, "Assets");
#else
                    	assetRootFolder = PathUtils.PathCombineCrossPlatform(AppDomain.CurrentDomain.BaseDirectory, "Assets");
#endif
                    }
                    return assetRootFolder;
                }
            }

            public static string GetFullAssetPath(string relativePath) {
                return PathUtils.PathCombineCrossPlatform(AssetRootFolder, relativePath);
            }

            public static string GetPageAssetPath(string viewFileName) {
                return PathUtils.PathCombineCrossPlatform(AssetRootFolder, "html/Pages", viewFileName);
            }

            public static string GetImageAssetPath(string imageFileName) {
                return PathUtils.PathCombineCrossPlatform(AssetRootFolder, "img", imageFileName);
            }

            //public static Uri GetViewUrl(AvailableViews view) {
            //    return new Uri(GetViewPath(view.ToString() + ".htm"), UriKind.Absolute);
            //}

            public static string ScriptsAssetPath {
                get {
                    return PathUtils.PathCombineCrossPlatform(AssetRootFolder, "js/");
                }
            }

            private static string DatafilesPath(string parentFolder) {
                return PathUtils.PathCombineCrossPlatform(parentFolder, "db/");

            }

            public static string DatafilesAssetPath {
                get {
                    return DatafilesPath(AssetRootFolder);
                }
            }

            public static string DatafilesWorkingPath {
                get {
                    string result = DatafilesPath(WorkingRootFolder);
                    if (!Directory.Exists(result)) {
                        Directory.CreateDirectory(result);
                    }
                    return result;
                }
            }

            #endregion

            private static string workingRootFolder;
            public static string WorkingRootFolder {
                get {
                    if (workingRootFolder == null) {
#if MACOSX
					    //workingRootFolder = PathUtils.PathCombineCrossPlatform(FoundationFramework.NSHomeDirectory(), "YourCompanyName", "YourAppName");//Monobjc way
						workingRootFolder = PathUtils.PathCombineCrossPlatform(ContainerDirectory, "YourCompanyName", "YourAppName");
#elif ANDROID
                        workingRootFolder = //Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;//TODO: find out alternatives for this, work it out
                        Environment.GetFolderPath(Environment.SpecialFolder.Personal);
#elif iOS
                        workingRootFolder = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
#else
                        workingRootFolder = PathUtils.PathCombineCrossPlatform(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "YourCompanyName", "YourCompanyName");//TODO: must come from settings
#endif
                        if (!Directory.Exists(workingRootFolder)) {
                            Directory.CreateDirectory(workingRootFolder);
                        }

                        DLogger.WriteLog("WorkingRootFolder: {0}", workingRootFolder);
                    }

                    return workingRootFolder;
                }
            }

            private static string temporaryRootFolder;
            public static string TemporaryRootFolder {
                get {
                    if (temporaryRootFolder == null) {
                        temporaryRootFolder = Path.Combine(WorkingRootFolder, "tmp");
                        if (!Directory.Exists(temporaryRootFolder)) {
                            Directory.CreateDirectory(temporaryRootFolder);
                        }

                        DLogger.WriteLog("TemporaryRootFolder: {0}", temporaryRootFolder);
                    }

                    return temporaryRootFolder;
                }
            }
            #endregion

            public static string GetFilePathInTemporaryFolder(string filename) {
                return PathUtils.PathCombineCrossPlatform(TemporaryRootFolder, filename);
            }
        }
        #endregion
    }
}

