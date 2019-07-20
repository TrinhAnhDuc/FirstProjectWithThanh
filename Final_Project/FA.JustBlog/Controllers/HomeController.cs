using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FA.JustBlog.Controllers
{
    public class HomeController : Controller
    {
        PostServices postServices = new PostServices();
        JustBlogContext db = new JustBlogContext();

        public ActionResult Index(string searchString)
        {
            return View(db.Posts.Where(c => c.Title.Contains(searchString) || c.PostContent.Contains(searchString) || searchString == null).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}