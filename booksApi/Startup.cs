using booksApi.Data;
using booksApi.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace booksApi
{
    public class Startup
    {
        public string ConnectionString { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            // ***Longhand
            // ***ConnectionString = Configuration.GetValue("ConnectionStrings", "MyConnectionString");
            
            // ***ShortHand
            ConnectionString = Configuration.GetConnectionString("MyConnectionString");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // ***Configure DBContext with SQL DB
            // ***This example registers a DbContext subclass called "AppDbContext" (user defined class) as a scoped service in the ASP.NET Core application service provider (a.k.a. the dependency injection container)
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));

            // ***Configure the added services
            services.AddTransient<BooksService>();
            services.AddTransient<AuthorsService>();
            services.AddTransient<PublishersService>();


            // *** Sets Defgault API version
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;

                // *** Used for header version specification 
                config.ApiVersionReader = new HeaderApiVersionReader("custome-version-header");
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo { Title = "booksApi_updatedtitle", Version = "v2" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "booksApi_updated v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //AppDbInitializer.Seed(app);
        }
    }
}
