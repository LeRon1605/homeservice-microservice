namespace Contracts.Application.Dtos.Contracts;

public class ContractActionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public string? Comment { get; set; }
    
    public Guid ContractId { get; set; }
    public Guid ActionByEmployeeId { get; set; }
    
    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
}