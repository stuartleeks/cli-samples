using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace HelloEf
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                        .UseKestrel()
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseDefaultHostingConfiguration(args)
                        .UseStartup<Startup>()
                        .Build();

            host.Run();
        }
    }
}