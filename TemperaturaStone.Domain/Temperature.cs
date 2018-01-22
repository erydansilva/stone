using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TemperatureStone.Domain
{
	public class Temperature
	{
		public int Id { get; set; }

		[Required]
		public DateTime Date { get; set; }

		[Required]
		public short LocalTemperature { get; set; }

		public int CityId { get; set; }
		public virtual City City { get; set; }
	}
}