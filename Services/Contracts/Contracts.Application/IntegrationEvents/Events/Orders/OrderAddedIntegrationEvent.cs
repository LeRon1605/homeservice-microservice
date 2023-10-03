﻿using BuildingBlocks.Application.IntegrationEvent;

namespace Contracts.Application.IntegrationEvents.Events.Orders;

public record OrderAddedIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; private set; }
    public Guid BuyerId { get; private set; }
    public string CustomerName { get; private set; }
    public string ContactName { get; private set; }
    public string? Email { get; private set; }
    public string Phone { get; private set; }
    public string? Address { get; private set; }
    public string? City { get; private set; }
    public string? State { get; private set; }
    public string? PostalCode { get; private set; }

    public OrderAddedIntegrationEvent(
        Guid orderId,
        Guid buyerId,
        string customerName,
        string contactName,
        string? email,
        string phone,
        string? address,
        string? city,
        string? state,
        string? postalCode)
    {
        OrderId = orderId;
        BuyerId = buyerId;
        CustomerName = customerName;
        ContactName = contactName;
        Email = email;
        Phone = phone;
        Address = address;
        City = city;
        State = state;
        PostalCode = postalCode;
    }
}