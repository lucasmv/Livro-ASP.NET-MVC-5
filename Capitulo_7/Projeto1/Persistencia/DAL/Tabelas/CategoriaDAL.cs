using Modelo.Tabelas;
using Persistencia.Contexts;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Persistencia.DAL.Tabelas
{
    public class CategoriaDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Categoria> ObterCategoriasClassificadasPorNome()
        {
            //return context.Categorias.OrderBy(b => b.Nome);

            return context.Database.SqlQuery<Categoria>("sp_GetAllCategorias").AsQueryable();
        }
        public Categoria ObterCategoriaPorId(long id)
        {
            //return context.Categorias.Where(x => x.CategoriaId == id).FirstOrDefault();

            var CategoriaId = new SqlParameter("@CategoriaId", id);

            return context.Database.SqlQuery<Categoria>("sp_GetCategoriaById @CategoriaId", CategoriaId).FirstOrDefault();
        }
        public void GravarCategoria(Categoria categoria)
        {
            //if (categoria.CategoriaId == null)
            //    context.Categorias.Add(categoria);
            //else
            //    context.Entry(categoria).State = EntityState.Modified;

            int categoriaId = categoria.CategoriaId.HasValue ? (int)categoria.CategoriaId.Value : 0;

            var id = new SqlParameter("@CategoriaId", categoriaId);
            var nome = new SqlParameter("@Nome", categoria.Nome);

            context.Database.ExecuteSqlCommand("execute sp_SalvarCategoria @CategoriaId, @Nome", id, nome);

            context.SaveChanges();

        }
        public Categoria EliminarCategoriaPorId(long id)
        {
            Categoria categoria = ObterCategoriaPorId(id);
            context.Categorias.Remove(categoria);
            context.SaveChanges();
            return categoria;
        }
    }
}
