﻿using BuildingBlocks.Domain.Event;

namespace Shopping.Domain.OrderAggregate.Events;

public class OrderRejectDomainEvent : IDomainEvent
{
    public Order Order { get; init; }

    public OrderRejectDomainEvent(Order order)
    {
        Order = order;
    }
}