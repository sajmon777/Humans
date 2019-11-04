using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humans.Business.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Humans.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
		readonly ICityService cityS;

		public CityController(ICityService cityS)
		{
			this.cityS = cityS;
		}

		[HttpGet("{id}")]
		public IActionResult GetCity(int id)
		{
			return new JsonResult(cityS.GetCity(id));
		}

		[HttpGet("citypopulation/{id}")]
		public IActionResult GetCityPopulationCount(int id)
		{
			return new JsonResult(cityS.GetCityPopulationCount(id));
		}

		[HttpGet]
		public IActionResult GetCityList(int? amount)
		{
			return new JsonResult(cityS.GetCityList(amount));
		}
	}
}