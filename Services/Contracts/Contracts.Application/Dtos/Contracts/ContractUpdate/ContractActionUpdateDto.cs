namespace Contracts.Application.Dtos.Contracts.ContractUpdate;

public class ContractActionUpdateDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public string? Comment { get; set; }
    public Guid ActionByEmployeeId { get; set; }
    public bool? IsDelete { get; set; }
}