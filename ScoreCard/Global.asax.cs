using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ScoreCard.Models;
using TwitterBootstrapMVC;
using ScoreCard.Controllers;
using ScoreCard.Properties;

namespace ScoreCard
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Bootstrap.Configure();
            BaseController.version = Resources.Version;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
#if DEBUG
            var user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            if (user == "GUYLISTER3546\\guy" || user == "NETFDOMAIN\\lister")
                user = "lister.g.1";
#else
            var user = Thread.CurrentPrincipal.Identity.Name;
#endif
            //scheduleDB _db = new scheduleDB();
            HttpContext.Current.Session["user"] = user;
            var yr = DateTime.Now.AddMonths(6).Year;
            HttpContext.Current.Session["year"] = yr;
            HttpContext.Current.Session["fyear"] = string.Format("{0}-{1}", yr % 100 - 1, yr % 100);
            string[] worker = user.ToString().Split('\\');
            user = worker[worker.Length - 1];

            using (scoreDB s = new scoreDB())
            {
                Score.yearsready = s.Fetch<int>("select distinct yearending from score order by yearending");
            }

                Worker emp = new Worker(user);
            HttpContext.Current.Session["worker"] = emp;

            //HttpContext.Current.Session["authority"] = _db.Fetch<User>(string.Format(Models.User.get_role, user)).FirstOrDefault();
        }
    }
}
