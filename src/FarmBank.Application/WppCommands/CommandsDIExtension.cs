using FarmBank.Application.WppCommands.Commands;
using FarmBank.Application.WppCommands.Commands.Rank;
using Microsoft.Extensions.DependencyInjection;

namespace FarmBank.Application.WppCommands;
public static class CommandsDIExtension
{
    public static IServiceCollection AddWppCommands(this IServiceCollection services)
    {
        services.AddTransient<JoinWppCommandHandler>();
        services.AddTransient<PixWppCommandHandler>();
        services.AddTransient<CreateEventWppCommandHandler>();
        services.AddTransient<RankWppCommandHandler>();
        return services;
    }
}
