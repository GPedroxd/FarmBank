using FluentValidation.Results;
using MediatR;

namespace FarmBank.Application.Base;

public interface ICommand : IRequest
{
    
}

public interface ICommand<TResult> : IRequest<TResult>
{
    
}