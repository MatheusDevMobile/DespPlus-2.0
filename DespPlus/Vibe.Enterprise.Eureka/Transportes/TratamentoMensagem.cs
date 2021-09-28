using System;

namespace PowerDev.Enterprise.Eureka.Transportes
{
    public static class TratamentoMensagem
    {
        public static T DeserializarComando<T>(String msg) where T : ComandoBase
        {
            var r = ComandoBase.Reconstruir<T>(msg);
            return r;
        }
    }
}