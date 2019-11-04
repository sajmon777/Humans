using System;
using System.Collections.Generic;
using System.Text;

namespace Humans.Business.Model
{
	public class Person
	{
		public int? Id { get; set; }
		public int? IdCity { get; set; }
		public int? IdMarriedPerson { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Email { get; set; }
		public DateTime? BirthDate { get; set; }
		public DateTime? MarriedDate { get; set; }
		public SexType Sex { get; set; }
		public bool IsDead { get; set; }
	}
}
