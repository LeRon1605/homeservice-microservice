using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using MediatR;
using Shopping.Domain.ProductAggregate;

namespace Shopping.Application.Commands;

public class AddedProductCommandHandler : ICommandHandler<AddedProductCommand>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddedProductCommandHandler(IRepository<Product> productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddedProductCommand request, CancellationToken cancellationToken)
    {
        var productCreated = new Product(request.Id, request.Name, 
            request.ProductGroupId, request.ProductUnitId, request.SellPrice);
        
        _productRepository.Add(productCreated);
        await _unitOfWork.SaveChangesAsync();
    }
}