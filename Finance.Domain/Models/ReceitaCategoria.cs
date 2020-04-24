using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Models
{
    public class ReceitaCategoria
    {
        [Key]
        public int ReceitaCategoriaId { get; set; }

        [Required]
        [StringLength(300)]
        [Index("IUQ_ReceitaCategoria_Nome", IsUnique = true)]
        public String Nome { get; set; }

        public virtual ICollection<Receita> Receitas { get; set; }
    }
}