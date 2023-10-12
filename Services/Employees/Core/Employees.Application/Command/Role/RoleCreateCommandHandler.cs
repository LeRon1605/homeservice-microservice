using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Employees.Domain.EmployeeAggregate;
using Microsoft.Extensions.Logging;

namespace Employees.Application.Command.Role;

public class RoleCreateCommandHandler : ICommandHandler<RoleCreateCommand>
{
    private readonly ILogger<RoleCreateCommandHandler> _logger;
    private readonly IRepository<Domain.RoleAggregate.Role> _roleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RoleCreateCommandHandler(ILogger<RoleCreateCommandHandler> logger,
        IUnitOfWork unitOfWork, IRepository<Domain.RoleAggregate.Role> roleRepository)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _roleRepository = roleRepository;
    }

    public async Task Handle(RoleCreateCommand request, CancellationToken cancellationToken)
    {
        var role = new Domain.RoleAggregate.Role(request.RoleId, request.RoleName);
        _roleRepository.Add(role);
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Role created with name {RoleName}", request.RoleName);
    }
}