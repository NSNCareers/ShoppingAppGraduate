using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Context;
using WebAPI.Dependency;

namespace WebAPI
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            //Connection string 
            var conString = _configuration.GetConnectionString("ShoppingCartDBConnection");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();
            services.AddDbContextPool<ShoppingCartContext>(options =>
            options.UseSqlServer(conString).EnableSensitiveDataLogging());
            services.ConfigureShoppingCartServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
