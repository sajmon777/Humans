using Humans.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Humans.Business.ServiceInterfaces
{
	public interface IPersonService
	{
		Person GetPerson(int personId);

		Person InsertPerson(Person person);

		void DivorcePersons(int personAId, int personBId);

		void MarryPersons(int personAId, int personBId);

		bool AreMarried(int personAId, int personBId);

		void PersonDeath(int personId);

		List<MarriedDateListView> GetMarriedDateListView(DateTime? date);

		List<MarriedListView> GetMarriedListView();
	}
}
