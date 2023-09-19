using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Commands.MaterialCommands.AddMaterial;
using Products.Application.Dtos;
using Products.Domain.MaterialAggregate;

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
    
    [HttpPost]
    public async Task<ActionResult<GetMaterialDto>> AddMaterial(AddMaterialCommand command)
    {
        var material = await _mediator.Send(command);
        return Ok(material);
        // return CreatedAtAction(nameof("GetMaterialById"), new {id = material.Id}, material);
    }
    
    
    
}