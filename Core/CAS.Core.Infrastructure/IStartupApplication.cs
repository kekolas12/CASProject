using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CAS.Core.Infrastructure
{
	public interface IStartupApplication
	{
		void ConfigureServices(IServiceCollection services, IConfiguration configuration);
		void Configure(IApplicationBuilder application, IWebHostEnvironment webHostEnvironment);
		int Priority { get; }
		bool BeforeConfigure { get; }
	}
}
