using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using CashRegister.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Microsoft.AspNetCore.Http;

namespace CashRegister.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            Env = env;
        }

        public IConfigurationRoot Configuration { get; }

        public IApplicationBuilder App { get; }

        public IHostingEnvironment Env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add database service
            services.AddDbContext<CashRegisterContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("SQLite"));
            });

            // Add MVC services with JSON config
            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opt.SerializerSettings.Converters.Add(new StringEnumConverter(true));
            });

            // Add options
            services.AddCustomOptions(Configuration);

            // Add built-in services
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add custom services.
            services.AddApplicationServices();

            // Add authentication services
            services.AddJwtAuthentication(Configuration, Env);

            // Add cross origin resource sharing services
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }

            app.UseCors(builder => builder
                .AllowCredentials()
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            );

            // Force database migration
            // using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            // {
            //     serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
            //     var userManager = app.ApplicationServices.GetService<UserManager<ApplicationUser>>();
            //     var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
            //     serviceScope.ServiceProvider.GetService<ApplicationDbContext>().EnsureSeedData(userManager, roleManager);
            // }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
