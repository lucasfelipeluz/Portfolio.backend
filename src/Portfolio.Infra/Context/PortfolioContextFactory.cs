using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Infra.Context
{

  public class PortfolioContextFactory : IDesignTimeDbContextFactory<PortfolioContext>
  {
    public PortfolioContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<PortfolioContext>();

      string connectionString = "Server=localhost;Database=portfolio;Uid=root;Pwd=Telegram2012*;";
      optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

      return new PortfolioContext(optionsBuilder.Options);
    }
  }
}
