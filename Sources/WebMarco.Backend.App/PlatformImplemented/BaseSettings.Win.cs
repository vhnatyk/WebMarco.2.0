using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebMarco.Backend.App.PlatformImplemented.Win {
    public abstract class BaseSettings : WebMarco.Backend.App.Common.BaseSettings {
        public override void Save() {
            //TODO: do nothing for now
        }

        protected override void Load() {
            base.Load();
            //TODO: do nothing for now
        }
    }
}
