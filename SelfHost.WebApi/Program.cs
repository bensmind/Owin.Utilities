using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Hosting;

namespace OwinSamples.SelfHost.WebApi
{
    class Program
    {
        static int Main(string[] args)
        {
            var url = args.Length > 0 ? args[0] : null;
            var uri = ReadUrl(url);
            var host = WebApp.Start<Startup>(uri.AbsoluteUri);

            Trace.WriteLine("Web server started");
            Trace.WriteLine(string.Format("Listening on: {0}", uri.AbsoluteUri));

            Console.ReadLine();
            Trace.WriteLine("Halt requested");

            host.Dispose();
            Trace.WriteLine("Web server disposed");

            return 0;
        }

        static Uri ReadUrl(string input = null)
        {
            string url;
            if (input != null)
            {
                url = input;
            }
            else
            {
                Console.WriteLine("Please specify a url (i.e. http://localhost:3000) for the server, or Ctrl+C to exit");
                url = Console.ReadLine();
            }
            //Validate
            Uri uri; 
            if (string.IsNullOrWhiteSpace(url) || !Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                //Reprompt
                Console.WriteLine("A valid url must be specified");
                uri = ReadUrl();
            }
            return uri;
        }
    }
}
