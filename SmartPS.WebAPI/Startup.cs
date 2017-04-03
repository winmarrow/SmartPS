using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using SmartPS.WebAPI.Db;
using SmartPS.WebAPI.Models;
using SmartPS.WebAPI.Models.Account;

namespace SmartPS.WebAPI
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();

            services.AddOptions();


            //-- EF Core DbContext--
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //-- Identity --
            services.AddIdentity<AccountModel, AccountRoleModel>()
                .AddEntityFrameworkStores<ApplicationDbContext, Guid>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<AccountModel, AccountRoleModel, ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<AccountRoleModel, ApplicationDbContext, Guid>>();

            //-- Rrepositories --
            //services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //services.AddScoped<ISubjectRepository, SubjectRepository>();
            //services.AddScoped<IPresentationRepository, PresentationRepository>();
            //services.AddScoped<IFileRepository, FileRepository>();

            //-- Mvc --
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }); ;

            //-- SignalR --
            services.AddSignalR(options =>
            {
                options.Hubs.EnableDetailedErrors = true;
                options.Hubs.EnableJavaScriptProxies = true;
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, ApplicationDbContext context)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            if (env.IsDevelopment())
            {
                loggerFactory.AddDebug();

                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
            });

            app.UseStaticFiles();



            app.UseSignalR();

            app.UseMvcWithDefaultRoute();

            // -- Db Init --
            ApplicationDbInitializer.Initialize(context);
        }
    }
}
