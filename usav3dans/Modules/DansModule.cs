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
    public InteractionService Commands { get; set; } = null!;
    public CommandHandler _handler;


    public DansModule(CommandHandler handler)
    {
        _handler = handler;
    }

    [SlashCommand("submit", "submit an mp for your dan")]
    public async Task Submit(string mp)
    {

    }
}