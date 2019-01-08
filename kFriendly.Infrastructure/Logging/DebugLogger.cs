using kFriendly.Core.Interfaces;
using System;
using System.Net.Http;

namespace kFriendly.Infrastructure.Logging
{
    public class DebugLogger : IHTTPLogger
    {
        public void Log(string message)
        {
           System.Diagnostics.Debug.WriteLine(message);
        }

        /// <summary>
        /// Logs HttpRequest information to the application logger.
        /// </summary>
        /// <param name="request">Request to log.</param>
        public void Log(HttpRequestMessage request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                var message = string.Format(
                    Environment.NewLine + "---------------------------------" + Environment.NewLine +
                    "WEB REQUEST to {0}" + Environment.NewLine +
                    "-Method: {1}" + Environment.NewLine +
                    "-Headers: {2}" + Environment.NewLine +
                    "-Contents: " + Environment.NewLine + "{3}" + Environment.NewLine +
                    "---------------------------------",
                    request.RequestUri.OriginalString,
                    request.Method.Method,
                    request.Headers?.ToString(),
                    request.Content?.ReadAsStringAsync().Result
                );
                this.Log(message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error during Log(HttpRequestMessage request): " + ex.ToString());
            }
        }

        /// <summary>
        /// Logs the HttpResponse object to the application logger.
        /// </summary>
        /// <param name="response">Response to log.</param>
        public void Log(HttpResponseMessage response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            this.Log(response.RequestMessage);

            try
            {
                var message = string.Format(
                    Environment.NewLine + "---------------------------------" + Environment.NewLine +
                    "WEB RESPONSE to {0}" + Environment.NewLine +
                    "-HttpStatus: {1}" + Environment.NewLine +
                    "-Reason Phrase: {2}" + Environment.NewLine +
                    "-ContentLength: {3:0.00 KB}" + Environment.NewLine +
                    "-Contents: " + Environment.NewLine + "{4}" + Environment.NewLine +
                    "---------------------------------",
                    response.RequestMessage.RequestUri.OriginalString,
                    string.Format("{0} {1}", (int)response.StatusCode, response.StatusCode.ToString()),
                    response.ReasonPhrase,
                    Convert.ToDecimal(Convert.ToDouble(response.Content.Headers.ContentLength) / 1024),
                    response.Content?.ReadAsStringAsync().Result
                    );
                this.Log(message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error during Log(HttpResponseMessage request): " + ex.ToString());
            }
        }
    }
}
