using Modelo.Tabelas;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Modelo.Cadastros
{
    public class Fabricante
    {
        [DisplayName("Id")]
        public long? FabricanteId { get; set; }
        public string Nome { get; set; }

        public long? EstadoID { get; set; }
        public long? CidadeID { get; set; }

        public virtual Cidade Cidade { get; set; }
        public virtual Estado Estado { get; set; }

        [DisplayName("Tipo Pessoa")]
        [Required(ErrorMessage = "Informe o tipo de possoa")]
        public string TipoPessoa { get; set; }

        [DisplayName("Esta Ativo")]
        public bool EstaAtivo { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}