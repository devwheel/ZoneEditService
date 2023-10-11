using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Net;
using System.Configuration;
using ZoneEdit;

namespace ZoneEditSvc
{
    public class Service1 : System.ServiceProcess.ServiceBase
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        Timer serviceTimer;
        private static int interval = 300;
        private static Config m_Config;

        #region ctor
        public Service1()
        {
            // This call is required by the Windows.Forms Component Designer.
            InitializeComponent();
        }
        #endregion

        #region Main
        // The main entry point for the process
        static void Main()
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;
            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new Service1() };
            System.ServiceProcess.ServiceBase.Run(ServicesToRun);
        }
        #endregion

        #region Initialize Component
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "Zone Edit Service";
            TimerCallback timerDelegate = new TimerCallback(CheckNetwork);
            serviceTimer = new Timer(timerDelegate, null, 0, 0);
        }
        #endregion

        #region Dispose Method
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Service OnStart
        /// <summary>
        /// Set things in motion so your service can do its work.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            try
            {
                m_Config = new Config();
                ZoneLib.WriteToLog("Config Loaded at: " + DateTime.Now, EventLogEntryType.Information);
                ZoneLib.WriteToLog(string.Format("Interval Set at {0}", m_Config.IP_Check_Interval),EventLogEntryType.Information);
                interval = int.Parse(m_Config.IP_Check_Interval) * 60000;
                ZoneLib.WriteToLog(string.Format("Setting interval at {0}", interval / 1000 / 60), EventLogEntryType.Information);
                serviceTimer.Change(0, interval);
            }
            catch (Exception e)
            {
                ZoneLib.WriteToLog("error starting: " + e.Message.ToString(), EventLogEntryType.Error);
            }

        }
        #endregion

        #region Service OnStart
        /// <summary>
        /// Stop this service.
        /// </summary>
        protected override void OnStop()
        {
            serviceTimer.Change(0, 0);
        }
        #endregion

        private void CheckNetwork(Object state)
        {
            ZoneLib.WriteToLog(string.Format("CheckNetwork called at {0}", DateTime.Now), EventLogEntryType.Information);
            try
            {
                if (m_Config != null)
                {
                    ZoneLib.WriteToLog(String.Format("Next check at {0}", DateTime.Now.Add(new TimeSpan(0, 0, 0, 0, int.Parse(m_Config.IP_Check_Interval)*60000))), EventLogEntryType.Information);
                    ZoneEdit.ZoneLib.ConfigureDNS(m_Config, false);
                }
            }
            catch (Exception e)
            {
                serviceTimer.Change(0, 300000); // set to check in 5 minutes
                ZoneLib.WriteToLog("error in sub [CheckNetwork]: " + e.Message.ToString(),EventLogEntryType.Error);
            }
        }
     
 
    }
}
