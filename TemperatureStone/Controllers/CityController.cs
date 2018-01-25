using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using TemperatureStone.Data.Repositories;
using TemperatureStone.Domain;
using TemperatureStone.Domain.Repositories;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TemperatureStone.Controllers
{
	[RoutePrefix("api")]
	public class CityController : ApiController
	{
		private ICityRepository cityRepository = new CityRepository();
		private ITemperatureRepository temperatureRepository = new TemperatureRepository();

		// GET: city (Não retorna lista de cidades, apenas os endpoints existentes.)
		[Route("city")]
		public HttpResponseMessage GetCity()
		{
			try
			{
				var avisoEndpoints = new
				{
					Aviso = "Apenas os seguintes endpoints estão disponíveis:",
					endPoint1 = "GET: city/{city_name} (Cidade com as temperaturas das últimas 30 horas.)",
					endPoint2 = "POST: city/{city_name} (Cadastra nova cidade para ter temperatura monitorada.)",
					endPoint3 = "POST: city/by_cep/{cep} (Cadastra nova cidade pelo CEP para ter temperatura monitorada.)",
					endPoint4 = "DELETE: city/{city_name} (Remove cidade do monitoramento.)",
					endPoint5 = "PATCH: city/{city_name} (Apaga histórico de temperaturas de uma cidade.)",
					endPoint6 = "GET: cities/max_temperatures (Lista as 3 cidades com maior temperatura já registrada.)"
				};

				var json = JsonConvert.SerializeObject(avisoEndpoints);

				var response = Request.CreateResponse(HttpStatusCode.OK, json);
				return response;
			}
			catch
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Não foi possível efetuar sua solicitação.");
			}
		}

		// GET: city/{city_name} (Cidade com as temperaturas das últimas 30 horas.)
		[ResponseType(typeof(City))]
		[Route("city/{city_name}")]
		public HttpResponseMessage GetCity(string city_name)
		{
			try
			{
				City response = new City();
				response = cityRepository.Get(city_name);

				if(response == null)
					return Request.CreateResponse(HttpStatusCode.NotFound, "Cidade " + city_name  + " não cadastrada.");

				return Request.CreateResponse(HttpStatusCode.OK, response);
			}
			catch
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError, "Não foi possível executar sua solicitação.");
			}
		}

		// POST: city/{city_name} (Cadastra nova cidade para ter temperatura monitorada.)
		[Route("city/{city_name}")]
		[HttpPost]
		public HttpResponseMessage PostCity(string city_name)
		{
			var message = cityRepository.Create(city_name);

			if(message != "ok")
				return Request.CreateResponse(HttpStatusCode.BadRequest, message);

			return Request.CreateResponse(HttpStatusCode.OK, "Cidade " + city_name + " cadastrada com sucesso.");
		}

		// POST: city/by_cep/{cep} (Cadastra nova cidade pelo CEP para ter temperatura monitorada.)
		[Route("city/by_cep/{cep}")]
		[HttpPost]
		public HttpResponseMessage PostCityByCEP(string cep)
		{
			var message = cityRepository.CreateCEP(cep);

			if (message != "ok")
				return Request.CreateResponse(HttpStatusCode.BadRequest, message);

			return Request.CreateResponse(HttpStatusCode.OK, "Cidade cadastrada com sucesso.");
		}

		// DELETE: city/{city_name} (Remove cidade do monitoramento.)
		[Route("city/{city_name}")]
		[HttpDelete]
		public HttpResponseMessage DeleteCity(string city_name)
		{
			var message = cityRepository.Delete(city_name);

			if(message != "ok")
				return Request.CreateResponse(HttpStatusCode.NotFound, "Cidade " + city_name + " não encontrada.");

			return Request.CreateResponse(HttpStatusCode.OK, "Cidade " + city_name + " removida com sucesso.");
		}

		// PATCH: city/{city_name} (Apaga histórico de temperaturas de uma cidade.)
		[Route("city/{city_name}")]
		[HttpPatch]
		public HttpResponseMessage PatchCity(string city_name)
		{
			var message = temperatureRepository.PatchCity(city_name);

			if (message != "ok")
				return Request.CreateResponse(HttpStatusCode.NotFound, message);

			return Request.CreateResponse(HttpStatusCode.OK, "Cidade " + city_name + " removida com sucesso.");
		}

		// GET: cities/max_temperatures (Lista as 3 cidades com maior temperatura já registrada.)
		[Route("city/max_temperatures")]
		[HttpGet]
		public HttpResponseMessage GetMaxCities()
		{
			try
			{
				List<City> response = new List<City>();
				response = cityRepository.GetMax();

				if (response == null)
					return Request.CreateResponse(HttpStatusCode.NotFound, "Nenhuma cidade ou temperatura cadastrada.");

				return Request.CreateResponse(HttpStatusCode.OK, response);
			}
			catch
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError, "Não foi possível executar sua solicitação.");
			}
		}

		protected override void Dispose(bool disposing)
		{
			cityRepository.Dispose();
		}
	}
}