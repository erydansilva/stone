using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureStone.Domain.Repositories
{
	public interface ITemperatureRepository : IDisposable
	{
		string PatchCity(string name);   //2.2.4
	}
}