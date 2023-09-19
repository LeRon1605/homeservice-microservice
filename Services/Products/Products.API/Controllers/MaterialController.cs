using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Commands.MaterialCommands.AddMaterial;
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
    
    [HttpPost]
    public async Task<ActionResult<GetMaterialDto>> AddMaterial(AddMaterialCommand command)
    {
        var material = await _mediator.Send(command);
        return Ok(material);
        // return CreatedAtAction(nameof("GetMaterialById"), new {id = material.Id}, material);
    }
}