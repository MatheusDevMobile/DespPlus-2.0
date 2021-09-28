using System;
using PowerDev.Enterprise.Eureka.Extensoes;

namespace PowerDev.Enterprise.Eureka.Utils
{
    public static class ConverteUtils
    {
        //private static Type[] TiposPrimitivos { get; } =
        //{
        //    typeof (DateTime),
        //    typeof (String),
        //    typeof (Boolean),
        //    typeof (Decimal),
        //    typeof (Double),
        //    typeof (Int32),
        //    typeof (Int64),
        //    typeof (float),
        //    typeof (int),
        //    typeof (string),
        //    typeof (bool),
        //    typeof (decimal),
        //    typeof (bool)
        //};
        //private static Type[] TiposPrimitivosNulllables { get; } =
        //{
        //    typeof (DateTime?),
        //    typeof (Boolean?),
        //    typeof (Decimal?),
        //    typeof (Double?),
        //    typeof (Int32?),
        //    typeof (Int64?),
        //    typeof (float?),
        //    typeof (int?),
        //    typeof (bool?),
        //    typeof (decimal?),
        //    typeof (bool?)
        //};
        public static Double SempreConverteDouble(Object vl, Double padraoParaNulo = 0)
        {
            Double r = 0;
            try
            {
                r = Convert.ToDouble(vl);
            }
            catch
            {
                r = padraoParaNulo;
            }
            return r;
        }
        public static Decimal SempreConverteDecimal(Object vl, Decimal padraoParaNulo = 0)
        {
            Decimal r = 0;
            try
            {
                r = Convert.ToDecimal(vl);
            }
            catch
            {
                r = padraoParaNulo;
            }
            return r;
        }
        public static int SempreConverteInt32(Object vl, int padraoParaNulo = 0)
        {
            int r = 0;
            try
            {
                r = Convert.ToInt32(vl);
            }
            catch
            {
                r = padraoParaNulo;
            }
            return r;
        }
        public static Int64 SempreConverteInt64(Object vl, Int64 padraoParaNulo = 0)
        {
            Int64 r = 0;
            try
            {
                r = Convert.ToInt64(vl);
            }
            catch
            {
                r = padraoParaNulo;
            }
            return r;
        }
        public static DateTime SempreConverteDate(Object vl, DateTime padraoParaNulo = default(DateTime))
        {
            switch (vl)
            {
                case DateTime dataHora:
                    return dataHora;
                case DateTimeOffset dataHoraOffset:
                    return dataHoraOffset.DateTime;
            }


            DateTime r;
            try
            {
                r = Convert.ToDateTime(vl);
            }
            catch
            {
                r = padraoParaNulo;
            }
            return r;
        }
        public static Boolean SempreConverteBoleano(Object vl, Boolean padraoParaNulo = false)
        {
            Boolean r;
            try
            {
                r = Convert.ToBoolean(vl);
            }
            catch
            {
                r = padraoParaNulo;
            }
            return r;
        }
        public static T SempreConverte<T>(Object vl, T padraoParaNulo = default(T))
        {
            if (typeof(T) == typeof(double))
                return SempreConverteDouble(vl, padraoParaNulo.Como<double>()).Como<T>();

            if (typeof(T) == typeof(decimal))
                return SempreConverteDecimal(vl, padraoParaNulo.Como<decimal>()).Como<T>();

            if (typeof(T) == typeof(int))
                return SempreConverteInt32(vl, padraoParaNulo.Como<int>()).Como<T>();

            if (typeof(T) == typeof(Int64))
                return SempreConverteInt64(vl, padraoParaNulo.Como<Int64>()).Como<T>();

            if (typeof(T) == typeof(bool))
                return SempreConverteBoleano(vl, padraoParaNulo.Como<bool>()).Como<T>();

            if (typeof(T) == typeof(DateTime))
                return SempreConverteDate(vl, padraoParaNulo.Como<DateTime>()).Como<T>();

            return padraoParaNulo;
        }
        public static object SempreConverte(Object vl, Type tipo, object padraoParaNulo = default(object))
        {
            if (vl == null)
                return padraoParaNulo;

            if (tipo == typeof(String))
                return vl.ToString().LimpoNuloBranco() ? padraoParaNulo : vl.ToString();

            if (tipo == typeof(int) || tipo == typeof(int?))
                return SempreConverteInt32(vl, padraoParaNulo.Como<int>());

            if (tipo == typeof(decimal) || tipo == typeof(decimal?))
                return SempreConverteDecimal(vl, padraoParaNulo.Como<decimal>());

            if (tipo == typeof(bool) || tipo == typeof(bool?))
                return SempreConverteBoleano(vl, padraoParaNulo.Como<bool>());

            if (tipo == typeof(DateTime) || tipo == typeof(DateTime?))
                return SempreConverteDate(vl, padraoParaNulo.Como<DateTime>());

            if (tipo == typeof(double) || tipo == typeof(double?))
                return SempreConverteDouble(vl, padraoParaNulo.Como<double>());

            if (tipo == typeof(Int64) || tipo == typeof(Int64?))
                return SempreConverteInt64(vl, padraoParaNulo.Como<Int64>());

            return padraoParaNulo;
        }
        public static T SempreConverteEnum<T>(Object vl, T padraoParaNulo = default(T))
        {
            try
            {
                var r = (T)Enum.Parse(typeof(T), vl.ToString(), true);
                return r;
            }
            catch (Exception)
            {
                return padraoParaNulo.Equals(default(T)) ? (T)Enum.GetValues(typeof(T)).GetValue(0) : padraoParaNulo;
            }
        }
    }
}