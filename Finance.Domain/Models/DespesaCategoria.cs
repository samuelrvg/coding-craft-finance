using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finance.Models
{
    public class DespesaCategoria
    {
        [Key]
        public int DespesaCategoriaId { get; set; }

        [Required]
        [StringLength(300)]
        [Index("IUQ_DespesaCategoria_Nome", IsUnique = true)]
        public String Nome { get; set; }

        public virtual ICollection<Despesa> Despesas { get; set; }
    }
}