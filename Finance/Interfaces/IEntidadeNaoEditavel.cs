using System;

namespace Finance.Interfaces
{
    public interface IEntidadeNaoEditavel
    {
        DateTime DataCriacao { get; set; }
        String UsuarioCriacao { get; set; }
    }
}