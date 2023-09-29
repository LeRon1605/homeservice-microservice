using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Shopping.Domain.ProductAggregate;

namespace Shopping.Application.Commands.Products.AddProduct;

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
            request.ProductGroupId, 
            request.SellPrice, 
            request.ProductUnitId);
        
        _productRepository.Add(productCreated);
        await _unitOfWork.SaveChangesAsync();
    }
}