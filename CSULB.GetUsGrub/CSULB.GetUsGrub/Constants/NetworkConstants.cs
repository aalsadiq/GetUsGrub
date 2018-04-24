using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSULB.GetUsGrub
{
    public static class NetworkConstants
    {
        // HEADERS
        public const string ACCESS_CONTROL_ALLOW_HEADERS = "Access-Control-Allow-Headers";
        public const string ACCESS_CONTROL_ALLOW_ORIGIN = "Access-Control-Allow-Origin";
        public const string ACCESS_CONTROL_ALLOW_CREDENTIALS = "Access-Control-Allow-Credentials";
        public const string ACCESS_CONTROL_ALLOW_METHODS = "Access-Control-Allow-Methods";
        public const string ACCESS_CONTROL_EXPOSE_HEADERS = "Access-Control-Expose-Headers";
        public const string AUTHORIZATION = "Authorization";
        public const string ORIGIN = "origin";
        public const string ACCEPT = "accept";
        public const string CONTENT_TYPE = "content-type";
        public const string REFERER = "referer";
        public const string X_REQUESTED_WITH = "X-Requested-With";
        public const string PREFLIGHT_VERB = "OPTIONS";
        public const string APPLICATION_JSON = "application/json";

        public const string ACCEPTED_HEADERS =
            ACCESS_CONTROL_ALLOW_HEADERS + "," +
            ACCESS_CONTROL_ALLOW_ORIGIN + "," +
            ACCESS_CONTROL_ALLOW_CREDENTIALS + "," +
            AUTHORIZATION + "," +
            ORIGIN + "," +
            ACCEPT + "," +
            CONTENT_TYPE + "," +
            REFERER + "," +
            X_REQUESTED_WITH;

        public const string HTTP_REQUEST = "HttpRequest";

        public const string ACCEPTED_METHODS = "POST,GET,OPTIONS";

        public const string EXPOSED_HEADERS = "location";

        public const string IS_ALLOW_CREDENTIALS = "true";

        public const int STATUS_CODE_SUCCESS = 200; 

        public const int STATUS_CODE_REFUSE = 403;

        public static readonly ICollection<string> ACCEPTED_URLS = new string[]
        {
            "localhost:8080",
            "localhost:8081",
            "https://fannbrian.github.io"
        };

        public static readonly ICollection<string> ACCEPTED_AUTHORITIES = new string[]
        {
            "localhost:8080",
            "localhost:8081",
            "https://fannbrian.github.io"
        };

        public static readonly ICollection<string> ACCEPTED_ORIGINS = new string[]
        {
            "localhost:8080",
            "localhost:8081",
            "https://fannbrian.github.io"
        };
    }
}