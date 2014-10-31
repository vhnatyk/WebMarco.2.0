using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using WebMarco.Utilities.Logging;
using System.Net.Mime;

namespace WebMarco.Backend.Bridge.Common {
    public class MicroServer : IDisposable {
        private const int MinSafeInstancePortValue = 2000;
        private const int MaxSafeInstancePortValue = 60000;


        private bool isInstanceUidReintialized = false;
        public bool IsInstanceUidReintialized {
            get {
                return isInstanceUidReintialized;
            }
            private set {
                if (!isInstanceUidReintialized) {
                    isInstanceUidReintialized = value;
                }
            }
        }

        #region Singleton realization

        private static readonly Lazy<MicroServer> lazy =
            new Lazy<MicroServer>(() => new MicroServer());

        public static MicroServer Instance { get { return lazy.Value; } }

        private MicroServer() {

        }
        #endregion

        //TODO: from config? for now !!!DON'T FORGET to set same values in main.js file!!!
        private string serverInstanceUid = "898316fb-f8d6-41ea-8ccf-a58090745d9f";//TODO: from config file, some very random string
        private string serverInstancePort = "38701"; //some very random port number
        private string serverIP = "127.0.0.1";

        public string ServerIP {
            get {
                return serverIP;
            }
        }

        public string ServerInstancePort {
            get {
                return serverInstancePort;
            }
        }

        public string ServerInstanceUid {
            get {
                return serverInstanceUid;
            }
        }

        public Func<CallConfig, CallResult> OnHandleRequest { private get; set; }

        public void ReinitializeInstanceUid(string instanceUid) {
            //if (!IsInstanceUidReintialized) {
                Start(instanceUid);
                IsInstanceUidReintialized = true;
            //}
        }

        public void Start(string instanceUid = null, string instancePort = null, string instanceIP = null, Func<CallConfig, CallResult> requestHandleCallback = null) {
            if (string.IsNullOrWhiteSpace(instanceUid)) {
                instanceUid = ServerInstanceUid;//Guid.NewGuid().ToString("N");
                DLogger.WriteLog("Warning: MicroServer instance UID value was empty, setting default of {0}", ServerInstanceUid);
            }

            if (string.IsNullOrWhiteSpace(instanceIP)) {
                instanceIP = ServerIP;
                DLogger.WriteLog("Warning: MicroServer instance IP value was empty, setting default of {0}", ServerIP);
            }

            if (string.IsNullOrWhiteSpace(instancePort)) {
                instancePort = ServerInstancePort;
                DLogger.WriteLog("Warning: MicroServer instance port value was empty, setting default of {0}", ServerInstancePort);
            } else {
                int instancePortValueAsInteger;
                if (int.TryParse(instancePort, out instancePortValueAsInteger)) {
                    if (instancePortValueAsInteger > MinSafeInstancePortValue && instancePortValueAsInteger < MaxSafeInstancePortValue) {
                        instancePort = instancePortValueAsInteger.ToString();
                    } else {
                        DLogger.WriteLog("Warning: MicroServer instance port value was {1}, beyond safe range of {2} - {3}, setting default of {0}",
                            ServerInstancePort, instancePort, MinSafeInstancePortValue, MaxSafeInstancePortValue);
                        instancePort = ServerInstancePort;
                    }
                } else {
                    DLogger.WriteLog("Warning: MicroServer instance port value was {1}, non integer, beyond safe range of {2} - {3}, setting default of {0}",
                        ServerInstancePort, instancePort, MinSafeInstancePortValue, MaxSafeInstancePortValue);
                    instancePort = ServerInstancePort;
                }
            }

            serverInstanceUid = instanceUid;
            serverInstancePort = instancePort;
            serverIP = instanceIP;

            DLogger.WriteLog("Warning: MicroServer instance IP:port/UID/  set to {0}:{1}/{2}/", ServerIP, ServerInstancePort, ServerInstanceUid);

            if (requestHandleCallback != null) {
                OnHandleRequest = requestHandleCallback;
            }


            var listener = new HttpListener();
            //listener.Prefixes.Add(string.Format("http://*:{1}/", ServerInstanceUid, ServerInstancePort));
            //listener.Prefixes.Add(string.Format("http://{2}:{1}/{0}/", ServerInstanceUid, ServerInstancePort, ServerIP));
			listener.Prefixes.Add(string.Format("http://{2}:{1}/{0}/", ServerInstanceUid, ServerInstancePort, ServerIP));
            try
            {
                listener.Start();
            }
            catch(Exception ex) { 
#if DEBUG && WIN
                if (ex.Message.Equals("Access is denied")) {
                    const string listnerExceptionWithFix = "Access is denied. Please set serverInstancePort to some >10000 value(like for example 38701) and run the following command line in admin mode:\nnetsh http add urlacl http://+127.0.0.1:38701/ user=Everyone\n";
                    DLogger.WriteLog(listnerExceptionWithFix);
                    throw new HttpListenerException(500, listnerExceptionWithFix);
                } 
#endif
            }

            listener.BeginGetContext(new AsyncCallback(HandleRequest), listener);
        }


        private void SendServerErrorCodeToResponse(HttpListenerContext context) {            
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.OutputStream.Flush();
            context.Response.OutputStream.Close();
        }


        private void CloseResponse(HttpListenerContext context) {
            context.Response.StatusCode = (int)HttpStatusCode.Continue;
            context.Response.OutputStream.Flush();
            context.Response.OutputStream.Close();
        }

        private void HandleRequest(IAsyncResult result) {
            //Get the listener context
            HttpListenerContext context = null;
            var listener = (HttpListener)result.AsyncState;
            context = listener.EndGetContext(result);

            //Start listening for the next request
            listener.BeginGetContext(new AsyncCallback(HandleRequest), listener);

            if (context == null) {
                return;
            }

            //process query string and make CallConfig object
            CallConfig config = null;

            //missing on mobile platforms
            //HttpUtility.ParseQueryString(context.Request.Url.Query).Get("params");

            //if (context.Request.Url.ToString().Contains("callnative")) {
            //response = "native code reply";
            string url = context.Request.Url.ToString();
            string callConfigAsJson = Uri.UnescapeDataString(url.Split(new string[] { "callbackend?params=" }, StringSplitOptions.RemoveEmptyEntries)[1]);
            try {
                config = CallConfig.FromJsonString(callConfigAsJson);
            } catch (Exception ex) {
                DLogger.WriteLog(ex);
            }

            if (config == null) {
                SendServerErrorCodeToResponse(context);
                return;
            }

            CallResult handleRequestResult = null;
            try {
                handleRequestResult = OnHandleRequest.Invoke(config);
            } catch (Exception ex) {
                DLogger.WriteLog(ex);
                SendServerErrorCodeToResponse(context);
#if DEBUG
                throw ex;
#endif
            }
            context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            if (handleRequestResult != null)
            {
                context.Response.ContentType = handleRequestResult.ContentType;
                context.Response.ContentLength64 = handleRequestResult.Bytes.LongLength;
                context.Response.OutputStream.Write(handleRequestResult.Bytes, 0, handleRequestResult.Bytes.Length);
            } else {
                context.Response.ContentType = MediaTypeNames.Text.Plain;
                context.Response.ContentLength64 = 0;
            }
            context.Response.OutputStream.Flush();
            context.Response.OutputStream.Close();
            context.Response.Close();
        }

        public void Dispose() {
            //listener = null;
        }
    }
}
