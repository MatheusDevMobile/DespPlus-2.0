using System;
using PowerDev.Enterprise.Eureka.Interfaces;
using System.Configuration;

namespace PowerDev.Enterprise.Eureka.Utils
{
    public class GestorConfiguracaoPadrao : IGestorConfiguracao
    {
        public GestorConfiguracaoPadrao()
        {
            ConfigUtils.GestorConfiguracao = this;
        }
        public string ObterConfiguracao(String chave)
        {
            var r = ConfigurationManager.AppSettings[chave];
            return r;
        }
    }
}