using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SimpleHttpServer
{
    public class Program
    {
        public static string NetworkCheckPath;
        public static void Main(string[] args)
        {
            if (!args.Where(a => a.StartsWith("--NetworkCheckPath:")).Any())
            {
                Console.WriteLine("Missing --NetworkCheckPath parameter, usage --NetworkCheckPath:<path to NetworkChecks folder>");
                Console.ReadKey();
                Environment.Exit(-1);
            }
            try
            {
                NetworkCheckPath = args.Where(a => a.StartsWith("--NetworkCheckPath:")).First().Split(":")[1];
                if (!Directory.Exists(NetworkCheckPath))
                {
                    throw new Exception($"{NetworkCheckPath} doesn't exist!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"invalid NetworkCheckPath {NetworkCheckPath}, error {e.Message}");
                Console.ReadKey();
                Environment.Exit(-1);
            }


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://localhost:8000");
                });


    }
}
