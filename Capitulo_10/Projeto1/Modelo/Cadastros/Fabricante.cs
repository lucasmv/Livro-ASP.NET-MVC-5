using Modelo.Tabelas;
using System.Collections.Generic;

namespace Modelo.Cadastros
{
    public class Fabricante
    {
        public long? FabricanteId { get; set; }
        public string Nome { get; set; }

        public long? EstadoID { get; set; }
        public long? CidadeID { get; set; }

        public virtual Cidade Cidade { get; set; }
        public virtual Estado Estado { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}