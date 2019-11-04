using Humans.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Humans.Business.ServiceInterfaces
{
	public interface ICityService
	{
		City GetCity(int cityId);

		int GetCityPopulationCount(int cityId);

		List<CityListView> GetCityList(int? minAmountOfPopulation);
	}
}
