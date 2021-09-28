using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PowerDev.Enterprise.Eureka.Filtros
{
    public class FiltroOpcoes<T>
    {
        private readonly Dictionary<String, Expression<Func<T, bool>>> condicoes;
        private readonly String regra;
        public FiltroOpcoes(String regra, Dictionary<String, Expression<Func<T, bool>>> condicoes)
        {
            this.regra = regra;
            this.condicoes = condicoes;
        }

        public void SobCondicional(Expression<Func<T, bool>> condicao)
        {
            condicoes.Add(regra, condicao);
        }
    }
}