﻿using System.Drawing;
using BuildingBlocks.Application.IntegrationEvent;

namespace Products.Application.IntegrationEvents.Events;

public record ProductUpdatedIntegrationEvent : IntegrationEvent
{
    public Guid ProductId { get; private set; }
    public string Name { get; private set; }
    public Guid ProductGroupId { get; private set; }
    public Guid? ProductUnitId { get; private set; }
    public decimal SellPrice { get; private set; }
    public string? Colors { get; private set; }

    public ProductUpdatedIntegrationEvent(
        Guid productId, 
        string name, 
        Guid productGroupId,
        Guid? productUnitId, 
        decimal sellPrice,
        string? colors)
    {
        ProductId = productId;
        Name = name;
        ProductGroupId = productGroupId;
        ProductUnitId = productUnitId;
        SellPrice = sellPrice;
        Colors = colors;
    }
}