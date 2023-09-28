using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Domain.Data;
using Microsoft.Extensions.Logging;
using Shopping.Domain.BuyerAggregate;

namespace Shopping.Application.Commands.AddSignedUpUser;

public class AddSignedUpUserCommandHandler : ICommandHandler<AddSignedUpUserCommand>
{
    private readonly IRepository<Buyer> _buyerRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AddSignedUpUserCommand> _logger;

    public AddSignedUpUserCommandHandler(IRepository<Buyer> buyerRepository,
                                             IUnitOfWork unitOfWork,
                                             ILogger<AddSignedUpUserCommand> logger)
    {
        _buyerRepository = buyerRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(AddSignedUpUserCommand request,
                       CancellationToken cancellationToken)
    {
        var buyer = Buyer.CreateWithId(request.Id, request.FullName, request.Phone, request.Email);
        
        _buyerRepository.Add(buyer);
        
        await _unitOfWork.SaveChangesAsync();
        
        _logger.LogInformation("Buyer {buyerId} is successfully created.", buyer.Id);
    }
}