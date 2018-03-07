using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GitGrub.GetUsGrub
{
    /// <summary>
    /// The <c>RequireHttpsAttribute</c> class.
    /// Defines a method to check if Hyper Text Transfer Protocol Secure (HTTPS) is in URI scheme.
    /// <para>
    /// @author: Jennifer Nguyen
    /// @updated: 03/05/2018
    /// </para>
    /// </summary>
    public class RequireHttpsAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// The OnAuthorization method.
        /// Checks request URI if using Hyper Text Transfer Protocol Secure (HTTPS)
        /// <para>
        /// @author: Jennifer Nguyen
        /// @updated: 03/05/2018
        /// </para>
        /// </summary>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                const string html = "<p>Https is required</p>";

                // TODO: Why only for GET methods?
                if (request.Method.Method == "GET")
                {
                    actionContext.Response = request.CreateResponse(HttpStatusCode.Found);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");

                    // Building the URI
                    var uriBuilder = new UriBuilder(request.RequestUri)
                    {
                        Scheme = Uri.UriSchemeHttps,
                        Port = 443
                    };

                    // Set location with the new URI to redirect user to HTTPS URI
                    actionContext.Response.Headers.Location = uriBuilder.Uri;
                }
                else
                {
                    actionContext.Response = request.CreateResponse(HttpStatusCode.NotFound);
                    actionContext.Response.Content = new StringContent(html, Encoding.UTF8, "text/html");
                }
            }
        }
    }
}