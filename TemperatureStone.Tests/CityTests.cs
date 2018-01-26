using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemperatureStone.Data.Repositories;
using TemperatureStone.Domain;
using TemperatureStone.Domain.Repositories;

namespace TemperatureStone.Tests
{
	[TestClass]
	public class CityTests
	{
		private ICityRepository cityRepository = new CityRepository();

		[TestMethod]
		public void InsertCity()
		{
			string name = "Maricá";

			string result = cityRepository.Create(name);

			Assert.AreEqual("ok", result);
		}

		[TestMethod]
		public void InsertCityDuplicate()
		{
			string name = "Maricá";

			string result = cityRepository.Create(name);

			Assert.AreEqual("Já existe a cidade " + name + " cadastrada.", result);
		}

		[TestMethod]
		public void InsertCityByCEP()
		{
			string cep = "12246-001";

			string result = cityRepository.CreateCEP(cep);

			Assert.AreEqual("ok", result);
		}

		[TestMethod]
		public void InsertCityDuplicateByCEP()
		{
			string cep = "12246-001";

			string result = cityRepository.CreateCEP(cep);

			Assert.AreNotEqual("ok", result);
		}

		[TestMethod]
		public void GetCity()
		{
			string name = "Rio de Janeiro";

			City response = new City();
			response = cityRepository.Get(name);
			
			Assert.AreEqual(name, response.Name);
		}

		[TestMethod]
		public void GetNonexistentCity()
		{
			string name = "São José do Vale do Rio Preto";

			City response = new City();
			response = cityRepository.Get(name);

			Assert.IsNull(response);
		}

		[TestMethod]
		public void DeleteCity()
		{
			string name = "Maricá";

			string result = cityRepository.Delete(name);

			Assert.AreEqual("ok", result);
		}

		[TestMethod]
		public void DeleteNonexistentCity()
		{
			string name = "Maricá";

			string result = cityRepository.Delete(name);

			Assert.AreEqual("Cidade não encontrada.", result);
		}

		
	}
}