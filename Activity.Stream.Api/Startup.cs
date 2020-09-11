using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Activity.Stream.Mediatr.Handlers;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Prometheus;

namespace Activity.Stream.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services
                .AddMultiTenant()
                .WithDelegateStrategy(context =>
                {
                    var request = ((HttpContext) context).Request;
                    return Task.FromResult(request.Headers["api_key"].SingleOrDefault());
                })
                .WithConfigurationStore();
            
            services.AddSwaggerGen(genOptions =>
            {
                genOptions.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Activity Stream API",
                    Version = "v1"
                });
                //c.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName));
                //c.TagActionsBy(api => new[]{api.GroupName});

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                genOptions.IncludeXmlComments(xmlPath);
                
                genOptions.AddSecurityDefinition("api_key", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "api_key"
                });
            });

            services.AddMediatR(typeof(GetActivityStreamHandler));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            
            app.UseSwagger();

            app.UseRouting();
            app.UseMultiTenant();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMetrics();
                endpoints.MapControllers();
            });
        }
    }
}