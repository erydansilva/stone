using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using TemperatureStone.Domain;

namespace TemperatureStone.Data.ExternalAccesses
{
	public class ExternalAccess
	{
		//Verifica se cidade a ser cadastrada existe em hgbrasil.com
		public static bool CheckCity(string name)
		{
			using (WebClient wc = new WebClient())
			{
				var json = wc.DownloadString(Constants.HgBrasilAddress + name + Constants.HgBrasilToken);
				string localidade = (string)JObject.Parse(json)["city"];

				if (localidade != RemoveAccents(name))
					return false;

				return true;
			}
		}

		//Busca temperatura da cidade selecionada
		public static Temperature GetTemperature(string name)
		{
			{
				Temperature temperature = new Temperature();
				using (WebClient wc = new WebClient())
				{
					var json = wc.DownloadString(Constants.HgBrasilAddress + name + Constants.HgBrasilToken);
					string data = (string)JObject.Parse(json)["results"]["date"];
					string hora = (string)JObject.Parse(json)["results"]["time"];
					string dataHora = data + " " + hora;

					temperature.LocalTemperature = (short)JObject.Parse(json)["results"]["temp"];
					temperature.Date = DateTime.ParseExact(dataHora, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
				}
				return temperature;
			}
		}

		public static string FindByCep(string cep)
		{
			string localidade = "";
			using (WebClient wc = new WebClient())
			{
				var json = wc.DownloadString(Constants.ViaCepAddress + cep + Constants.ViaCepJsonFormat);

				if ((string)JObject.Parse(json)["localidade"] != null)
					localidade = (string)JObject.Parse(json)["localidade"];
			}

			localidade = EncodeUTF8(localidade);
			return localidade;
		}

		//Remove os acentos dos nomes das cidades
		public static string RemoveAccents(string city)
		{
			StringBuilder response = new StringBuilder();
			var arrayText = city.Normalize(NormalizationForm.FormD).ToCharArray();
			foreach (char letter in arrayText)
			{
				if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
					response.Append(letter);
			}
			return response.ToString();
		}

		public static string EncodeUTF8(string city_name)
		{
			if (city_name.Replace(" ","").Any(ch => !Char.IsLetterOrDigit(ch)))
			{ 
				byte[] bytes = Encoding.Default.GetBytes(city_name);
				city_name = Encoding.UTF8.GetString(bytes);
			}
			return city_name;
		}
	}
}