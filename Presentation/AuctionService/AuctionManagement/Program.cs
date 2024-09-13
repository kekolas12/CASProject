using AuctionService.Data;
using CAS.DBProvider.SqLServer.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StartupBase = CAS.Core.Infrastructure.StartupBase;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AuctionService API", Version = "v1" });
});

StartupBase.ConfigureServices(builder.Services, builder.Configuration);
builder.Services.AddRazorPages();







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

StartupBase.ConfigureRequestPipeline(app, builder.Environment);




try
{
    DbInitializer.InitDb(app);
} 
catch(Exception ex)
{
    Console.Write(ex.InnerException.Message);
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
