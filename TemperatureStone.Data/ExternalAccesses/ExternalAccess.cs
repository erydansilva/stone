using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
				var json = wc.DownloadString("https://api.hgbrasil.com/weather/?format=json&city_name=" + name + "key=86eb7b15");
				string localidade = (string)JObject.Parse(json).First["city"];

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
					var json = wc.DownloadString("https://api.hgbrasil.com/weather/?format=json&city_name=" + name + "&key=86eb7b15");
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
				var json = wc.DownloadString("https://viacep.com.br/ws/" + cep + "/json");

				if ((string)JObject.Parse(json)["localidade"] != null)
					localidade = (string)JObject.Parse(json)["localidade"];
			}

			localidade = EncodeUTF7(localidade);
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

		public static string EncodeUTF7(string city_name)
		{
			byte[] bytes = Encoding.Default.GetBytes(city_name);
			city_name = Encoding.UTF7.GetString(bytes);

			return city_name;
		}
	}
}