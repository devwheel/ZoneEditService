using System;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Resources;
using System.Reflection;
using Microsoft.Win32;
	
namespace ZoneEdit
{
	/// <summary>
	/// Summary description for EditClass.
	/// </summary>
	/// 
	[Serializable()]
	public class Config
	{

		#region Constants
		private const string CONFIG_FILE = "ZoneEditSvc.config";
		private string _AppId = string.Empty;
		#endregion


        private RegistryKey _Registry = null;

		#region Debug Property
		public String Debug { get; set; } = "True";
		#endregion

		#region DDNS_URL Property
		[System.ComponentModel.Category("")]
		[System.ComponentModel.Description("")]
		[Bindable(true)]
		public String DDNS_URL { get; set; } = "https://dynamic.zoneedit.com/auth/dynamic.html";
		#endregion

		#region DDNS_UserName Property
		[System.ComponentModel.Category("")]
		[System.ComponentModel.Description("")]
		[Bindable(true)]
		public String DDNS_UserName { get; set; } = string.Empty;
		#endregion

		#region DDNS_Password Property
		[System.ComponentModel.Category("")]
		[System.ComponentModel.Description("")]
		[Bindable(true)]
		public String DDNS_Password { get; set; } = string.Empty;
		#endregion

		#region IP_Check_Interval Property
		[System.ComponentModel.Category("")]
		[System.ComponentModel.Description("")]
		[Bindable(true)]
		public String IP_Check_Interval { get; set; } = "5";
		#endregion

		#region DDNS_IPPage Property
		public String DDNS_IPPage { get; set; } = "http://www.netapps.net/ip/default.aspx";
		#endregion

		#region DDNS_ZONES
		public string DDNS_Zones { get; set; } = string.Empty;
		#endregion

		#region LastIPAddress
		public string LastIPAddress { get; set; } = "10.10.10.10";
        #endregion


        #region Constructor
        public Config()
		{
            ResourceManager mgr = new ResourceManager("ZoneEdit.Resource1", Assembly.GetExecutingAssembly());
            _AppId = mgr.GetString("AppGuid");
            _Registry = Registry.ClassesRoot.CreateSubKey("CLSID\\" + _AppId);

            this.DDNS_IPPage = _Registry.GetValue("IpPage", DDNS_IPPage).ToString();
            this.DDNS_Password = _Registry.GetValue("Password", DDNS_Password).ToString();
            this.DDNS_URL = _Registry.GetValue("PostUrl", DDNS_URL).ToString();
            this.DDNS_UserName = _Registry.GetValue("UserName", DDNS_UserName).ToString();
            this.Debug = _Registry.GetValue("Debug", "True").ToString();
            this.IP_Check_Interval = _Registry.GetValue("Interval", IP_Check_Interval).ToString();
            this.LastIPAddress = _Registry.GetValue("LastIpAddress", LastIPAddress).ToString();
			this.DDNS_Zones = _Registry.GetValue("Zones",DDNS_Zones).ToString();
            
		}
        #endregion

        #region Save to Registry
        public void SaveToRegistry()
		{
            _Registry.SetValue("IpPage", DDNS_IPPage);
            _Registry.SetValue("Password", DDNS_Password);
            _Registry.SetValue("PostUrl", DDNS_URL);
            _Registry.SetValue("UserName", DDNS_UserName);
            _Registry.SetValue("Debug", "True");
            _Registry.SetValue("Interval", IP_Check_Interval);
            _Registry.SetValue("LastIpAddress", LastIPAddress);
			_Registry.SetValue("Zones", DDNS_Zones);
        }
        #endregion

    }
}
