using System;

namespace Finance.Interfaces
{
    public interface IEntidade<TAuditoria> : IEntidadeNaoEditavel
        where TAuditoria : class
    {
        DateTime UltimaModificacao { get; set; }
        string UsuarioModificacao { get; set; }
    }
}