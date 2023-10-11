using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.Win32;
using System.Resources;
using System.Reflection;


namespace ZoneEdit
{
	/// <summary>
	/// Summary description for ZoneLib.
	/// </summary>
	public class ZoneLib
	{
		#region Constants
		const string CONFIG_FILE = "ZoneEditSvc.config";
		const string LOG_FILE = "ZoneEditSvc.log";
		#endregion

        private static string m_GUID = "{537C147D-BB0E-4690-B9A0-CB5F7D503993}";
		private static string m_SystemPath = string.Empty;
		private static string m_ConfigPath = string.Empty;
		private static string m_LogPath = string.Empty;
		private static Config m_Config = null;
		private static bool DEBUG = true;


		public static void UpdateDNS(Config cfg, string IPAddress)
		{
            try
            {
                //pull values from configuration file
                string ddnsUrl = string.Concat(cfg.DDNS_URL, string.Format("&dnsto={0}", IPAddress));
                string ddnsUserName = cfg.DDNS_UserName;
                string ddnsPassword = cfg.DDNS_Password;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ddnsUrl);
                request.Credentials = new NetworkCredential(ddnsUserName, ddnsPassword);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == (HttpStatusCode)201)
                {
                    WriteToLog("DNS provider successfully updated", EventLogEntryType.Information);
                }
                else
                {
                    string errMsg = Convert(response.GetResponseStream());
                    WriteToLog(string.Format("Failed to update www.zoneedit.com. Recieved return code {0} and message \"{1}\"", response.StatusCode, errMsg), EventLogEntryType.Information);
                }
            }
            catch (Exception ex)
            {
                WriteToLog("error in sub [UpdateDNS]: " + ex.Message.ToString(), EventLogEntryType.Error);
            }
		}
        public static void UpdateDNS(string url, string zone, string IPAddress, Config cfg)
        {
            try
            {
                //pull values from configuration file
                string ddnsUrl = string.Concat(url,"?host=",zone);//, string.Format("&dnsto={0}", IPAddress));
                string ddnsUserName = cfg.DDNS_UserName;
                string ddnsPassword = cfg.DDNS_Password;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ddnsUrl);
                request.Credentials = new NetworkCredential(ddnsUserName, ddnsPassword);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == (HttpStatusCode)201)
                {
                    WriteToLog("DNS provider successfully updated", EventLogEntryType.Information);
                }
                else
                {
                    string errMsg = Convert(response.GetResponseStream());
                    WriteToLog(string.Format("Failed to update www.zoneedit.com. Recieved return code {0} and message \"{1}\"", response.StatusCode, errMsg), EventLogEntryType.Information);
                }
            }
            catch (Exception ex)
            {
                WriteToLog("error in sub [UpdateDNS]: " + ex.Message.ToString(), EventLogEntryType.Error);
            }
        }
        public static void RegisterCPL(string folderName)
        {
            try
            {
                string guid = "{537C147D-BB0E-4690-B9A0-CB5F7D503993}";
                string title = "Zone Edit Config";
                string filename = Path.Combine(folderName, "ZoneEdit.dll");
                RegistryKey cplKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Explorer\\ControlPanel\\NameSpace\\" + guid);
                cplKey.SetValue("", title);

                RegistryKey cplKeyDesc = Registry.ClassesRoot.CreateSubKey("CLSID\\" + guid);
                cplKeyDesc.SetValue("", title);
                cplKeyDesc.SetValue("InfoTip", "ZoneEdit Service Configuration Utility");
                cplKeyDesc.SetValue("LocalizedString", "Zone Edit Service Configuration");
                cplKeyDesc.SetValue("System.ApplicationName", "ZoneEditCPL");
                cplKeyDesc.SetValue("System.ControlPanel.Category", "3,8");

                //icon
                RegistryKey cplKeyIcon = Registry.ClassesRoot.CreateSubKey("CLSID\\" + guid + "\\DefaultIcon");
                cplKeyIcon.SetValue("", string.Format("{0},-32512", filename));

                //launch
                RegistryKey cplKeyCmd = Registry.ClassesRoot.CreateSubKey("CLSID\\" + guid + "\\Shell\\Open\\Command");
                cplKeyCmd.SetValue("", string.Format("{0}\\zoneeditcpl.exe", folderName), RegistryValueKind.ExpandString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
		public static void ConfigureDNS(Config cfg, bool forceUpdate)
		{
			m_Config = cfg;
			try
			{
				DEBUG = bool.Parse(m_Config.Debug);

                WriteToLog("Getting IP address", EventLogEntryType.Information);
				string currentIP = GetCurrentIP();
                WriteToLog(string.Format("Retrieved IP address {0}", currentIP), EventLogEntryType.Information);

				/* Step 2. See if IP had changed
				 */
                WriteToLog("Retrieving last known IP", EventLogEntryType.Information);
				string lastIP = GetLastKnownIP();
                WriteToLog(string.Format("Last known IP is {0}", lastIP), EventLogEntryType.Information);

				/* Step 3. See if the IP has changed. If it hasn't then we don't
				 *		   need to post a change to DNS provider
				 */
				if(currentIP != lastIP || forceUpdate)
				{
                    WriteToLog(string.Format("IP Address has changed from {0} to {1}. Updating DNS provider", lastIP, currentIP), EventLogEntryType.Information);

					/* Step 4. Update our DNS provider
					 */
					WriteToLog("Updating DNS at zone with new ip",EventLogEntryType.Information);
					UpdateDNS();
					WriteToLog("DNS now updated",EventLogEntryType.Information);

					/* Step 5. Save new IP to our IP file
					 */
                    WriteToLog(string.Format("Saving ip {0} to file ZoneEdit_IP.txt", currentIP), EventLogEntryType.Information);
					SaveCurrentIP(currentIP);
                    WriteToLog("IP saved to file", EventLogEntryType.Information);
				}
				else
				{
                    WriteToLog("IP has not changed", EventLogEntryType.Information);
				}
			}
			catch(Exception e)
			{
                WriteToLog(string.Format("Unexpected Exception {1}, {0}", e.Message, e.GetType().FullName), EventLogEntryType.Error);
			}
		}
		private static void UpdateDNS()
		{
            try
            {
                //pull values from configuration file
                string ddnsUrl = m_Config.DDNS_URL;
                string ddnsUserName = m_Config.DDNS_UserName;
                string ddnsPassword = m_Config.DDNS_Password;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ddnsUrl);
                request.Credentials = new NetworkCredential(ddnsUserName, ddnsPassword);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == (HttpStatusCode)201)
                {
                    WriteToLog(string.Format("DNS provider successfully updated.\r\n{0}", Convert(response.GetResponseStream())), EventLogEntryType.Information);
                }
                else
                {
                    string errMsg = Convert(response.GetResponseStream());
                    WriteToLog(string.Format("Failed to update www.zoneedit.com. Recieved return code {0} and message \"{1}\"", response.StatusCode, errMsg), EventLogEntryType.Information);
                }
            }
            catch (Exception ex)
            {
                WriteToLog("error in sub [ZoneEditLogWatcher_Changed]: " + ex.Message.ToString(), EventLogEntryType.Error);
            }

		}
		private static void SaveCurrentIP(string IP)
		{
            ResourceManager mgr = new ResourceManager("ZoneEdit.Resource1", Assembly.GetExecutingAssembly());
            string _AppId = mgr.GetString("AppGuid");
            RegistryKey _Registry = Registry.ClassesRoot.CreateSubKey("CLSID\\" + _AppId);
            _Registry.SetValue("LastIpAddress", m_Config.LastIPAddress);
		}
		private static string GetLastKnownIP()
		{
            return m_Config.LastIPAddress;
		}
		private static string GetCurrentIP()
		{
			string ddnsIPCheckPage = m_Config.DDNS_IPPage;
            string html = string.Empty;
			try
			{
                using (WebClient wc = new WebClient())
                {
                    html = wc.DownloadString(ddnsIPCheckPage);
                    wc.Dispose();
                }

                Regex oRegex = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");

                MatchCollection oMatchCollection = oRegex.Matches(html);

                string currentIP = string.Empty;

                foreach (Match oMatch in oMatchCollection)
                {
                    if (oMatch.Groups.Count > 0)
                    {
                    currentIP = oMatch.Groups[0].ToString();
                    break;
                    }
                }

				return currentIP;
			}
			catch(System.Net.WebException we)
			{
				//the check ip site is probably down, or we don't have a internet connection
                WriteToLog(string.Format("Unexpected error while retrieving IP from web page. Message = {0}", we.Message), EventLogEntryType.Error);
				return GetLastKnownIP();
			}
		}
		private static string Convert(Stream Input)
		{
			string data = "";
			using(StreamReader reader = new StreamReader(Input))
			{
				data = reader.ReadToEnd();
			}

			return data;
		}
		public static void WriteToLog(string Message, EventLogEntryType msgType)
		{
            Config c = new Config();
            if (bool.Parse(c.Debug))
            {
                string LogName = "ZoneEdit";
                if (!EventLog.SourceExists(LogName))
                {
                    EventSourceCreationData eventSourceData = new EventSourceCreationData(LogName, LogName);
                    EventLog.CreateEventSource(eventSourceData);
                }

                using (EventLog myLogger = new EventLog(LogName, ".", LogName))
                {
                    myLogger.WriteEntry(Message, msgType);
                }
            }
           
		}
        public static List<CustomEvents> LoadEvents()
        {
            List<CustomEvents> events = new List<CustomEvents>();
            EventLog myLog = new EventLog();
            myLog.Log = "ZoneEdit";
            foreach (EventLogEntry entry in myLog.Entries)
            {
                CustomEvents ce = new CustomEvents();
                ce.EntryTime = entry.TimeWritten;
                ce.Message = entry.Message;
                ce.LogType = entry.EntryType;
                events.Add(ce);
            }

            events.Reverse();
            return events;
        }
        public static string GetIP(Config cfg)
        {
            m_Config = cfg;
            string currentIP = string.Empty;
            try
            {
                DEBUG = bool.Parse(m_Config.Debug);

                //interval = int.Parse(m_Config.IP_Check_Interval) * 60000;

                /* Step 1. Get the ip
                 */
                WriteToLog("Getting IP address", EventLogEntryType.Information);
                currentIP = GetCurrentIP();
                WriteToLog(string.Format("Retrieved IP address {0}", currentIP), EventLogEntryType.Information);
                return currentIP;
            }
            catch
            {
            }
            return currentIP;
        }
	}

    public class CustomEvents
    {
        public EventLogEntryType LogType { get; set; }
        public string Message { get; set; }
        public DateTime EntryTime { get; set; }

    }
}
