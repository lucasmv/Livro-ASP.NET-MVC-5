using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Projeto1.Areas.Seguranca.Models;
using Projeto1.Infraestrutura;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projeto1.Areas.Seguranca.Controllers
{
    [Authorize]
    public class RoleAdminController : Controller
    {
        private GerenciadorRole RoleManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorRole>();
            }
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
                ModelState.AddModelError("", error);
        }

        private GerenciadorUsuario UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<GerenciadorUsuario>();
            }
        }

        public ActionResult Index()
        {

            var roles = RoleManager.Roles.ToList();

            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Required]string nome)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = RoleManager.Create(new Role(nome));

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    AddErrorsFromResult(result);
            }

            return View(nome);
        }

        [Authorize(Roles = "Administradores")]
        public ActionResult Edit(string id)
        {
            Role role = RoleManager.FindById(id);

            string[] memberIDs = role.Users.Select(x => x.UserId).ToArray();
            IEnumerable<Usuario> membros = UserManager.Users.Where(x => memberIDs.Any(y => y == x.Id));
            IEnumerable<Usuario> naoMembros = UserManager.Users.Except(membros);

            return View(new RoleEditModel
            {
                Role = role,
                Membros = membros,
                NaoMembros = naoMembros
            });
        }

        [Authorize(Roles = "Administradores")]
        [HttpPost]
        public ActionResult Edit(RoleModificationModel model)
        {
            IdentityResult result;

            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsParaAdicionar ?? new string[] { })
                {
                    result = UserManager.AddToRole(userId, model.NomePapel);

                    if (!result.Succeeded)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                }
                foreach (string userId in model.IdsParaRemover ?? new string[] { })
                {
                    result = UserManager.RemoveFromRole(userId, model.NomePapel);

                    if (!result.Succeeded)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}