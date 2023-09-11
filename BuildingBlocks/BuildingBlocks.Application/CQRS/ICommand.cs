using MediatR;
namespace BuildingBlocks.Application.CQRS;


public interface ICommandBase
{
}

public interface ICommand : ICommandBase, IRequest
{
}

public interface ICommand<out TResponse> : ICommandBase, IRequest<TResponse>
{
}