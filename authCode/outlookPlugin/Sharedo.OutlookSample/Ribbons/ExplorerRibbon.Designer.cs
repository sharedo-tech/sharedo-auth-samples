namespace Sharedo.OutlookSample.Ribbons
{
    partial class ExplorerRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ExplorerRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tab1 = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this._btnConfiguration = this.Factory.CreateRibbonButton();
            this._separator1 = this.Factory.CreateRibbonSeparator();
            this._linkedButtons = this.Factory.CreateRibbonButtonGroup();
            this._btnDebug = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.group1.SuspendLayout();
            this._linkedButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.ControlId.OfficeId = "TabMail";
            this.tab1.Groups.Add(this.group1);
            this.tab1.Label = "TabMail";
            this.tab1.Name = "tab1";
            // 
            // group1
            // 
            this.group1.Items.Add(this._btnConfiguration);
            this.group1.Items.Add(this._separator1);
            this.group1.Items.Add(this._linkedButtons);
            this.group1.Label = "Sharedo";
            this.group1.Name = "group1";
            this.group1.Position = this.Factory.RibbonPosition.BeforeOfficeId("GroupMailNew");
            // 
            // _btnConfiguration
            // 
            this._btnConfiguration.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this._btnConfiguration.Image = global::Sharedo.OutlookSample.Properties.Resources.dobutton;
            this._btnConfiguration.Label = "Configuration";
            this._btnConfiguration.Name = "_btnConfiguration";
            this._btnConfiguration.ShowImage = true;
            this._btnConfiguration.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this._btnConfiguration_Click);
            // 
            // _separator1
            // 
            this._separator1.Name = "_separator1";
            // 
            // _linkedButtons
            // 
            this._linkedButtons.Enabled = false;
            this._linkedButtons.Items.Add(this._btnDebug);
            this._linkedButtons.Name = "_linkedButtons";
            // 
            // _btnDebug
            // 
            this._btnDebug.Image = global::Sharedo.OutlookSample.Properties.Resources.bug;
            this._btnDebug.Label = "Show debug info";
            this._btnDebug.Name = "_btnDebug";
            this._btnDebug.ShowImage = true;
            this._btnDebug.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this._btnDebug_Click);
            // 
            // ExplorerRibbon
            // 
            this.Name = "ExplorerRibbon";
            this.RibbonType = "Microsoft.Outlook.Explorer";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ExplorerRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this._linkedButtons.ResumeLayout(false);
            this._linkedButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton _btnConfiguration;
        internal Microsoft.Office.Tools.Ribbon.RibbonSeparator _separator1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButtonGroup _linkedButtons;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton _btnDebug;
    }
}
