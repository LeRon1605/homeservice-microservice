using MediatR;
using Microsoft.AspNetCore.Mvc;
using Products.Application.Dtos;

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
    
}