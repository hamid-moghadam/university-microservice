using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Core.API.Extensions;
using Core.API.Grpc;
using Core.Infrastructure.Extensions;
using Core.Persistence;
using Dahomey.Json;
using FluentValidation.AspNetCore;
using Kasp.FormBuilder.Extensions;
using Kasp.FormBuilder.FluentValidation.Extensions;
using Kasp.HttpException.Extensions;
using Kasp.HttpException.Internal;
using Kasp.ObjectMapper.Extensions;
using Kasp.Panel.EntityManager.Extensions;
using Kasp.Panel.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.API
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

            services.AddGrpc(options => options.EnableDetailedErrors = true);
            services.AddIdentity(_configuration);
            services.AddMassTransit(_configuration);
            services.AddDb(_configuration);


            services.AddFormBuilder(options => { options.AddFluentValidation(); });
            services.AddEntityManager(builder => builder.AddFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddObjectMapper<Kasp.ObjectMapper.AutoMapper.AutoMapper>();
            services.AddLogging();
            services.AddCors(options => options.AddDefaultPolicy(builder =>
                builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            services.AddAppSwagger();
            services.AddInfrastructure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CoreDbContext context)
        {
            context.Database.Migrate();
            app.UseExceptionHandler(new KaspExceptionHandlerOptions());
            app.UseObjectMapper();
            // app
            // app.Use(async (context, next) =>
            // {
            //     await next();
            //
            //     if (context.Response.StatusCode == 404 && !context.Request.Path.Value.StartsWith("/api"))
            //     {
            //         if (context.Request.Path.Value.StartsWith("/panel"))
            //             context.Request.Path = new PathString("/panel/index.html");
            //         else
            //             context.Request.Path = new PathString("/index.html");
            //         await next();
            //     }
            // });
            var provider = new FileExtensionContentTypeProvider();
            provider.Mappings[".webmanifest"] = "application/manifest+json";

            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                ContentTypeProvider = provider
            });
            app.UseCors();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<StudentGrpcService>();
                endpoints.MapControllers();
                endpoints.MapPanel(builder => { builder.MapEntityManager(); });
            });
            app.UseAppSwagger(_configuration);
        }
    }
}