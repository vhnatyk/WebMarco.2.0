using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using WebMarco.Utilities.Paths;

namespace WebMarco.Utilities
{
    namespace Logging
    {
        public enum LoggingLevel
        {
            Default = 0, //everything
            Low,
            Medium,
            High,
            ExceptionsOnly
        }

        public static class DLogger
        {
            //private static readonly Lazy<Logger> lazy = new Lazy<Logger>(() => new Logger());

            //public static Logger Instance { get { return lazy.Value; } }

            //private Logger()
            //{
            //}

            private static string lockFlag = string.Empty;

            #region Logging
            public static LoggingLevel CurrentLoggingLevel;
            public static string RootLoggingFolder { get; private set; }
            public static bool IsEncryptionOfLogEnabled { get; private set; }

            public static void Initialize(string rootLoggingFolder = null, bool isEncryptionOfLogEnabled = false) {
                if (String.IsNullOrWhiteSpace(rootLoggingFolder)) {
                    IsLoggingToFileEnabled = false;
                }
                else {
                    RootLoggingFolder = PathUtils.PathCombineCrossPlatform(rootLoggingFolder, "Log");
                    if (!Directory.Exists(RootLoggingFolder)) {
                        Directory.CreateDirectory(RootLoggingFolder);
                    }
                    IsLoggingToFileEnabled = true;
                }
            }

            public static bool? isLoggingToFileEnabled = false;//TODO: defaults to true only on debug builds
            public static bool IsLoggingToFileEnabled {
                get {
                    if (isLoggingToFileEnabled == null) {
                        //string strIsLoggingEnabled = DataManager.GetSettingValue ("IsLoggingEnabled");
                        //isLoggingEnabled = strIsLoggingEnabled == bool.TrueString;
                    }

                    return (bool)isLoggingToFileEnabled;
                }
                set {
                    isLoggingToFileEnabled = value;
                    string strIsLoggingEnabled = value ? bool.TrueString : bool.FalseString;
                    //DataManager.SaveSetting ("IsLoggingEnabled", strIsLoggingEnabled);
                }
            }

            public static void WriteLog(string messageFormat, LoggingLevel loggingLevel = LoggingLevel.Default, params object[] args) {
                //WriteLog(String.Format(message, args), loggingLevel);
#if DEBUG
                lock (lockFlag) {
#endif
                    {
                        string message = messageFormat;
                        bool isNotMessageFormatted = true;

                        try {
                            message = string.Format(messageFormat, args);
                            isNotMessageFormatted = false;
                        }
                        catch { }

                        WriteConsole(message);

                        if (IsLoggingToFileEnabled) {
                            if (message.Length < 20 * 1024) {
                                LogToFile(message);
                            }
                        }
                    }
#if DEBUG
                }
#endif
            }

            public static void WriteLog(string messageFormat, params object[] args) {
                //WriteLog(String.Format(message, args));
                WriteLog(messageFormat, LoggingLevel.Default, args);
            }

            //            public static void WriteLog(string message, LoggingLevel loggingLevel = LoggingLevel.Default)
            //            {
            //                if (loggingLevel >= CurrentLoggingLevel)
            //                {
            //                    lock (message)
            //                    {
            //#if DEBUG
            //                        //Console.WriteLine(message);
            //                        System.Diagnostics.Debug.WriteLine(message);
            //#endif
            //                        if (IsLoggingToFileEnabled && (message.Length < 20 * 1024))
            //                        {
            //                            LogToFile(message);
            //                        }
            //                    }
            //                }
            //            }

            public static void WriteLog(Exception exception) {
                string message = "************exception start***************" + Environment.NewLine + GetExceptionDescription(exception);
                while (exception.InnerException != null && message.Length < 100000) {
                    message += GetExceptionDescription(exception.InnerException);
                    exception = exception.InnerException;
                }
                message += Environment.NewLine + "************exception end***************" + Environment.NewLine;
                WriteLog(message, LoggingLevel.ExceptionsOnly);
            }

            private static void LogToFile(string message) {
                //return;
                try {
                    string logFileName = PathUtils.PathCombineCrossPlatform(RootLoggingFolder, "global.log");//"daily" + DateTime.Today.ToString("ddMMyy") + ".log";                    
#if !DEBUG
                    if (IsEncryptionOfLogEnabled) {
                        //message = EncryptorDecryptor.Encrypt (message);//TODO: uncommit on release;
                    }
#endif
                    if (File.Exists(logFileName)) {
                        FileInfo logFileInfo = new FileInfo(logFileName);
                        if (logFileInfo.Length > (200 * 1024)) {
                            string tmpFileContent = File.ReadAllText(logFileName, ASCIIEncoding.ASCII);
                            File.WriteAllText(logFileName, string.Empty);
                            File.WriteAllText(logFileName, tmpFileContent.Substring(100 * 1024), ASCIIEncoding.ASCII);
                        }
                    }

                    string formattedMessage = string.Format("{0:dd-MM-yy HH:mm:ss} {1}{2}", DateTime.Now, message, Environment.NewLine);
                    File.AppendAllText(logFileName, formattedMessage, ASCIIEncoding.ASCII);
                }
                catch (Exception ex) {
                    WriteConsole(GetExceptionDescription(ex));
                }
            }

            private static string GetExceptionDescription(Exception exception) {
                //DateTime now = DateTime.Now;

                StringBuilder sb = new StringBuilder();

                //sb.AppendLine("Date: " + now.ToString());
                //sb.AppendLine("Time: " + now.TimeOfDay.ToString());
                //sb.AppendLine("Inner message: ");
                //string innerMessage = string.Empty;
                //if (exception.InnerException != null)
                //{
                //    innerMessage = exception.InnerException.Message;
                //}
                //sb.AppendLine(innerMessage);
                //sb.AppendLine("**************************************************************************************");
                //sb.AppendLine("Inner stack trace: ");
                //string innerStackTrace = string.Empty;

                string stackEntrySeparator = Environment.NewLine + "\t at ";
                //if (exception.InnerException != null)
                //{
                //    try
                //    {
                //        innerStackTrace = exception.InnerException.StackTrace.Replace(" at ", stackEntrySeparator);
                //    }
                //    catch
                //    {
                //    }
                //}
                //sb.AppendLine(innerStackTrace);
                sb.AppendLine("**************************************************************************************");
                sb.AppendLine("Message: ");
                sb.AppendLine(exception.Message);
                sb.AppendLine("**************************************************************************************");
                sb.AppendLine("Stack trace: ");
                sb.AppendLine(exception.StackTrace.Replace(" at ", stackEntrySeparator));
                sb.AppendLine("**************************************************************************************");
                sb.AppendLine("Source: ");
                sb.AppendLine(exception.Source.Replace(" at ", stackEntrySeparator));
                sb.AppendLine("**************************************************************************************\n");

                return sb.ToString();
            }

            private static void WriteConsole(string message) {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(message);
#else
                Console.WriteLine(message);                        
#endif
            }
            #endregion
        }
    }

    namespace Paths
    {
        public static class PathUtils
        {
            public static string FixPathCrossPlatform(string path) {
                return string.IsNullOrWhiteSpace(path) ? string.Empty : path.Replace("\\", "/");
            }

            public static string PathCombineCrossPlatform(string pathComponent1, string pathComponent2, string pathComponent3 = null) {
                return FixPathCrossPlatform(string.IsNullOrEmpty(pathComponent3) ?
                    Path.Combine(pathComponent1, pathComponent2)
                    : Path.Combine(pathComponent1, pathComponent2, pathComponent3));
            }

            public static class UrlUtils
            {
#if iOS
                public static MonoTouch.Foundation.NSUrl GetNSUrlFromUri(Uri uri) {
                    try {
                        return new MonoTouch.Foundation.NSUrl(uri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped));
                    } catch {
                        return new MonoTouch.Foundation.NSUrl(uri.GetComponents(UriComponents.Path, UriFormat.UriEscaped));
                    }
                }
#endif
                public static string GetUrlFromPath(String pathToFile) {
                    return new Uri(pathToFile, UriKind.Absolute).AbsoluteUri;
                }

            }
        }
    }

    namespace Misc
    {

        public static class UiCallbackTimer
        {
            public static void DelayExecution(TimeSpan delay, Action action, Action<Action> invoker) {
                Timer timer = null;


                timer = new Timer(
                    (ignore) => {
                        timer.Dispose();
                        //threadContext.Post(ignore2 => action(), null);
                        invoker.Invoke(action);
                    }, null, delay, TimeSpan.FromMilliseconds(-1)
                );
            }

            public static void DelayExecution(double delay, Action action, Action<Action> invoker) {
                DelayExecution(TimeSpan.FromSeconds(delay), action, invoker);
            }
        }

        public static class ComparisonHelper
        {
            public static bool Equality(byte[] a1, byte[] b1) {
                // If not same length, done
                if (a1.Length != b1.Length) {
                    return false;
                }

                // If they are the same object, done
                if (object.ReferenceEquals(a1, b1)) {
                    return true;
                }

                // Loop all values and compare
                for (int i = 0; i < a1.Length; i++) {
                    if (a1[i] != b1[i]) {
                        return false;
                    }
                }

                // If we got here, equal
                return true;
            }
        }
    }
}
