﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using TemperatureStone.Data.DataContexts;
using TemperatureStone.Data.ExternalAccesses;
using TemperatureStone.Domain;
using TemperatureStone.Domain.Repositories;

namespace TemperatureStone.Data.Repositories
{
	public class TemperatureRepository : ITemperatureRepository
	{
		private TemperatureStoneDataContext db = new TemperatureStoneDataContext();

		public Temperature GetSingleTemperature()
		{
			return db.Temperatures.FirstOrDefault();
		}

		public string PatchCity(string name)
		{
			name = ExternalAccess.EncodeUTF8(name);

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

		public void SaveTemperature(List<Temperature> list)
		{
			db.Temperatures.AddRange(list);
			db.SaveChanges();
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}
