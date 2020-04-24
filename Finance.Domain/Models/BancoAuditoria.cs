using Finance.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Models
{
    [Table(name:"BancoAuditoria")]
    public class BancoAuditoria : IEntidadeNaoEditavel
    {
        [Key]
        public int BancoAuditoriaId { get; set; }

        public int BancoId { get; set; }

        [Required]
        [Index("IUQ_BancosAuditoria_Nome")]
        [StringLength(200)]
        public String Nome { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public string UsuarioCriacao { get; set; }

        public virtual ICollection<Conta> Contas { get; set; }
    }
}