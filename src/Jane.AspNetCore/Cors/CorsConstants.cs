namespace Jane.AspNetCore.Cors
{
    /// <summary>
    /// Needed to copy these in since some of them are internal to the Microsoft.AspNet.Cors project
    /// </summary>
    public static class CorsConstants
    {
        /// <summary>The Access-Control-Allow-Credentials response header.</summary>
        public static readonly string AccessControlAllowCredentials = "Access-Control-Allow-Credentials";

        /// <summary>The Access-Control-Allow-Headers response header.</summary>
        public static readonly string AccessControlAllowHeaders = "Access-Control-Allow-Headers";

        /// <summary>The Access-Control-Allow-Methods response header.</summary>
        public static readonly string AccessControlAllowMethods = "Access-Control-Allow-Methods";

        /// <summary>The Access-Control-Allow-Origin response header.</summary>
        public static readonly string AccessControlAllowOrigin = "Access-Control-Allow-Origin";

        /// <summary>The Access-Control-Expose-Headers response header.</summary>
        public static readonly string AccessControlExposeHeaders = "Access-Control-Expose-Headers";

        /// <summary>The Access-Control-Max-Age response header.</summary>
        public static readonly string AccessControlMaxAge = "Access-Control-Max-Age";

        /// <summary>The Access-Control-Request-Headers request header.</summary>
        public static readonly string AccessControlRequestHeaders = "Access-Control-Request-Headers";

        /// <summary>The Access-Control-Request-Method request header.</summary>
        public static readonly string AccessControlRequestMethod = "Access-Control-Request-Method";

        /// <summary>
        /// The value for the Access-Control-Allow-Origin response header to allow all origins.
        /// </summary>
        public static readonly string AnyOrigin = "*";

        /// <summary>The Origin request header.</summary>
        public static readonly string Origin = "Origin";

        /// <summary>The HTTP method for the CORS preflight request.</summary>
        public static readonly string PreflightHttpMethod = "OPTIONS";

        internal static readonly string[] SimpleMethods = new string[3]
        {
          "GET",
          "HEAD",
          "POST"
        };

        internal static readonly string[] SimpleRequestHeaders = new string[4]
        {
          "Origin",
          "Accept",
          "Accept-Language",
          "Content-Language"
        };

        internal static readonly string[] SimpleResponseHeaders = new string[6]
        {
          "Cache-Control",
          "Content-Language",
          "Content-Type",
          "Expires",
          "Last-Modified",
          "Pragma"
        };
    }
}