using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos;
using Products.Domain.ProductAggregate;

namespace Products.Application.Commands.ProductCommands.AddProduct;

public class AddProductCommandHandler : ICommandHandler<AddProductCommand, GetProductDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public AddProductCommandHandler(IUnitOfWork unitOfWork,
                                    IRepository<Product> productRepository,
                                    IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetProductDto> Handle(AddProductCommand request,
                                      CancellationToken cancellationToken)
    {
        var product = new Product(request.Name);

        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync();
        
        return _mapper.Map<GetProductDto>(product); 
    }
}