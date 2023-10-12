using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZoneEdit;
using Microsoft.Win32;

namespace Harness
{
    class Program
    {
        static void Main(string[] args)
        {
           

            ZoneEdit.Config config = new ZoneEdit.Config();
            ZoneEdit.ZoneLib.ConfigureDNS(config, false);

        }
    }
}
