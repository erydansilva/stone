using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemperatureStone.Domain
{
	public class City
	{
		public int Id { get; set; }

		[Required]
		[Index(IsUnique = true)]
		[StringLength(100)]
		[Column(Order = 0)]
		public string Name { get; set; }

		[Column(Order = 1)]
		public virtual ICollection<Temperature> Temperatures { get; set; }
	}
}