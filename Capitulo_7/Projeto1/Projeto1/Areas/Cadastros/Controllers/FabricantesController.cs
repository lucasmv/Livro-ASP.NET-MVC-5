using Modelo.Cadastros;
using Servico.Cadastros;
using System.Net;
using System.Web.Mvc;

namespace Projeto1.Areas.Cadastros.Controllers
{
    [Authorize]
    public class FabricantesController : Controller
    {

        private FabricanteServico fabricanteServico = new FabricanteServico();

        private ActionResult ObterVisaoFabricantePorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var fabricante = fabricanteServico.ObterFabricantePorId((long)id);

            if (fabricante == null)
                return HttpNotFound();

            return View(fabricante);
        }

        private ActionResult GravarFabricante(Fabricante fabricante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    fabricanteServico.GravarFabricante(fabricante);
                    return RedirectToAction("Index");
                }
                return View(fabricante);
            }
            catch
            {
                return View(fabricante);
            }
        }

        public ActionResult Index()
        {
            return View(fabricanteServico.ObterFabricantesClassificadosPorNome());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fabricante fabricante)
        {
            return GravarFabricante(fabricante);
        }

        public ActionResult Edit(long? id)
        {
            return ObterVisaoFabricantePorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Fabricante fabricante)
        {
            return GravarFabricante(fabricante);
        }

        public ActionResult Details(long? id)
        {
            return ObterVisaoFabricantePorId(id);
        }

        public ActionResult Delete(long? id)
        {
            return ObterVisaoFabricantePorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            var fabricante = fabricanteServico.EliminarFabricantePorId(id);

            TempData["Message"] = "Fabricante " + fabricante.Nome.ToUpper() + " foi removido";

            return RedirectToAction("Index");
        }
    }
}