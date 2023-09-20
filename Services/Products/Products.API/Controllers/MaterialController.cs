using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Commands.MaterialCommands.AddMaterial;
using Products.Application.Commands.MaterialCommands.DeleteMaterial;
using Products.Application.Commands.MaterialCommands.UpdateMaterial;
using Products.Application.Dtos;
using Products.Application.Queries.MaterialQuery.GetMaterialById;
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GetMaterialDto>> GetMaterialById(Guid id)
    {
        var material = await _mediator.Send(new GetMaterialByIdQuery(id));
        return Ok(material);
    }
    
    [HttpPost]
    public async Task<ActionResult<GetMaterialDto>> AddMaterial(AddMaterialCommand command)
    {
        var material = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetMaterialById), new {id = material.Id}, material);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<GetMaterialDto>> UpdateMaterial(Guid id, MaterialUpdateDto dto)
    {
        var material = await _mediator.Send(new UpdateMaterialCommand(id, dto));
        return Ok(material);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMaterial(Guid id)
    {
        await _mediator.Send(new DeleteMaterialCommand(id));
        return NoContent();
    }
}