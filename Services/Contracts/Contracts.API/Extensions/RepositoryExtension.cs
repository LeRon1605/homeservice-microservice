using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.EfCore.UnitOfWorks;
using Contracts.Domain.ContractAggregate;
using Contracts.Infrastructure.EfCore;
using Contracts.Infrastructure.EfCore.Repositories;

namespace Contracts.API.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork<ContractDbContext>>();
        services.AddScoped<IContractRepository, ContractRepository>();
        return services;
    }
}