using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarco.Frontend.Common.Helpers {
    /// <summary>
    /// This class should be used for methods and functions that process/return HTML markup
    /// </summary>
    class HtmlUiHelper {
        internal static string GetPdfEmbeddedHtml(string pdfAsBase64String) {
            string result = string.Format("<embed src=\"data:application/pdf;base64,{0}\" style=\"width: 100%; height: 100%; z-index: 100 !important; border: 0px none;\">", pdfAsBase64String);
            //WebMarcoHelper.WriteLog(string.Format("pdf as base 64 \n***************\n{0}\n**************", result));
            return result;
        }
    }
}
