﻿using System.Text.Json.Serialization;
using Installations.Domain.InstallationAggregate.Enums;

namespace Installations.Application.Dtos;

public class InstallationDto
{
    public Guid Id { get; set; }
    public ContractInInstallationDto Contract { get; set; } = null!;
    public ProductInInstallationDto Product { get; set; } = null!;
    public DateTime InstallDate { get; set; }
    
    public InstallerDto Installer { get; set; } = null!;
    
    public string FloorType { get; set; } = null!;
    
    public double EstimatedHours { get; set; }
    public double ActualHours { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public InstallationStatus? Status { get; set; }
}

public class ProductInInstallationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}

public class ContractInInstallationDto
{
    public Guid Id { get; set; }
    public int No { get; set; }
}