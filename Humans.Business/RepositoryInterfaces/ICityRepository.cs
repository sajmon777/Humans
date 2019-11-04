using Humans.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Humans.Business.RepositoryInterfaces
{
	public interface ICityRepository
	{
		City GetCity(int cityId);
		int GetCityPopulationCount(int cityId);
		List<CityListView> GetCityList(int minAmountOfPopulation);
	}
}
