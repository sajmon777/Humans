using Humans.Business.Model;
using Humans.Business.RepositoryInterfaces;
using Humans.Business.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Humans.Business.Service
{
	public class CityService : ICityService
	{
		private ICityRepository cityR;

		public CityService(ICityRepository cityR)
		{
			this.cityR = cityR;
		}

		public City GetCity(int cityId)
		{
			return cityR.GetCity(cityId);
		}

		public List<CityListView> GetCityList(int? minAmountOfPopulation)
		{
			return cityR.GetCityList(minAmountOfPopulation ?? -1);
		}

		public int GetCityPopulationCount(int cityId)
		{
			return cityR.GetCityPopulationCount(cityId);
		}
	}
}
