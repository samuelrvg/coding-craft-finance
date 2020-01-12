using System;

namespace Finance.Interfaces
{
    public interface IEntidadeNaoEditavel
    {
        DateTime DataCriacao { get; set; }
        string UsuarioCriacao { get; set; }
    }
}