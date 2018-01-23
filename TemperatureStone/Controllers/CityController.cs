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

namespace TemperatureStone.Controllers
{
	[RoutePrefix("api")]
	public class CityController : ApiController
	{
		private ICityRepository repository = new CityRepository();

		// GET: city (Não retorna lista de cidades, apenas os endpoints existentes.)
		[Route("city")]
		public HttpResponseMessage GetCity()
		{
			try
			{
				string[] texto = new string[7]
				{	"Apenas os seguintes endpoints estão disponíveis:",
					"GET: city/{city_name} (Cidade com as temperaturas das últimas 30 horas.)",
					"POST: city/{city_name} (Cadastra nova cidade para ter temperatura monitorada.)",
					"POST: city/by_cep/{cep} (Cadastra nova cidade pelo CEP para ter temperatura monitorada.)",
					"DELETE: city/{city_name} (Remove cidade do monitoramento.)",
					"PATCH: city/{city_name} (Apaga histórico de temperaturas de uma cidade.)",
					"GET: cities/max_temperatures (Lista as 3 cidades com maior temperatura já registrada.)"
				};

				var response = Request.CreateResponse(HttpStatusCode.OK, string.Join("\n", texto));
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
				response = repository.Get(city_name);

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
			var message = repository.Create(city_name);
			
			return Request.CreateResponse(HttpStatusCode.OK, "Cidade " + city_name + " cadastrada com sucesso.");
		}

		//// POST: city/by_cep/{cep} (Cadastra nova cidade pelo CEP para ter temperatura monitorada.)
		//[Route("city/by_cep/{cep}")]

		// DELETE: city/{city_name} (Remove cidade do monitoramento.)
		[Route("city/{city_name}")]
		[HttpDelete]
		public HttpResponseMessage DeleteCity(string city_name)
		{
			var message = repository.Delete(city_name);

			if(message != "ok")
				return Request.CreateResponse(HttpStatusCode.NotFound, "Cidade " + city_name + " não encontrada.");

			return Request.CreateResponse(HttpStatusCode.OK, "Cidade " + city_name + " removida com sucesso.");
		}

		//// PATCH: city/{city_name} (Apaga histórico de temperaturas de uma cidade.)
		//[Route("city/{city_name}")]

		//// GET: cities/max_temperatures (Lista as 3 cidades com maior temperatura já registrada.)
		//[Route("city/max_temperatures")]

		protected override void Dispose(bool disposing)
		{
			repository.Dispose();
		}
	}
}