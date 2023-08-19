using FluentValidation.Results;
using MediatR;

namespace FarmBank.Application.Base;

public interface ICommand : IRequest<ResponseResult>
{
    
}

public interface ICommand<TResult> : IRequest<ResponseResult<TResult>>
{
    
}