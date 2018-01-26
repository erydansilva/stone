using System;
using System.Web;
using System.Web.Http;
using TemperatureStone.Job;

namespace TemperatureStone
{
	public class WebApiApplication : HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);
			JobScheduler.Start();
		}
	}
}