using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using MediatR;
using Persistence.Repositories;
using Microsoft.OpenApi.Models;

namespace Infrastracture.Configuration
{
    public static class ServiceInjector
    {

        public static void IncludeBaseServices(this IServiceCollection services)
        {

            services.AddMemoryCache();
           services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
     
        }


        public static void IncludeDataServices<T>(this IServiceCollection services, T unitOfWork) where T : Type
        {
            if (!typeof(IUnitOfWork).IsAssignableFrom(unitOfWork))
            {
                throw new Exception("UnitOfWork type parameters must impletement IUnitOfWork");
            }



            AddGenericInterfaceService(services, typeof(IQuery));
            AddGenericInterfaceService(services, typeof(IRepository<>));

            //AddGenericInterfaceService(services, typeof(IRepository<>)); will ignore interfaces of Repository<> 
            //So must add this separately
            services.AddScoped(typeof(IRepository<>),
                typeof(Repository<>));

            //  services.AddScoped(UnitOfWork,IUnitOfWork);

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<ITraverse, Traverse>();
        }

        //public static void RegisterInterfaces(this IServiceCollection @this)
        //{


        //    var servicesTypes = AppDomain.CurrentDomain.GetAssemblies()
        //        .Where(x => x.FullName != null && x.FullName.Contains("MongoDB") == false)
        //        .SelectMany(s => s.GetTypes())
        //        .Where(p => typeof(IRepository<>).IsAssignableFrom(p) && p.IsClass &&
        //                    p.GetInterfaces().Any(x => x.Name == "I" + p.Name));

        //    foreach (var type in servicesTypes)
        //    {
        //        @this.AddScoped(type.GetInterfaces().FirstOrDefault(i => i.Name == "I" + type.Name)!, type);
        //    }


        //}

        public static void IncludeMediator(this IServiceCollection services, List<Type> pipelines)
        {
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            foreach (var pipeline in pipelines)
            {
                services.AddTransient(typeof(IPipelineBehavior<,>), pipeline);
            }

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

        }

        static IEnumerable<Type> runtimeTypes;
        private static void AddGenericInterfaceService(IServiceCollection services, Type baseInterface)
        {
            if (runtimeTypes == null)
            {
                runtimeTypes = AppDomain.CurrentDomain.GetAssemblies()
                    //.Where(x => x.FullName != null && !x.FullName.Contains("MongoDB"))
                    .Where(a => !a.IsDynamic)
                    .SelectMany(a => a.GetExportedTypes());
            }
            var rep = runtimeTypes.FirstOrDefault(x => x.Name == "UserRepository");
            //Get All Classes that implement the interface which inherit from IService
            //CustomService: ICustomerService (ICustomerService:IService)
            var servicesTypes = runtimeTypes.Where(p =>
                (baseInterface.IsAssignableFrom(p) || p.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == baseInterface))
                && p.IsClass && !p.IsAbstract);

            foreach (var type in servicesTypes)
            {
                //Note: class may have other interfaces except ICustomerService
                var interfaces = type.GetInterfaces().Where(i => i != baseInterface
                                                                 && (baseInterface.IsAssignableFrom(i)
                                                                     || !(i.IsGenericType && i.GetGenericTypeDefinition() == baseInterface)));
                foreach (var i in interfaces)
                    services.AddScoped(i, type);
            }
        }

        private static void IncludeSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Farcode Template", new OpenApiInfo
                {
                    Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
                    Title = "FarCode Template",
                    Description = "Its a Template ",
                    TermsOfService = new Uri(""),
                    Contact = new OpenApiContact
                    {
                        Name = "Farhad Salehi",
                        Email = "farcodehub@gmail.com",
                        Url = new Uri("www.farcoding.ir")
                    }
                });



                c.AddSecurityDefinition("Authorization", new()
                {
                    Description = "",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Type = SecuritySchemeType.Http,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme, // or ReferenceType.Parameter
                                Id = "Authorization"
                            }
                        },
                        new string[] {}
                    }
                });

                var xmlFile = $"{Assembly.GetEntryAssembly()?.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }


    }
}
