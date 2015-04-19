using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortURLService.Infrastructure
{
    /// <summary>
    /// Constants values that are often used.
    /// </summary>
    class Constants
    {
        public const string UNKNOWN = "Unknown";

        /// <summary>
        /// Protocols used in this application.
        /// </summary>
        public class Protocol
        {
            public const string HTTP = "http://";
            public const string HTTPS = "https://";
        }
    }
}