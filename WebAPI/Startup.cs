using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
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
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();
            services.AddDbContextPool<ShoppingCartContext>(options =>
            options.UseSqlServer(conString).EnableSensitiveDataLogging());
            services.ConfigureShoppingCartServices();
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ShoppingCart API",
                    Description = "An API to display items in a Users Shopping Cart",
                    TermsOfService = new Uri("http://loging.nsncareers.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Ngwesse Elvis",
                        Email = "NSNCareers@outlook.com",
                        Url = new Uri("http://loging.nsncareers.com"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("http://loging.nsncareers.com"),
                    }
                });

                c.AddSecurityDefinition("[auth scheme: same name as defined for asp.net]", new OpenApiSecurityScheme()
                {
                    Description = "Api key needed to access the endpoints. X-Api-Key: SNSCareers_API_Key",
                    In = ParameterLocation.Header,
                    Name = "X-ApiKey",
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "X-ApiKey",
                            Type = SecuritySchemeType.ApiKey,
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "X-ApiKey",
                            },
                        },
                        new string[] {}
                    }});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ShoppingCartContext context)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NSNCareers Shopping App API");
            });

            app.UseForwardedHeaders();

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
            context.Database.Migrate();
        }
    }
}
