using System;
using ZoneEdit;
using System.IO;
using System.Xml;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;

namespace ZoneEditCPL
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{

		#region Constants
		private const string CONFIG_FILE = "ZoneEditSvc.config";
		private const string LOG_FILE = "ZoneEditSvc.log";
		#endregion

		#region Fields
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageOptions;
		private System.Windows.Forms.TabPage tabPageLog;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox ckLog;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtZones;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtUpdateURL;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtCheckURL;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.Button cmdCancel;
		private System.Windows.Forms.Button cmdSave;
		private System.Windows.Forms.TextBox txtIPInterval;
		private System.Windows.Forms.Button cmdConfigure;
		private System.ServiceProcess.ServiceController ZoneEditServiceController;
		private System.Windows.Forms.TabPage tabPageService;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.StatusBarPanel SvcStatus;
		private System.Windows.Forms.Button cmdServiceController;
		private System.Windows.Forms.Timer ServiceTimerChecker;
		private System.Windows.Forms.Button cmdInstallSvc;
		private System.Windows.Forms.TextBox txtConsoleOut;
		private System.Windows.Forms.StatusBarPanel statusBarPanelNextCheck;
        private System.ComponentModel.IContainer components;	
		#endregion

		#region Private Variables
        private ZoneEdit.Config m_Config = null;
        private string m_AppPath = string.Empty;
		private System.Windows.Forms.TabPage tabPageManualUpdate;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button cmdUpdateManual;
		private System.Windows.Forms.TextBox txtManualIP;
        private System.Windows.Forms.Panel panel1;
        private ListView lvLog;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private Label lblLogMessage;
        private Button btnReloadEvents;
        private List<CustomEvents> _LogEvents;
		#endregion

		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			ShowStatus();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.statusBar1 = new System.Windows.Forms.StatusBar();
            this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this.statusBarPanelNextCheck = new System.Windows.Forms.StatusBarPanel();
            this.SvcStatus = new System.Windows.Forms.StatusBarPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.cmdConfigure = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckLog = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIPInterval = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtZones = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUpdateURL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCheckURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.lvLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReloadEvents = new System.Windows.Forms.Button();
            this.lblLogMessage = new System.Windows.Forms.Label();
            this.tabPageService = new System.Windows.Forms.TabPage();
            this.txtConsoleOut = new System.Windows.Forms.TextBox();
            this.cmdInstallSvc = new System.Windows.Forms.Button();
            this.cmdServiceController = new System.Windows.Forms.Button();
            this.tabPageManualUpdate = new System.Windows.Forms.TabPage();
            this.cmdUpdateManual = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtManualIP = new System.Windows.Forms.TextBox();
            this.ZoneEditServiceController = new System.ServiceProcess.ServiceController();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.ServiceTimerChecker = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelNextCheck)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SvcStatus)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPageOptions.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPageService.SuspendLayout();
            this.tabPageManualUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this.statusBar1.Location = new System.Drawing.Point(0, 319);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel1,
            this.statusBarPanelNextCheck,
            this.SvcStatus});
            this.statusBar1.ShowPanels = true;
            this.statusBar1.Size = new System.Drawing.Size(773, 30);
            this.statusBar1.TabIndex = 17;
            this.statusBar1.Text = "statusBar1";
            // 
            // statusBarPanel1
            // 
            this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel1.Name = "statusBarPanel1";
            this.statusBarPanel1.Text = "Ready...";
            this.statusBarPanel1.Width = 528;
            // 
            // statusBarPanelNextCheck
            // 
            this.statusBarPanelNextCheck.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
            this.statusBarPanelNextCheck.Name = "statusBarPanelNextCheck";
            this.statusBarPanelNextCheck.Width = 200;
            // 
            // SvcStatus
            // 
            this.SvcStatus.Icon = ((System.Drawing.Icon)(resources.GetObject("SvcStatus.Icon")));
            this.SvcStatus.MinWidth = 20;
            this.SvcStatus.Name = "SvcStatus";
            this.SvcStatus.ToolTipText = "Service Status";
            this.SvcStatus.Width = 20;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageOptions);
            this.tabControl1.Controls.Add(this.tabPageLog);
            this.tabControl1.Controls.Add(this.tabPageService);
            this.tabControl1.Controls.Add(this.tabPageManualUpdate);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(773, 319);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 20;
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.Controls.Add(this.cmdConfigure);
            this.tabPageOptions.Controls.Add(this.cmdCancel);
            this.tabPageOptions.Controls.Add(this.cmdSave);
            this.tabPageOptions.Controls.Add(this.groupBox1);
            this.tabPageOptions.Location = new System.Drawing.Point(4, 29);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Size = new System.Drawing.Size(765, 286);
            this.tabPageOptions.TabIndex = 0;
            this.tabPageOptions.Text = "Options";
            // 
            // cmdConfigure
            // 
            this.cmdConfigure.Location = new System.Drawing.Point(472, 294);
            this.cmdConfigure.Name = "cmdConfigure";
            this.cmdConfigure.Size = new System.Drawing.Size(136, 34);
            this.cmdConfigure.TabIndex = 22;
            this.cmdConfigure.Text = "Update";
            this.cmdConfigure.Click += new System.EventHandler(this.cmdConfigure_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(275, 294);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(136, 34);
            this.cmdCancel.TabIndex = 21;
            this.cmdCancel.Text = "Close";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(77, 294);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(136, 34);
            this.cmdSave.TabIndex = 20;
            this.cmdSave.Text = "Save";
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckLog);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtIPInterval);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtZones);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtUpdateURL);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCheckURL);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Location = new System.Drawing.Point(5, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(750, 281);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zone Edit Info";
            // 
            // ckLog
            // 
            this.ckLog.Checked = true;
            this.ckLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckLog.Location = new System.Drawing.Point(555, 213);
            this.ckLog.Name = "ckLog";
            this.ckLog.Size = new System.Drawing.Size(181, 30);
            this.ckLog.TabIndex = 46;
            this.ckLog.Text = "Log Events";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(13, 213);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 30);
            this.label6.TabIndex = 43;
            this.label6.Text = "Check IP Interval:";
            // 
            // txtIPInterval
            // 
            this.txtIPInterval.Location = new System.Drawing.Point(213, 213);
            this.txtIPInterval.Name = "txtIPInterval";
            this.txtIPInterval.Size = new System.Drawing.Size(56, 26);
            this.txtIPInterval.TabIndex = 42;
            this.txtIPInterval.Text = "5";
            this.txtIPInterval.Enter += new System.EventHandler(this.TextBox_ShowHelp);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(13, 177);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 29);
            this.label5.TabIndex = 41;
            this.label5.Text = "Zones";
            // 
            // txtZones
            // 
            this.txtZones.Location = new System.Drawing.Point(213, 177);
            this.txtZones.Name = "txtZones";
            this.txtZones.Size = new System.Drawing.Size(523, 26);
            this.txtZones.TabIndex = 40;
            this.txtZones.Enter += new System.EventHandler(this.TextBox_ShowHelp);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(13, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(147, 29);
            this.label4.TabIndex = 39;
            this.label4.Text = "Update URL:";
            // 
            // txtUpdateURL
            // 
            this.txtUpdateURL.Location = new System.Drawing.Point(213, 142);
            this.txtUpdateURL.Name = "txtUpdateURL";
            this.txtUpdateURL.Size = new System.Drawing.Size(523, 26);
            this.txtUpdateURL.TabIndex = 38;
            this.txtUpdateURL.Text = "https://dynamic.zoneedit.com/auth/dynamic.html";
            this.txtUpdateURL.Enter += new System.EventHandler(this.TextBox_ShowHelp);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 30);
            this.label3.TabIndex = 37;
            this.label3.Text = "Check IP URL:";
            // 
            // txtCheckURL
            // 
            this.txtCheckURL.Location = new System.Drawing.Point(213, 106);
            this.txtCheckURL.Name = "txtCheckURL";
            this.txtCheckURL.Size = new System.Drawing.Size(523, 26);
            this.txtCheckURL.TabIndex = 36;
            this.txtCheckURL.Text = "http://www.netapps.net/ip/default.aspx";
            this.txtCheckURL.Enter += new System.EventHandler(this.TextBox_ShowHelp);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 30);
            this.label2.TabIndex = 35;
            this.label2.Text = "Password:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(213, 68);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(523, 26);
            this.txtPassword.TabIndex = 34;
            this.txtPassword.Enter += new System.EventHandler(this.TextBox_ShowHelp);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 27);
            this.label1.TabIndex = 33;
            this.label1.Text = "User Name:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(213, 34);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(523, 26);
            this.txtUserName.TabIndex = 32;
            this.txtUserName.Enter += new System.EventHandler(this.TextBox_ShowHelp);
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.lvLog);
            this.tabPageLog.Controls.Add(this.panel1);
            this.tabPageLog.Location = new System.Drawing.Point(4, 29);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size(781, 379);
            this.tabPageLog.TabIndex = 1;
            this.tabPageLog.Text = "Log";
            // 
            // lvLog
            // 
            this.lvLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLog.FullRowSelect = true;
            this.lvLog.Location = new System.Drawing.Point(0, 0);
            this.lvLog.Name = "lvLog";
            this.lvLog.Size = new System.Drawing.Size(781, 316);
            this.lvLog.TabIndex = 2;
            this.lvLog.UseCompatibleStateImageBehavior = false;
            this.lvLog.View = System.Windows.Forms.View.Details;
            this.lvLog.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvLog_ColumnClick);
            this.lvLog.SelectedIndexChanged += new System.EventHandler(this.lvLog_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date";
            this.columnHeader1.Width = 90;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Level";
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Message";
            this.columnHeader3.Width = 390;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReloadEvents);
            this.panel1.Controls.Add(this.lblLogMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 316);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(781, 63);
            this.panel1.TabIndex = 1;
            // 
            // btnReloadEvents
            // 
            this.btnReloadEvents.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReloadEvents.Location = new System.Drawing.Point(681, 10);
            this.btnReloadEvents.Name = "btnReloadEvents";
            this.btnReloadEvents.Size = new System.Drawing.Size(100, 43);
            this.btnReloadEvents.TabIndex = 1;
            this.btnReloadEvents.Text = "Reload";
            this.btnReloadEvents.UseVisualStyleBackColor = true;
            this.btnReloadEvents.Click += new System.EventHandler(this.btnReloadEvents_Click);
            // 
            // lblLogMessage
            // 
            this.lblLogMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLogMessage.Location = new System.Drawing.Point(0, 0);
            this.lblLogMessage.Name = "lblLogMessage";
            this.lblLogMessage.Size = new System.Drawing.Size(781, 63);
            this.lblLogMessage.TabIndex = 0;
            // 
            // tabPageService
            // 
            this.tabPageService.Controls.Add(this.txtConsoleOut);
            this.tabPageService.Controls.Add(this.cmdInstallSvc);
            this.tabPageService.Controls.Add(this.cmdServiceController);
            this.tabPageService.Location = new System.Drawing.Point(4, 29);
            this.tabPageService.Name = "tabPageService";
            this.tabPageService.Size = new System.Drawing.Size(781, 379);
            this.tabPageService.TabIndex = 2;
            this.tabPageService.Text = "Service";
            // 
            // txtConsoleOut
            // 
            this.txtConsoleOut.BackColor = System.Drawing.Color.Black;
            this.txtConsoleOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsoleOut.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsoleOut.ForeColor = System.Drawing.Color.White;
            this.txtConsoleOut.Location = new System.Drawing.Point(0, 0);
            this.txtConsoleOut.Multiline = true;
            this.txtConsoleOut.Name = "txtConsoleOut";
            this.txtConsoleOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsoleOut.Size = new System.Drawing.Size(781, 379);
            this.txtConsoleOut.TabIndex = 2;
            this.txtConsoleOut.Visible = false;
            // 
            // cmdInstallSvc
            // 
            this.cmdInstallSvc.Location = new System.Drawing.Point(24, 18);
            this.cmdInstallSvc.Name = "cmdInstallSvc";
            this.cmdInstallSvc.Size = new System.Drawing.Size(285, 29);
            this.cmdInstallSvc.TabIndex = 1;
            this.cmdInstallSvc.Text = "Install Service";
            this.cmdInstallSvc.Click += new System.EventHandler(this.cmdInstallSvc_Click);
            // 
            // cmdServiceController
            // 
            this.cmdServiceController.Location = new System.Drawing.Point(24, 63);
            this.cmdServiceController.Name = "cmdServiceController";
            this.cmdServiceController.Size = new System.Drawing.Size(285, 29);
            this.cmdServiceController.TabIndex = 0;
            this.cmdServiceController.Click += new System.EventHandler(this.cmdServiceController_Click);
            // 
            // tabPageManualUpdate
            // 
            this.tabPageManualUpdate.Controls.Add(this.cmdUpdateManual);
            this.tabPageManualUpdate.Controls.Add(this.label9);
            this.tabPageManualUpdate.Controls.Add(this.txtManualIP);
            this.tabPageManualUpdate.Location = new System.Drawing.Point(4, 29);
            this.tabPageManualUpdate.Name = "tabPageManualUpdate";
            this.tabPageManualUpdate.Size = new System.Drawing.Size(781, 379);
            this.tabPageManualUpdate.TabIndex = 3;
            this.tabPageManualUpdate.Text = "Manual";
            this.tabPageManualUpdate.ToolTipText = "Update to Specified IP Address";
            // 
            // cmdUpdateManual
            // 
            this.cmdUpdateManual.Location = new System.Drawing.Point(419, 20);
            this.cmdUpdateManual.Name = "cmdUpdateManual";
            this.cmdUpdateManual.Size = new System.Drawing.Size(154, 29);
            this.cmdUpdateManual.TabIndex = 40;
            this.cmdUpdateManual.Text = "Update";
            this.cmdUpdateManual.Click += new System.EventHandler(this.cmdUpdateManual_Click);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(13, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 29);
            this.label9.TabIndex = 39;
            this.label9.Text = "IP Address:";
            // 
            // txtManualIP
            // 
            this.txtManualIP.Location = new System.Drawing.Point(165, 18);
            this.txtManualIP.Name = "txtManualIP";
            this.txtManualIP.Size = new System.Drawing.Size(222, 26);
            this.txtManualIP.TabIndex = 38;
            // 
            // ZoneEditServiceController
            // 
            this.ZoneEditServiceController.ServiceName = "ZoneEditSvc";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            // 
            // ServiceTimerChecker
            // 
            this.ServiceTimerChecker.Enabled = true;
            this.ServiceTimerChecker.Interval = 5000;
            this.ServiceTimerChecker.Tick += new System.EventHandler(this.ServiceTimerChecker_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(773, 349);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zone Edit DNS Updater";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanelNextCheck)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SvcStatus)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPageOptions.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageLog.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPageService.ResumeLayout(false);
            this.tabPageService.PerformLayout();
            this.tabPageManualUpdate.ResumeLayout(false);
            this.tabPageManualUpdate.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}


		private void TextBox_ShowHelp(object sender, System.EventArgs e)
		{
			TextBox tb = (TextBox) sender;
			switch (tb.Name)
			{
				case "txtUserName":
					ShowHelp("Enter the username for ZoneEdit account");
					break;
				case "txtPassword":
					ShowHelp("Enter the password for ZoneEdit account");
					break;
				case "txtCheckURL":
					ShowHelp("URL from ZoneEdit to check external IP address");
					break;
				case "txtUpdateURL":
					ShowHelp("URL from ZoneEdit to update IP addresses");
					break;
				case "txtZones":
					ShowHelp("Enter a comma separated list of zones to update.  i.e. *.foo.com,*.bar.com");
					break;
				case "txtIPInterval":
					ShowHelp("Enter the interval in minutes to check for IP Address changes");
					break;
			}
		}
		private void ShowHelp(string message)
		{
			this.statusBarPanel1.Text = message;
		}

		private void UpdateData()
		{
			
			//update the class
			this.m_Config.DDNS_IPPage = this.txtCheckURL.Text;
			this.m_Config.DDNS_Password = this.txtPassword.Text;
			this.m_Config.DDNS_URL = string.Concat(this.txtUpdateURL.Text,"?host=",txtZones.Text);
			this.m_Config.DDNS_UserName = this.txtUserName.Text;
			this.m_Config.Debug = this.ckLog.Checked.ToString();
			this.m_Config.IP_Check_Interval = (Convert.ToInt32(this.txtIPInterval.Text)).ToString();
            m_Config.LastIPAddress = txtManualIP.Text;
			this.m_Config.Save();

		
		}

		private void LoadFormFromConfig()
		{
			this.txtCheckURL.Text = m_Config.DDNS_IPPage;
			this.txtPassword.Text = m_Config.DDNS_Password;
            if (m_Config.DDNS_URL.IndexOf("?host") > -1)
                this.txtUpdateURL.Text = m_Config.DDNS_URL.Substring(0, m_Config.DDNS_URL.IndexOf("?host"));
            else
                this.txtUpdateURL.Text = m_Config.DDNS_URL;
			this.txtZones.Text = m_Config.DDNS_URL.Substring(m_Config.DDNS_URL.IndexOf("?host")+6);
			this.txtUserName.Text = m_Config.DDNS_UserName;
			this.ckLog.Checked = bool.Parse(m_Config.Debug);
			this.txtIPInterval.Text = (int.Parse(m_Config.IP_Check_Interval)).ToString();

		}

        string _currentIP = string.Empty;

		private void frmMain_Load(object sender, System.EventArgs e)
		{
            //make sure this is registered
            m_AppPath = new FileInfo(Application.ExecutablePath).DirectoryName;
            try
            {
                m_Config = new Config();
                ZoneLib.WriteToLog("Starting Zone Edit Configuration", EventLogEntryType.Information);
                
               // ZoneEdit.ZoneLib.RegisterCPL(m_AppPath);

                ShowHelp("Loading Config...");

                LoadLogFile();
                m_Config = new Config();
                LoadFormFromConfig();
                ShowHelp("Ready...");
                txtManualIP.Text = ZoneEdit.ZoneLib.GetIP(m_Config);
                statusBar1.Panels[1].Text = txtManualIP.Text;
                this.txtUserName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

		}

		private void ZoneEditLogWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
		{
			LoadLogFile();
		}

		private void ZoneEditLogWatcher_Changed(object sender, System.IO.FileSystemEventArgs e)
		{
			LoadLogFile();		
		}

		private void LoadLogFile()
		{
            lvLog.Items.Clear();
            _LogEvents = ZoneEdit.ZoneLib.LoadEvents();
            LoadLogList();

		}

        private void LoadLogList()
        {
            for (int i = 0; i < _LogEvents.Count; i++)
            {
                ListViewItem item = new ListViewItem(_LogEvents[i].EntryTime.ToString());
                item.SubItems.Add(_LogEvents[i].LogType.ToString());
                item.SubItems.Add(_LogEvents[i].Message);
                lvLog.Items.Add(item);
            }

        }


		private void cmdSave_Click(object sender, System.EventArgs e)
		{
			ShowHelp("Saving Config...");
			UpdateData();
			ShowHelp("Ready...");
		}
		
		private void cmdConfigure_Click(object sender, System.EventArgs e)
		{
			this.UpdateData();
			ZoneEdit.ZoneLib.ConfigureDNS(m_Config,true);
		}

		private void ServiceTimerChecker_Tick(object sender, System.EventArgs e)
		{
			try
			{
               
				this.ServiceTimerChecker.Enabled = false; // when it is down - threads will backup
				ShowStatus();
               
			}

			catch(Exception ex)
			{
                ZoneLib.WriteToLog(ex.Message.ToString(), EventLogEntryType.Error);
			}
			finally
			{
				this.ServiceTimerChecker.Enabled = true; // when it is down - threads will backup
			}
		}
		private void ShowStatus()
		{
			try
			{
				if (this.ZoneEditServiceController.Status == System.ServiceProcess.ServiceControllerStatus.Running)
				{
					this.SvcStatus.Icon = ResourceMgr.Icons["IconRun"];
					this.cmdServiceController.Text = "Stop Service";
				}
				else
				{
					this.SvcStatus.Icon = ResourceMgr.Icons["IconStop"];
					this.cmdServiceController.Text = "Start Service";
				}
				this.cmdInstallSvc.Text = "Uninstall Service";
			}
			catch (System.InvalidOperationException)
			{
				//service not installed
				this.SvcStatus.Icon = ResourceMgr.Icons["IconStop"];
				this.cmdInstallSvc.Visible = true;
				this.cmdServiceController.Visible = false;
			}
			catch (Exception exNoSvc)
			{
				Console.WriteLine(exNoSvc.Message.ToString());
			}
		}

		private void cmdServiceController_Click(object sender, System.EventArgs e)
		{
			Button btn = sender as Button;
			switch (btn.Text)
			{
				case "Start Service":
					try
					{
						this.ZoneEditServiceController.Start();
						this.ZoneEditServiceController.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running,new TimeSpan(0,0,0,5));
					}
					catch (Exception)
					{
						MessageBox.Show("Error starting service");
					}
					finally
					{
						this.ShowStatus();
					}

					break;
				case "Stop Service":
					try
					{
						this.ZoneEditServiceController.Stop();
						this.ZoneEditServiceController.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped,new TimeSpan(0,0,0,5));
					}
					catch (Exception)
					{
						MessageBox.Show("Error starting service");
					}
					finally
					{
						this.ShowStatus();
					}
					break;
			}
		}

		private void cmdInstallSvc_Click(object sender, System.EventArgs e)
		{
			Button btn = sender as Button;
			switch (btn.Text)
			{
				case "Install Service":
					this.RunInstaller(true);
					this.cmdServiceController.Visible = true;
                    cmdInstallSvc.Text = "Uninstall Service";
					break;
				case "Uninstall Service":
					this.RunInstaller(false);
					this.cmdServiceController.Visible = false;
                    cmdInstallSvc.Text = "Install Service";
					break;
			}
		}

		private void RunInstaller(bool Install)
		{

            try
            {
                string args = string.Concat("\"", Path.Combine(m_AppPath, "ZoneEditSvc.exe"),"\"");
                
                if (!Install)
                    args += " /uninstall";

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = string.Concat("\"",Path.Combine(m_AppPath, "InstallUtil.exe"),"\"");
                psi.Arguments = args;
                psi.CreateNoWindow = true;
                psi.UseShellExecute = false;
                psi.RedirectStandardOutput = true;
                psi.WorkingDirectory = m_AppPath;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                this.txtConsoleOut.Text = string.Empty;
                this.txtConsoleOut.Visible = true;
                Process proc = Process.Start(psi);
                proc.WaitForExit();
                string res;

                res = proc.StandardOutput.ReadToEnd();
                ZoneLib.WriteToLog(res, EventLogEntryType.Information);
                this.txtConsoleOut.Text += res + "\r\n";


                if (txtConsoleOut.Text.IndexOf("The installation failed, and the rollback has been performed.") > 0)
                    MessageBox.Show("Install Failed");
                this.txtConsoleOut.Visible = false;
                this.ShowStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
		}

		private void cmdUpdateManual_Click(object sender, System.EventArgs e)
		{
			if (this.txtManualIP.Text != string.Empty)
			{
                var zones = this.txtZones.Text.Split(',');
                foreach (string zone in zones)
                {
                    ZoneLib.UpdateDNS(txtUpdateURL.Text, zone, txtManualIP.Text, m_Config);
                }

				//ZoneLib.UpdateDNS(m_Config,this.txtManualIP.Text);
			}
			else
				MessageBox.Show("You must enter an IP Address to use");
		}

		private void cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

        private void lvLog_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvLog.SelectedItems.Count > 0)
            {
                string msg = this.lvLog.SelectedItems[0].SubItems[2].Text;
                lblLogMessage.Text = msg;
            }
        }

        private void lvLog_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            _LogEvents.Reverse();
            LoadLogList();
        }


        internal static void RestartElevated()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            startInfo.Verb = "runas";
            try
            {
                Process p = Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                return;
            }

            Application.Exit();
        }

        private void btnReloadEvents_Click(object sender, EventArgs e)
        {
            LoadLogFile();
        }
	}
}
