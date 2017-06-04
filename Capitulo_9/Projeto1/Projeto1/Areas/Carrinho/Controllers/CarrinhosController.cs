using Modelo.Carrinho;
using Servico.Cadastros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Projeto1.Areas.Carrinho.Controllers
{
    public class CarrinhosController : Controller
    {
        private ProdutoServico produtoServico = new ProdutoServico();

        public ActionResult Create()
        {
            IEnumerable<ItemCarrinho> carrinho = HttpContext.Session["carrinho"] as IEnumerable<ItemCarrinho>;

            if (carrinho == null)
            {
                carrinho = new List<ItemCarrinho>();
                HttpContext.Session["carrinho"] = carrinho;
            }

            return View(carrinho);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduto(FormCollection collection)
        {
            var carrinho = HttpContext.Session["carrinho"] as List<ItemCarrinho>;

            if (carrinho == null)
                carrinho = new List<ItemCarrinho>();

            if (string.IsNullOrEmpty(collection.Get("idproduto")))
                return RedirectToAction("Create");

            var produto = produtoServico.ObterProdutoPorId(Convert.ToInt32(collection.Get("idproduto")));


            var produtoNoCarrinho = carrinho.Where(x => x.Produto.ProdutoId == produto.ProdutoId).FirstOrDefault();

            if (produtoNoCarrinho != null)
                produtoNoCarrinho.Quantidade += 1;
            else
            {

                var itemCarrinho = new ItemCarrinho()
                {
                    Produto = produto,
                    Quantidade = 1,
                    ValorUnitario = produto.ValorUnitario
                };

                carrinho.Add(itemCarrinho);
            }

            HttpContext.Session["carrinho"] = carrinho;

            return RedirectToAction("Create");
        }
    }
}