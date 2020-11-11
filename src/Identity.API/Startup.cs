using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Grpc.Identity;
using Identity.API.Data;
using Identity.API.Grpc;
using Identity.API.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Logging;

namespace Identity.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(_configuration["ConnectionString"],
                    o =>
                    {
                        o.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                        o.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), null!);
                    }));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            // Adds IdentityServer
            services.AddIdentityServer(x => { x.IssuerUri = "http://localhost:3000"; })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseNpgsql(_configuration["ConnectionString"],
                        o =>
                        {
                            o.MigrationsAssembly(migrationsAssembly);
                            //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                            o.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
                                null!);
                        });
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => builder.UseNpgsql(_configuration["ConnectionString"],
                        o =>
                        {
                            o.MigrationsAssembly(migrationsAssembly);
                            //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                            o.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30),
                                null!);
                        });
                });
            // .Services.AddTransient<IProfileService, ProfileService>();
            services.AddGrpc(options => options.EnableDetailedErrors = true);

            var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });
            var cors = new DefaultCorsPolicyService(loggerFactory.CreateLogger<DefaultCorsPolicyService>())
            {
                AllowedOrigins =
                {
                    "http://127.0.0.1:5500", "http://127.0.0.1:4200", "http://127.0.0.1:8080",
                    "http://localhost:8080", "http://localhost:4200",
                    "http://95.156.254.68"
                }
            };
            services.AddSingleton<ICorsPolicyService>(cors);
            services.AddCors(options => options.AddDefaultPolicy(builder =>
                builder.WithOrigins("http://127.0.0.1:5500", "http://127.0.0.1:4200", "http://127.0.0.1:8080",
                        "http://localhost:8080", "http://localhost:4200",
                        "http://95.156.254.68")
                    .AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 10;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseForwardedHeaders();
            app.UseIdentityServer();

            // app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = AspNetCore.Http.SameSiteMode.Lax });
            app.UseCors();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<UserGrpcService>();
                endpoints.MapControllers();
            });
        }
    }
}