using Projeto1.Contexts;
using Projeto1.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Projeto1.Controllers
{
    public class ProdutosController : Controller
    {
        private EFContext db = new EFContext();

        public ActionResult Index()
        {
            var produtos = db.Produtos.Include(p => p.Categoria).Include(p => p.Fabricante).OrderBy(x => x.Nome);

            return View(produtos);
        }

        public ActionResult Details(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Produto produto = db.Produtos.Where(p => p.ProdutoId == id).Include(c => c.Categoria).Include(f => f.Fabricante).First();

            if (produto == null)
                return HttpNotFound();

            return View(produto);
        }

        public ActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome");
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "FabricanteId", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produto produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Produtos.Add(produto);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

                ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(db.Fabricantes, "FabricanteId", "Nome", produto.FabricanteId);

                return View(produto);
            }
            catch (System.Exception)
            {
                ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(db.Fabricantes, "FabricanteId", "Nome", produto.FabricanteId);

                return View(produto);
            }

        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Produto produto = db.Produtos.Find(id);

            if (produto == null)
                return HttpNotFound();

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "FabricanteId", "Nome", produto.FabricanteId);

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produto produto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produto).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "CategoriaId", "Nome", produto.CategoriaId);
            ViewBag.FabricanteId = new SelectList(db.Fabricantes, "FabricanteId", "Nome", produto.FabricanteId);

            return View(produto);
        }

        public ActionResult Delete(long? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Produto produto = db.Produtos.Where(p => p.ProdutoId == id).Include(c => c.Categoria).Include(f => f.Fabricante).First();

            if (produto == null)
                return HttpNotFound();

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Produto produto = db.Produtos.Find(id);

            db.Produtos.Remove(produto);
            db.SaveChanges();

            TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi removido";

            return RedirectToAction("Index");
        }

    }
}
