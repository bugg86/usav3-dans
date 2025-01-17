﻿using usav3dans.Handlers;
using usav3dans.Services;
using usav3dans.Services.Interfaces;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace usav3dans;

public class Program
{
    private DiscordSocketClient? _client;
    private InteractionService _commands = null!;
    private string tokenPath = "./token";
    
    public static Task Main(string[] args) => new Program().MainAsync();

    private async Task MainAsync()
    {
        async void ConfigureDelegate(IServiceCollection services)
        {
            string? token;
            
            if (!File.Exists(tokenPath))
            {
                Console.Write("Please enter your discord bot token: ");
                token = Console.ReadLine();
                await File.WriteAllTextAsync(tokenPath, token);
            }
            else
            {
                token = await File.ReadAllTextAsync(tokenPath);
            }
            
            ConfigureServices(services);
            
            var serviceProvider = services.BuildServiceProvider();
            _client = serviceProvider.GetRequiredService<DiscordSocketClient>();
            _commands = serviceProvider.GetRequiredService<InteractionService>();

            _client.Log += Log;
            _commands.Log += Log;
            _client.Ready += ReadyAsync;
            // _client.MessageReceived += HandleMessage(services);

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            
            await serviceProvider.GetRequiredService<CommandHandler>().InitializeAsync();
        }
        
        var host = new HostBuilder()
            .ConfigureServices(ConfigureDelegate);

        await host.RunConsoleAsync();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        DiscordSocketConfig config = new()
        {
            UseInteractionSnowflakeDate = false,
            GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent | GatewayIntents.GuildMembers | GatewayIntents.GuildMessages,
            AlwaysDownloadUsers = true
        };
        services.AddSingleton(config);
        services.AddSingleton<DiscordSocketClient>();
        services.AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()));
        services.AddSingleton<CommandHandler>();

        services.AddScoped<IGoogleService, GoogleService>();
        services.AddScoped<IOsuApiService, OsuApiService>();

        services.AddHttpClient();
    }
    
    private async Task ReadyAsync()
    {
        await _commands.RegisterCommandsGloballyAsync(true);
    }
    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg.ToString());
        return Task.CompletedTask;
    }
}