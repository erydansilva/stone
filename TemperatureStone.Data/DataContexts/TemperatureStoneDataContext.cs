using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemperatureStone.Domain;

namespace TemperatureStone.Data.DataContexts
{
	public class TemperatureStoneDataContext : DbContext
	{
		public TemperatureStoneDataContext() : base("ConexaoStone")
		{
			Database.SetInitializer<TemperatureStoneDataContext>(new TemperatureStoneDataContextInitializer());
		}

		public DbSet<City> Cities { get; set; }
		public DbSet<Temperature> Temperatures { get; set; }
	}

	public class TemperatureStoneDataContextInitializer : CreateDatabaseIfNotExists<TemperatureStoneDataContext>
	{
	}
}