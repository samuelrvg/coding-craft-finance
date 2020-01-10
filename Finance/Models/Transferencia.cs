using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Finance.Models
{
    public class Transferencia
    {
        [Key]
        public int TransferenciaId { get; set; }
        public int ContaOrigemId { get; set; }
        public int ContaDestinoId { get; set; }

        [DataType(DataType.Currency)]
        public decimal Valor { get; set; }

        [ForeignKey(nameof(ContaOrigemId))]
        public virtual Conta ContaOrigem { get; set; }
        [ForeignKey(nameof(ContaDestinoId))]
        public virtual Conta ContaDestino { get; set; }
    }
}