using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using PatientOptOutAPI.Data;
using PatientOptOutAPI.Models;
using PatientOptOutAPI.Services;

namespace PatientOptOutAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            //Read settings from config (appsettings.json)
            var applicationSettings = Configuration.GetSection("ApplicationSettings");
            var frontEndUrl = applicationSettings.GetValue<string>("FrontEndUrl");
            services.Configure<ApplicationSettings>(options => Configuration.GetSection("ApplicationSettings").Bind(options));

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:4200", frontEndUrl).AllowAnyHeader().AllowAnyMethod().AllowCredentials()));
            services.AddDbContext<DataWarehouseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DataWarehouseContext")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Setup Windows Authentication and Policy to restrict access to specific Active Directory group (Specified in appsettings.json)
            services.AddTransient<IClaimsTransformation, WindowsClaimsTransformer>();
            services.AddAuthentication(IISDefaults.AuthenticationScheme);
            services.AddAuthorization(options =>
            {
                options.AddPolicy("OptOutCheckerAccess", policy => policy.RequireClaim(WindowsClaimsTransformer.OptOutClaim));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OptOut Checker API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.UseCors("CorsPolicy");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OptOut Checker API v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc(); 
        }
    }
}
