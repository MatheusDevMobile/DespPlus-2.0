using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerDev.Enterprise.Eureka.Validadores
{
    public class ValidacaoResultado
    {
        public List<ErroValidacao> Erros { get; set; }
        public bool Valido => Erros == null || Erros.Count == 0;
        public ElementoValidado this[string propriedade]
        {
            get
            {
                var e = Erros?.FirstOrDefault(er => TratarPropriedades(er, propriedade));
                return e == null ? new ElementoValidado(false, string.Empty) : new ElementoValidado(true, e.Mensagem);
            }
        }
        public void RemoveValidacao(string propriedade)
        {
            if (Erros == null || Erros.Count == 0)
                return;
            var indice = Erros.FindIndex(er => TratarPropriedades(er, propriedade));
            if (indice == -1)
                return;
            Erros.RemoveAt(indice);
        }
        public void LimpaValidacao()
        {
            if (Erros == null || Erros.Count == 0)
                return;
            Erros.Clear();
        }
        private static bool TratarPropriedades(ErroValidacao erroValidacao, string propriedade)
        {
            return erroValidacao.NomePropriedade == propriedade || erroValidacao.NomePropriedade.Replace(".", "_") == propriedade.Replace(".", "_");
        }
    }
}
