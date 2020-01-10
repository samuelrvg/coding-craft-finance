using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Finance.Models
{
    public class Despesa
    {
        [Key]
        public int DespesaId { get; set; }
        public int DespesaCategoriaId { get; set; }

        [Required]
        [StringLength(1024)]
        public String Descricao { get; set; }

        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        public virtual DespesaCategoria DespesaCategoria { get; set; }
    }
}