using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Application.Queries;

namespace Shopping_.API.Controllers
{
	[Route("api/orders")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IMediator _mediator;

		public OrderController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetOrdersAsync([FromQuery] OrderFilterAndPagingQuery query)
		{
			var orders = await _mediator.Send(query);
			return Ok(orders);
		}
	}
}
