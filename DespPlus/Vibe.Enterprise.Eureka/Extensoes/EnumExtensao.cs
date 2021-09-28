using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using PowerDev.Enterprise.Eureka.Utils;

namespace PowerDev.Enterprise.Eureka.Extensoes
{
    public static class EnumExtensao
    {
        public static String Descricao(this Enum tipo)
        {
            if (tipo == null)
                return String.Empty;

            if (!tipo.GetType().TemAnotacao<FlagsAttribute>())
                return ObterDescricaoEnum(tipo);

            var sb = new StringBuilder();
            var valores = Enum.GetValues(tipo.GetType());
            foreach (var e in valores.Cast<Enum>().Where(tipo.HasFlag))
            {
                sb.Append(ObterDescricaoEnum(e));
                sb.Append(",");
            }
            if (sb.Length > 0)
                sb.Length -= 1;
            return sb.ToString();
        }
        private static string ObterDescricaoEnum(Enum tipo)
        {
            FieldInfo fieldInfo = tipo.GetType().GetRuntimeField(tipo.ToString());
            if (fieldInfo == null)
                return tipo.ToString().CamelCaseParaNormal();
            var atributo = fieldInfo.GetCustomAttribute<DisplayAttribute>();
            if (atributo != null)
                return atributo.Description;
            var nameAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
            if (nameAttribute != null)
                return nameAttribute.Description;

            return tipo.ToString().CamelCaseParaNormal();
        }

        public static String DescricaoUnica(this Enum tipo)
        {
            if (tipo == null)
                return String.Empty;

            return ObterDescricaoEnum(tipo);
        }

        public static String Nome(this Enum tipo)
        {
            return tipo.ToString().ToLower();
        }

        public static String ValorComoString(this Enum tipo)
        {
            int valor = Convert.ToInt32(tipo);
            return valor.ToString();
        }

        public static int Valor(this Enum tipo)
        {
            return Convert.ToInt32(tipo);
        }

        public static IEnumerable ListaChaveValor(this Enum tipo)
        {
            var tipoEnum = tipo.GetType();
            var valores = Enum.GetValues(tipoEnum);
            foreach (var e in valores.Cast<Enum>().Where(tipo.HasFlag))
            {
                yield return new ChaveValor { Valor = e.ValorComoString(), Descricao = e.DescricaoUnica()};
            }
        }
        public static List<T> ObterValores<T>(this T valor)
        {
            var comoEnum = valor.Como<Enum>();
            var r = new List<T>();
            foreach (var v in Enum.GetValues(typeof(T)))
            {
                if (comoEnum.HasFlag(v.Como<Enum>()))
                    r.Add(v.Como<T>());
            }
            return r;
        }
        public static List<T> ObterValores<T>(this T valor, Type tipo)
        {
            var comoEnum = valor.Como<Enum>();
            var r = new List<T>();
            foreach (var v in Enum.GetValues(tipo))
            {
                if (comoEnum.HasFlag(v.Como<Enum>()))
                    r.Add((T)v);
            }
            return r;
        }
    }
}