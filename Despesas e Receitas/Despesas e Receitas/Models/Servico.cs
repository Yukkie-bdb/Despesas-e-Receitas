using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Despesas_e_Receitas.Models
{
    public class Servico
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        [DisplayName("Descrição")]
        public string? Descricao { get; set; }
        public char Categoria { get; set; }

        [Range(1, 999999, ErrorMessage = "Valor Obrigatorio entre 1 e 1 Milhão!")]

        public decimal Valor { get; set; }
        public char CapexOpex { get; set; }
    }
}
