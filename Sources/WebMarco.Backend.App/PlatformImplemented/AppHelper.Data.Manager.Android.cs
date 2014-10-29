using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WebMarco.Backend.App.Common;
using System.IO;
using WebMarco.Utilities.Logging;
using WebMarco.Utilities.Paths;

namespace WebMarco.Backend.App.PlatformImplemented.Android
{
    public class Manager : AppHelper.Data.Manager {

        Context context;

        public Manager(Context context) {
            this.context = context;
        }

        public override string RestoreWorkingCopyOfDatabase(string databaseWorkingPath) { 
            string dataBaseName = Path.GetFileName(databaseWorkingPath);

            string databaseInAssetsPath = PathUtils.PathCombineCrossPlatform(AppHelper.Paths.DatafilesAssetPath, dataBaseName);
            var databaseContentFromAssets = GetBinaryAssetContent(databaseInAssetsPath);

            databaseWorkingPath = PathUtils.PathCombineCrossPlatform(AppHelper.Paths.DatafilesWorkingPath, dataBaseName);

            File.WriteAllBytes(databaseWorkingPath, databaseContentFromAssets);

            return databaseWorkingPath;
        }

        private byte[] GetBinaryAssetContent(string pathOfItemInAssets) {
            byte[] result = null;

            string baseDirectoryNameInAssets = Path.GetDirectoryName(pathOfItemInAssets);
            string baseDirectoryInRoot = "/";
            try {
                baseDirectoryInRoot = baseDirectoryNameInAssets.Split(new string[] { AppHelper.Paths.AssetRootFolder }, StringSplitOptions.RemoveEmptyEntries)[0];
            } catch { }
            
            string baseFileName = Path.GetFileName(pathOfItemInAssets);

            HashSet<string> chunkFileNames = new HashSet<string>(context.Assets.List(baseDirectoryInRoot).Where(fn => fn.StartsWith(baseFileName)));

            using (MemoryStream memoryStream = new MemoryStream()) {

                int i = 0;
                string chunkFileName = baseFileName;

                while (chunkFileNames.Contains(chunkFileName)) {

                    using (Stream stream = context.Assets.Open(Path.Combine(baseDirectoryInRoot, chunkFileName))) {
                        byte[] buffer = new byte[32 * 1024];

                        int read;
                        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0) {
                            memoryStream.Write(buffer, 0, read);
                        }

                        i++;
                        chunkFileName = string.Format("{0}.part_{1}", baseFileName, i);
                    }
                }

                result = memoryStream.ToArray();
            }

            DLogger.WriteLog("GetBinaryAssetContent: {0} ({1})", pathOfItemInAssets, (result == null) ? "null" : result.Length.ToString());

            return result;
        }
    }
}