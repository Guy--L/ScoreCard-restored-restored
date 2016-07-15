using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoreCard.Models;
using System.Diagnostics;

namespace ScoreCard.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Year(int year)
        {
            Session["year"] = year;
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            scoreDB s = new scoreDB();

            string usr = _user;

            if (string.IsNullOrWhiteSpace(usr))
                return RedirectToAction("Contact");

            Worker emp = Session["worker"] as Worker;
            if (emp.IonName == null)
                return RedirectToAction(Setting.AllowCorrection?"ContactCorrection":"Contact");

            int? year = Session["year"] as int?;
            int? oldyear = TempData["oldyear"] as int?;

            if (oldyear.HasValue)
                year = oldyear;

            year = year.HasValue ? year.Value : DateTime.Now.AddMonths(6).Year;
            Session["year"] = year;
            Session["fyear"] = string.Format("{0}-{1}", year%100 - 1, year%100);

            var card = new Card(year.Value, emp);
            if (Line.NoScores)
            {
                TempData["Year"] = year;
                if (_worker.IsAdmin)
                    return RedirectToAction("AddYear", "Admin", new { id = year.Value });
                return RedirectToAction("AskAdmin", "Home");
            }
            Session["oldyear"] = year;
            TempData["Card"] = card;
            return View(card);
        }

        public ActionResult EditScore(int id)
        {
            var card = TempData["Card"] as Card;

            if (card == null)
                return RedirectToAction("Index", "Home");

            var score = new ScoreView(card, id);
            return View(score);
        }

        [HttpPost]
        public ActionResult SaveScore(ScoreView sv)
        {
            sv.Save();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult AskAdmin()
        {
            int? year = TempData["year"] as int?;
            return View(year);
        }

        public ActionResult ContactCorrection()
        {
            string[] worker = _user.ToString().Split('\\');
            var user = worker[worker.Length - 1];
            user = user.Substring(0, user.IndexOf('.'));

            List<Worker> w = Worker.Candidates(user);
            if (w == null || w.Count == 0)
                RedirectToAction("Contact");

            return View(w);
        }

        [HttpPost]
        public ActionResult Correction(int workerid)
        {
            Worker w = null;
            string[] worker = _user.ToString().Split('\\');

            using (scoreDB s = new scoreDB())
            {
                w = s.Fetch<Worker>(" where workerid = @0", workerid).SingleOrDefault();
                if (w == null)
                    RedirectToAction("Contact");
                w.IonName = worker[worker.Length - 1];
                w.Update();
            }
            Session["worker"] = w;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Please contact your administrator to request access to this site.";

            return View();
        }

        public ActionResult About()
        {
            throw new Exception("Test Error");
            return View();
        }
    }
}