using System;

namespace PowerDev.Enterprise.Eureka.Anotacoes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColunaAttribute : Attribute
    {
        public int Index { get; set; }
        public string PropriedadeLabel { get; set; }
        //        public bool Complexa { get; set; }

        /// <summary>
        /// Utilizado para exoprtar a entidade para excel.
        /// </summary>
        /// <param name="index">O index relacionado a ordem que a coluna ficará na planilha</param>
        public ColunaAttribute(int index = 0)
        {
            Index = index;
        }

        /// <summary>
        /// Utilizado para exoprtar a entidade para excel.
        /// </summary>
        /// <param name="index">O index relacionado a ordem que a coluna ficará na planilha</param>
        /// <param name="PropriedadeLabel">Caso a o nome da coluna seja o de outra propriedade preencha o nome da propriedade alvo.</param>
        public ColunaAttribute(int index = 0, string propriedadeLabel = null)
        {
            Index = index;
            PropriedadeLabel = propriedadeLabel;
        }
    }
}