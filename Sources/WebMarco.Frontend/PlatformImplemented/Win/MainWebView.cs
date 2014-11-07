using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMarco.Frontend.Common;

namespace WebMarco.Frontend.PlatformImplemented.Win {
    public class MainWebView : BaseWebView {
        private Action loadMainViewUrlOnceOnInitialization;

        public MainWebView(IBaseWindow window, BaseWebPage defaultPage)
            : base(window, defaultPage) {

        }

        private class MenuHandlerToDisableContextMenu : IMenuHandler {

            public bool OnBeforeContextMenu(IWebBrowser browser) {
                return false;
            }
        }

        /// <summary>
        /// Lets mark such methods with some special tag, like:
        /// 
        /// #ExtraInit MainWebView
        /// 
        /// so we would be able to locate them, if necessary, with text search.
        /// </summary>
        protected override void InitializeComponent() {//TODO: why override and where that initialization should really happen, here!?
            base.InitializeComponent();
            //some extra initialization, specific to current task only
            //this.AutomaticUpdater = new wyDay.Controls.AutomaticUpdater();
            //((System.ComponentModel.ISupportInitialize)(this.automaticUpdater)).BeginInit();
            //this.SuspendLayout();
            // 
            // webBrowser
            // 
            //AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            BrowserSettings = new CefSharp.BrowserSettings() {
                FileAccessFromFileUrlsAllowed = true,
                UniversalAccessFromFileUrlsAllowed = true,
                TextAreaResizeDisabled = true
            };

            BackColor = System.Drawing.SystemColors.Window;
            //BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            CausesValidation = false;
            //Cursor = System.Windows.Forms.Cursors.Hand;
            Dock = System.Windows.Forms.DockStyle.Fill;
            Location = new System.Drawing.Point(0, 0);
            //Size = new System.Drawing.Size(300, 400);

            Name = "webBrowser";
            RightToLeft = System.Windows.Forms.RightToLeft.No;
            TabIndex = 0;
            //Address = ViewUrl.ToString();

            //this.ResumeLayout(false);
            //this.PerformLayout();

            ContextMenu = null;
            MenuHandler = new MenuHandlerToDisableContextMenu();
            loadMainViewUrlOnceOnInitialization = new Action(this.Load);            
        }

        protected override void OnHandleCreated(EventArgs e) {
            //Address = ViewUrl.ToString(); //a hack to load markup
            base.OnHandleCreated(e);
            
            //TODO: initial Load() here ?? safe?..
            loadMainViewUrlOnceOnInitialization.Invoke();
            loadMainViewUrlOnceOnInitialization = () => { };
        }
    }
}
