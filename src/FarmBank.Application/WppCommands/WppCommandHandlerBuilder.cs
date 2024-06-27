using FarmBank.Application.Interfaces;
using FarmBank.Application.Member;
using Microsoft.Extensions.DependencyInjection;

namespace FarmBank.Application.WppCommands;

public abstract class WppCommandHandlerBuilder
{
    public static IWppCommandHandler Builder(WppCommandType commandType, IServiceProvider serviceProvider)
    {
        if(commandType == WppCommandType.Join)
        {
            var memberRepository = serviceProvider.GetService<IMemberRepository>();
            var wppService = serviceProvider.GetService<IWppService>();

            return new InWppCommandHandler(memberRepository, wppService);
        }

        if(commandType == WppCommandType.Ranking)
        {
            var memberRepository = serviceProvider.GetService<IMemberRepository>();
            var wppService = serviceProvider.GetService<IWppService>();

            return new RankingWppCommandHandler(memberRepository, wppService);
        }

        throw new NotImplementedException();
    }
}
