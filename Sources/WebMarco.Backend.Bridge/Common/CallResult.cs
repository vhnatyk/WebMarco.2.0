using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mime;

namespace WebMarco.Backend.Bridge.Common {
    public class CallResult {
        public string ContentType { get; set; }

        public byte[] Bytes {
            get {
                return Encoding.UTF8.GetBytes(value);
            }
        }

        private string value;
        public string Value { get { return value; } }

        public CallResult(string value, string contentType = null) {
            if (contentType == null) {
                ContentType = MediaTypeNames.Text.Plain;
            } else {
                ContentType = contentType;
            }
            
            this.value = value;
        }
    }
}
