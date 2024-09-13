using AuctionService.Data;
using CAS.Domain.AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAS.DBProvider.SqLServer.SeedDatas
{
    public class AuctionSeedData
    {
        public static void SeedData(AuctionDbContext context)
        {
            context.Database.Migrate();

            if (context.Auctions.Any())
            {
                Console.WriteLine("Data var bu yüzden beslemeye gerek yok!");
                return;
            }

            var auctions = new List<Auction>
            {
                new Auction
                {
                    Id = Guid.NewGuid(),
                    Status = Status.Live,
                    ReservePrice = 20000,
                    Seller = "Bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(10),
                    Item = new Item
                    {
                        Id = Guid.NewGuid(), // Unique ID for the item
                        Make = "Ford",
                        Model = "GT",
                        Color = "White",
                        Mileage = 500000,
                        Year = 2020,
                        ImageUrl = "https://cdn.pixabay.com/photo/2016/05/06/16/32/car-1376190_960_720.jpg",
                        AuctionId = Guid.NewGuid(),
                    }
                },
                new Auction
                {
                    Id = Guid.NewGuid(),
                    Status = Status.Live,
                    ReservePrice = 15000,
                    Seller = "Alice",
                    AuctionEnd = DateTime.UtcNow.AddDays(8),
                    Item = new Item
                    {
                        Id = Guid.NewGuid(),
                        Make = "Tesla",
                        Model = "Model 3",
                        Color = "Red",
                        Mileage = 30000,
                        Year = 2021,
                        ImageUrl = "https://cdn.pixabay.com/photo/2017/12/27/14/53/tesla-3052064_960_720.jpg",
                        AuctionId = Guid.NewGuid(),
                    }
                },
                new Auction
                {
                    Id = Guid.NewGuid(),
                    Status = Status.Live,
                    ReservePrice = 12000,
                    Seller = "Charlie",
                    AuctionEnd = DateTime.UtcNow.AddDays(5),
                    Item = new Item
                    {
                        Id = Guid.NewGuid(),
                        Make = "Honda",
                        Model = "Civic",
                        Color = "Blue",
                        Mileage = 80000,
                        Year = 2018,
                        ImageUrl = "https://cdn.pixabay.com/photo/2016/09/09/09/05/honda-1652877_960_720.jpg",
                        AuctionId = Guid.NewGuid(),
                    }
                },
                new Auction
                {
                    Id = Guid.NewGuid(),
                    Status = Status.Live,
                    ReservePrice = 18000,
                    Seller = "David",
                    AuctionEnd = DateTime.UtcNow.AddDays(7),
                    Item = new Item
                    {
                        Id = Guid.NewGuid(),
                        Make = "BMW",
                        Model = "X5",
                        Color = "Black",
                        Mileage = 50000,
                        Year = 2019,
                        ImageUrl = "https://cdn.pixabay.com/photo/2016/11/29/10/07/bmw-1866767_960_720.jpg",
                        AuctionId = Guid.NewGuid(),
                    }
                },
                new Auction
                {
                    Id = Guid.NewGuid(),
                    Status = Status.Live,
                    ReservePrice = 22000,
                    Seller = "Eve",
                    AuctionEnd = DateTime.UtcNow.AddDays(12),
                    Item = new Item
                    {
                        Id = Guid.NewGuid(),
                        Make = "Audi",
                        Model = "A6",
                        Color = "Gray",
                        Mileage = 40000,
                        Year = 2022,
                        ImageUrl = "https://cdn.pixabay.com/photo/2015/09/09/14/55/audi-933103_960_720.jpg",
                        AuctionId = Guid.NewGuid(),
                    }
                },
                new Auction
                {
                    Id = Guid.NewGuid(),
                    Status = Status.Live,
                    ReservePrice = 25000,
                    Seller = "Frank",
                    AuctionEnd = DateTime.UtcNow.AddDays(3),
                    Item = new Item
                    {
                        Id = Guid.NewGuid(),
                        Make = "Mercedes",
                        Model = "Benz",
                        Color = "Silver",
                        Mileage = 30000,
                        Year = 2021,
                        ImageUrl = "https://cdn.pixabay.com/photo/2015/04/20/13/43/mercedes-benz-731793_960_720.jpg",
                        AuctionId = Guid.NewGuid()
                    }
                }
            };

            context.AddRange(auctions);

            context.SaveChanges();
        }
    }
}
