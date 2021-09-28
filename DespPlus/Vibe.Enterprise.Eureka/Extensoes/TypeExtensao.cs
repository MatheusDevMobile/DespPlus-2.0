using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using PowerDev.Enterprise.Eureka;
using PowerDev.Enterprise.Eureka.Utils;

namespace PowerDev.Enterprise.Eureka.Extensoes
{
    public static class TypeExtensao
    {
        private static readonly Type[] tiposPrimitivos = { typeof(DateTime), typeof(String), typeof(Decimal) };
        
        public static TAtributo ObterAnotacao<TAtributo>(this Type tipo) where TAtributo : Attribute
        {
            return tipo.GetCustomAttributes(typeof(TAtributo), true).FirstOrDefault() as TAtributo;
        }
        public static TAtributo ObterAnotacao<TAtributo, TRetorno>(this Type tipo, String metodo) where TAtributo : Attribute
        {
            return tipo.GetMethods().FirstOrDefault(a => a.Name == metodo && a.ReturnType == typeof(TRetorno))
                                    ?.GetCustomAttributes<TAtributo>(true)?.FirstOrDefault();
        }
        public static TAtributo ObterAnotacao<TAtributo>(this Type tipo, String metodo) where TAtributo : Attribute
        {
            //return tipo.GetMethod(metodo)?.GetCustomAttributes<TAtributo>(true)?.FirstOrDefault();
            return tipo.GetMethods().FirstOrDefault(a => a.Name == metodo)?.GetCustomAttributes<TAtributo>(true)?.FirstOrDefault();
        }
        public static TAtributo ObterAnotacao<TAtributo>(this MemberInfo propriedade) where TAtributo : Attribute
        {
            return propriedade.GetCustomAttributes<TAtributo>(true).FirstOrDefault();
        }
        public static TAtributo ObterAnotacao<TAtributo>(this PropertyInfo propriedade) where TAtributo : Attribute
        {
            return propriedade.GetCustomAttributes<TAtributo>(true).FirstOrDefault();
        }
        public static bool TemAnotacao<TAtributo>(this PropertyInfo propriedade) where TAtributo : Attribute
        {
            return propriedade.GetCustomAttributes<TAtributo>(true).Any();
        }
        public static bool TemAnotacao<TAtributo>(this Type tipo) where TAtributo : Attribute
        {
            return tipo.GetCustomAttributes(typeof(TAtributo), true).Any();
        }
        public static bool TemAnotacao<TAtributo>(this MemberInfo tipo) where TAtributo : Attribute
        {
            return tipo.GetCustomAttributes<TAtributo>(true).Any();
        }
        public static bool TemAnotacao<TAtributo>(this Type tipo, String metodo) where TAtributo : Attribute
        {
            //return tipo.GetMethod(metodo)?.GetCustomAttributes<TAtributo>(true)?.Any() ?? false;
            return tipo.GetMethods().FirstOrDefault(a => a.Name == metodo)?.GetCustomAttributes<TAtributo>(true)?.Any() ?? false;
        }
        public static bool TemAnotacao<TAtributo, TRetorno>(this Type tipo, String metodo) where TAtributo : Attribute
        {
            return tipo.GetMethods().FirstOrDefault(a => a.Name == metodo && a.ReturnType == typeof(TRetorno))
                                    ?.GetCustomAttributes<TAtributo>(true)?.Any() ?? false;
        }
        public static object ObterValorAnotacao<TAtributo>(this Type tipo, String propriedade) where TAtributo : Attribute
        {
            return tipo.ObterAnotacao<TAtributo>()?.GetType().GetProperty(propriedade).GetValue(tipo.ObterAnotacao<TAtributo>(), null);
        }
        public static object ObterValorAnotacao<TAtributo, TModel>(this Type tipo, Expression<Func<TModel, object>> propriedade) where TAtributo : Attribute
        {
            if (propriedade.Body.NodeType == ExpressionType.MemberAccess)
                return tipo.ObterAnotacao<TAtributo>().GetType().GetProperty(((MemberExpression)propriedade.Body).Member.Name)
                                                                .GetValue(tipo.ObterAnotacao<TAtributo>(), null);
            return null;
        }
        public static List<PropertyInfo> ObterPropriedadesAnotadas<TAtributo>(this Type tipo) where TAtributo : Attribute
        {
            return tipo.GetProperties().Where(prop => prop.GetCustomAttributes<TAtributo>(true).Any()).ToList();
        }
        /// <summary>
        /// Retorna se o tipo implementa a interface passada como parametro
        /// </summary>
        /// <typeparam name="TInterface">Interface que sera testada se o tipo implementa</typeparam>
        /// <param name="tipo">Tipo que sera testado</param>
        /// <returns>Retorna true se a interface for implementada pelo titulo ou false, caso não seja</returns>
        public static bool ImplementaInterface<TInterface>(this Type tipo)
        {
            return tipo.GetInterfaces().Any(i => i.FullName == typeof(TInterface).FullName);
        }
        //[DebuggerNonUserCode]
        public static T Injete<T, D>(this T obj, D complemento)
        {
            return GestorDependencia.Instancia.ObterInstancia<IMapper>().Map(complemento, obj);
        }

        public static T Como<T>(this object obj, bool naoMapper = false, bool permitirExcecao = false, T valorPadrao = default(T))
        {
            try
            {
                if (obj == null)
                    return valorPadrao;

                if (obj.GetType() == typeof(T))
                    return (T)obj;

                if (obj is Enum)
                {
                    if (typeof(T) == typeof(int) || typeof(T) == typeof(byte) || typeof(T) == typeof(Enum) || typeof(T) == typeof(long) || typeof(T) == typeof(short))
                        return (T)obj;

                    if (valorPadrao.Equals(default(T)))
                        return ConverteUtils.SempreConverteEnum<T>(obj);
                    return ConverteUtils.SempreConverteEnum(obj, valorPadrao);
                }

                if (!naoMapper)
                {
                    //return Mapper.Map<T>(obj);
                    var r = GestorDependencia.Instancia.ObterInstancia<IMapper>().Map<T>(obj);
                    return r;
                }

                return (T)obj;
            }
            catch (Exception ex)
            {
                if (permitirExcecao)
                    throw ex;

                if (typeof(T) == obj?.GetType() || typeof(T) == obj?.GetType().GetTypeInfo().BaseType)
                    return (T)obj;

                if (valorPadrao?.Equals(default(T)) ?? true)
                    return ConverteUtils.SempreConverte<T>(obj);
                return ConverteUtils.SempreConverte(obj, valorPadrao);

            }
        }
        /// <summary>
        /// Testa se o Type é um tipo primitivo nullable ex: (int?)
        /// </summary>
        public static bool EhPrimitivoNullable(this Type tipo)
        {
            var typeInfo = tipo.GetTypeInfo();
            return typeInfo.IsGenericType && typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>) && tipo.GetGenericArguments().Any(t => t.GetTypeInfo().IsValueType && t.GetTypeInfo().IsPrimitive);
        }
        /// <summary>
        /// Testa se o Type é um tipo basico nullable ex: (int?) ou se é um DateTime? Decimal?
        /// </summary>
        public static bool EhBasicoNullable(this Type tipo)
        {
            var typeInfo = tipo.GetTypeInfo();
            return typeInfo.IsGenericType && tipo.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                   tipo.GetGenericArguments().Any(t => t.GetTypeInfo().IsValueType && (t.GetTypeInfo().IsPrimitive || tiposPrimitivos.Contains(t)));
        }
        /// <summary>
        /// Testa se o Type é um tipo primitivo ex: (int, bool, etc..) ou se é um DateTime, Decimal ou String
        /// </summary>
        public static bool EhBasico(this Type tipo)
        {
            var typeInfo = tipo.GetTypeInfo();
            return typeInfo.IsPrimitive || tiposPrimitivos.Contains(tipo);
        }
        /// <summary>
        /// Testa primeiro se o Type é um tipo primitivo ou DateTime, Decimal ou String
        /// Caso contrário testa se é um primitivo nullable ou se é um DateTime? Decimal?
        /// </summary>
        public static bool EhBasicoOuNullable(this Type tipo)
        {
            return EhBasico(tipo) || EhBasicoNullable(tipo);
        }
        public static bool EhColecao(this Type tipo)
        {
            return typeof(IEnumerable).IsAssignableFrom(tipo);
        }
        public static bool EhNullable(this Type type)
        {
            if (type.GetTypeInfo().IsGenericType)
                return type.GetGenericTypeDefinition() == typeof(Nullable<>);
            return false;
        }
        public static Type ObterTipoDoNullable(this Type type)
        {
            return type.GetTypeInfo().GenericTypeArguments[0];
        }
        public static Type PrimeiroTipoGenerico(this Type tipo)
        {
            return tipo.GetTypeInfo().IsGenericType ? tipo.GetGenericArguments()[0] : null;
        }
        public static Object ConstruirNovo(this Type tipo)
        {
            var construtores = tipo.GetConstructors();
            var construtor = construtores.FirstOrDefault(c => c.GetParameters().Length == 0);
            if (construtor == null)
                throw new NotSupportedException();
            var obj = construtor.Invoke(null);
            return obj;
        }
        public static Type ObterMemberType(this MemberInfo memberInfo)
        {
            var methodInfo = memberInfo as MethodInfo;
            if (methodInfo != null)
                return methodInfo.ReturnType;

            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
                return propertyInfo.PropertyType;

            var fieldInfo = memberInfo as FieldInfo;
            return fieldInfo?.FieldType;
        }
        public static TypeInfo ObterTypeInfoSemProxy(this Type tipo)
        {
            var type = tipo.GetTypeInfo();
            if (type.IsClass && type.Namespace.Equals("System.Data.Entity.DynamicProxies"))
                type = type.BaseType.GetTypeInfo();
            return type;
        }
        public static TypeInfo ObterTypeInfoSemProxy(this object tipo)
        {
            return ObterTypeInfoSemProxy(tipo.GetType());
        }
        public static Type ObterTypeSemProxy(this Type tipo)
        {
            if (tipo.GetTypeInfo().IsClass && tipo.Namespace.Equals("System.Data.Entity.DynamicProxies"))
                tipo = tipo.GetTypeInfo().BaseType;
            return tipo;
        }
        public static Type ObterTypeSemProxy(this object tipo)
        {
            if (tipo == null)
                return null;
            return ObterTypeSemProxy(tipo.GetType());
        }
        public static String ObterNomePropriedade<TModel, TResult>(this Expression<Func<TModel, TResult>> prop)
        {
            if (prop == null)
                return "";
            MemberExpression member;
            switch (prop.Body.NodeType)
            {
                case ExpressionType.Convert:
                case ExpressionType.ConvertChecked:
                    var ue = prop.Body as UnaryExpression;
                    member = ue?.Operand as MemberExpression;
                    break;
                default:
                    member = prop.Body as MemberExpression;
                    break;
            }
            return member?.Member.Name ?? String.Empty;
        }
    }
}