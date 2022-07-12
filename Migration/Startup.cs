using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Queries.Person;
using Application.Queries.User;
using Microsoft.EntityFrameworkCore;
using Infrastracture.Configuration;

namespace Migration
{
    public class Startup
    {
        readonly string CORSOpenPolicy = "OpenCORSPolicy";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: CORSOpenPolicy,
                    builder => {
                        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                    });
            });

            services.AddControllersWithViews();
            var connectionString = Configuration.GetConnectionString("MigrationDb");
            services.AddScoped<IUserQueries, UserQueries>();
            services.AddScoped<IPersonQueries, PersonQueries>();
            services.AddDbContext<MigrationContext>(x => x.UseSqlServer(connectionString));
            services.AddMvc();


            services.IncludeBaseServices();
            services.IncludeDataServices(typeof(IUnitOfWork));


            services.IncludeMediator(new List<Type> { });
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseCors(CORSOpenPolicy);
            app.UseAuthorization();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
