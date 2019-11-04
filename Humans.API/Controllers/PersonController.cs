using System;
using Humans.Business.Model;
using Humans.Business.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Humans.API.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
		readonly IPersonService personS;

		public PersonController(IPersonService personS)
		{
			this.personS = personS;
		}

		[HttpGet("{id}")]
		public IActionResult GetPerson(int id)
		{
			return new JsonResult(personS.GetPerson(id));
		}

		[HttpPost]
		public IActionResult Post([FromBody] Person person)
		{
			return new JsonResult(personS.InsertPerson(person));
		}

		[HttpGet("marrieddatelist")]
		public IActionResult GetCityList(DateTime? date)
		{
			return new JsonResult(personS.GetMarriedDateListView(date));
		}

		[HttpGet("marriedlist")]
		public IActionResult GetCityList()
		{
			return new JsonResult(personS.GetMarriedListView());
		}

		[HttpPost("marrypersons")]
		public void MarryPersons([FromBody]JObject model)
		{
			int idA = model["idA"].ToObject<int>();
			int idB = model["idB"].ToObject<int>();
			personS.MarryPersons(idA, idB);
		}

		[HttpPost("divorcepersons")]
		public void DivorcePersons([FromBody]JObject model)
		{
			int idA = model["idA"].ToObject<int>();
			int idB = model["idB"].ToObject<int>();
			personS.DivorcePersons(idA, idB);
		}

		[HttpPost("persondeath")]
		public void PersonDeath([FromBody]JObject model)
		{
			int id = model["id"].ToObject<int>();
			personS.PersonDeath(id);
		}

	}
}