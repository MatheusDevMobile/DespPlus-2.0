using System;
using System.Threading;
using PowerDev.Enterprise.Eureka.Extensoes;

namespace PowerDev.Enterprise.Eureka.Utils
{
    public class LogAplicacaoUtils
    {
        //private ILogger _log;
        //private const string outputTemplate = "{Timestamp:dd/MM/yyyy HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}";
        //public String ArquivoPath { get; }
        //public bool CopiaLogGlobal { get; set; }

        //public LogAplicacaoUtils(string nomeArquivo, string caminho, bool dataPadrao = true)
        //{
        //    ArquivoPath = String.Format(@"{0}\{1}.txt", caminho, nomeArquivo);
        //    _log = dataPadrao ? new LoggerConfiguration().WriteTo.ColoredConsole(outputTemplate: outputTemplate)
        //                                                 .WriteTo.RollingFile(ArquivoPath, outputTemplate: outputTemplate)
        //                                                 .CreateLogger() :
        //                   new LoggerConfiguration().WriteTo.ColoredConsole(outputTemplate: outputTemplate)
        //                                            .WriteTo.File(ArquivoPath, outputTemplate: outputTemplate)
        //                                            .CreateLogger();
        //}

        //private void log(LogEventLevel level, Exception ex = null, bool assincrono = false, string msg = "", params object[] parametros)
        //{
        //    var msgFormatada = msg;
        //    if (parametros != null && parametros.Length > 0)
        //        msgFormatada = string.Format(msg, parametros);

        //    if (assincrono)
        //    {
        //        Thread newThread = new Thread(logThread);
        //        newThread.Start(new { level, msg = msgFormatada, ex });
        //        return;
        //    }

        //    transcreve(level, msgFormatada, ex);
        //    if (CopiaLogGlobal)
        //        transcreveGlobal(level, msgFormatada, ex);
        //}

        //private void transcreve(LogEventLevel level, string msg, Exception ex = null)
        //{
        //    if (ex == null)
        //        _log.Write(level, msg);
        //    else
        //        _log.Write(level, ex, msg ?? string.Empty);
        //}

        //private void logThread(Object param)
        //{
        //    var ex = param.valorPropriedade<Exception>("ex");
        //    var msg = param.valorPropriedade<String>("msg");
        //    var level = param.valorPropriedade<LogEventLevel>("level");

        //    transcreve(level, msg, ex);
        //    if (CopiaLogGlobal)
        //        transcreveGlobal(level, msg, ex);
        //}

        //#region Logs Padrões
        //public void info(string msg, params object[] parametros)
        //{
        //    log(LogEventLevel.Information, msg: msg, parametros: parametros);
        //}

        //public void infoAsync(string msg, params object[] parametros)
        //{
        //    log(LogEventLevel.Information, msg: msg, parametros: parametros, assincrono: true);
        //}

        //public void verbose(string msg, params object[] parametros)
        //{
        //    log(LogEventLevel.Verbose, msg: msg, parametros: parametros);
        //}

        //public void verboseAsync(string msg, params object[] parametros)
        //{
        //    log(LogEventLevel.Verbose, msg: msg, parametros: parametros, assincrono: true);

        //}

        //public void debug(string msg, params object[] parametros)
        //{
        //    log(LogEventLevel.Debug, msg: msg, parametros: parametros);
        //}

        //public void debugAsync(string msg, params object[] parametros)
        //{
        //    log(LogEventLevel.Verbose, msg: msg, parametros: parametros, assincrono: true);
        //}

        //public void warning(string msg, params object[] parametros)
        //{
        //    log(LogEventLevel.Warning, msg: msg, parametros: parametros);
        //}

        //public void warningAsync(string msg, params object[] parametros)
        //{
        //    log(LogEventLevel.Warning, msg: msg, parametros: parametros, assincrono: true);
        //}

        //public void error(Exception ex = null, string msg = null, params object[] parametros)
        //{
        //    log(LogEventLevel.Error, ex, msg: msg, parametros: parametros);
        //}

        //public void errorAsync(Exception ex = null, string msg = null, params object[] parametros)
        //{
        //    log(LogEventLevel.Error, ex, true, msg, parametros);
        //}

        //public void fatal(Exception ex = null, string msg = null, params object[] parametros)
        //{
        //    log(LogEventLevel.Fatal, ex, msg: msg, parametros: parametros);
        //}

        //public void fatalAsync(Exception ex = null, string msg = null, params object[] parametros)
        //{
        //    log(LogEventLevel.Fatal, ex, true, msg, parametros);
        //}

        //#endregion

        //public void fecharArquivo()
        //{
        //    _log = null;
        //}

        //private static void logGlobal(LogEventLevel level, Exception ex = null, bool assincrono = false, string msg = "", params object[] parametros)
        //{
        //    var msgFormatada = msg;
        //    if (parametros != null && parametros.Length > 0)
        //        msgFormatada = string.Format(msg, parametros);

        //    if (assincrono)
        //    {
        //        Thread newThread = new Thread(logThreadGlobal);
        //        newThread.Start(new { level, msg = msgFormatada, ex });
        //        return;
        //    }

        //    transcreveGlobal(level, msgFormatada, ex);
        //}

        //private static void transcreveGlobal(LogEventLevel level, string msg, Exception ex = null)
        //{
        //    msg = msg ?? String.Empty;
        //    if (ex == null)
        //        Log.Write(level, msg);
        //    else
        //        Log.Write(level, ex, msg);
        //}

        //private static void logThreadGlobal(Object param)
        //{
        //    var ex = param.valorPropriedade<Exception>("ex");
        //    var msg = param.valorPropriedade<String>("msg") ?? String.Empty;
        //    var level = param.valorPropriedade<LogEventLevel>("level");

        //    transcreveGlobal(level, msg, ex);
        //}

        //#region Logs Padrões Estáticos
        //public static void infoGlobal(string msg, params object[] parametros)
        //{
        //    logGlobal(LogEventLevel.Information, msg: msg, parametros: parametros);
        //}
        //public static void infoGlobalAsync(string msg, params object[] parametros)
        //{
        //    logGlobal(LogEventLevel.Information, msg: msg, parametros: parametros, assincrono: true);
        //}

        //public static void verboseGlobal(string msg, params object[] parametros)
        //{
        //    logGlobal(LogEventLevel.Verbose, msg: msg, parametros: parametros);
        //}

        //public static void verboseGlobalAsync(string msg, params object[] parametros)
        //{
        //    logGlobal(LogEventLevel.Verbose, msg: msg, parametros: parametros, assincrono: true);
        //}
        //public static void debugGlobal(string msg, params object[] parametros)
        //{
        //    logGlobal(LogEventLevel.Debug, msg: msg, parametros: parametros);
        //}

        //public static void debugGlobalAsync(string msg, params object[] parametros)
        //{
        //    logGlobal(LogEventLevel.Debug, msg: msg, parametros: parametros, assincrono: true);
        //}

        //public static void warningGlobal(string msg, params object[] parametros)
        //{
        //    logGlobal(LogEventLevel.Warning, msg: msg, parametros: parametros);
        //}

        //public static void WarningGlobalAsync(string msg, params object[] parametros)
        //{
        //    logGlobal(LogEventLevel.Warning, msg: msg, parametros: parametros, assincrono: true);
        //}

        //public static void errorGlobal(Exception ex = null, string msg = null, params object[] parametros)
        //{
        //    logGlobal(LogEventLevel.Error, ex, msg: msg, parametros: parametros);
        //}

        //public static void errorGlobalAsync(Exception ex = null, string msg = null, params object[] parametros)
        //{
        //    logGlobal(LogEventLevel.Error, ex, true, msg, parametros);
        //}

        //public static void fatalGlobal(Exception ex = null, string msg = null, params object[] parametros)
        //{
        //    logGlobal(LogEventLevel.Fatal, ex, msg: msg, parametros: parametros);
        //}

        //public static void fatalGlobalAsync(Exception ex = null, string msg = null, params object[] parametros)
        //{
        //    logGlobal(LogEventLevel.Fatal, ex, true, msg, parametros);
        //}

        //public static void logErroAssincrono(Exception ex)
        //{
        //    errorGlobalAsync(ex, "");
        //}
        //#endregion
    }
}