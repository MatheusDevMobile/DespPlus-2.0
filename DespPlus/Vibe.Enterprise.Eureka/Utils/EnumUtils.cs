using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using PowerDev.Enterprise.Eureka.Extensoes;

namespace PowerDev.Enterprise.Eureka.Utils
{
    public static class EnumUtils
    {
        public static IEnumerable<ChaveValor> ListaChaveValor<TEnum>()
        {
            var valores = ExtrairValores(typeof(TEnum));
            return valores.Cast<Enum>().Select(e => new ChaveValor { Valor = e.ValorComoString(), Descricao = e.DescricaoUnica()});
        }
        public static String JavaScriptChaveValor(Type tipoEnum)
        {
            var valores = ExtrairValores(tipoEnum);
            var r = new StringBuilder("[");
            foreach (var e in valores.Cast<Enum>())
            {
                r.AppendFormat("{{{0} : '{1}', {2} : '{3}' }},", nameof(ChaveValor.Valor), e.ValorComoString(), nameof(ChaveValor.Descricao), e.DescricaoUnica());
            }
            r.Length -= 1;
            r.Append("]");
            return r.ToString();
        }
        public static String JavaScriptChaveValor<TEnum>()
        {
            return JavaScriptChaveValor(typeof (TEnum));
        }
        public static IEnumerable<ChaveValor> ListaChaveValor<TEnum>(params TEnum[] filtros)
        {
            var valores = ExtrairValores(typeof(TEnum));
            var listaEnum = valores.Cast<TEnum>().Where(filtros.Contains);
            return listaEnum.Cast<Enum>().Select(e => new ChaveValor { Valor = e.ValorComoString(), Descricao = e.DescricaoUnica() });
        }
        public static IEnumerable<ChaveValor> ListaChaveValorExceto<TEnum>(params TEnum[] filtros)
        {
            var valores = ExtrairValores(typeof(TEnum));
            var listaEnum = valores.Cast<TEnum>().Where(i => !filtros.Contains(i));
            return listaEnum.Cast<Enum>().Select(e => new ChaveValor { Valor = e.ValorComoString(), Descricao = e.DescricaoUnica() });
        }
        public static String JavaScriptChaveValor(IEnumerable<ChaveValor> lista)
        {
            var r = new StringBuilder("[");
            foreach (var l in lista)
            {
                r.AppendFormat("{{{0} : '{1}', {2} : '{3}' }},", nameof(ChaveValor.Valor), l.Valor, nameof(ChaveValor.Descricao), l.Descricao);
            }
            r.Length -= 1;
            r.Append("]");
            return r.ToString();
        }
        public static String ParaJavaScript(this IEnumerable<ChaveValor> lista)
        {
            return JavaScriptChaveValor(lista);
        }

        private static Array ExtrairValores(Type tipoEnum)
        {
            tipoEnum = tipoEnum.EhNullable() ? Nullable.GetUnderlyingType(tipoEnum) : tipoEnum;
            if (!tipoEnum.GetTypeInfo().IsEnum)
                throw new ArgumentException(String.Format("O valor para {0} deve ser do tipo Enum.", nameof(tipoEnum)));

            var valores = Enum.GetValues(tipoEnum);
            return valores;
        }
        public static IEnumerable<TEnum> Valores<TEnum>()
        {
            var valores = ExtrairValores(typeof(TEnum));
            return valores.Cast<TEnum>();
        }
    }
}