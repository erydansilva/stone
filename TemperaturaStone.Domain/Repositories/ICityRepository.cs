using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureStone.Domain.Repositories
{
	public interface ICityRepository : IDisposable
	{
		List<City> Get();
		City Get(string name);
		void Create(City city);
		void Create(int cep);
		void Update(City city);
		void Delete(string name);
		void PatchDelete(string name);
		void GetMax(string name);
	}
}