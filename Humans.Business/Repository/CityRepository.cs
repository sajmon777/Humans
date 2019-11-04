using Dapper;
using Humans.Business.Model;
using Humans.Business.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Humans.Business.Repository
{
	public class CityRepository : ICityRepository
	{
		private readonly IDbConnection db;

		public CityRepository(IDbConnection db)
		{
			this.db = db;
		}

		public City GetCity(int cityId)
		{
			return db.Query<City>(@"SELECT * 
                                     FROM City
                                     WHERE Id = @id",
								  new { id = cityId }).SingleOrDefault();
		}

		//Populations of cities with more than x amount of residents 
		public List<CityListView> GetCityList(int minAmountOfPopulation)
		{
			return db.Query<CityListView>(@"SELECT* FROM(
										           SELECT C.Name, COUNT(P.Id) as Population FROM City C
										             LEFT JOIN Person P ON C.Id = P.IdCity
										            WHERE P.IsDead = 0 OR P.IsDead is null
												  GROUP BY C.Id, C.Name) AS CT
												WHERE CT.Population > @amount", new { amount = minAmountOfPopulation }).ToList(); 

		}

		//Total count of the population of city
		public int GetCityPopulationCount(int cityId)
		{
			return db.Query<int>(@"SELECT COUNT(*) FROM City C
									JOIN Person P ON C.Id = P.IdCity
									WHERE C.Id = @id and P.IsDead = 0", 
								  new { id = cityId }).Single();
		}
	}
}
