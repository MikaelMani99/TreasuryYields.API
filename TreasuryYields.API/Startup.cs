using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AutoMapper;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using TreasuryYields.CronJobs;
using TreasuryYields.Repositories.Contexts.Implementations;
using TreasuryYields.Repositories.Contexts.Interfaces;
using TreasuryYields.Repositories.Implementations;
using TreasuryYields.Repositories.Interfaces;
using TreasuryYields.Services.Implementations;
using TreasuryYields.Services.Interfaces;
using TreasuryYields.API.Mappings;

namespace TreasuryYields.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TreasuryYields.API", Version = "v1" });
            });

            // adding Hangfire service
            services.AddHangfire(options =>
            {
                options.UseMemoryStorage();
            });
            // Adding Mapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());

            // Tries to get the connection string for the sql database from the azure storage
            // if the connectionString is null, it means we're running locally
            // so we get the connection string from secrets
            var sqlConnectionString = Configuration.GetConnectionString("POSTGRE:connectionString") ?? Configuration["POSTGRE:connectionString"];

            services.AddDbContext<TreasuryYieldsDbContext>(options =>
            {
                options.UseNpgsql(sqlConnectionString,
                      options =>
                      {
                          options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                      }
                    );
            });
            // Adding CORS
            services.AddCors(options =>
        {
            options.AddPolicy(name: MyAllowSpecificOrigins,
                              builder =>
                              {
                                  builder.WithOrigins("http://localhost:3000")
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                              });
        });
            
            // Register service and implementation for the db context
            services.AddScoped<ITreasuryYieldsDbContext>(provider => provider.GetService<TreasuryYieldsDbContext>());

            // Adding Transients, mapping Interfaces to their implementations
            services.AddTransient<ITreasuryYieldsService, TreasuryYieldsService>();
            services.AddTransient<ITreasuryYieldsRepository, TreasuryYieldsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TreasuryYields.API v1"));
            }

            app.UseHangfireDashboard();
            app.UseHangfireServer();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            RecurringJob.AddOrUpdate<FetchData>(x => x.Get(Configuration.GetSection("TreasuryYieldURL").Value), Cron.Daily);
        }
    }
}
