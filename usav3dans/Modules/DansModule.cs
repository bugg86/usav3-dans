using System.Text.RegularExpressions;
using System.Timers;
using usav3dans.Handlers;
using usav3dans.Services.Interfaces;
using usav3dans.Services;
using Discord;
using Discord.Interactions;

namespace usav3dans.Modules;

[Group("dan", "commands for interacting with the dan system")]
public class  DansModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IGoogleService _googleService;
    public InteractionService Commands { get; set; } = null!;
    public CommandHandler _handler;

    public DansModule(CommandHandler handler, IGoogleService googleService)
    {
        _handler = handler;
        _googleService = googleService;
    }

    [SlashCommand("submit", "submit an mp for your dan, sheet must be in the format of YXQX. Example: Y1Q1")]
    public async Task Submit(string mp, string sheet)
    {
        if (Regex.IsMatch(sheet, @"Y\dQ\d"))
            await RespondAsync("Could not add mp to the sheet, please follow the correct format for sheet.");
        
        var values = _googleService.PushDansMp(mp, sheet);

        await RespondAsync("Added the mp to the sheet");
    }

    [SlashCommand("test", "test random shit")]
    public async Task Test()
    {
        var values = _googleService.PushDansMp("balls", "");

        Console.WriteLine(values);
        foreach (var column in values.Result.Values)
        {
            foreach (var row in column)
            {
                Console.WriteLine(row);
            }
        }
    }
}