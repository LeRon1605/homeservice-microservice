﻿namespace Contracts.Application.Dtos.Contracts.ContractCreate;

public class ContractActionCreateDto
{
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
    public string? Comment { get; set; }
    public Guid ActionByEmployeeId { get; set; }
}