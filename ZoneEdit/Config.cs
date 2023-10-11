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
		private const string LOG_FILE		= "ZoneEditSvc.log";
		private const string IP_FILE		= "ZoneEdit_IP.txt";
		#endregion

		private String _dDNS_UserName = string.Empty; 
		private String _dDNS_Password = string.Empty; 
		private String _dDNS_IPPage = "http://www.netapps.net/ip/default.aspx"; 
		private String _dDNS_URL = "https://dynamic.zoneedit.com/auth/dynamic.html"; 
		private String _iP_Check_Interval = "5"; 
		private String _debug = "True";
        private string _AppId = string.Empty;
        private RegistryKey _Registry = null;
        private string _LastIPAddress = "10.10.10.10";

		#region Debug Property
		public String Debug
		{
			get { return _debug; }
			set { _debug = value; }
		}
		#endregion

		#region DDNS_URL Property
		[System.ComponentModel.Category("")]
		[System.ComponentModel.Description("")]
		[Bindable(true)]
		public String DDNS_URL
		{
			get { return _dDNS_URL; }
			set { _dDNS_URL = value; }
		}
		#endregion

		#region DDNS_UserName Property
		[System.ComponentModel.Category("")]
		[System.ComponentModel.Description("")]
		[Bindable(true)]
		public String DDNS_UserName
		{
			get { return _dDNS_UserName; }
			set { _dDNS_UserName = value; }
		}
		#endregion

		#region DDNS_Password Property
		[System.ComponentModel.Category("")]
		[System.ComponentModel.Description("")]
		[Bindable(true)]
		public String DDNS_Password
		{
			get { return _dDNS_Password; }
			set { _dDNS_Password = value; }
		}
		#endregion

		#region IP_Check_Interval Property
		[System.ComponentModel.Category("")]
		[System.ComponentModel.Description("")]
		[Bindable(true)]
		public String IP_Check_Interval
		{
			get { return _iP_Check_Interval; }
			set { _iP_Check_Interval = value; }
		}
		#endregion

		#region DDNS_IPPage Property
		public String DDNS_IPPage
		{
			get { return _dDNS_IPPage; }
			set { _dDNS_IPPage = value; }
		}
		#endregion

        #region LastIPAddress
        public string LastIPAddress
        {
            get { return _LastIPAddress; }
            set { _LastIPAddress = value; }
        }
        #endregion


        #region Constructor
        public Config()
		{
            ResourceManager mgr = new ResourceManager("ZoneEdit.Resource1", Assembly.GetExecutingAssembly());
            _AppId = mgr.GetString("AppGuid");
            _Registry = Registry.ClassesRoot.CreateSubKey("CLSID\\" + _AppId);

            this.DDNS_IPPage = _Registry.GetValue("IpPage", _dDNS_IPPage).ToString();
            this.DDNS_Password = _Registry.GetValue("Password", _dDNS_Password).ToString();
            this.DDNS_URL = _Registry.GetValue("PostUrl", _dDNS_URL).ToString();
            this.DDNS_UserName = _Registry.GetValue("UserName", _dDNS_UserName).ToString();
            this.Debug = _Registry.GetValue("Debug", "True").ToString();
            this.IP_Check_Interval = _Registry.GetValue("Interval", _iP_Check_Interval).ToString();
            this.LastIPAddress = _Registry.GetValue("LastIpAddress", _LastIPAddress).ToString();
            
		}
		#endregion

		public void Save()
		{
            _Registry.SetValue("IpPage", _dDNS_IPPage);
            _Registry.SetValue("Password", _dDNS_Password);
            _Registry.SetValue("PostUrl", _dDNS_URL);
            _Registry.SetValue("UserName", _dDNS_UserName);
            _Registry.SetValue("Debug", "True");
            _Registry.SetValue("Interval", _iP_Check_Interval);
            _Registry.SetValue("LastIpAddress", _LastIPAddress);
        }
		
	}
}
