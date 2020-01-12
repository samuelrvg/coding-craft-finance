using Finance.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;

namespace Finance.Models
{
    public class Auditoria : IEntidade
    {
        [Key]
        public int AuditoriaId { get; set; }
        public string EntidadeModificada { get; set; }
        public int IdentificadorEntidadeId { get; set; }
        public string NomeCampoModificado { get; set; }
        public string ValorAntigo { get; set; }
        public string ValorNovo { get; set; }
        public DateTime DataModificacao { get; set; }
        public string UsuarioModificacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public string UsuarioCriacao { get; set; }
        public DateTime UltimaModificacao { get; set; }
    }
}