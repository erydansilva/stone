﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TemperatureStone.Domain
{
	public class Temperature
	{
		public int Id { get; set; }

		[Required]
		[Column(Order = 0)]
		public DateTime Date { get; set; }

		[Required]
		[Column(Order = 1)]
		public short LocalTemperature { get; set; }

		public int CityId { get; set; }
	}
}