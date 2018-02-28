using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChequeIN
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
            services.AddOptions();

            services.Configure<Configurations.Authentication>(Configuration.GetSection("Authentication"));

            if (!bool.TryParse(Configuration["Authentication:DisableAuthentication"], out bool disableAuth)) {
                disableAuth = false;
            }

            // Allow anonymous if the disable authentication setting is set
            if (disableAuth)
            {
                services.AddMvc(opts =>
                {
                    opts.Filters.Add(new AllowAnonymousFilter());
                });
            }
            else
            {
                services.AddMvc();
            }

            // For development purposes only. Allows the frontend to be served
            // from a different domain.
            services.AddCors(o => o.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            // Setting up Auth0 authentication
            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:ApiIdentifier"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var dbctx = new DatabaseContext()) {
                dbctx.Database.Migrate();
                Database.Seed.SeedDatabase();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("AllowAllOrigins");
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
