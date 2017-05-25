using Microsoft.AspNet.Identity.Owin;
using Projeto1.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto1.Areas.Seguranca.Controllers
{
    public class AdminController : Controller
    {
        private GerenciadorUsuario GerenciadorUsuario
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuario>();
            }
        }

        public ActionResult Index()
        {
            return View(GerenciadorUsuario.Users);
        }
    }
}