namespace HavenBeans;

public class Log
{
    public static void Trace(Exception exception, string messageTemplate, params object[] propertyValues) => Service.Logger.Verbose(exception, messageTemplate, propertyValues);
    public static void Trace(string messageTemplate, params object[] propertyValues) => Service.Logger.Verbose(messageTemplate, propertyValues);
    public static void Trace(string messageTemplate) => Service.Logger.Verbose(messageTemplate);

    public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues) => Service.Logger.Debug(exception, messageTemplate, propertyValues);
    public static void Debug(string messageTemplate, params object[] propertyValues) => Service.Logger.Debug(messageTemplate, propertyValues);
    public static void Debug(string messageTemplate) => Service.Logger.Debug(messageTemplate);

    public static void Info(Exception exception, string messageTemplate, params object[] propertyValues) => Service.Logger.Information(exception, messageTemplate, propertyValues);
    public static void Info(string messageTemplate, params object[] propertyValues) => Service.Logger.Information(messageTemplate, propertyValues);
    public static void Info(string messageTemplate) => Service.Logger.Information(messageTemplate);

    public static void Warn(Exception exception, string messageTemplate, params object[] propertyValues) => Service.Logger.Warning(exception, messageTemplate, propertyValues);
    public static void Warn(string messageTemplate, params object[] propertyValues) => Service.Logger.Warning(messageTemplate, propertyValues);
    public static void Warn(string messageTemplate) => Service.Logger.Warning(messageTemplate);

    public static void Error(Exception exception, string messageTemplate, params object[] propertyValues) => Service.Logger.Error(exception, messageTemplate, propertyValues);
    public static void Error(string messageTemplate, params object[] propertyValues) => Service.Logger.Error(messageTemplate, propertyValues);
    public static void Error(string messageTemplate) => Service.Logger.Error(messageTemplate);

    public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues) => Service.Logger.Fatal(exception, messageTemplate, propertyValues);
    public static void Fatal(string messageTemplate, params object[] propertyValues) => Service.Logger.Fatal(messageTemplate, propertyValues);
    public static void Fatal(string messageTemplate) => Service.Logger.Fatal(messageTemplate);

}