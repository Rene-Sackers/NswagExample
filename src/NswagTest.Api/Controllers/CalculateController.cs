using System.Net;
using Microsoft.AspNetCore.Mvc;
using NswagTest.Api.Models;

namespace NswagTest.Api.Controllers;

[ApiController]
[Route("[controller]")]
[Consumes("application/json")]
[Produces("application/json")]
public class CalculateController : ControllerBase
{
	[HttpPost(Name = "Add")]
	[ProducesResponseType(typeof(AddResponseData), (int)HttpStatusCode.OK)]
	public AddResponseData Add([FromBody] AddRequestData data)
	{
		return new()
		{
			Sum = data.Value1 + data.Value2
		};
	}
}
