using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
