using ShortURLService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortURLService.Models
{
    public class UrlStat
    {
        public int UrlStatId { get; set; }
        public string UserAgent { get; set; }
        public string UserHostAddress { get; set; }
        public string UserLanguage { get; set; }
        public string UrlRefferer { get; set; }
        public bool IsMobile { get; set; }
        public string Browser { get; set; }
        public int MajorVersion { get; set; }
        public int UrlId { get; set; }
        public virtual URL Url { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public UrlStat()
        {

        }
        /// <summary>
        /// Bind properties with data that is coming from the request
        /// </summary>
        /// <param name="request">HTTP request</param>
        public UrlStat(HttpRequestBase request)
        {
            UrlRefferer = request.UrlReferrer.Host;
            UserAgent = request.UserAgent;
            UserHostAddress = request.UserHostAddress;
            UserLanguage = request.UserLanguages[0];
            if (string.IsNullOrEmpty(request.Browser.Browser))
                Browser = Constants.UNKNOWN;
            else
                Browser = request.Browser.Browser;
            MajorVersion = request.Browser.MajorVersion;
            IsMobile = request.Browser.IsMobileDevice;
        }
    }
}