using Microsoft.VisualStudio.TestTools.UnitTesting;
using TemperatureStone.Data.Repositories;
using TemperatureStone.Domain.Repositories;

namespace TemperatureStone.Tests
{
	[TestClass]
	public class TemperatureTests
	{
		private ITemperatureRepository temperatureRepository = new TemperatureRepository();

		[TestMethod]
		public void PatchCity()
		{
			string name = "Rio de Janeiro";

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