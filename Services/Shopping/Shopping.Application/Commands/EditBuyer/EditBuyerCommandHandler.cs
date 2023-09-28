using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.EventBus.Interfaces;
using Microsoft.Extensions.Logging;
using Shopping.Application.Dtos;
using Shopping.Application.IntegrationEvents.Events;
using Shopping.Domain.BuyerAggregate;
using Shopping.Domain.Exceptions;

namespace Shopping.Application.Commands.EditBuyer;

public class EditBuyerCommandHandler : ICommandHandler<EditBuyerCommand, BuyerDto>
{
    private readonly IRepository<Buyer> _buyerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<EditBuyerCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IEventBus _eventBus;

    public EditBuyerCommandHandler(IRepository<Buyer> buyerRepository,
                                      IUnitOfWork unitOfWork,
                                      ILogger<EditBuyerCommandHandler> logger,
                                      IMapper mapper,
                                      IEventBus eventBus)
    {
        _buyerRepository = buyerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _eventBus = eventBus;
    }

    public async Task<BuyerDto> Handle(EditBuyerCommand request,
                                    CancellationToken cancellationToken)
    {
        var buyer = await _buyerRepository.GetByIdAsync(request.Id);
        if (buyer is null)
            throw new BuyerNotFoundException(request.Id);
        
        var isIdentityInfoChanged = buyer.FullName != request.FullName ||
                                    buyer.Phone != request.Phone ||
                                    buyer.Email != request.Email;
        
        buyer.Update(request.FullName, request.Phone, request.Email, request.Address,
            request.City, request.State, request.PostalCode, request.AvatarUrl);
        
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Customer with id: {customerId} was updated!", request.Id);

        if (isIdentityInfoChanged)
        {
            var customerNameChangedEvent =
                new BuyerInfoChangedIntegrationEvent(buyer.Id, buyer.FullName, buyer.Phone, buyer.Email);
            
            _logger.LogInformation("Publishing {integrationEvent} integration event", nameof(BuyerInfoChangedIntegrationEvent));
            
            _eventBus.Publish(customerNameChangedEvent);
            
            _logger.LogInformation("{integrationEvent} integration event was published", nameof(BuyerInfoChangedIntegrationEvent));
        }
        
        return _mapper.Map<BuyerDto>(buyer);
    }
}