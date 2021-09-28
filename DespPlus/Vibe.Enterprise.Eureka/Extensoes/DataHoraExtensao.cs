using System;

namespace PowerDev.Enterprise.Eureka.Extensoes
{
    public static class DataHoraExtensao
    {
        

        public static String TotalEmHorasMinutos(this TimeSpan ts)
        {
            if (ts.Days == 0)
                return ts.ToString(@"hh\:mm");
            var hr = ts.TotalHours.Como<int>();
            hr = hr < 0 ? hr * -1 : hr;
            var min = ts.Minutes;
            min = min < 0 ? min * -1 : min;
            return String.Format("{0:D2}:{1:D2}", hr, min);
        }
        
        public static DateTime FimDoDia(this DateTime data)
        {
            return data.Date.AddDays(1).AddSeconds(-1);
        }

        public static DateTime UltimoDiaMes(this DateTime data)
        {
            return new DateTime(data.Year, data.Month, 1).AddMonths(1).AddDays(-1);
        }

        public static string DataDecorridaLegivel(this DateTime data, bool simplificarSegundos = false)
        {
            var intervalo = data - DateTime.Now;
            var passado = intervalo.Ticks < 0;

            var segundos = Math.Abs(passado ? Math.Ceiling(intervalo.TotalSeconds) : Math.Round(intervalo.TotalSeconds));
            var minutos = Math.Abs(passado ? Math.Ceiling(intervalo.TotalMinutes) : Math.Round(intervalo.TotalMinutes));
            var horas = Math.Abs(passado ? Math.Ceiling(intervalo.TotalHours) : Math.Round(intervalo.TotalHours));
            var dias = Math.Abs(passado ? Math.Ceiling(intervalo.TotalDays) : Math.Round(intervalo.TotalDays));

            var prefixo = passado ? "Há" : "Em";

            if (segundos < 60)
            {
                if (!simplificarSegundos)
                    return $"{prefixo} {segundos} segundo{(segundos > 1 ? "s" : "")}";

                return $"{prefixo} menos de 1 minuto";
            }

            if (minutos < 60)
                return $"{prefixo} {minutos} minuto{(minutos > 1 ? "s" : "")}";

            if (horas < 24)
                return $"{prefixo} {horas} hora{(horas > 1 ? "s" : "")}";

            return $"{prefixo} {dias} dia{(dias > 1 ? "s" : "")}";
        }
    }
}