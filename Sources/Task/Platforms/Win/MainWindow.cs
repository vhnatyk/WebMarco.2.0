using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BridgeTry.Views;
using WebMarco.Backend.App.PlatformImplemented.Win;
using TinyIoC;
using WebMarco.Backend.App.Common;
using System.Windows.Forms;
using WebMarco.Frontend.Common;
using CefSharp.WinForms;
using CefSharp;
using WebMarco.Frontend.PlatformImplemented.Win;

namespace BridgeTry {
    class MainWindow : WebMarco.Frontend.PlatformImplemented.Win.MainWindow {

        private ChromiumWebBrowser browser;
        public MainWindow()
            : base() {
            TinyIoCContainer.Current.Register<WebMarco.Backend.App.Common.BaseAppDelegate, AppDelegate>(AppDelegate.Instance);
            TinyIoCContainer.Current.Register<AppHelper.Data.Manager, Manager>(new Manager());
            AppHelper.Data.ConnectDatabase();
            /// Program entry point is here
            /// 

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 490);
            InitializeComponent();

            MainView = new MainView(this);
            var mainWebView = (MainWebView)MainView;
            //browser = new ChromiumWebBrowser("http://html5test.com/");
            mainWebView.Dock = DockStyle.Fill;
            //browser.BackColor = System.Drawing.Color.Blue;            
            //mainWebView.DownloadHandler = new DownloadHandler();
            //browser.MenuHandler = new MenuHandler();
            mainWebView.NavStateChanged += OnBrowserNavStateChanged;
            mainWebView.ConsoleMessage += OnBrowserConsoleMessage;
            mainWebView.StatusMessage += OnBrowserStatusMessage;
            mainWebView.TitleChanged += OnBrowserTitleChanged;
            mainWebView.AddressChanged += OnBrowserAddressChanged;
            Controls.Add(mainWebView);

            //MainView.Load(); //TODO: here??! is it safe?..no:(
        }

        #region Browser event stubs
        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args) {
            //DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args) {
            //this.Invoke(() => statusLabel.Text = args.Value);
        }

        private void OnBrowserNavStateChanged(object sender, NavStateChangedEventArgs args) {
            //this.Invoke(() => SetIsLoading(!args.CanReload));
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args) {
            //this.Invoke(() => Text = args.Title);
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args) {
            //this.Invoke(new Action(() => urlTextBox.Text = args.Address));
        }
        #endregion


        public void ExecuteScript(string script) {
            browser.ExecuteScriptAsync(script);
        }

        public object EvaluateScript(string script) {
            return browser.EvaluateScript(script);
        }

        private void ExitMenuItemClick(object sender, EventArgs e) {
            browser.Dispose();
            Cef.Shutdown();
            Close();
        }

        private void LoadUrl(string url) {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute)) {
                browser.Load(url);
            }
        }

        protected virtual void InitializeComponent() {
            ///
            /// May be necessary if we'll decide to implement autoupdates later on
            ///
            //this.AutomaticUpdater = new wyDay.Controls.AutomaticUpdater();
            //((System.ComponentModel.ISupportInitialize)(this.automaticUpdater)).BeginInit();
                        
            //this.SuspendLayout();           
            
            /*
            // 
            // automaticUpdater
            //             
            //this.automaticUpdater.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            //this.automaticUpdater.ContainerForm = this;
            //this.automaticUpdater.GUID = "bfd6d166-650b-4e53-8504-a5fc5c1367e0";
            //this.automaticUpdater.Location = new System.Drawing.Point(220, 87);
            //this.automaticUpdater.Name = "automaticUpdater";
            //this.automaticUpdater.Size = new System.Drawing.Size(16, 16);
            //this.automaticUpdater.TabIndex = 0;
            //this.automaticUpdater.wyUpdateCommandline = null;
            //this.automaticUpdater.ForeColor = Color.Black;
            //this.automaticUpdater.UpdateType = wyDay.Controls.UpdateType.DoNothing;
            //this.automaticUpdater.ReadyToBeInstalled += new EventHandler(automaticUpdater_ReadyToBeInstalled);
            //
             */
             
            ///
            /// MainForm
            /// 
            ///This code has all the symptoms of being a part of a framework - 
            ///thus needs to be moved to base class?
            this.Icon = ResourceFile1.Icon2;
            this.Size = new System.Drawing.Size(800, 600);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            //this.BackColor = System.Drawing.SystemColors.Window;
            //this.Controls.Add(this.automaticUpdater);
            //this.ForeColor = System.Drawing.SystemColors.Window;
            this.Name = "MainForm";
            //this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BridgeTry";

            ///autoupdater
            //((System.ComponentModel.ISupportInitialize)(this.automaticUpdater)).EndInit();

            //this.ResumeLayout(false);
            //this.PerformLayout();
        }

    }
}