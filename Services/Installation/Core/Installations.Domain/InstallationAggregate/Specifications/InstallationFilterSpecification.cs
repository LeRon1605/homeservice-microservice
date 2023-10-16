using BuildingBlocks.Domain.Specification;
using Installations.Domain.InstallationAggregate.Enums;

namespace Installations.Domain.InstallationAggregate.Specifications;

public class InstallationFilterSpecification : Specification<Installation>
{
    public InstallationFilterSpecification(
        InstallationStatus? status,
        DateTime? fromInstallDate,
        DateTime? toInstallDate,
        Guid? productId,
        Guid? installerId,
        string? search,
        int pageIndex,
        int pageSize)
    {
        if (status.HasValue)
        {
            AddFilter(x => x.Status == status.Value);
        }
        
        if (fromInstallDate.HasValue)
        {
            AddFilter(x => (x.InstallDate.HasValue && x.InstallDate.Value.Date >= fromInstallDate.Value.Date));
        }
        
        if (toInstallDate.HasValue)
        {
            AddFilter(x => (x.InstallDate.HasValue && x.InstallDate.Value.Date <= toInstallDate.Value.Date));
        }
        
        if (productId.HasValue)
        {
            AddFilter(x => x.ProductId == productId.Value);
        }
        
        if (installerId.HasValue)
        {
            AddFilter(x => x.InstallerId == installerId.Value);
        }
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddFilter(x => 
                           x.ProductName.Contains(search) ||
                           (x.FloorType != null && x.FloorType.Contains(search)) ||
                           ((string)(object)x.Status).Contains(search) ||
                           x.No.ToString().Contains(search));
        }
        
        ApplyPaging(pageIndex, pageSize);
    }
}