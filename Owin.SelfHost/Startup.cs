using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Owin;

namespace ApiSelfHostTest
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        {
            // Configure Web API for self-host. 
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "Api",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            appBuilder.LogRequests("Api");
            appBuilder.UseWebApi(config);
        }
    }

    public static class LogRequestMiddlewareExtensions
    {
        public static void LogRequests(this IAppBuilder appBuilder, string label)
        {
            appBuilder.Use<LogRequestMiddleware>(appBuilder, label);
        }
    }

    public class LogRequestMiddleware : OwinMiddleware
    {
        private readonly ILogger _logger;

        public LogRequestMiddleware(OwinMiddleware next, IAppBuilder app, string label) 
            : base(next)
        {
            _logger = app.CreateLogger(label);            
        }

        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);

            var message = string.Format("{0} {1}: {2} {3}",
                context.Request.Scheme,
                context.Request.Method,
                context.Response.StatusCode,
                context.Request.Path);

            _logger.WriteVerbose(message);
        }
    }
}