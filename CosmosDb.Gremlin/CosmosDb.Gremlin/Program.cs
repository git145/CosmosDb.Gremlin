using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;

namespace CosmosDb.Gremlin
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5055")
                .UseContentRoot(AppContext.BaseDirectory);

        public static HttpClient NotificationClient
        {
            get;
            private set;
        }

        public static void Main(string[] args)
        {
            //Set security protocol to TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Initialise();

            var host = CreateWebHostBuilder(args).Build();

            if (Debugger.IsAttached)
                host.Run();
            else
                host.RunAsService(); 
        }

        public static void Initialise()
        {
            NotificationClient = new HttpClient();
        }
    }
}
