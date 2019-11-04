using Humans.Business.Model;
using Humans.Business.RepositoryInterfaces;
using Humans.Business.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Humans.Business.Service
{
	public class PersonService : IPersonService
	{
		private IPersonRepository personR;

		public PersonService(IPersonRepository personR)
		{
			this.personR = personR;
		}


		public Person GetPerson(int personId)
		{
			return personR.GetPerson(personId);  
		}

		public Person InsertPerson(Person person)
		{
			personR.InsertPerson(person);
			return person; 
		}

		public void DivorcePersons(int personAId, int personBId)
		{
			var personA = personR.GetPerson(personAId);
			var personB = personR.GetPerson(personBId);

			if (personA != null && personB != null)
			{
				if (personA.IdMarriedPerson.HasValue && !personA.IsDead && personB.IdMarriedPerson.HasValue && !personA.IsDead)
				{
					personR.DivorcePersons(personAId, personBId);
				}
			}
		}

		public void MarryPersons(int personAId, int personBId)
		{
			var personA = personR.GetPerson(personAId);
			var personB = personR.GetPerson(personBId);

			if (personA != null && personB != null)
			{
				if (!personA.IdMarriedPerson.HasValue && !personA.IsDead && !personB.IdMarriedPerson.HasValue && !personA.IsDead)
				{
					personR.MarryPersons(personAId, personBId);
				}
			}
		}

		//Given two persons, are they related to each other
		public bool AreMarried(int personAId, int personBId)
		{
			var personA = personR.GetPerson(personAId);
			var personB = personR.GetPerson(personBId);

			if (personA != null && personB != null)
			{
				if (personA.IdMarriedPerson == personB.Id && personB.IdMarriedPerson == personA.Id)
				{
					return true; 
				}
			}
			return false; 
		}

		public List<MarriedDateListView> GetMarriedDateListView(DateTime? date)
		{
			return personR.GetMarriedDateListView(date ?? DateTime.Today);
		}

		public void PersonDeath(int personId)
		{
			personR.PersonDeath(personId);
		}

		public List<MarriedListView> GetMarriedListView()
		{
			return personR.GetMarriedListView();
		}
	}
}
