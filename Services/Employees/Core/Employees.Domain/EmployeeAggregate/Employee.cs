using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Domain.Models;
using Employees.Domain.EmployeeAggregate.Enums;
using Employees.Domain.EmployeeAggregate.Specifications;
using Employees.Domain.Exceptions;
using Employees.Domain.RoleAggregate;

namespace Employees.Domain.EmployeeAggregate;

public class Employee : AggregateRoot
{
    public int EmployeeCode { get; private set; }
    public string FullName { get; private set; }
    public string Position { get; private set; }
    public string Email { get; private set; }
    public string? Phone { get; private set; }
    public string RoleId { get; private set; }
    public Role Role { get; private set; }
    public Status Status { get; private set; }

    private Employee()
    {
    }

    public Employee(int employeeCode, string fullName, string position, string email, string roleId, string roleName,
        string? phone, Status status)
    {
        EmployeeCode = employeeCode;
        FullName = Guard.Against.NullOrWhiteSpace(fullName, nameof(fullName));
        Position = Guard.Against.NullOrWhiteSpace(position, nameof(position));
        Email = Guard.Against.NullOrWhiteSpace(email, nameof(email));
        Phone = phone;
        RoleId = roleId;
        Status = status;
    }


    public static async Task<Employee> InitAsync(int employeeCode, string fullName,
        string position,
        string email,
        string? phone,
        string roleId,
        string roleName,
        Status status,
        IRepository<Employee> employRepository)
    {
        await IsExistingEmployeeCode(employeeCode, employRepository);
        await IsExistingEmployeeEmail(email, employRepository);
        var employee = new Employee(employeeCode, fullName, position, email, roleId, roleName, phone, status);
        return employee;
    }

    public async Task UpdateAsync(int employeeCode, string fullName,
        string position,
        string email,
        string? phone,
        string roleId,
        string roleName,
        IRepository<Employee> employRepository)
    {
        await SetCodeAsync(employeeCode, employRepository);
        await SetEmailsync(email, employRepository);
        FullName = fullName;
        Position = position;
        Phone = phone;
        RoleId = roleId;
        Role.Name = roleName;
    }

    private async Task SetCodeAsync(int employeeCode, IRepository<Employee> employeeRepository)
    {
        if (EmployeeCode != employeeCode)
        {
            await IsExistingEmployeeCode(employeeCode, employeeRepository);
            EmployeeCode = employeeCode;
        }
    }

    private async Task SetEmailsync(string email, IRepository<Employee> employeeRepository)
    {
        if (Email != email)
        {
            await IsExistingEmployeeEmail(email, employeeRepository);
            Email = email;
        }
    }

    private static async Task IsExistingEmployeeCode(int employeeCode, IRepository<Employee> employRepository)
    {
        var isExistingEmployeeCode =
            await employRepository.AnyAsync(new IsExistingEmployeeCodeSpecification(employeeCode));
        if (isExistingEmployeeCode)
            throw new DuplicateEmployeeCodeException(employeeCode.ToString());
    }

    private static async Task IsExistingEmployeeEmail(string email, IRepository<Employee> employRepository)
    {
        var isExistingEmployeeCode = await employRepository.AnyAsync(new IsExistingEmployeeEmailSpecification(email));
        if (isExistingEmployeeCode)
            throw new DuplicateEmployeeEmailException(email);
    }
}