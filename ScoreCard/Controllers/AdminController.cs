using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoreCard.Models;
using System.Runtime.InteropServices;

namespace ScoreCard.Controllers
{
    public class AdminController : BaseController
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Workers()
        {
            Workers p = new Workers();

            using (scoreDB db = new scoreDB())
            {
                p.list = db.Fetch<Worker>(Models.Worker._workers);
                p.IsAdmin = _worker.IsAdmin;
            }
            return View(p);
        }

        public ActionResult Worker(int id)
        {
            WorkerView w = new WorkerView(id);
            return View(w);
        }

        public ActionResult Impersonate(int id)
        {
            if (id > 0)
            {
                Session["admin"] = Session["user"];
                using (scoreDB db = new scoreDB())
                {
                    var user = db.FirstOrDefault<Worker>("where WorkerId = @0", id);
                    if (user == null)
                    {
                        Session.Remove("admin");
                        return RedirectToAction("Workers", "Admin");
                    }
                    if (string.IsNullOrWhiteSpace(user.IonName)) 
                        Session["user"] = user.WorkerId.ToString();
                    else if (Session["user"].ToString().Contains(user.IonName))     // joker
                        Session.Remove("admin");
                    else
                        Session["user"] = user.WorkerId.ToString();
                }
            }
            else
            {
                Session["user"] = Session["admin"];
                Session.Remove("admin");
            }
            return RedirectToAction("Index", "Home");    
        }

        [HttpPost]
        public ActionResult SaveWorker(WorkerView wv)
        {
            wv.w.Save();
            return RedirectToAction("Workers");
        }

        public ActionResult Lines()
        {
            Lines p = new Lines();

            using (scoreDB db = new scoreDB())
            {
                p.list = db.Fetch<Line>(Models.Line._lines);
                p.IsAdmin = _worker.IsAdmin;
            }
            return View(p);
        }
    }
}
