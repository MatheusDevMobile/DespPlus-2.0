using System;
using PowerDev.Enterprise.Eureka.Interfaces;
using PowerDev.Enterprise.Eureka.Utils;

namespace PowerDev.Enterprise.Eureka.Extensoes
{
    public static class GestorConfiguracaoExtensao
    {
        public static int ObterConfiguracaoComoInt(this IGestorConfiguracao configuracao, String chave)
        {
            if (configuracao == null)
                throw new NullReferenceException(String.Format("{0} não pode ser nulo.", nameof(IGestorConfiguracao)));
            return ConverteUtils.SempreConverteInt32(configuracao?.ObterConfiguracao(chave));
        }
        public static bool ObterConfiguracaoComoBool(this IGestorConfiguracao configuracao, String chave)
        {
            if (configuracao == null)
                throw new NullReferenceException(String.Format("{0} não pode ser nulo.", nameof(IGestorConfiguracao)));
            return ConverteUtils.SempreConverteBoleano(configuracao?.ObterConfiguracao(chave));
        }
    }
}