using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShortURLService.Models;
using System.Data.Entity;

namespace ShortURLService.DAL
{
    public class UrlContext : DbContext
    {
        public DbSet<URL> Urls { get; set; }
    }
}