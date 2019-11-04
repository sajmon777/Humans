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
	public class PersonRepository : IPersonRepository
	{
		private readonly IDbConnection db;

		public PersonRepository(IDbConnection db)
		{
			this.db = db;
		}

		public Person GetPerson(int personId)
		{
			return db.Query<Person>(@"SELECT * 
                                     FROM Person
                                     WHERE Id = @id",
								  new { id = personId }).SingleOrDefault();
		}

		//Birth of new person
		public void InsertPerson(Person person)
		{
			var result = db.Query<int>(@"INSERT INTO Person (IdCity, IdMarriedPerson, FirstName, LastName, Address, Email, BirthDate, MarriedDate, Sex) 
                                VALUES (@idCity, @idMarriedPerson, @firstName, @lastName, @address, @email, @birthDate, @marriedDate, @sex);
                                SELECT CAST(scope_identity() as int)",
									   new
									   {
										   idCity = person.IdCity,
										   idMarriedPerson = person.IdMarriedPerson,
										   firstName = person.FirstName,
										   lastName = person.LastName,
										   address = person.Address,
										   email = person.Email,
										   birthDate = person.BirthDate,
										   marriedDate = person.MarriedDate,
										   sex = person.Sex,
									   }).Single();
			person.Id = result;
		}

		//Divorce between couples.
		public void DivorcePersons(int personAId, int personBId)
		{
			db.Execute(@"UPDATE Person SET IdMarriedPerson = NULL, MarriedDate = NULL
                            WHERE Id = @idA

						UPDATE Person SET IdMarriedPerson = NULL, MarriedDate = NULL
                            WHERE Id = @idB", new { idA = personAId, idB = personBId });
		}

		//Marry two persons
		public void MarryPersons(int personAId, int personBId)
		{
			db.Execute(@"UPDATE Person SET IdMarriedPerson = @idB, MarriedDate = @date
                            WHERE Id = @idA

						UPDATE Person SET IdMarriedPerson = @idA, MarriedDate = @date
                            WHERE Id = @idB", new { idA = personAId, idB = personBId, date = DateTime.Today });
		}

		//Death of person
		public void PersonDeath(int personId)
		{
			db.Execute(@"UPDATE Person SET IsDead = 1
                            WHERE Id = @id", new { id = personId });
		}

		//How many married in each day of a given month
		public List<MarriedDateListView> GetMarriedDateListView(DateTime date)
		{
			return db.Query<MarriedDateListView>(@"SELECT DAY(P.MarriedDate) AS Day, COUNT(*) AS NumberOfMarrages FROM Person P  
												WHERE MONTH(P.MarriedDate) = @month AND YEAR(P.MarriedDate) = @year
											   GROUP BY P.MarriedDate",
											new { month = date.Month, year = date.Year }).ToList();
		}

		//How many married and unmarried females are in each city
		public List<MarriedListView> GetMarriedListView()
		{
			return db.Query<MarriedListView>(@"SELECT C.Name as CityName, coalesce (MER.Married,0)  as Married , coalesce (MER.Unmarried,0)  as Unmarried  FROM CITY C
											LEFT JOIN ( 
											   SELECT coalesce(UM.Id, M.Id) as Id, UM.Unmarried , M.Married  FROM (
											       SELECT C.Id, COUNT(*) as Unmarried FROM City C
											          JOIN Person P ON C.Id = P.IdCity 
											       WHERE  P.Sex = 2 AND P.IsDead = 0 AND P.IdMarriedPerson is NULL
											       GROUP BY C.Id ) UM 
											   FULL OUTER JOIN  (
												   SELECT C.Id, COUNT(*) as Married FROM City C
												     JOIN Person P ON C.Id = P.IdCity 
												   WHERE  P.Sex = 2 AND P.IsDead = 0 AND P.IdMarriedPerson is not NULL
											     GROUP BY C.Id )M ON   M.Id = UM.Id
											) MER ON MER.Id = C.Id").ToList();
		}
	}
}
