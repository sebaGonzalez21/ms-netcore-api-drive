using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ms_api_drive.Imp;
using ms_api_drive.Service;
using ms_api_drive.Util;
using Microsoft.OpenApi.Models;
using HealthChecks.UI.Client;
using log4net;
using System.Reflection;
using System.Net;
/**
* Sebastian Gonzalez
* seba_gonza_@hotmail.com
* */
namespace ms_api_drive
{
    public class Startup
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddHealthChecks();
            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("Api Drive", "https://"+Configuration.GetConnectionString(Constant.Host)+":" +Configuration.GetConnectionString(Constant.Port)+Constant.ApiHealth);
            });

            services.AddCors(o => o.AddPolicy(Constant.Policy, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            
            services.AddTransient<IDriveService, ImpDrive>();
            services.AddTransient<IGoogleDriveCredentialService, ImpGoogleDriveCredential>();
            services.AddTransient<IGoogleDriveService, ImpGoogleDrive>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API Drive",
                    Description = "Service for upload folders and files into Google Drive",
                    Contact = new OpenApiContact
                    {
                        Name = "Sebastian Gonzalez",
                        Email = "seba_gonza_@hotmail.com"
                    }
                });

            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseStaticFiles();

            //https://localhost:44385/healthchecks-ui#/healthchecks
            app.UseHealthChecks(Constant.ApiHealth, new HealthCheckOptions()
            {
                //that's to the method you created 
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            }
            ).UseHealthChecksUI(setup =>
            {
                setup.AddCustomStylesheet(@"/src/app/HealthCheck/Ui/dotnet.css");
            });
            //politica de cors
            app.UseCors(Constant.Policy);
            // Enable middleware to serve generated Swagger as a JSON endpoint.

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API DRIVE V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHealthChecksUI();
        }

        
    }
}
