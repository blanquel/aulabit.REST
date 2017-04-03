using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NG.Interfaces;
using NG.Models;
using System;

namespace Zeta
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
        }

        //public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    // Add framework services.
        //    services.AddMvc();

        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Aulabit API 0.001");
            });
            app.UseMvc();
            
            //app.UseSwaggerUi();
            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }

        // ================================================
        // Init IOC config
        // ================================================

        public IContainer ApplicationContainer { get; private set; }
        public IConfigurationRoot Configuration { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Aulabit API", Version = "V 0.001" }));

            var builder = new ContainerBuilder();
            builder.RegisterType<Student>().As<IStudent>();
            builder.RegisterType<SchoolSubject>().As<ISchoolSubject>();
            builder.RegisterType<SchoolAttendance>().As<ISchoolAttendance>();
            builder.RegisterType<Message>();
            builder.Populate(services);
            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);

        }

    }
}
