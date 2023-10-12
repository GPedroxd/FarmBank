using FluentValidation.Results;

namespace FarmBank.Application.Base;

public class ResponseResult<TResult> : ResponseResult
{
    public ResponseResult(List<ValidationFailure> erros) : base(erros) { }
    public ResponseResult(ValidationFailure erro) : base(erro) { }

    public ResponseResult(TResult result)
    {
        ArgumentNullException.ThrowIfNull(result);
        IsValid = true;
        Result = result;
    }
    public TResult Result { get; private set; }
}

public class ResponseResult
{
    public ResponseResult(List<ValidationFailure> erros)
    {
        IsValid =  !erros.Any();
        _erros = erros;
    }
    public ResponseResult(ValidationFailure erro)
    {
        IsValid = false;
        _erros.Add(erro);
    }
    
    protected ResponseResult(){ }
    public bool IsValid { get; protected set; }
    protected List<ValidationFailure> _erros = new();
    public IReadOnlyList<ValidationFailure> Erros => _erros.AsReadOnly();

    public static ResponseResult ValidResult()
    {
        return new ResponseResult()
        {
            IsValid = true
        };
    }
}
