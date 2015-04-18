using ShortURLService.DAL;
using ShortURLService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace ShortURLService.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        UrlContext db = new UrlContext();
        public ActionResult Index()
        {
            IEnumerable<URL> model = new List<URL>();
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                model = db.Urls.Where(u => u.UserId == userId).OrderByDescending(u => u.GeneratedDate).AsEnumerable();
            }
            return View(model);
        }

        public ActionResult RedirectToLong(string shortURL)
        {
            if (string.IsNullOrEmpty(shortURL))
                return RedirectToAction("NotFound", "Home");
            else
            {
                URL url = db.Urls.Where(u => u.ShortUrl == shortURL).FirstOrDefault();

                if (url == null)
                    return RedirectToAction("NotFound", "Home");
                else
                {
                    #region Statistics collected for this URL
                    UrlStat stats = new UrlStat(Request);
                    stats.UrlId = url.UrlId; // relation

                    try
                    {
                        db.UrlStats.Add(stats);
                        db.SaveChanges();
                    }
                    catch (Exception exc)
                    {
                        log.Error(exc);
                    }
                    #endregion

                    url.Hits++; // increment visits
                    db.SaveChanges();
                    Response.StatusCode = 302;
                    return Redirect(url.LongUrl); // redirects to the long URL
                }
            }
        }

        public ActionResult NotFound()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ShorterURL(string longUrl)
        {
            if (string.IsNullOrEmpty(longUrl))
                return Json(new { status = false, message = "Please provide URL" }, JsonRequestBehavior.AllowGet);
            else
            {
                if (!new URL().HasHTTPProtocol(longUrl))
                    longUrl = "http://" + longUrl;

                // Check if long URL already exists in the database
                URL existingURL = db.Urls.Where(u => u.LongUrl.ToLower() == longUrl.ToLower()).FirstOrDefault();

                if (existingURL == null)
                {
                    URL shortUrl = new URL()
                    {
                        LongUrl = longUrl,
                        GeneratedDate = DateTime.UtcNow,
                        Hits = 0
                    };
                    string userId = User.Identity.GetUserId();

                    if (shortUrl.CheckLongUrlExists())
                    {
                        shortUrl.GenerateRandomShortUrl();
                        if (!string.IsNullOrEmpty(userId)) // only if user is authorized
                            shortUrl.UserId = userId;

                        db.Urls.Add(shortUrl);
                        try
                        {
                            db.SaveChanges();
                            shortUrl.ShortUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/" + shortUrl.ShortUrl;

                            return Json(new { status = true, url = shortUrl }, JsonRequestBehavior.AllowGet);
                        }
                        catch (Exception exc)
                        {
                            log.Error(exc);
                            return Json(new { status = false, message = exc.Message }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                        return Json(new { status = false, message = "Not valid URL provided" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    existingURL.ShortUrl = Request.Url.Scheme + "://" + Request.Url.Authority + "/" + existingURL.ShortUrl;
                    return Json(new { status = true, url = existingURL }, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}