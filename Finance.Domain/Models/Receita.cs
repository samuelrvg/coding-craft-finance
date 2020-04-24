using System;
using System.ComponentModel.DataAnnotations;

namespace Finance.Models
{
    public class Receita
    {
        [Key]
        public int ReceitaId { get; set; }
        public int ReceitaCategoriaId { get; set; }
        public int BancoId { get; set; }

        [Required]
        [StringLength(1024)]
        public String Descricao { get; set; }

        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        public virtual Banco Banco { get; set; }
        public virtual ReceitaCategoria ReceitaCategoria { get; set; }
    }
}