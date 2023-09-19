using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductAggregate.Exceptions;

namespace Products.Application.Commands.ProductCommands.DeleteProduct;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
     private readonly IUnitOfWork _unitOfWork;
     private readonly IRepository<Product> _productRepository;
     private readonly ILogger<DeleteProductCommand> _logger;

     public DeleteProductCommandHandler(
        IUnitOfWork unitOfWork,
        IRepository<Product> productRepository,
        ILogger<DeleteProductCommand> logger)
     {
         _unitOfWork = unitOfWork;
         _productRepository = productRepository;
         _logger = logger;
     }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id);
        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        
        _productRepository.Delete(product);
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogTrace("Deleted product with id '{ProductId}' successfully", request.Id);
    }
}