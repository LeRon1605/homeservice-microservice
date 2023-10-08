using System.Linq.Expressions;
using BuildingBlocks.Domain.Specification;
using Contracts.Domain.ContractAggregate.Enums;

namespace Contracts.Domain.ContractAggregate.Specifications;

public class GetContractsSpecification : Specification<Contract>
{
    public GetContractsSpecification(string? search,
                                     int pageSize,
                                     int pageIndex,
                                     ICollection<ContractStatus>? statusList,
                                     DateTime? fromDate,
                                     DateTime? toDate,
                                     DateFilterType? dateType,
                                     Guid? salePersonId,
                                     Guid? customerServiceRepId)
    {
        AddInclude(x => x.Customer);
        AddInclude(x => x.Items);

        if (!string.IsNullOrWhiteSpace(search))
        {
            AddFilter(x => x.No.ToString().Contains(search) || x.Customer!.Name.Contains(search));
        }

        if (statusList != null)
            AddFilter(x => statusList.Contains(x.Status));

        if (dateType.HasValue)
        {
            Expression<Func<Contract, bool>> dateFilter = dateType.Value switch
            {
                DateFilterType.DateQuoted => x => x.QuotedAt >= fromDate && x.QuotedAt <= toDate,
                DateFilterType.DateSold => x => x.SoldAt >= fromDate && x.SoldAt <= toDate,
                DateFilterType.InvoiceDate => x => x.InvoiceDate >= fromDate && x.InvoiceDate <= toDate,
                DateFilterType.ActualInstallDate => x => x.ActualInstallationDate >= fromDate && x.ActualInstallationDate <= toDate,
                DateFilterType.EstimatedInstallDate => x => x.EstimatedInstallationDate >= fromDate && x.EstimatedInstallationDate <= toDate,
                _ => x => false
            };

            AddFilter(dateFilter);
        }

        if (salePersonId.HasValue)
            AddFilter(x => x.SalePersonId == salePersonId.Value);

        if (customerServiceRepId.HasValue)
            AddFilter(x => x.CustomerServiceRepId == customerServiceRepId.Value);

        ApplyPaging(pageIndex: pageIndex, pageSize: pageSize);
    }
}
