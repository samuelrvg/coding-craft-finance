using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finance.Models
{
    public class Conta
    {
        [Key]
        public int ContaId { get; set; }
        public int BancoId { get; set; }
        public String UsuarioId { get; set; }

        [Required]
        [StringLength(300)]
        public String Nome { get; set; }

        [Display(Name = "Descrição")]
        [DataType(DataType.MultilineText)]
        public String Descricao { get; set; }

        [Display(Name = "Saldo Atual")]
        [DataType(DataType.Currency)]
        public decimal SaldoAtual { get; set; }

        public virtual Banco Banco { get; set; }
        public virtual Usuario Usuario { get; set; }

        [InverseProperty(nameof(Transferencia.ContaOrigem))]
        public virtual ICollection<Transferencia> TransferenciasComoOrigem { get; set; }
        [InverseProperty(nameof(Transferencia.ContaDestino))]
        public virtual ICollection<Transferencia> TransferenciasComoDestino { get; set; }

        public virtual ICollection<Receita> Receitas { get; set; }
    }
}