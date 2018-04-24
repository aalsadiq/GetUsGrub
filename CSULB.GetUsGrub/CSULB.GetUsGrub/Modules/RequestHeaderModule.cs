using System;
using System.Collections.Generic;
using System.Web;

namespace CSULB.GetUsGrub
{
    internal class RequestHeaderModule : IHttpModule
    {
        private bool IsAcceptedHeader(HttpRequest request, string headerType, ICollection<string> acceptedHeaders)
        {
            var requestHeader = request.Headers[headerType];
            return requestHeader != null && acceptedHeaders.Contains(requestHeader);
        }

        private void OnHttpRequest(object sender, EventArgs e)
        {
            // End early if not HttpApplication
            if (!(sender is HttpApplication)) { return; }

            // If sender is an HttpApplication, cast it into HttpApplication.
            var app = (HttpApplication)sender;

            Console.WriteLine(app.Request.GetType().Name);
            var name = app.Request.GetType().Name;
            // End early if not http request
            if (!app.Request.GetType().Name.Equals(NetworkConstants.HTTP_REQUEST)) { return; }

            var request = app.Request;

            var isAcceptedReferer = true;//IsAcceptedHeader(request, NetworkConstants.REFERER, NetworkConstants.ACCEPTED_URLS);
            var isAcceptedOrigin = IsAcceptedHeader(request, NetworkConstants.ORIGIN, NetworkConstants.ACCEPTED_ORIGINS);
            var isAcceptedAuthority = NetworkConstants.ACCEPTED_AUTHORITIES.Contains(request.Url.Authority);

            var isValidRequest = isAcceptedReferer && isAcceptedOrigin && isAcceptedAuthority;

            // If referer, origin, or authority are bad, return bad request.
            if (!isValidRequest)
            {
                app.Response.StatusCode = NetworkConstants.STATUS_CODE_REFUSE;
                app.Response.End();
            }

            // End early if not preflight.
            if (app.Request.HttpMethod != NetworkConstants.PREFLIGHT_VERB) { return; }

            app.Response.StatusCode = NetworkConstants.STATUS_CODE_SUCCESS;
            app.Response.AddHeader(NetworkConstants.ACCESS_CONTROL_ALLOW_HEADERS, NetworkConstants.ACCEPTED_HEADERS);
            app.Response.AddHeader(NetworkConstants.ACCESS_CONTROL_EXPOSE_HEADERS, NetworkConstants.EXPOSED_HEADERS);
            app.Response.AddHeader(NetworkConstants.ACCESS_CONTROL_ALLOW_ORIGIN, request.Headers[NetworkConstants.ORIGIN]);
            app.Response.AddHeader(NetworkConstants.ACCESS_CONTROL_ALLOW_CREDENTIALS, NetworkConstants.IS_ALLOW_CREDENTIALS);
            app.Response.AddHeader(NetworkConstants.ACCESS_CONTROL_ALLOW_METHODS, NetworkConstants.ACCEPTED_METHODS);
            app.Response.AddHeader(NetworkConstants.CONTENT_TYPE, NetworkConstants.APPLICATION_JSON);
            app.Response.End();
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnHttpRequest;
        }
    }
}