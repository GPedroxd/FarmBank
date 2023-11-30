using FarmBank.Application.Base;
using FarmBank.Application.Dto;
using FarmBank.Application.WppCommands;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace FarmBank.Application.Commands.WppCommand;

public class WppCommandHandler : ICommandHandler<WppCommand, ResponseResult<bool>>
{
    private readonly IServiceProvider _serviceProvider;

    public WppCommandHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<ResponseResult<bool>> Handle(WppCommand request, CancellationToken cancellationToken)
    {
        if(!IsValidCommandFromFazendinha(request)) 
            return new ResponseResult<bool>(new ValidationFailure());
        
        var commandsParts = request.Body.Message.Text.Split(' ');
        var commandString = commandsParts[1];

        commandString = commandString[0].ToString().ToUpper() + commandString.Substring(1);

        if(!Enum.TryParse(commandString, out WppCommandType command))
            return new ResponseResult<bool>(new ValidationFailure());

        var wppCommandHandler = WppCommandHandlerBuilder.Builder(command, _serviceProvider);

        var senderInfo = new WppSenderInfo(request.Body.PushName, request.Body.SenderInfo.Participant);

        await wppCommandHandler.Handler(senderInfo, cancellationToken);

        return new ResponseResult<bool>(true);
    }

    private bool IsValidCommandFromFazendinha(WppCommand request)
    {
        var generalConfig =  _serviceProvider.GetService<GeneralConfigs>();

        if(!"message".Equals(request.Type))
            return false;
        
        if(!generalConfig.GroupId.Equals(request.Body.SenderInfo.GroupId))
            return false;

        if(!request.Body.Message.Text.StartsWith("!farm")) 
            return false;

        return true;
    }
}
