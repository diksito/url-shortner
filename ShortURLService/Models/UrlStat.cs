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
        public bool IsMobile { get; set; }
    }
}