using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Loki;

namespace LokiLogs {
    
    public class Program {
        
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog((context, config) => {
                    var credentials = new NoAuthCredentials(Environment.GetEnvironmentVariable("LOKI_HOST"));

                    config
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .MinimumLevel.Override("System", LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("ServerName", Environment.MachineName)
                        .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                        //.WriteTo.Console(new RenderedCompactJsonFormatter())
                        .WriteTo.Console()
                        .WriteTo.LokiHttp(credentials);
                });
    }
}
