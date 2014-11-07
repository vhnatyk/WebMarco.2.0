using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarco.Frontend.Common {
    public interface INativeWebView : INativeView {
        Uri ViewUrl { get; }

        void LoadMarkup(string markup);
        void LoadMarkup(Uri url);
        object CallFrontend(string script); 
    }
}
