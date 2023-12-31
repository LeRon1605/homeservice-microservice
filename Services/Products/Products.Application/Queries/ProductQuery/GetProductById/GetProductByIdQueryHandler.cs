﻿using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Products.Application.Dtos;
using Products.Application.Dtos.Products;
using Products.Domain.ProductAggregate;
using Products.Domain.ProductAggregate.Exceptions;
using Products.Domain.ProductAggregate.Specifications;

namespace Products.Application.Queries.ProductQuery.GetProductById;

public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductDto>
{
    private readonly IReadOnlyRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IReadOnlyRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }
    public async Task<GetProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.FindAsync(new ProductByIdSpecification(request.Id));
        if (product == null)
        {
            throw new ProductNotFoundException(request.Id);
        }
        return _mapper.Map<GetProductDto>(product);
    }
}