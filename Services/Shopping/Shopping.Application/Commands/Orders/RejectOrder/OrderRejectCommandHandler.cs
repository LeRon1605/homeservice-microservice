using AutoMapper;
using BuildingBlocks.Application.CQRS;
using BuildingBlocks.Application.MailSender;
using BuildingBlocks.Domain.Data;
using BuildingBlocks.Infrastructure.MailSender;
using Shopping.Application.Dtos.Orders;
using Shopping.Domain.OrderAggregate;
using Shopping.Domain.OrderAggregate.Exceptions;
using Shopping.Domain.OrderAggregate.Specifications;

namespace Shopping.Application.Commands.Orders.RejectOrder;

public class OrderRejectCommandHandler : ICommandHandler<OrderRejectCommand, OrderDto>
{
    private readonly IRepository<Order> _oderRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;

    public OrderRejectCommandHandler(IRepository<Order> oderRepository, IMapper mapper, IUnitOfWork unitOfWork,
        IEmailSender emailSender)
    {
        _oderRepository = oderRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _emailSender = emailSender;
    }

    public async Task<OrderDto> Handle(OrderRejectCommand request, CancellationToken cancellationToken)
    {
        var orderByIdSpecification = new OrderByIdSpecification(request.Id);
        var order = await _oderRepository.FindAsync(orderByIdSpecification);
        if (order == null)
            throw new OrderNotFoundException(request.Id);
        order.Reject();
        _oderRepository.Update(order);
        await _unitOfWork.SaveChangesAsync();
        if (order.ContactInfo.Email != null)
        {
            //var message = new Message(new string[] { order.ContactInfo.Email }, "Reject Order", request.Description);
            string content = $"<p>Dear {order.ContactInfo.CustomerName},</p>\n " +
                             $"<p>We regret to inform you that your order for order No: {order.OrderNo} has been rejected.</p>\n " +
                             $"<p>The reason for the rejection is: {request.Description}</p>\n " +
                             "<p>If you have any questions or concerns, please do not hesitate to contact us at <strong>homeserviceapp@gmail.com</strong>.</p>\n " +
                             "<p>Thank you for your understanding.</p>\n <p>Sincerely,</p>\n <p><strong>Home Service Company</strong></p>";
            var message = new Message(new string[] { order.ContactInfo.Email  }, "Reject Order", content);
            _emailSender.SendEmail(message);
        }

        return _mapper.Map<OrderDto>(order);
    }
}