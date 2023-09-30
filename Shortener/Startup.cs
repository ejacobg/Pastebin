using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shortener.Models;

namespace Shortener
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IGenerator, IpGenerator>();
            services.AddScoped<IPasteStore, ConsoleStore>();
            services.AddScoped<IShortenerService, TextShortener>();
            services.AddControllers();
            services.AddDbContext<PasteContext>(options =>
            {
                if (Environment.IsProduction())
                    options.UseSqlServer(Configuration.GetConnectionString("Kubernetes"));
                else
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IPasteStore, SqlServerStore>(); // Comment out this line if you want to use ConsoleStore.
            // services.AddScoped<IShortenerService, EchoShortener>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}