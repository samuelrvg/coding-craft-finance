using System;

namespace Finance.Interfaces
{
    public interface IEntidade : IEntidadeNaoEditavel
    {
        DateTime UltimaModificacao { get; set; }
        string UsuarioModificacao { get; set; }
    }
}