using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Dtos;
using Products.Application.Queries.MaterialQuery.GetMaterialWithPagination;

namespace Products.API.Controllers;

[ApiController]
[Route("api/materials")]
public class MaterialController : ControllerBase
{
    private readonly IMediator _mediator;

    public MaterialController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetMaterials([FromQuery] MaterialFilterAndPagingDto materialFilterAndPagingDto)
    {
        var materials = await _mediator.Send(new GetMaterialsWithPaginationQuery(materialFilterAndPagingDto));
        return Ok(materials);
    }
}