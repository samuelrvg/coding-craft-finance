using Finance.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Finance.Models
{
    [Table("Bancos")]
    public class Banco //: IEntidade<BancoAuditoria>
    {
        [Key]
        public int BancoId { get; set; }

        [Required]
        [Index("IUQ_Bancos_Nome")]
        [StringLength(200)]
        [Display(Name = "Banco")]
        public String Nome { get; set; }

        public virtual ICollection<Conta> Contas { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime UltimaModificacao { get; set; } = DateTime.Now;
        [StringLength(200)]
        public string UsuarioModificacao { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        [StringLength(200)]
        public string UsuarioCriacao { get; set; }
    }
}