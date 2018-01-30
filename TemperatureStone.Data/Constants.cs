using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureStone.Data
{
	public static class Constants
	{
		public static string HgBrasilAddress { get { return "https://api.hgbrasil.com/weather/?format=json&city_name="; } }
		public static string HgBrasilToken { get { return "&key=86eb7b15"; } }

		public static string ViaCepAddress { get { return "https://viacep.com.br/ws/"; } }
		public static string ViaCepJsonFormat { get { return "/json"; } }
	}
}
