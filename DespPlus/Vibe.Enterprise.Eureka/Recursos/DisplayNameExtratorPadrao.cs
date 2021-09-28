using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using PowerDev.Enterprise.Eureka.Extensoes;
using PowerDev.Enterprise.Eureka.Interfaces;

namespace PowerDev.Enterprise.Eureka.Recursos
{
    public class DisplayNameExtratorPadrao : IDisplayNameExtrator
    {
        private static readonly ConcurrentDictionary<String, String> cache = new ConcurrentDictionary<string, string>();
        private IMapper Mapper { get; }
        public DisplayNameExtratorPadrao(IMapper mapper)
        {
            Mapper = mapper;
        }

        public string ObterDisplay(object obj, string propNome)
        {
            return ObterDisplay(obj.GetType(), propNome);
        }
        public string ObterDisplay(Type tipo, string propNome)
        {
            return ObterDisplay(tipo, tipo.ObterTypeSemProxy().GetRuntimeProperty(propNome)); //.DeclaredMembers.FirstOrDefault(e => e.Name == propNome));
        }
        public string ObterDisplay(object obj, MemberInfo membro)
        {
            return ObterDisplay(obj.GetType(), membro);
        }
        public string ObterDisplay<TSource, TProperty>(TSource tipo, Expression<Func<TSource, TProperty>> prop)
        {
            return ObterDisplay(tipo, tipo.ObterPropertyInfo(prop));
        }
        public string ObterDisplay(Type tipo, MemberInfo membro)
        {
            if (membro == null)
                return null;

            var chave = string.Format("{0}{1}", tipo.Name, membro.Name);
            if (cache.TryGetValue(chave, out var saida))
                return saida;

            var displayName = membro.GetCustomAttribute<DisplayNameAttribute>();
            if (displayName != null && !displayName.DisplayName.LimpoNuloBranco())
                return cache.GetOrAdd(chave, displayName.DisplayName);  //displayName.DisplayName;

            if (displayName != null && displayName.DisplayName.LimpoNuloBranco())
            {
                var nome = string.Format(@"{0}{1}", tipo.ObterTypeInfoSemProxy().Name, membro.Name);
                var recurso = ConfiguracaoRecursos.RecursosConfigurados.FirstOrDefault(r => r.GetRuntimeProperty(nome) != null);

                if (recurso == null)
                {
                    nome = string.Format(@"{0}{1}", tipo.ObterTypeInfoSemProxy()?.BaseType?.Name, membro.Name);
                    recurso = ConfiguracaoRecursos.RecursosConfigurados.FirstOrDefault(r => r.GetRuntimeProperty(nome) != null);
                }
                if (recurso != null)
                {
                    var propriedade = recurso.GetRuntimeProperty(nome);
                    return cache.GetOrAdd(chave, (string) propriedade.GetValue(recurso, null)); //(string)propriedade.GetValue(recurso, null);
                }
                //foreach (var propriedade in ConfiguracaoRecursos.RecursosConfigurados.Select(r => r.GetRuntimeProperty(string.Format(@"{0}{1}", tipo.obterTypeInfoSemProxy().Name, membro.Name))).Where(propriedade => propriedade != null))
                //{
                //    return (string)propriedade.GetValue(propriedade.DeclaringType, null);
                //}
            }

            TypeMap[] maps = Mapper.ConfigurationProvider.GetAllTypeMaps();
            foreach (var propriedade in maps.Where(m => m.DestinationType == tipo.ObterTypeSemProxy()).SelectMany(tm => ConfiguracaoRecursos.RecursosConfigurados.Select(r => r.GetRuntimeProperty(string.Format(@"{0}{1}", tm.SourceType.Name, membro.Name))).Where(propriedade => propriedade != null)))
            {
                return cache.GetOrAdd(chave, (string)propriedade.GetValue(propriedade.DeclaringType, null)); //(string) propriedade.GetValue(propriedade.DeclaringType, null);
            }
            foreach (var propriedade in maps.Where(m => m.DestinationType == tipo.ObterTypeSemProxy()).SelectMany(tm => ConfiguracaoRecursos.RecursosConfigurados.Select(r => r.GetRuntimeProperty(string.Format(@"{0}{1}", tm.SourceType?.BaseType?.Name, membro.Name))).Where(propriedade => propriedade != null)))
            {
                return cache.GetOrAdd(chave, (string)propriedade.GetValue(propriedade.DeclaringType, null)); //(string) propriedade.GetValue(propriedade.DeclaringType, null);
            }
            return cache.GetOrAdd(chave, displayName == null || displayName.DisplayName.LimpoNuloBranco() ? membro.Name.CamelCaseParaNormal() : displayName.DisplayName); //displayName == null || displayName.DisplayName.limpoNuloBranco() ? membro.Name.separarCamelCase() : displayName.DisplayName; 
        }
    }
}