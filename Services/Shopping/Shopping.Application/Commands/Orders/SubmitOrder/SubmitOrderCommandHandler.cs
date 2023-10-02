using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.Identity;
using BuildingBlocks.Domain.Data;
using Shopping.Application.Dtos.Orders;
using Shopping.Domain.BuyerAggregate;
using Shopping.Domain.BuyerAggregate.Exceptions;
using Shopping.Domain.OrderAggregate;
using Shopping.Domain.ProductAggregate;
using Shopping.Domain.ProductAggregate.Exceptions;
using Shopping.Domain.ProductAggregate.Specifications;
using Shopping.Domain.ProductUnitAggregate;

namespace Shopping.Application.Commands.Orders.SubmitOrder;

public class SubmitOrderCommandHandler : ICommandHandler<SubmitOrderCommand>
{
    private readonly ICurrentUser _currentUser;
    private readonly IRepository<Buyer> _buyerRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<ProductUnit> _productUnitRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public SubmitOrderCommandHandler(
        IRepository<Order> orderRepository,
        IRepository<Buyer> buyerRepository,
        IRepository<Product> productRepository,
        IRepository<ProductUnit> productUnitRepository,
        ICurrentUser currentUser, 
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _buyerRepository = buyerRepository;
        _productRepository = productRepository;
        _productUnitRepository = productUnitRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(SubmitOrderCommand request, CancellationToken cancellationToken)
    {
        var buyer = await _buyerRepository.GetByIdAsync(Guid.Parse(_currentUser.Id!));
        if (buyer == null)
        {
            throw new BuyerNotFoundException(Guid.Parse(_currentUser.Id!));
        }
        
        var order = await CreateOrderAsync(buyer, request.Items);
        
        _orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync();
    }

    private async Task<Order> CreateOrderAsync(Buyer buyer, IEnumerable<SubmitOrderLineDto> items)
    {
        var productIds = items.Select(x => x.ProductId).ToArray();
        var products = await _productRepository.FindListAsync(new ProductByIncludedIdsSpecification(productIds));

        var order = new Order(
            Guid.NewGuid().ToString(), 
            buyer.Id, 
            buyer.FullName, 
            buyer.FullName, 
            buyer.Email, 
            buyer.Phone, 
            buyer.Address.FullAddress, 
            buyer.Address.City, 
            buyer.Address.State, 
            buyer.Address.PostalCode);
        
        foreach (var item in items)
        {
            var product = products.FirstOrDefault(x => x.Id == item.ProductId);

            if (product == null)
            {
                throw new ProductNotFoundException(item.ProductId);
            }

            order.AddOrderLine(
                item.ProductId, 
                product.Name, 
                product.ProductUnit?.Name, 
                item.Color, 
                item.Quantity, 
                product.Price);
        }

        return order;
    }
}