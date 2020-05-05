using System.Web.Mvc;

namespace Finance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult InkTemplate()
        {
            Session["Template"] = "_LayoutInk";
            return View("Index");
        }

        public ActionResult FoundationTemplate()
        {
            Session["Template"] = "_LayoutFoundation";
            return View("Index");
        }

        public ActionResult BootstrapTemplate()
        {
            Session["Template"] = "_Layout";
            return View("Index");
        }
    }
}