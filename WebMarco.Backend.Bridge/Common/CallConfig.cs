using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebMarco.Utilities.Library;
using WebMarco.Utilities.Logging;
//using Newtonsoft.Json; //brakes crosplatform compatibility supposedly, at least on Mac


namespace WebMarco.Backend.Bridge.Common {
    /// <summary>
    /// 
    /// </summary>
    public class CallConfig {

        //[JsonProperty("id")] //brakes crosplatform compatibility supposedly, at least on Mac
        public Guid UID { get; private set; }
        public string MethodName { get; private set; }
        public object Params { get; private set; }
        public bool IsAsync { get; private set; }

        private CallConfig() {
        }

        //!!! : No more constructors allowed because of deserialization
        public CallConfig(Guid uid, string methodName, object parameters, bool isAsync = false) {
            UID = uid;
            MethodName = methodName;
            Params = parameters;
            IsAsync = isAsync;
        }

        public static CallConfig NewCallConfig(string methodName, object parameters, bool isAsync = false) {
            return new CallConfig(Guid.NewGuid(), methodName, parameters, isAsync);
        }

        public string ToJsonString() {
            //var tempContainer = new Dictionary<string, object>();
            //tempContainer.Add("id", UID.ToString());
            //tempContainer.Add("name", MethodName);
            //tempContainer.Add("arguments", Params);
            //tempContainer.Add("async", IsAsync);
            return SerializationHelper.Instance.JsonSerialize(this);
        }

        public static CallConfig FromJsonString(string jsonString) {
            try {
                CallConfig result = SerializationHelper.Instance.Deserialize<CallConfig>(jsonString);
                if (result.Params == null) {//Params supposedly are not empty
                    var tempObject = SerializationHelper.Instance.JsonDeserialize(jsonString);
                    try {
                        result.Params = ((Dictionary<string, object>)tempObject)["Params"];
                    } catch { }
                }

                return result;
            } catch (Exception ex) {
                DLogger.WriteLog(ex);
            }
            return null;
        }
    }
}
