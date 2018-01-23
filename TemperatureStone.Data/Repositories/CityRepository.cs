using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemperatureStone.Data.DataContexts;
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
				City city = new City();
				city = db.Cities.Find(name);

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

				//Verifica se existe cidade com o mesmo nome na base

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
		public void Create(int cep)
		{
			throw new NotImplementedException();
		}
		//---------------------------------------------------------------------------------------------
		public string Delete(string name)
		{
			try
			{
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
		public void GetMax(string name)
		{
			throw new NotImplementedException();
		}
		//---------------------------------------------------------------------------------------------
		public void PatchDelete(string name)
		{
			throw new NotImplementedException();
		}
		//---------------------------------------------------------------------------------------------
		public void Dispose()
		{
			db.Dispose();
		}
	}
}