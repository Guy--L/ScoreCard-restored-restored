using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoreCard.Models;

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
                return RedirectToAction("Contact");

            var card = new Card((Session["year"] as int?), emp);
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

        public ActionResult Contact()
        {
            ViewBag.Message = "Please contact your administrator to request access to this site.";

            return View();
        }
    }
}