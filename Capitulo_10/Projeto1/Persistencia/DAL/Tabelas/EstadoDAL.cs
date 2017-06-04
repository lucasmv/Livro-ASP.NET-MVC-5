using Modelo.Tabelas;
using Persistencia.Contexts;
using System.Linq;

namespace Persistencia.DAL.Tabelas
{
    public class EstadoDAL
    {
        private EFContext context = new EFContext();

        public IQueryable<Estado> ObterEstadosClassificadosPorNome()
        {
            return context.Estados.OrderBy(b => b.Nome);
        }
    }
}
