using System.Web.Mvc;

namespace Projeto1.Controllers
{
    public class ErrorsController : Controller
    {
        public ActionResult General()
        {
            return View();
        }

        public ActionResult Http400()
        {
            return View();
        }

        public ActionResult Http404()
        {
            return View();
        }
    }
}