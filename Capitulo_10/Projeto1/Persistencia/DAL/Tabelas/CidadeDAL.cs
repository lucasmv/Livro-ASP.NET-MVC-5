using Modelo.Tabelas;
using Persistencia.Contexts;
using System.Linq;

namespace Persistencia.DAL.Tabelas
{
    public class CidadeDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Cidade> ObterCidadesPorEstado(long? estadoID)
        {
            return context.Cidades.Where(c => c.EstadoID == estadoID).OrderBy(c => c.Nome);
        }
    }
}
