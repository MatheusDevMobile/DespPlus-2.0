using System;
using PowerDev.Enterprise.Eureka.Interfaces;

namespace PowerDev.Enterprise.Eureka.Utils
{
    public static class ConfigUtils
    {
        public static IGestorConfiguracao GestorConfiguracao { get; set; }
        public static String ObterStrConfig(String chave)
        {
            return GestorConfiguracao?.ObterConfiguracao(chave);
        }

        public static int ObterIntConfig(String chave)
        {
            return ConverteUtils.SempreConverteInt32(GestorConfiguracao?.ObterConfiguracao(chave));
        }

        public static bool ObterBoolConfig(String chave)
        {
            return ConverteUtils.SempreConverteBoleano(GestorConfiguracao?.ObterConfiguracao(chave));
        }
    }
}