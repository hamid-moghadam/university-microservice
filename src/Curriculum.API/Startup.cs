using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Curriculum.API.Configurations;
using Curriculum.API.Grpc;
using Curriculum.API.Hubs;
using Dahomey.Json;
using FluentValidation.AspNetCore;
using Kasp.HttpException.Extensions;
using Kasp.HttpException.Internal;
using Kasp.ObjectMapper.Extensions;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Curriculum.API.Consumers;
using Curriculum.API.Extensions;
using Curriculum.API.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Curriculum.API
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
            services.AddControllers()
                .AddHttpExceptions(options =>
                {
                    options.IncludeExceptionDetails = context => false;
                    options.ShouldLogException = exception => true;
                })
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(
                        new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                    options.JsonSerializerOptions.SetupExtensions();
                });

            services.AddMassTransit(_configuration);
            services.AddAuthentication(options =>
                {
                    // Identity made Cookie authentication the default.
                    // However, we want JWT Bearer Auth to be the default.
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    // Configure the Authority to the expected value for your authentication provider
                    // This ensures the token is appropriately validated
                    options.Audience = "curriculum";
                    options.RequireHttpsMetadata = false;
                    options.Authority = _configuration["Identity_Url"];

                    // We have to hook the OnMessageReceived event in order to
                    // allow the JWT authentication handler to read the access
                    // token from the query string when a WebSocket or 
                    // Server-Sent Events request comes in.

                    // Sending the access token in the query string is required due to
                    // a limitation in Browser APIs. We restrict it to only calls to the
                    // SignalR hub in this code.
                    // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
                    // for more information about security considerations when using
                    // the query string to transmit the access token.
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            Console.WriteLine($"token {accessToken}");
                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            Console.WriteLine($"path 1 {path}");
                            Console.WriteLine($"path 2 {path.Value}");
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/hubs/events")))
                            {
                                Console.WriteLine($"set token for url {path.Value}");

                                // Read the token out of the query string
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization();
            services.AddGrpc(options => options.EnableDetailedErrors = true);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddObjectMapper<Kasp.ObjectMapper.AutoMapper.AutoMapper>();
            services.AddLogging();
            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, NameUserIdIdentifier>();
            services.AddCors(options => options.AddDefaultPolicy(builder =>
                builder.WithOrigins("http://127.0.0.1:5500", "http://127.0.0.1:8080", "http://localhost:8080",
                        "http://95.156.254.68")
                    .AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
            services.AddAppSwagger();
            services.AddApplication(_configuration);
            services.AddDb(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CurriculumDbContext context)
        {
            context.Database.Migrate();
            app.UseExceptionHandler(new KaspExceptionHandlerOptions());
            app.UseObjectMapper();
            // app.AddSignalRWithJwt();
            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<StudentCurriculumGrpcService>();
                endpoints.MapHub<EventsHub>("/hubs/events");
                endpoints.MapControllers();
            });
            app.UseAppSwagger(_configuration);
        }
    }
}