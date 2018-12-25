using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Rhino.Geometry;

namespace Ourchitecture.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://localhost:88/")
                .Build();

            host.Run();
        }
    }
}