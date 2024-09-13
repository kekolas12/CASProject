using AuctionService.Data;
using func = CAS.DBProvider.SqLServer.SeedDatas.AuctionSeedData;

namespace CAS.DBProvider.SqLServer.Providers
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            var scope = app.Services.CreateScope();

            func.SeedData(scope.ServiceProvider.GetService<AuctionDbContext>());
        }



    }
}


