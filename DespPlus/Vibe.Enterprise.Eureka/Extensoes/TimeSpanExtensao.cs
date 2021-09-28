using System;
using System.Collections.Generic;
using System.Linq;

namespace PowerDev.Enterprise.Eureka.Extensoes
{
    public static class TimeSpanExtensao
    {
        public static string IntervaloTempoLegivel(this TimeSpan intervalo, bool resumido = false)
        {
            var segundos = intervalo.Seconds;
            var minutos = intervalo.Minutes;
            var horas = intervalo.Hours;
            var dias = intervalo.Days;

            if (resumido)
            {
                var maiorValor = (new[]
                {
                        Tuple.Create(segundos, "s"),
                        Tuple.Create(minutos, "m"),
                        Tuple.Create(horas, "h"),
                        Tuple.Create(dias, "d"),
                }).OrderByDescending(t => t.Item1).First();

                return $"{maiorValor.Item1}{maiorValor.Item2}";
            }

            List<string> listaTempos = new List<string>();
            ;

            if (dias > 0)
                listaTempos.Add($"{dias}d");

            if (horas > 0)
                listaTempos.Add($"{horas}h");

            if (minutos > 0)
                listaTempos.Add($"{minutos}m");

            if (segundos > 0)
                listaTempos.Add($"{segundos}s");

            return string.Join(" ", listaTempos);
        }

        public static TimeSpan SomarSemColocarDias(this TimeSpan tempo, TimeSpan tempoAdicionado)
        {
            var r = tempo.Add(tempoAdicionado);
            return r.Days > 0 ? r.Subtract(TimeSpan.FromDays(r.Days)) : r;
        }

        public static TimeSpan QuantoTempoEntre(this TimeSpan tempoInicio, TimeSpan tempoFim)
        {
            return tempoInicio < tempoFim ? tempoFim.Subtract(tempoInicio) : tempoFim.Add(TimeSpan.FromDays(1)).Subtract(tempoInicio);
        }
    }
}