using BuildingBlocks.Domain.Specification;

namespace Installations.Domain.InstallationAggregate.Specifications;

public class InstallationsByContractIdSpecification : Specification<Installation>
{
    public InstallationsByContractIdSpecification(Guid contractId, 
                                                  string? search = null, 
                                                  int pageSize = 0, 
                                                  int pageIndex = 0)
    {
        AddFilter(i => i.ContractId == contractId);
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddFilter(i => 
                           i.ProductName.Contains(search) ||
                           (i.FloorType != null && i.FloorType.Contains(search)) ||
                           ((string)(object)i.Status).Contains(search) ||
                           i.No.ToString().Contains(search));
        }
        
        ApplyPaging(pageIndex: pageIndex, pageSize: pageSize);
    } 
}