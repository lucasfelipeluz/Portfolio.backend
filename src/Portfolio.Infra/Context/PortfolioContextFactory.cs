using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Portfolio.Infra.Context;

public class PortfolioContextFactory : IDesignTimeDbContextFactory<PortfolioContext>
{
	public PortfolioContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<PortfolioContext>();

		string dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
		string dbName = Environment.GetEnvironmentVariable("DB_NAME");
		string dbUser = Environment.GetEnvironmentVariable("DB_USER");
		string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

		string connectionString = $"Server={dbServer};Database={dbName};Uid={dbUser};Pwd={dbPassword};";
		optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

		return new PortfolioContext(optionsBuilder.Options);
	}
}
