using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finance.Models
{
    [Table("Bancos")]
    public class Banco
    {
        [Key]
        public int BancoId { get; set; }

        [Required]
        [Index("IUQ_Bancos_Nome")]
        [StringLength(200)]
        public String Nome { get; set; }

        public virtual ICollection<Conta> Contas { get; set; }
    }
}