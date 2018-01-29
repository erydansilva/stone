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

		//Teste de inserção de uma cidade na base
		[TestMethod]
		public void InsertCity()
		{
			string name = "Maricá";

			string result = cityRepository.Create(name);

			Assert.AreEqual("ok", result);
		}

		//Teste de inserção de uma cidade existente na base
		[TestMethod]
		public void InsertCityDuplicate()
		{
			string name = "Rio de Janeiro";

			string resultOk = cityRepository.Create(name);
			string result = cityRepository.Create(name);

			Assert.AreEqual("Já existe a cidade " + name + " cadastrada.", result);
		}

		//Teste de inserção de uma cidade por CEP
		[TestMethod]
		public void InsertCityByCEP()
		{
			string cep = "90450-000";

			string result = cityRepository.CreateCEP(cep);

			Assert.AreEqual("ok", result);
		}

		//Teste de inserção de uma cidade com cep errado
		[TestMethod]
		public void InsertCityNonexistentCEP()
		{
			string cep = "00000111";
			
			string result = cityRepository.CreateCEP(cep);

			Assert.AreEqual("CEP não encontrado.", result);
		}

		//Teste de inserção de uma cidade com CEP já existente na base
		[TestMethod]
		public void InsertCityDuplicateByCEP()
		{
			string cep = "20520202";

			string resultOk = cityRepository.CreateCEP(cep);
			string result = cityRepository.CreateCEP(cep);

			Assert.AreEqual("Já existe a cidade Rio de Janeiro cadastrada.", result);
		}

		//Teste de busca de informações de uma cidade
		[TestMethod]
		public void GetCity()
		{
			string name = "Rio de Janeiro";

			string resultOk = cityRepository.Create(name);

			City response = new City();
			response = cityRepository.Get(name);
			
			Assert.AreEqual(name, response.Name);
		}

		//Teste de busca de informações de uma cidade n~~ao existente na base
		[TestMethod]
		public void GetNonexistentCity()
		{
			string name = "São José do Vale do Rio Preto";

			City response = new City();
			response = cityRepository.Get(name);

			Assert.IsNull(response);
		}

		//Teste de remoção de uma cidade
		[TestMethod]
		public void DeleteCity()
		{
			string name = "Maricá";

			string resultOk = cityRepository.Create(name);

			string result = cityRepository.Delete(name);

			Assert.AreEqual("ok", result);
		}

		//Teste de remoção de uma cidade não presente na base
		[TestMethod]
		public void DeleteNonexistentCity()
		{
			string name = "Maricá";

			string result = cityRepository.Delete(name);

			Assert.AreEqual("Cidade não encontrada.", result);
		}
	}
}