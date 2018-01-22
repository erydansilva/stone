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
		[StringLength(150)]
		//[Index(IsUnique = true)]
		public string Name { get; private set; }

		public virtual ICollection<Temperature> Temperatures { get; set; }
	}
}