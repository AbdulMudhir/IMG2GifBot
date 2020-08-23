using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using IMG2GifBot.Modules;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace IMG2GifBot.Factories
{
    public class Factory
    {
       
        public static CommandService CreateCommandService()
        {

            return new CommandService();
        }

        public async static Task<CommandHandler> CreateCommandHandler(DiscordSocketClient _client)
        {
            var commandhandler = new CommandHandler(_client, commands: CreateCommandService());
            await commandhandler.InstallCommandsAsync();
            return commandhandler;
        }
    }
}
