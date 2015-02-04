using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OwinSamples.SelfHost.Logging
{
    /// <summary>
    /// Color format and parameters intended to generally follow the expressjs morgan logger https://github.com/expressjs/morgan
    /// </summary>
    public class RequestColorConsoleTraceListener : ConsoleTraceListener
    {
        public override void WriteLine(string message)
        {
            if (message == null) return;

            var pieces = message.Split('|');

            WriteMethod(pieces[0]);
            WritePath(pieces[1]);
            WriteStatus(pieces[2]);
            WriteElapsed(pieces[3]);
            base.WriteLine("");
            Console.ResetColor();
        }

        private void WriteMethod(string raw)
        {
            Console.ForegroundColor = ConsoleColor.White;
            base.Write(raw + " ");
        }

        private void WritePath(string raw)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            base.Write(raw + " ");
        }

        private void WriteStatus(string raw)
        {
            HttpStatusCode status;
            if (Enum.TryParse(raw, out status))
            {
                switch (status)
                {
                    case HttpStatusCode.Continue:
                    case HttpStatusCode.SwitchingProtocols:
                    case HttpStatusCode.OK:
                    case HttpStatusCode.Created:
                    case HttpStatusCode.Accepted:
                    case HttpStatusCode.NonAuthoritativeInformation:
                    case HttpStatusCode.NoContent:
                    case HttpStatusCode.ResetContent:
                    case HttpStatusCode.PartialContent:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case HttpStatusCode.MultipleChoices:
                    case HttpStatusCode.MovedPermanently:
                    case HttpStatusCode.Found:
                    case HttpStatusCode.SeeOther:
                    case HttpStatusCode.NotModified:
                    case HttpStatusCode.UseProxy:
                    case HttpStatusCode.Unused:
                    case HttpStatusCode.TemporaryRedirect:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.Unauthorized:
                    case HttpStatusCode.PaymentRequired:
                    case HttpStatusCode.Forbidden:
                    case HttpStatusCode.NotFound:
                    case HttpStatusCode.MethodNotAllowed:
                    case HttpStatusCode.NotAcceptable:
                    case HttpStatusCode.ProxyAuthenticationRequired:
                    case HttpStatusCode.RequestTimeout:
                    case HttpStatusCode.Conflict:
                    case HttpStatusCode.Gone:
                    case HttpStatusCode.LengthRequired:
                    case HttpStatusCode.PreconditionFailed:
                    case HttpStatusCode.RequestEntityTooLarge:
                    case HttpStatusCode.RequestUriTooLong:
                    case HttpStatusCode.UnsupportedMediaType:
                    case HttpStatusCode.RequestedRangeNotSatisfiable:
                    case HttpStatusCode.ExpectationFailed:
                    case HttpStatusCode.UpgradeRequired:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case HttpStatusCode.InternalServerError:
                    case HttpStatusCode.NotImplemented:
                    case HttpStatusCode.BadGateway:
                    case HttpStatusCode.ServiceUnavailable:
                    case HttpStatusCode.GatewayTimeout:
                    case HttpStatusCode.HttpVersionNotSupported:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }

            base.Write(raw + " ");
        }

        private void WriteElapsed(string raw)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            base.Write(raw + " ");
        }
    }
}
