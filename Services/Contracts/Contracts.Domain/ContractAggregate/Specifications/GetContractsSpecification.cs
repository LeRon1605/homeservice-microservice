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

        if (dateType.HasValue && fromDate.HasValue && toDate.HasValue)
        {
            fromDate = fromDate.Value.Date;
            toDate = toDate.Value.Date;
            Expression<Func<Contract, bool>> dateFilter = dateType.Value switch
            {
                DateFilterType.DateQuoted => x => x.QuotedAt.Date >= fromDate && x.QuotedAt.Date <= toDate,
                DateFilterType.DateSold => x => x.SoldAt.HasValue 
                                                && x.SoldAt.Value.Date >= fromDate 
                                                && x.SoldAt.Value.Date <= toDate,
                DateFilterType.InvoiceDate => x => x.InvoiceDate.HasValue 
                                                   && x.InvoiceDate.Value.Date >= fromDate 
                                                   && x.InvoiceDate.Value.Date <= toDate,
                DateFilterType.ActualInstallDate => x => x.ActualInstallationDate.HasValue 
                                                         && x.ActualInstallationDate.Value.Date >= fromDate 
                                                         && x.ActualInstallationDate.Value.Date <= toDate,
                DateFilterType.EstimatedInstallDate => x => x.EstimatedInstallationDate.HasValue 
                                                            && x.EstimatedInstallationDate.Value.Date >= fromDate 
                                                            && x.EstimatedInstallationDate.Value.Date <= toDate,
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
