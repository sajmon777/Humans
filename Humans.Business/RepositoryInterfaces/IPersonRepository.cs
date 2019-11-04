using Humans.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Humans.Business.RepositoryInterfaces
{
	public interface IPersonRepository
	{
		Person GetPerson(int personId);

		void InsertPerson(Person person);

		void MarryPersons(int personAId, int personBId);

		void DivorcePersons(int personAId, int personBId);

		void PersonDeath(int personId);

		List<MarriedDateListView> GetMarriedDateListView(DateTime date);

		List<MarriedListView> GetMarriedListView();
	}
}
