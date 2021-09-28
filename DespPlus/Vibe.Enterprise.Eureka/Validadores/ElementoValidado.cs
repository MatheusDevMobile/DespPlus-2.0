using System;

namespace PowerDev.Enterprise.Eureka.Validadores
{
    public class ElementoValidado
    {
        public ElementoValidado(bool valido, string mensagem)
        {
            Invalido = valido;
            Mensagem = mensagem;
        }
        public bool Invalido { get; }
        public string Mensagem { get; }
    }
}
