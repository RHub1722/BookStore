using System.IO;
using AutoMapper;
using Entities;
using Entities.Entities;
using Entities.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Repository;
using Repository.Interfaces;
using Services;
using Shared;
using Swashbuckle.AspNetCore.Swagger;

namespace BookStore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDataContextAsync, EFContext>();
            //register Repos
            services.AddSingleton<IRepository<Book>, Repository<Book>>();
            services.AddSingleton<IRepository<Category>, Repository<Category>>();
            services.AddSingleton<IRepository<Order>, Repository<Order>>();
            services.AddSingleton<IRepository<User>, Repository<User>>();
            //register BL services
            services.AddSingleton<IBooksServices, BooksServices>();
            services.AddSingleton<ICategoriesService, CategoriesService>();
            services.AddSingleton<IOrdersService, OrdersService>();
          

            // Add EFframework services.
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EFContext>(x =>x.UseSqlServer(connection));

            services.AddMvc();

            services.AddAutoMapper(conf => conf.AddProfile(new MappingProfile()));

            services.AddSwaggerGen(
                x => x.SwaggerDoc("V1", new Info() {Title = "My API", Version = "v1"}));

            var pathDoc = Configuration["Swagger:FileName"];
            services.ConfigureSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "My API",
                        Version = "v1",
                        TermsOfService = "None"
                    }
                );

                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, pathDoc);
                options.IncludeXmlComments(filePath);
                options.DescribeAllEnumsAsStrings();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseMvcWithDefaultRoute();//added
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseDefaultFiles();
            app.UseSwagger();
            app.Seed();

            app.UseMvc();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
