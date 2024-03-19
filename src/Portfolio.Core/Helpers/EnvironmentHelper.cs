namespace Portfolio.Core.Helpers;

public static class EnvironmentHelper
{
	public static bool IsDevelopmentMode = Environment.GetEnvironmentVariable("SERVER_MODE") == "development";
	public static string DbServer = Environment.GetEnvironmentVariable("DB_SERVER");
	public static string DbPort = Environment.GetEnvironmentVariable("DB_PORT");
	public static string DbName = Environment.GetEnvironmentVariable("DB_NAME");
	public static string DbUser = Environment.GetEnvironmentVariable("DB_USER");
	public static string DbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
}
