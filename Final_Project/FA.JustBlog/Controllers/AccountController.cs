using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FA.JustBlog.Controllers
{
    public class AccountController : Controller
    {
        // GET: Acount
        JustBlogContext db = new JustBlogContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ActionLogin(FA.JustBlog.Core.Models.Category userLogin)
        {
            var userDetail = db.Categories.Where(x => x.Name == userLogin.Name).FirstOrDefault();
            if (userDetail == null) return new HttpNotFoundResult("optional description");
            return View();
        }
    }
}