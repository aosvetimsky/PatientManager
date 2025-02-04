using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using System.Linq;
using PatientManagement.Persistence;

namespace PatientManagement.Services.Api.Tests.Setup
{
    public class ApiWebApplicationFactory : WebApplicationFactory<Program>
    {
        public AppDbContext Db => Services.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<AppDbContext>));

                services.Remove(dbContextDescriptor);

                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbConnection));

                services.Remove(dbConnectionDescriptor);

                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();

                    return connection;
                });

                services.AddDbContext<AppDbContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                }, ServiceLifetime.Transient);
            });
        }
        protected void Given(params object[] entities)
        {
            foreach (var entity in entities)
            {
                Db.Attach(entity);
            }

            Db.SaveChanges();
        }
    }
}
