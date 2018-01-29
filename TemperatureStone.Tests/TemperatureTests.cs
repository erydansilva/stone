using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using TemperatureStone.Data.ExternalAccesses;
using TemperatureStone.Data.Repositories;
using TemperatureStone.Domain;
using TemperatureStone.Domain.Repositories;

namespace TemperatureStone.Tests
{
	[TestClass]
	public class TemperatureTests
	{
		private ITemperatureRepository temperatureRepository = new TemperatureRepository();
		private ICityRepository cityRepository = new CityRepository();

		[TestMethod]
		public void InsertTemperature()
		{
			string name = "Rio de Janeiro";
			string resultOk = cityRepository.Create(name);

			City city = new City();
			city = cityRepository.Get(name);

			//Adiciona informação de temperatura
			Temperature temperature = ExternalAccess.GetTemperature(city.Name);
			temperature.CityId = city.Id;
			List<Temperature> list = new List<Temperature>();
			list.Add(temperature);
			temperatureRepository.SaveTemperature(list);

			//Busca temperatura inserida
			Temperature resp = new Temperature();
			resp = temperatureRepository.GetSingleTemperature();

			Assert.IsNotNull(resp);
		}

		[TestMethod]
		public void PatchCity()
		{
			string name = "Rio de Janeiro";
			string resultOk = cityRepository.Create(name);

			City city = new City();
			city = cityRepository.Get(name);

			//Adiciona informação de temperatura
			Temperature temperature = ExternalAccess.GetTemperature(city.Name);
			temperature.CityId = city.Id;
			List<Temperature> list = new List<Temperature>();
			list.Add(temperature);

			string result = temperatureRepository.PatchCity(name);

			Assert.AreEqual("ok", result);
		}

		[TestMethod]
		public void PatchNonexistentCity()
		{
			string name = "São José do Vale do Rio Preto";

			string result = temperatureRepository.PatchCity(name);

			Assert.AreEqual("Cidade " + name + " não cadastrada.", result);
		}

	}
}