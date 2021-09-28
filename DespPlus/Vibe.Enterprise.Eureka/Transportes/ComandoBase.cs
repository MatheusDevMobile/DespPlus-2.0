using Newtonsoft.Json.Linq;
using System;

namespace PowerDev.Enterprise.Eureka.Transportes
{
    public abstract class ComandoBase : IComando
    {
        public static T Reconstruir<T>(String representacaoJson) where T : ComandoBase
        {
            var jObject = JObject.Parse(representacaoJson);
            if (!jObject.ContainsKey(nameof(Id)) || !jObject.ContainsKey(nameof(DataHora)))
                throw new Exception("Impossível remontar o Comando com Json passado.");
            var r = jObject.ToObject<T>();
            r.Id = jObject.Value<String>(nameof(Id));
            r.DataHora = jObject.Value<DateTime>(nameof(DataHora));
            return r;
        }

        protected ComandoBase()
        {
            Id = Guid.NewGuid().ToString();
            DataHora = DateTime.Now;
        }

        public string Id { get; protected set; }
        public DateTime DataHora { get; protected set; }
    }
}