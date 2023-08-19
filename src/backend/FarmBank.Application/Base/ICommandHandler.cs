using FluentValidation.Results;
using MediatR;

namespace FarmBank.Application.Base;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, ResponseResult>
    where TCommand : ICommand 
{

}

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, ResponseResult<TResult>>
    where TCommand : ICommand<TResult>
{

}
