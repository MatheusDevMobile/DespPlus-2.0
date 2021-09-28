using System;

namespace PowerDev.Enterprise.Eureka
{
    public class EurekaExcecao : Exception
    {
        public EurekaExcecao(string msg, Exception inException) : base(msg, inException)
        {

        }
    }
}