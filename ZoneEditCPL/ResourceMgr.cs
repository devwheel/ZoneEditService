using System;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;

namespace ZoneEditCPL
{
	/// <summary>
	/// ResourceMgr string and image resources
	/// </summary>
	public class ResourceMgr
	{
		/// <summary>
		/// Initiates the ResourceMgr Class
		/// </summary>
		public ResourceMgr()
		{
		}
		/// <summary>
		/// initialize a stringResources class
		/// </summary>
		public static StringResources Strings = new StringResources();
		/// <summary>
		/// initialize an IconResources class
		/// </summary>
		public static IconResources Icons = new IconResources();
	}

	/// <summary>
	/// Base class for resources
	/// </summary>
	public class ResourceBase
	{
		/// <summary>
		/// ctor for the base Resource class
		/// </summary>
		public ResourceBase()
		{
		}
	}

	/// <summary>
	/// Derived Class to handle String resources
	/// </summary>
	public class StringResources : ResourceBase
	{
		private ResourceManager m_rm;

		internal StringResources()
		{
            m_rm = new ResourceManager("ZoneEditCPL.Properties.Resources", Assembly.GetExecutingAssembly());
		}
		/// <summary>
		/// returns the string requested from the resource file
		/// </summary>
		public string this[string name]
		{
			get { return m_rm.GetString(name); }
		}
	}
	/// <summary>
	/// Derived class to handle Icon Resources 
	/// </summary>
	public class IconResources : ResourceBase
	{
		private ResourceManager m_rm;

		internal IconResources()
		{
			m_rm = new ResourceManager("ZoneEditCPL.Properties.Resources", Assembly.GetExecutingAssembly());
		}
		/// <summary>
		/// Returns an icon from the resource file
		/// </summary>
		public Icon this[string name]
		{
			get 
			{
                m_rm = new ResourceManager("ZoneEditCPL.Properties.Resources", Assembly.GetExecutingAssembly());
				return (Icon) m_rm.GetObject(name);
			}
		}
		/// <summary>
		/// Loads an image from a .resource file
		/// </summary>
		/// <param name="name">name of the .resource file without the extension</param>
		/// <returns>Bitmap Image</returns>
		public Bitmap LoadBitmap(string name)
		{
			m_rm = new ResourceManager("ZoneEditCPL.Icons.resources", Assembly.GetExecutingAssembly());
			return (Bitmap) m_rm.GetObject(name);
		}
	}
}
