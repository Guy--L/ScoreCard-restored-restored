using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoreCard.Models;

namespace ScoreCard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var card = new Card(2015);
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
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}