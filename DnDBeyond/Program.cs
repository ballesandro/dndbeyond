using DnDBeyond.DB;
using DnDBeyond.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DnDBeyond
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                CharactersRepository repo = scope.ServiceProvider.GetRequiredService<CharactersRepository>();
                _ = repo.Add(new Character()
                {
                    Id = 1,
                    Name = "Eressil"
                });
                _ = repo.Add(new Character()
                {
                    Id = 2,
                    Name = "Rhone"
                });
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
