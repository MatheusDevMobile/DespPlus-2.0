using System;

namespace PowerDev.Enterprise.Eureka.Utils
{
    public class ChaveValor
    {
        public String Descricao { get; set; }
        public String Valor { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Equals((ChaveValor)obj);
        }

        public bool Equals(ChaveValor obj)
        {
            return obj.Valor == Valor;
        }
        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }
    }
}
