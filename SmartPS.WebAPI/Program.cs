using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace SmartPS.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5000", "http://192.168.0.103:5000")
                .UseContentRoot(Directory.GetCurrentDirectory())

                .UseKestrel()
                .UseIISIntegration()

                .UseApplicationInsights()

                .Build();

            host.Run();
        }
    }
}
