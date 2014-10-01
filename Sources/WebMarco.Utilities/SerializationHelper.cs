using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace WebMarco.Utilities.Library {

    /// <summary>
    /// Lets keep all JSON stuff here, so we will deal with 
    /// portability issues of Newtonsoft.Json only here
    /// </summary>
    public class SerializationHelper {
        #region Singleton realization

        private static readonly Lazy<SerializationHelper> lazy =
            new Lazy<SerializationHelper>(() => new SerializationHelper());

        public static SerializationHelper Instance { get { return lazy.Value; } }

        private SerializationHelper() {

        }

        #endregion


        public virtual string JsonSerialize(object obj) {
            return JsonConvert.SerializeObject(obj);
        }

        public virtual object JsonDeserializeToObject(string objAsJsonString) {
            return JsonConvert.DeserializeObject(objAsJsonString);
        }

        public virtual Array JsonDeserializeToArray(string objAsJsonString) {
            JContainer tempObject = (JContainer)JsonConvert.DeserializeObject(objAsJsonString);
            var result = tempObject.Select(p => ((JValue)p).Value).ToArray();
            return result;
        }

        public virtual Dictionary<string, object> JsonDeserializeToDictionary(string objAsJsonString) {
            var result = new Dictionary<string, object>();
            JObject tempObject = (JObject)JsonConvert.DeserializeObject(objAsJsonString);
            IList<string> keys = tempObject.Properties().Select(p => p.Name).ToList();
            foreach (var key in keys) {
                try {
                    result.Add(key, ((JValue)tempObject[key]).Value);
                } catch {
                    result.Add(key, JsonDeserialize(tempObject[key].ToString()));
                }
            }
            return result;
        }

        public virtual object JsonDeserialize(string objAsJsonString) {
            try {
                return JsonDeserializeToDictionary(objAsJsonString);
            } catch {
                try {
                    return JsonDeserializeToArray(objAsJsonString);
                } catch {
                    return JsonDeserializeToObject(objAsJsonString);
                }
            }
        }

        public virtual string Serialize(object obj) {
            throw new NotImplementedException();
        }
        public virtual object Deserialize(string objAsString) {
            throw new NotImplementedException();
        }

        public virtual T Deserialize<T>(string objAsString) {
            return JsonConvert.DeserializeObject<T>(objAsString);
        }
    }
}