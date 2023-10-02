namespace Contracts.Domain.ContractAggregate;

public enum ContractStatus
{
    Open,
    Quotation,
    Signed,
    InProgress,
    PartiallyPaid,
    FullyPaid,
    Overdue,
    Closed
}