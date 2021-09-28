using System;

namespace PowerDev.Enterprise.Eureka.Interfaces
{
    public interface ICacheObjetos
    {
        void RegistrarOuAtualizarCache(String chave, object obj, TimeSpan tempo);
        object RecuperarCache(String chave);
        T RecuperarCache<T>(String chave);
        void RemoverCache(String chave);
    }
}