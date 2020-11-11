using System.IO;
using System.Net;
using Identity.API.Data;
using Identity.API.Extensions;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Identity.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = GetConfiguration();
            var host = CreateHostBuilder(args, configuration).Build();
            host.MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
                .MigrateDbContext<ApplicationDbContext>((context, services) =>
                {
                    var env = services.GetService<IWebHostEnvironment>();
                    var dbContextLogger = services.GetService<ILogger<ApplicationDbContextSeed>>();
                    new ApplicationDbContextSeed()
                        .SeedAsync(context, env, dbContextLogger)
                        .Wait();
                })
                .MigrateDbContext<ConfigurationDbContext>((context, services) =>
                {
                    new ConfigurationDbContextSeed()
                        .SeedAsync(context, configuration)
                        .Wait();
                });
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, config) =>
                {
                    config
                        .Enrich.FromLogContext()
                        .WriteTo.Console();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.ConfigureKestrel(options =>
                    {
                        var ports = GetDefinedPorts(configuration);
                        options.Listen(IPAddress.Any, ports.httpPort,
                            listenOptions => { listenOptions.Protocols = HttpProtocols.Http1AndHttp2; });

                        options.Listen(IPAddress.Any, ports.grpcPort,
                            listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
                    });
                });

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            return builder.Build();
        }

        private static (int httpPort, int grpcPort) GetDefinedPorts(IConfiguration config)
        {
            var grpcPort = config.GetValue("GRPC_PORT", 81);
            var port = config.GetValue("PORT", 80);
            return (port, grpcPort);
        }
    }
}