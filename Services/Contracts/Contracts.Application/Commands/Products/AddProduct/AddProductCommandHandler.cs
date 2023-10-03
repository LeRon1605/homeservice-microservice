using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Contracts.Domain.ProductAggregate;

namespace Contracts.Application.Commands.Products.AddProduct;

public class AddProductCommandHandler : ICommandHandler<AddProductCommand>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddProductCommandHandler(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var productCreated = new Product(
            request.Id, 
            request.Name,
            request.ProductUnitId,
            request.SellPrice,
            request.Colors?.Split(",").ToList());
        
        _productRepository.Add(productCreated);
        await _unitOfWork.SaveChangesAsync();
    }
}