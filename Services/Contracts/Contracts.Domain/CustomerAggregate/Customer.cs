using Ardalis.GuardClauses;
using BuildingBlocks.Domain.Models;

namespace Contracts.Domain.CustomerAggregate;

 public class Customer : AggregateRoot
 {
     public string Name { get; private set; }
     public string ContactName { get; private set; }
     public string? Email { get; private set; }
     public string Phone { get; private set; }
     public CustomerAddress Address { get; private set; }
 
     public Customer(
         string? name,
         string? contactName,
         string? phone,
         string? email = null,
         string? address = null,
         string? city = null,
         string? state = null,
         string? postalCode = null)
     {
         Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
         ContactName = Guard.Against.NullOrWhiteSpace(contactName, nameof(contactName));
         Phone = Guard.Against.NullOrWhiteSpace(phone, nameof(phone));
         Email = email;
         Address = new CustomerAddress(address, city, state, postalCode);
     }
 
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
     private Customer()
     {
     }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
     
     public void Update(string? name,
                        string? contactName,
                        string? phone,
                        string? email = null,
                        string? address = null,
                        string? city = null,
                        string? state = null,
                        string? postalCode = null)
     {
         Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
         ContactName = Guard.Against.NullOrWhiteSpace(contactName, nameof(contactName));
         Phone = Guard.Against.NullOrWhiteSpace(phone, nameof(phone));
         Email = email;
         Address = new CustomerAddress(address, city, state, postalCode);
     }
     
     public void Delete()
     {
         // TODO:
         // Add domain event
         // Check constraints? (customer has no orders, etc.)
     }
     
     public static Customer CreateWithId(
                            Guid id,
                            string? contactName,
                            string? phone,
                            string? email = null,
                            string? address = null,
                            string? city = null,
                            string? state = null,
                            string? postalCode = null)
     {
         var customer = new Customer("Retail Customer", contactName, phone, email, address, city, state, postalCode)
         {
             Id = id
         };
         return customer;
     }
 }   
