using Modelo.Cadastros;
using Servico.Cadastros;
using Servico.Tabelas;
using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projeto1.Areas.Cadastros.Controllers
{
    [Authorize]
    public class ProdutosController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();
        private CategoriaServico categoriaServico = new CategoriaServico();
        private FabricanteServico fabricanteServico = new FabricanteServico();

        private ActionResult ObterVisaoProdutoPorId(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Produto produto = produtoServico.ObterProdutoPorId((long)id);

            if (produto == null)
                return HttpNotFound();

            return View(produto);
        }

        private void PopularViewBag(Produto produto = null)
        {
            if (produto == null)
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome");
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome");
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(categoriaServico.ObterCategoriasClassificadasPorNome(), "CategoriaId", "Nome", produto.CategoriaId);
                ViewBag.FabricanteId = new SelectList(fabricanteServico.ObterFabricantesClassificadosPorNome(), "FabricanteId", "Nome", produto.FabricanteId);
            }
        }

        private byte[] SetLogotipo(HttpPostedFileBase logotipo)
        {
            var bytesLogotipo = new byte[logotipo.ContentLength];
            logotipo.InputStream.Read(bytesLogotipo, 0, logotipo.ContentLength);
            return bytesLogotipo;
        }

        private ActionResult GravarProduto(Produto produto, HttpPostedFileBase imagem, string chkRemoverImagem)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (chkRemoverImagem != null)
                        produto.Logotipo = null;

                    if (imagem != null)
                    {
                        produto.LogotipoMimeType = imagem.ContentType;
                        produto.Logotipo = SetLogotipo(imagem);
                        produto.NomeArquivo = imagem.FileName;
                        produto.TamanhoArquivo = imagem.ContentLength;
                    }

                    produtoServico.GravarProduto(produto);
                    return RedirectToAction("Index");
                }

                PopularViewBag(produto);
                return View(produto);
            }
            catch
            {
                PopularViewBag(produto);
                return View(produto);
            }
        }


        public ActionResult Index()
        {
            return View(produtoServico.ObterProdutosClassificadosPorNome());
        }

        public ActionResult Details(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        public ActionResult Create()
        {
            PopularViewBag();
            return View(new Produto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Produto produto, HttpPostedFileBase imagem = null)
        {
            return GravarProduto(produto, imagem, null);
        }

        public ActionResult Edit(long? id)
        {
            PopularViewBag(produtoServico.ObterProdutoPorId((long)id));
            return ObterVisaoProdutoPorId(id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Produto produto, HttpPostedFileBase imagem = null, string chkRemoverImagem = null)
        {
            return GravarProduto(produto, imagem, chkRemoverImagem);
        }

        public ActionResult Delete(long? id)
        {
            return ObterVisaoProdutoPorId(id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                Produto produto = produtoServico.EliminarProdutoPorId(id);

                TempData["Message"] = "Produto " + produto.Nome.ToUpper() + " foi removido";

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public FileContentResult GetLogotipo(long id)
        {
            Produto produto = produtoServico.ObterProdutoPorId(id);

            if (produto != null)
                return File(produto.Logotipo, produto.LogotipoMimeType);
           
            return null;
        }

        public FileResult DownloadArquivo(long id)
        {
            Produto produto = produtoServico.ObterProdutoPorId(id);

            FileStream fileStream = new FileStream(Server.MapPath("~/TempData/" + produto.NomeArquivo), FileMode.Create, FileAccess.Write);
            fileStream.Write(produto.Logotipo, 0, Convert.ToInt32(produto.TamanhoArquivo));
            fileStream.Close();
            return File(fileStream.Name, produto.LogotipoMimeType, produto.NomeArquivo);
        }

        public JsonResult GetProdutosPorNome(string param)
        {
            var p = produtoServico.ObterProdutosPorNome(param);
            return Json(p, JsonRequestBehavior.AllowGet);
        }

    }
}
