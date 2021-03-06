﻿using System;
using System.Collections.Generic;

namespace TemperatureStone.Domain.Repositories
{
	public interface ITemperatureRepository : IDisposable
	{
		Temperature GetSingleTemperature();
		string PatchCity(string name);   //2.2.4
		void SaveTemperature(List<Temperature> list);
	}
}