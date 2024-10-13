using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Pharmacy_2.Data
{
	public class PharmacyContextFactory: IDesignTimeDbContextFactory<PharmacyContext>
	{
		public PharmacyContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<PharmacyContext>();

			var builder = new ConfigurationBuilder();
			builder.SetBasePath(Directory.GetCurrentDirectory());
			builder.AddJsonFile("appsettings.json");
			var config = builder.Build();

			string connectionString = config.GetConnectionString("DefaultConnection");
			optionsBuilder.UseSqlServer(connectionString);
			return new PharmacyContext(optionsBuilder.Options);
		}
	}
}
