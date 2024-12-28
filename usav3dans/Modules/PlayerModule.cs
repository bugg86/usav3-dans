using usav3dans.Handlers;
using usav3dans.Services.Interfaces;
using Discord.Interactions;

namespace usav3dans.Modules;

[Group("player", "commands for managing players")]
public class PlayerModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IGoogleService _googleService;
    public InteractionService Commands { get; set; } = null!;
    public CommandHandler _handler;

    public PlayerModule(CommandHandler handler, IGoogleService googleService)
    {
        _handler = handler;
        _googleService = googleService;
    }

    [SlashCommand("reset", "reset player's prices to 0")]
    public async Task ResetPlayers()
    {
        bool response = true;
        
        if (response)
        {
            await RespondAsync("Player prices successfully set to 0.");
        }
        else
        {
            await RespondAsync("There was an issue resetting player prices.");
        }
    }
}