using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarco.Backend.App.Common {

    [Serializable]
    public abstract class BaseSettings {

        private Dictionary<string, string> settingsStorage;
        protected Dictionary<string, string> SettingsStorage {
            get {
                return settingsStorage;
            }
            private set {
                settingsStorage = value;
            }
        }

        public BaseSettings() {
            Load();
        }

        //public BaseSettings(Dictionary<string, string> initValues = null) {
        //    if(initValues != null) {
        //        settingsStorage = initValues;
        //    } else {
        //        Load();
        //    }
        //}

        protected virtual string GetSetting(string key) {
            string result = null;
            settingsStorage.TryGetValue(key, out result);
            return result;
        }

        protected virtual string GetSetting(string key, string defaultValue = null) {
            string result = GetSetting(key);
            return result ?? defaultValue;
        }

        protected virtual void SetSetting(string key, string value) {
            try {
                settingsStorage[key] = value;
            } catch {
                settingsStorage.Add(key, value);
            }
        }

        /// <summary>
        /// Abstract method that must be implemented on each platform.
        /// Saves options to system storage.
        /// </summary>
        public abstract void Save();

        /// <summary>
        /// Abstract method that must be implemented on each platform.
        /// Loads options from system storage. 
        /// Result must inherit from this class.
        /// </summary>
        protected virtual void Load() {
            ///this is stub, it has to be implemented on each platform
            settingsStorage = new Dictionary<string, string>();
        }
    }
}
