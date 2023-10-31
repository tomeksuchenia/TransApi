using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Trans.Infrascture.EF_DB;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;
using Trans.Infrastructure.Services;

namespace TestsEndToEnd
{
    public abstract class TestsControllerBase<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
    {
        protected readonly WebApplicationFactory<Program> Factory;
        protected readonly HttpClient Client;
        protected TestsControllerBase()
        {
            Factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                            typeof(DbContextOptions<TransContext>));

                    services.Remove(descriptor);

                    services.AddDbContext<TransContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    var sp = services.BuildServiceProvider();

                    using (var scope = sp.CreateScope())
                    {
                        var scopedServices = scope.ServiceProvider;
                        var db = scopedServices.GetRequiredService<TransContext>();

                        db.Database.EnsureCreated();

                        try
                        {
                            // Seed the database with test data.
                            DataInitializer.InitializeTestDatabase(db);
                        }
                        catch (Exception ex)
                        {
                            
                        }

                    }


                }));

            Client = Factory.CreateClient();
        }

        protected static StringContent MakePayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            var payload = new StringContent(json, Encoding.UTF8, "application/json");

            return payload;
        }

/*        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<TransContext>));

                services.Remove(descriptor);

                services.AddDbContext<TransContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<TransContext>();

                }
            });
        }*/

        protected void GetAuthentication(string jwtToken)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
        }
    }
}



