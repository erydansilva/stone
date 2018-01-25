using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemperatureStone.Data.DataContexts;
using TemperatureStone.Domain;
using TemperatureStone.Domain.Repositories;

namespace TemperatureStone.Data.Repositories
{
	public class TemperatureRepository : ITemperatureRepository
	{
		private TemperatureStoneDataContext db = new TemperatureStoneDataContext();
		
		public string PatchCity(string name)
		{
			byte[] bytes = Encoding.Default.GetBytes(name);
			name = Encoding.UTF8.GetString(bytes);

			City city = new City();
			city = db.Cities.FirstOrDefault(x => x.Name == name);

			//Verifica se existe cidade cadastrada na base
			if (city == null)
				return "Cidade " + name + " não cadastrada.";

			var all = db.Temperatures.Where(x => x.CityId == city.Id);
			db.Temperatures.RemoveRange(all);
			db.SaveChanges();

			return "ok";
		}
		public void Dispose()
		{
			db.Dispose();
		}
	}
}
