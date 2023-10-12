using BuildingBlocks.Domain.Specification;

namespace Installations.Domain.InstallationAggregate.Specifications;

public class InstallationsByContractIdSpecification : Specification<Installation>
{
    public InstallationsByContractIdSpecification(Guid contractId, 
                                                  string? search, 
                                                  int pageSize, 
                                                  int pageIndex)
    {
        AddFilter(i => i.ContractId == contractId);
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddFilter(i => i.ProductName.Contains(search) ||
                           (i.FloorType != null && i.FloorType.Contains(search)) ||
                           i.Status.ToString().Contains(search) ||
                           i.No.ToString().Contains(search));
        }
        
        ApplyPaging(pageIndex: pageIndex, pageSize: pageSize);
    } 
}