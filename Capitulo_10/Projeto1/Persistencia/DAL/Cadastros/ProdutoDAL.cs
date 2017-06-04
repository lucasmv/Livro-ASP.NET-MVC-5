using Modelo.Cadastros;
using Persistencia.Contexts;
using System.Collections;
using System.Data.Entity;
using System.Linq;

namespace Persistencia.DAL.Cadastros
{
    public class ProdutoDAL
    {
        private EFContext context = new EFContext();

        public IQueryable ObterProdutosClassificadosPorNome()
        {
            return context.Produtos
                .Include(c => c.Categoria)
                .Include(f => f.Fabricante).OrderBy(n => n.Nome);
        }
        public Produto ObterProdutoPorId(long id)
        {
            return context.Produtos
                .Where(p => p.ProdutoId == id)
                .Include(c => c.Categoria)
                .Include(f => f.Fabricante)
                .FirstOrDefault();
        }
        public void GravarProduto(Produto produto)
        {
            if (produto.ProdutoId == null)
                context.Produtos.Add(produto);
            else
                context.Entry(produto).State = EntityState.Modified;

            context.SaveChanges();
        }
        public Produto EliminarProdutoPorId(long id)
        {
            Produto produto = ObterProdutoPorId(id);
            context.Produtos.Remove(produto);
            context.SaveChanges();
            return produto;
        }
        public IList ObterProdutosPorNome(string param)
        {
            var p = from produto in context.Produtos
                    where 
                        produto.Nome.ToUpper().StartsWith(param.ToUpper())
                    orderby (produto.Nome)
                    select new
                    {
                        id = produto.ProdutoId,
                        label = produto.Nome,
                        value = produto.Nome
                    };

            return p.ToList();
        }
    }
}
