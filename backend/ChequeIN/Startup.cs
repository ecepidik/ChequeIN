using System;
using ChequeIN.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            // Get the environment
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            IHostingEnvironment env = serviceProvider.GetService<IHostingEnvironment>();

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
                    opts.Filters.Add(new ValidateModelStateFilter());
                });
            }
            else
            {
                services.AddMvc(opts =>
                {
                    opts.Filters.Add(new ValidateModelStateFilter());
                });
            }

            // For development purposes only. Allows the frontend to be served
            // from a different domain.
            services.AddCors(o => o.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            // Setup the database context
            services.AddDbContext<DatabaseContext>(options =>
            {
                if (env.IsDevelopment())
                {
                    options.UseSqlite(Configuration["Database:LocalDatabaseName"]);
                }
                else
                {
                    var connection = Environment.GetEnvironmentVariable("DB_CONNTECTION_STRING");
                    options.UseSqlServer(connection);
                }
            });

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
            // Database options
            var options = new DbContextOptionsBuilder<DatabaseContext>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCors("AllowAllOrigins");
                options.UseSqlite(Configuration["Database:LocalDatabaseName"]);
            }
            else
            {
                var connection = Environment.GetEnvironmentVariable("DB_CONNTECTION_STRING");
                options.UseSqlServer(connection);
            }

            // Migrate the database and fill it with seed data
            using (var ctx = new DatabaseContext(options.Options))
            {
                ctx.Database.Migrate();
                if (Configuration["DesignTime"] != "true")
                {
                    Database.Seed.SeedDatabase(ctx);
                }

            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
