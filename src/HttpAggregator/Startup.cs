using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Dahomey.Json;
using FluentValidation.AspNetCore;
using HttpAggregator.Extensions;
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

namespace HttpAggregator
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
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", options =>
                {
                    options.ApiName = "aggregator";
                    options.RequireHttpsMetadata = false;
                    options.Authority = _configuration["Identity_Url"];
                });
            services.AddAuthorization();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddObjectMapper<Kasp.ObjectMapper.AutoMapper.AutoMapper>();
            services.AddLogging();
            services.AddCors(options => options.AddDefaultPolicy(builder =>
                builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
            services.AddAppSwagger();
            services.AddApplication(_configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(new KaspExceptionHandlerOptions());
            app.UseObjectMapper();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapGrpcService<TestService>();
                endpoints.MapControllers();
            });
            app.UseAppSwagger(_configuration);
        }
    }
}