using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TemperatureStone.Data.DataContexts;
using TemperatureStone.Domain;
using TemperatureStone.Domain.Repositories;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace TemperatureStone.Data.Repositories
{
	public class CityRepository : ICityRepository
	{
		private TemperatureStoneDataContext db = new TemperatureStoneDataContext();

		public City Get(string name)
		{
			try
			{
				byte[] bytes = Encoding.Default.GetBytes(name);
				name = Encoding.UTF8.GetString(bytes);

				City city = new City();
				city = db.Cities.FirstOrDefault(x => x.Name == name);

				return city;
			}
			catch (Exception e)
			{
				throw e;
			}
		}
		//---------------------------------------------------------------------------------------------
		public string Create(string name)
		{
			try
			{
				if (name is null || name == "")
					throw new Exception();

				byte[] bytes = Encoding.Default.GetBytes(name);
				name = Encoding.UTF8.GetString(bytes);

				//Verifica se existe cidade com o mesmo nome na base
				if (db.Cities.Count(e => e.Name == name) > 0)
					return "Já existe a cidade " + name + " cadastrada.";

				//Verifica se nome da cidade existe na base hgbrasil

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
		//---------------------------------------------------------------------------------------------
		public string CreateCEP(string cep)
		{
			if (Regex.Matches(cep, @"[a-zA-Z]").Count > 0 || cep.Count(Char.IsDigit) != 8)
				return "CEP informado em formato incorreto";

			cep = string.Concat(cep.Where(char.IsDigit));

			using (WebClient wc = new WebClient())
			{
				var json = wc.DownloadString("https://viacep.com.br/ws/" + cep + "/json");
				string localidade = (string)JObject.Parse(json)["localidade"];

				if (localidade is null)
					return "CEP não encontrado.";

				byte[] bytes = Encoding.Default.GetBytes(localidade);
				localidade = Encoding.UTF8.GetString(bytes);

				//Verifica se nome da cidade existe na base hgbrasil

				City city = new City { Name = localidade };

				db.Cities.Add(city);
				db.SaveChanges();
			}

			return "ok";
		}
		//---------------------------------------------------------------------------------------------
		public string Delete(string name)
		{
			try
			{
				byte[] bytes = Encoding.Default.GetBytes(name);
				name = Encoding.UTF8.GetString(bytes);

				City city = db.Cities.Find(name);
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
		//---------------------------------------------------------------------------------------------
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
	//---------------------------------------------------------------------------------------------

	public void Dispose()
		{
			db.Dispose();
		}
	}
}