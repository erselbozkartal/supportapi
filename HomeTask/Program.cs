using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace HomeTask
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task.Run(() =>
            {
                var thread = new Thread(new HomeTask.Service.Monitor().ChatQueue);
                thread.Start();
                thread.Join();
            });
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