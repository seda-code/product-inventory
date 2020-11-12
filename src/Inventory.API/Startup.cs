using Inventory.API.Data.Repositories;
using Inventory.API.Data.StorageProviders;
using Inventory.API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Inventory.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // 1. Read app settings from AppSettings key from appsettings.json file
            var endpoints = Configuration.GetSection("EndPoints");
            var appSettings = Configuration.GetSection("AppSettings");
            // 2. Register it with a strongly typed object to access it using dependency injection 
            services.Configure<Endpoints>(endpoints);
            services.Configure<AppSettings>(appSettings);

            services.AddSingleton<IStorageProvider, MemoryStorageProvider>();
            services.AddScoped<IDataRepository<Category>, CategoryRepository>();
            services.AddScoped<IDataRepository<Product>, ProductRepository>();


            // services.AddMvcCore().AddAuthorization().AddNewtonsoftJson();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => { configuration.RootPath = "Inventory.Web/"; });


            // services.AddScoped<IInventoryService, InventoryService>();

            services.AddControllers();

            services.AddCors(o => o.AddPolicy("AllowCors", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCors("AllowCors");
            app.UseAuthentication();

            //TODO: distinto orden que el ejemplo de MongoDB-->Ojo que el orden importa!!!
            app.UseHsts();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa => { spa.Options.SourcePath = "Inventory.Web/"; });// no sé si es valido como lo he puesto
        }
    }
}