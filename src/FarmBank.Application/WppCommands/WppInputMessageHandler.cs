using FarmBank.Application.Base;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace FarmBank.Application.WppCommands;

public class WppInputMessageHandler : ICommandHandler<WppInputMessage>
{
    private readonly ILogger<WppInputMessageHandler> _logger;
    private readonly WppCommandHandlerFactory _commandHandlerFactory;

    public WppInputMessageHandler(ILogger<WppInputMessageHandler> logger, WppCommandHandlerFactory handlerFactory)
    {
        _logger = logger;
        _commandHandlerFactory = handlerFactory;
    }

    public async Task Handle(WppInputMessage request, CancellationToken cancellationToken)
    {
        var textInput = request.Message.Content;

        if (IsValidInput(textInput) is not true)
            return;

        var partsOfInput = SplitWithQuotes(textInput);

        var command = partsOfInput[0];
        var commandArgs = partsOfInput.Skip(1);

        try
        {
            var commandHandler = _commandHandlerFactory.Create(command);
            await commandHandler.ProcessAsync(request, commandArgs.ToArray());
        }
        catch (Exception ex) 
        {
            _logger.LogError(ex, "An Error Occured while tried to perform command {command} at {date}. Ex.: {ex}", command, DateTime.Now, ex.Message);
        }  
    }

    private bool IsValidInput(string textCommand)
    {
        if (string.IsNullOrEmpty(textCommand))
            return false;

        if (!textCommand.StartsWith("!"))
            return false;

        return true;
    }
    private string[] SplitWithQuotes(string input)
    {
        input = input.Trim().Replace("!", "");

        var result = new List<string>();

        // Regular expression to match words or quoted parts (everything inside double quotes)
        string pattern = @"(?:\""(.*?)\"")|(\S+)";

        foreach (Match match in Regex.Matches(input, pattern))
        {
            // If the match is a quoted part, remove quotes, otherwise add the word as is
            if (match.Groups[1].Success)
            {
                result.Add(match.Groups[1].Value);  // This is the quoted content without quotes
            }
            else if (match.Groups[2].Success)
            {
                result.Add(match.Groups[2].Value);  // This is the non-quoted word
            }
        }

        return result.ToArray();
    }
}
