using System.Text.RegularExpressions;
using Discord.Interactions;
using usav3dans.Handlers;
using usav3dans.Models;
using usav3dans.Services.Interfaces;

namespace usav3dans.Modules;

[Group("tryouts", "commands related to tryouts")]
public class TryoutsModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IOsuApiService _osuApiService;
    private readonly IGoogleService _googleService;
    public InteractionService Commands { get; set; } = null!;
    public CommandHandler _handler;

    public TryoutsModule(IOsuApiService osuApiService, IGoogleService googleService, CommandHandler handler)
    {
        _osuApiService = osuApiService;
        _googleService = googleService;
        _handler = handler;
    }

    [SlashCommand("submit", "submit a lobby for your tryouts")]
    public async Task Submit(string mp)
    {
        Console.WriteLine(Regex.Match(mp, @"([\d]+)").Value);
        int matchId = int.Parse(Regex.Match(mp, @"([\d]+)").Value);

        MatchBase match = await _osuApiService.GetMatch(matchId);

        bool isValid = VerifyMatch(match);

        if (!isValid)
        {
            await RespondAsync("Your mp is not valid, likely because you played a map before sending it to us.", ephemeral: true);
            return;
        }

        var temp = _googleService.PushTryoutsMp(mp);

        await RespondAsync($"Submitted your mp to the sheet!", ephemeral: true);
    }

    private bool VerifyMatch(MatchBase match)
    {
        foreach (Events e in match.events)
        {
            if (e.detail.type.Equals("other"))
            {
                return false;
            }
        }

        return true;
    }
}