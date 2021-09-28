using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PowerDev.Enterprise.Eureka.Interfaces;

namespace PowerDev.Enterprise.Eureka.Filtros
{
    public abstract class FiltroBaseAbstrato<T> : IFiltroBase<T>
    {
        protected internal Dictionary<String, Expression<Func<T, bool>>> Criterios { get; }
        protected internal Dictionary<String, Expression<Func<T, bool>>> Condicoes { get; }

        protected FiltroBaseAbstrato()
        {
            Criterio = Activator.CreateInstance<T>();
            Criterios = new Dictionary<string, Expression<Func<T, bool>>>();
            Condicoes = new Dictionary<string, Expression<Func<T, bool>>>();
        }

        public virtual T Criterio { get; set; }

        public virtual FiltroOpcoes<T> AdicionarRegraSimples(Expression<Func<T, bool>> criterio)
        {
            var regra = Guid.NewGuid().ToString();
            var r = new FiltroOpcoes<T>(regra, Condicoes);
            Criterios.Add(regra, criterio);
            return r;
        }
        public virtual IQueryable<T> ObterFiltro(IQueryable<T> lista)
        {
            foreach (var k in Criterios.Keys)
            {
                if (Condicoes.ContainsKey(k))
                {
                    if (Condicoes[k].Compile().Invoke(Criterio))
                        lista = lista.Where(Criterios[k]);
                }
                else
                    lista = lista.Where(Criterios[k]);
            }
            return lista;
        }
        public virtual bool Satifaz(T entidade)
        {
            var lista = new List<T> { entidade }.AsQueryable();
            var satifaz = ObterFiltro(lista).Any();
            return satifaz;
        }
    }
}
