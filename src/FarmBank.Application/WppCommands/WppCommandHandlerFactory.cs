using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace FarmBank.Application.WppCommands;

public sealed class WppCommandHandlerFactory
{
    private IServiceProvider _serviceProvider;

    public WppCommandHandlerFactory(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public IWppCommand Create(string command)
    {
        command = command.ToLower().Replace("-", " ");

        TextInfo info = CultureInfo.CurrentCulture.TextInfo;
        command = info.ToTitleCase(command).Replace(" ",string.Empty);

        var commandClassName = $"FarmBank.Application.WppCommands.Commands.{command}WppCommandHandler";

        var ClassCommandType = Type.GetType(commandClassName);

        if(ClassCommandType is null)
            throw new ArgumentException("Command not found!");

        var commandHandler =_serviceProvider.GetRequiredService(ClassCommandType);

        return (IWppCommand)commandHandler;
    }
}
