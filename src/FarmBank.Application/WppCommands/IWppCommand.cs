using System.Collections.Generic;

namespace FarmBank.Application.WppCommands;

public interface IWppCommand
{
    Task ProcessAsync(WppInputMessage inputMessage, string[] args);
}
