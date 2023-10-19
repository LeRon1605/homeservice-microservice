namespace Contracts.Application.Dtos.Contracts;

public class ContractActionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime Date { get; set; }
    public string? Comment { get; set; }
    
    public Guid ContractId { get; set; }
    public EmployeeInContractActionDto Employee { get; set; } = null!;
    
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
}

public class EmployeeInContractActionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}