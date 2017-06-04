using Modelo.Tabelas;
using Persistencia.DAL.Tabelas;
using System.Linq;

namespace Servico.Tabelas
{
    public class CidadeServico
    {
        private CidadeDAL cidadeDAL = new CidadeDAL();

        public IQueryable<Cidade> ObterCidadesPorEstado(long? estadoID)
        {
            return cidadeDAL.ObterCidadesPorEstado(estadoID);
        }
    }
}
