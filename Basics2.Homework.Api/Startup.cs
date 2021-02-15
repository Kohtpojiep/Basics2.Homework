using System.IO;
using System.Linq;
using Basics2.Homework.BusinessLogic.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Basics2.Homework.DataAccess.MSSQL;
using Basics2.Homework.DataAccess.MSSQL.Repositories;
using Basics2.Homework.Domain.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Basics2.Homework.Api
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
            services.AddAutoMapper(typeof(DataAccessMappingProfile));

            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IShowcaseRepository, ShowcaseRepository>();
            services.AddTransient<IShowcaseProductRepository, ShowcaseProductRepository>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IShowcaseService, ShowcaseService>();
            services.AddTransient<IShowcaseProductService, ShowcaseProductService>();

            services.AddDbContext<ShopContext>(x =>
                x.UseSqlServer(Configuration.GetConnectionString("ConnectionDbContext")));

            services.AddControllers().AddFluentValidation();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basics2 API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.IncludeXmlComments("Basics2.Homework.Api.xml");
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basics2.Api v1");
                });
                app.UseReDoc(c =>
                {
                    c.SpecUrl("/swagger/v1/swagger.json");
                    c.EnableUntrustedSpec();
                    c.ScrollYOffset(10);
                    c.HideHostname();
                    c.HideDownloadButton();
                    c.ExpandResponses("200,201");
                    c.RequiredPropsFirst();
                    c.NoAutoAuth();
                    c.PathInMiddlePanel();
                    c.HideLoading();
                    c.NativeScrollbars();
                    c.DisableSearch();
                    c.OnlyRequiredInSamples();
                    c.SortPropsAlphabetically();
                });
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async ex =>
                    {
                        ex.Response.StatusCode = 500;
                        await ex.Response.WriteAsync("smth went wrong, check it later");
                    });
                });
            }
 
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
