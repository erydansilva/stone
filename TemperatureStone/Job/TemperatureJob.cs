using Quartz;
using System.Collections.Generic;
using System.Net;
using TemperatureStone.Data.ExternalAccesses;
using TemperatureStone.Data.Repositories;
using TemperatureStone.Domain;

namespace TemperatureStone.Job
{
	public class TemperatureJob : IJob
	{
		public void Execute(IJobExecutionContext context)
		{
			CityRepository cityRepository = new CityRepository();
			TemperatureRepository temperatureRepository = new TemperatureRepository();

			//lista todas as cidades
			var cities = cityRepository.GetAllCities();

			List<Temperature> list = new List<Temperature>();

			using (WebClient wc = new WebClient())
			{
				//varre uma por uma pegando a temperatura atual
				foreach (City city in cities)
				{
					Temperature temperature = ExternalAccess.GetTemperature(city.Name);
					temperature.CityId = city.Id;
					list.Add(temperature);
				}
			}

			temperatureRepository.SaveTemperature(list);
		}
	}
}