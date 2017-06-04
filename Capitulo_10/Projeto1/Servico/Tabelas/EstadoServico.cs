using Modelo.Tabelas;
using Persistencia.DAL.Tabelas;
using System.Linq;

namespace Servico.Tabelas
{
    public class EstadoServico
    {
        private EstadoDAL estadoDAL = new EstadoDAL();

        public IQueryable<Estado> ObterEstadosClassificadosPorNome()
        {
            return estadoDAL.ObterEstadosClassificadosPorNome();
        }
    }
}
