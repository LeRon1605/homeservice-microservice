using Contracts.Domain.ContractAggregate;
using Contracts.Infrastructure.EfCore.Repositories;

namespace Contracts.API.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IContractRepository, ContractRepository>();
        
        return services;
    }
}