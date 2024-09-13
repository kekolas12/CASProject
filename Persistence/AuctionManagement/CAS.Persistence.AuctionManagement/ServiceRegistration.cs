using AuctionService.Data;
using CAS.Core.Infrastructure;
using CAS.DBProvider.SqLServer.Configuration;
using CAS.DBProvider.SqLServer.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CAS.Persistence.GeneralManagement
{

    public class ServiceRegistration : IStartupApplication
    {
        public int Priority => 0;
        public bool BeforeConfigure => false;

        private void RegisterInstallService(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            RegisterInstallService(services, configuration);
        }
        public void Configure(IApplicationBuilder application, IWebHostEnvironment webHostEnvironment)
        {

        }
    }
}
