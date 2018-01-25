using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureStone.Domain.Repositories
{
	public interface ICityRepository : IDisposable
	{
		City Get(string name);				//2.2.1
		string Create(string name);		//2.2.2
		string CreateCEP(string cep);		//3.1
		string Delete(string name);		//2.2.3
		List<City> GetMax();					//2.2.5
	}
}