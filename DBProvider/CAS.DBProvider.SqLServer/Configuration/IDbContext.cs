using Microsoft.EntityFrameworkCore;

namespace CAS.DBProvider.SqLServer.Configuration
{
	public interface IDbContext
	{
		DbSet<T> Set<T>() where T : class;
		int SaveChanges();
		Task<int> SaveChangesAsync();
	}
}
