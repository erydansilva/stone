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
				name = ExternalAccess.EncodeUTF7(name);

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

				name = ExternalAccess.EncodeUTF7(name);

				//Verifica se existe cidade com o mesmo nome na base
				if (db.Cities.Count(e => e.Name == name) > 0)
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

			if (localidade is null)
				return "CEP não encontrado.";

			//Verifica se existe cidade com o mesmo nome na base
			if (db.Cities.Count(e => e.Name == localidade) > 0)
				return "Já existe a cidade " + localidade + " cadastrada.";

			//Verifica se nome da cidade existe na base hgbrasil
			if (ExternalAccess.CheckCity(localidade))
				return "Cidade " + localidade + " não encontrada na base de consulta de clima.";

			City city = new City { Name = localidade };

			db.Cities.Add(city);
			db.SaveChanges();

			return "ok";
		}
		
		public string Delete(string name)
		{
			try
			{
				name = ExternalAccess.EncodeUTF7(name);

				City city = db.Cities.Where(t => t.Name == name).FirstOrDefault();
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
				//cities = db.Cities.Find(x => x.Name == "Rio");

				return cities;
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