using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Installations.API.Controllers;

[ApiController]
[Route("api/installations")]
public class InstallationController : ControllerBase
{
    private readonly IMediator _mediator;

    public InstallationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
}