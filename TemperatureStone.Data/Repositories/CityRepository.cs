using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TemperatureStone.Data.DataContexts;
using TemperatureStone.Data.ExternalAccesses;
using TemperatureStone.Domain;
using TemperatureStone.Domain.Repositories;

namespace TemperatureStone.Data.Repositories
{
	public class CityRepository : ICityRepository
	{
		private TemperatureStoneDataContext db = new TemperatureStoneDataContext();

		public City Get(string name)
		{
			try
			{
				name = ExternalAccess.EncodeUTF8(name);

				City city = new City();
				city = db.Cities.FirstOrDefault(x => x.Name == name);

				return city;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		
		public string Create(string name)
		{
			try
			{
				if (name is null || name == "")
					return "Nome da cidade inválido.";

				name = ExternalAccess.EncodeUTF8(name);

				//Verifica se existe cidade com o mesmo nome na base
				if (db.Cities.Any(e => e.Name == name))
					return "Já existe a cidade " + name + " cadastrada.";

				//Verifica se nome da cidade existe na base hgbrasil
				if (ExternalAccess.CheckCity(name))
					return "Cidade " + name + " não encontrada na base de consulta de clima.";

				City city = new City { Name = name };

				db.Cities.Add(city);
				db.SaveChanges();

				return "ok";
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		
		public string CreateCEP(string cep)
		{
			if (Regex.Matches(cep, @"[a-zA-Z]").Count > 0 || cep.Count(Char.IsDigit) != 8)
				return "CEP informado em formato incorreto";

			cep = string.Concat(cep.Where(char.IsDigit));

			string localidade = ExternalAccess.FindByCep(cep);

			if (localidade == "")
				return "CEP não encontrado.";

			return Create(localidade);
		}
		
		public string Delete(string name)
		{
			try
			{
				name = ExternalAccess.EncodeUTF8(name);

				City city = db.Cities.FirstOrDefault(x => x.Name == name);
				if (city == null)
				{
					return "Cidade não encontrada.";
				}

				db.Cities.Remove(city);
				db.SaveChanges();

				return "ok";
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		
		public List<City> GetMax()
		{
			try
			{
				List<City> cities = new List<City>();
				List<City> topCities = new List<City>();
				List<Temperature> temperatures = new List<Temperature>();

				//Guarda a maior temperatura registrada para cada cidade
				cities = GetAllCities().ToList();
				foreach(var item in cities)
				{
					var topCityTemperature = db.Temperatures
						.Where(x => x.CityId == item.Id)
						.OrderByDescending(t => t.LocalTemperature)
						.Take(1).ToList();
					if(topCityTemperature != null)
						temperatures.Add(topCityTemperature[0]);
					item.Temperatures.Clear();
				}

				List<Temperature> SortedTemperatures = temperatures.OrderByDescending(t => t.LocalTemperature).Take(3).ToList();
				foreach(var temperature in SortedTemperatures)
				{
					var city = cities.FirstOrDefault(x => x.Id == temperature.CityId);
					city.Temperatures.Add(temperature);
					topCities.Add(city);
				}

				return topCities;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public IQueryable<City> GetAllCities()
		{
			return db.Cities;
		}

		public void Dispose()
		{
			db.Dispose();
		}
	}
}