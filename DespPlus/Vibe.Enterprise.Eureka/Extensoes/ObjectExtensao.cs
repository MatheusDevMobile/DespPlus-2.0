using PowerDev.Enterprise.Eureka;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace PowerDev.Enterprise.Eureka.Extensoes
{
    public static class ObjectExtensao
    {
        public static T ValorPropriedade<T>(this object obj, string propriedade)
        {
            try
            {
                return (T)obj.GetType().GetProperty(propriedade).GetValue(obj, null);
            }
            catch
            {
                return default(T);
            }
        }
        public static void SetaValorPropriedade(this object obj, string propriedade, object valor)
        {
            obj.GetType().GetProperty(propriedade).SetValue(obj, valor);
        }
        public static PropertyInfo ObterPropertyInfo<TSource, TProperty>(this object obj, Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(String.Format("Expressão '{0}', não se refere a uma propriedade.", propertyLambda));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(String.Format("Expressão '{0}', não se refere a uma propriedade.", propertyLambda));

            if (type != propInfo.PropertyType && !type.GetTypeInfo().IsSubclassOf(propInfo.PropertyType))
                throw new ArgumentException(String.Format("Expressão '{0}' se refere a uma propriedade que não é do tipo {1}.", propertyLambda, type));

            return propInfo;
        }
        public static List<PropertyInfo> ObterPropriedades(this Object obj)
        {
            return new List<PropertyInfo>(obj.GetType().GetRuntimeProperties());
        }
        /// <summary>
        /// Obtém os displays configurados nos resources ou utiliza o padrão caso não tenha.
        /// Importante!! Enterprise.Eureka.Configuracao.DisplayNameExtrator precisa estar setado para funcionar.
        /// </summary>
        /// <typeparam name="TModel">Tipo que contém a propriedade</typeparam>
        /// <typeparam name="TResult">Tipo da propriedade a se obter o display</typeparam>
        /// <param name="obj">Instância do tipo a se obter a propriedade</param>
        /// <param name="prop">Propriedade a se obter o display</param>
        /// <returns>Uma string formatada ou obtida dos resouces do projeto</returns>
        public static string ObterDisplay<TModel, TResult>(this TModel obj, Expression<Func<TModel, TResult>> prop)
        {
            return Configuracao.DisplayNameExtrator().ObterDisplay(obj, prop.ObterNomePropriedade());
        }
        public static bool NuloOuDefault<T>(this T obj)
        {
            return obj == null || EqualityComparer<T>.Default.Equals(obj, default(T));
        }
    }
}