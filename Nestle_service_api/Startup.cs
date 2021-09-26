using Nestle_service_api.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Nestle_service_api.BL.Inbound;
using Nestle_service_api.BL.Outbound;

namespace Nestle_service_api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                        bulider => bulider
                                    .AllowAnyHeader()
                                    .AllowAnyOrigin()
                                    .AllowAnyMethod().Build());

            });
            services.AddDbContext<Nestle_Connect>(options => options.UseSqlServer(Configuration.GetConnectionString("DbNestle_CEM")));
            services.AddDbContext<SPContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DbNestle_CEM")));
            services.AddDbContext<Fcc_Connect>(options => options.UseSqlServer(Configuration.GetConnectionString("DbiFcc")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Nestle-Servive-Api",
                    Version = "v1"
                });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddScoped(typeof(IEfRepository<>), typeof(EfRepository<>));
            services.AddTransient<ICallDetail, CallDetail>();
            services.AddTransient<IFristCallDetail, FristCallDetail>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Use(async (context, next) =>
            {
                //Enabling CORS
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
                context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type,Accept, x - client - key, x - client - token, x - client - secret, Authorization");

                await next();
                //});
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });
            app.UseHttpsRedirection();
            app.UseCors("AllowAllOrigins");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors();
        }
    }
}
