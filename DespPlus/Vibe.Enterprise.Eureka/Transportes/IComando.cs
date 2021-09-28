using System;

namespace PowerDev.Enterprise.Eureka.Transportes
{
    public interface IComando
    {
        String Id { get; }
        DateTime DataHora { get; }
    }
}