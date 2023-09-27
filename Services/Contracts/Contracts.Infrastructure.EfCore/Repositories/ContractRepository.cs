using BuildingBlocks.Infrastructure.EfCore;
using BuildingBlocks.Infrastructure.EfCore.Repositories;
using Contracts.Domain.ContractAggregate;

namespace Contracts.Infrastructure.EfCore.Repositories;

public class ContractRepository : EfCoreRepository<Contract>, IContractRepository
{
    public ContractRepository(DbContextFactory dbContextFactory) : base(dbContextFactory)
    {
    }
}