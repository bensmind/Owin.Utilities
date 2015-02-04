using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Logging;
using Owin;

namespace OwinSamples.SelfHost.WebApi.Logging
{
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
        private readonly Stopwatch _timer;
        public LogRequestMiddleware(OwinMiddleware next, IAppBuilder app, string label)
            : base(next)
        {
            _logger = app.CreateLogger(label);
            _timer = new Stopwatch();
        }

        public override async Task Invoke(IOwinContext context)
        {
            _timer.Start();
            await Next.Invoke(context);
            _timer.Stop();

            var message = string.Format("{0}|{1}|{2}|{3}ms",
                context.Request.Method,
                context.Request.Path,
                context.Response.StatusCode,
                _timer.ElapsedMilliseconds
                );

            _timer.Reset();
            _logger.WriteVerbose(message);
        }
    }
}
