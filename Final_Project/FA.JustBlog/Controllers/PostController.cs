using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Services;

namespace FA.JustBlog.Controllers
{
    public class PostController : Controller
    {
        private JustBlogContext db = new JustBlogContext();
        PostServices postService = new PostServices();

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.Category);
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,PostContent,UrlSlug,Published,PostedOn,Modified,ViewCount,RateCount,TotalRate,CategoryID")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", post.CategoryID);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", post.CategoryID);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,PostContent,UrlSlug,Published,PostedOn,Modified,ViewCount,RateCount,TotalRate,CategoryID")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Id", "Name", post.CategoryID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult DetailPost(int id)
        {
            Post post = postService.FindPost(id);
            return View(post);
        }

        public ActionResult Details(int year, int month, string title)
        {
            var post = postService.FindPost(year, month, title);
            if (post == null) return HttpNotFound();
            return View(post);
        }

        public ActionResult lastgestPost()
        {
            return PartialView("PostList", postService.getLastestPost());
        }

        public ActionResult get3PostMostView()
        {
            return PartialView("PostList", postService.get3MostViewPost());
        }

        public ActionResult ListPostByCategory(int id)
        {
            return View("PostList", postService.getPostByCategory(id));
        }

        [HttpGet]
        public ActionResult SearchPost(string searchString)
        {
            var result = postService.GetAllPosts().Where(c => c.Title.Contains(searchString) || c.PostContent.Contains(searchString)).ToList();
            return View("Index", result);
        }
    }
}
