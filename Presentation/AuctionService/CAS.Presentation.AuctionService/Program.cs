using AuctionService.Data;
using CAS.DBProvider.SqLServer.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StartupBase = CAS.Core.Infrastructure.StartupBase;

var builder = WebApplication.CreateBuilder(args);

// Diðer servisleri ekleyin
builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// MediatR'ý ekleyin ve tüm handler'larý kaydedin
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAuctionCommandHandler).Assembly));
// Swagger'ý yapýlandýrýn

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuctionService API", Version = "v1" });
});
try
{
    StartupBase.ConfigureServices(builder.Services, builder.Configuration);
}
catch (Exception ex)
{
    Console.WriteLine(ex.InnerException.Message);
}

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuctionService API V1");
        c.RoutePrefix = string.Empty;
    });
}
app.UseAuthorization();
app.MapControllers();

try
{
    DbInitializer.InitDb(app);
}
catch (Exception ex)
{
    Console.WriteLine(ex.InnerException.Message);
}

StartupBase.ConfigureRequestPipeline(app, builder.Environment);
app.Run();