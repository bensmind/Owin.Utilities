using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace OwinSamples.SelfHost
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Server url must be specified");
                return 1;
            }
            var url = args[0];

            var host = WebApp.Start<Startup>(url);
            Trace.WriteLine(string.Format("Web server started @ {0}", url));

            Console.ReadLine();
            Trace.WriteLine("Halt requested");

            host.Dispose();
            Trace.WriteLine("Web server disposed");

            return 0;
        }
    }
}
