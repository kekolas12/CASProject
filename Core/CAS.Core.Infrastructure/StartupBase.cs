using AutoMapper;
using CAS.Core.Infrastructure.Mapper;
using CAS.Core.Infrastructure.Middleware;
using CAS.Core.Infrastructure.TypeSearch;
using MassTransit.Initializers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using SME.Core.Infrastructure.TypeConverters;

namespace CAS.Core.Infrastructure
{
    public static class StartupBase
    {
        private static void AddMediator(this IServiceCollection services, ITypeSearcher typeSearcher)
        {
            var assemblies = typeSearcher.GetAssemblies().ToArray();
            services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(assemblies);
            });
        }
        private static void InitAutoMapper(ITypeSearcher typeSearcher)
        {
            var mapperConfigurations = typeSearcher.ClassesOfType<IAutoMapperProfile>();
            var instances = mapperConfigurations
                .Select(mapperConfiguration => (IAutoMapperProfile)Activator.CreateInstance(mapperConfiguration))
                .OrderBy(mapperConfiguration => mapperConfiguration!.Order);
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var instance in instances)
                {
                    cfg.AddProfile(instance.GetType());
                }
            });
            AutoMapperConfig.Init(config);
        }
        private static void RegisterTypeConverter(ITypeSearcher typeSearcher)
        {
            var converters = typeSearcher.ClassesOfType<ITypeConverter>();
            var instances = converters
                .Select(converter => (ITypeConverter)Activator.CreateInstance(converter))
                .OrderBy(converter => converter!.Order);

            foreach (var item in instances)
                item.Register();
        }
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {



            services.AddCors(opts =>
            {
                opts.AddDefaultPolicy(pol =>
                {
                    pol.WithOrigins("http://localhost:5173", "https://localhost:5000", "http://localhost:5001")
                       .AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();

                });
            });

            var typeSearcher = new TypeSearcher();
            services.AddSingleton<ITypeSearcher>(typeSearcher);
            var startupConfigurations = typeSearcher.ClassesOfType<IStartupApplication>();
            var instancesBefore = startupConfigurations
                .Select(startup => (IStartupApplication)Activator.CreateInstance(startup))
                .Where(startup => startup!.BeforeConfigure)
                .OrderBy(startup => startup.Priority);

            InitAutoMapper(typeSearcher);

            RegisterTypeConverter(typeSearcher);

            AddMediator(services, typeSearcher);

            foreach (var instance in instancesBefore)
                instance.ConfigureServices(services, configuration);

            var instancesAfter = startupConfigurations
                .Select(startup => (IStartupApplication)Activator.CreateInstance(startup))
                .Where(startup => !startup!.BeforeConfigure)
                .OrderBy(startup => startup.Priority);

            foreach (var instance in instancesAfter)
                instance.ConfigureServices(services, configuration);
        }
        public static void ConfigureRequestPipeline(IApplicationBuilder application, IWebHostEnvironment webHostEnvironment)
        {
            var typeSearcher = new TypeSearcher();
            var startupConfigurations = typeSearcher.ClassesOfType<IStartupApplication>();

            var instances = startupConfigurations
                .Select(startup => (IStartupApplication)Activator.CreateInstance(startup))
                .OrderBy(startup => startup!.Priority);

            foreach (var instance in instances)
                instance.Configure(application, webHostEnvironment);

            application.UseHttpsRedirection();

            application.UseRouting();

            application.UseCors();
            application.UseSwagger();
            application.UseSwaggerUI(opt => opt.DefaultModelsExpandDepth(-1));
            application.UseMiddleware<GeneralExceptionHandlerMiddleware>();
            application.UseMiddleware<WorkContextMiddleware>();
            application.Use((async (httpContext, next) =>
            {
                if (httpContext.Request.Headers.TryGetValue("x-trace-id", out StringValues stringValues))
                {
                    httpContext.TraceIdentifier = stringValues;
                }

                httpContext.TraceIdentifier ??= Guid.NewGuid().ToString();
                await next();
            }));

            application.UseAuthentication();
            application.UseAuthorization();

            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
        private static T StartupConfig<T>(this IServiceCollection services, IConfiguration configuration) where T : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var config = new T();
            configuration.Bind(config);
            services.AddSingleton(config);
            return config;
        }

    }
}
