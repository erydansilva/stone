using System.Data.Entity;
using TemperatureStone.Domain;

namespace TemperatureStone.Data.DataContexts
{
	public class TemperatureStoneDataContext : DbContext
	{
		public TemperatureStoneDataContext() : base("ConexaoStone")
		{
			Database.SetInitializer(new TemperatureStoneDataContextInitializer());
		}

		public DbSet<City> Cities { get; set; }
		public DbSet<Temperature> Temperatures { get; set; }
	}

	public class TemperatureStoneDataContextInitializer : CreateDatabaseIfNotExists<TemperatureStoneDataContext>
	{
	}
}