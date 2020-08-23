using Discord;
using Discord.Commands;
using Discord.WebSocket;
using IMG2GifBot.Factories;
using System;
using IMG2GifBot.Modules;
using System.Threading.Tasks;

namespace IMG2GifBot
{
    class Program
    {
        private DiscordSocketClient _client;
       

        static void Main(string[] args)
       => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {

            _client = new DiscordSocketClient();


            _client.Log += Log;

            _client.MessageReceived += MessageReceived;

            var commandHandler = Factory.CreateCommandHandler(_client);


            // Remember to keep token private or to read it from an 
            // external source! In this case, we are reading the token 
            // from an environment variable. If you do not know how to set-up
            // environment variables, you may find more information on the 
            // Internet or by using other methods such as reading from 
            // a configuration.
            await _client.LoginAsync(TokenType.Bot,
                Environment.GetEnvironmentVariable("c#bot"));
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }


        private async Task MessageReceived(SocketMessage message)
        {
            if (message.Content == "!ping")
            {
                await message.Channel.SendMessageAsync("Pong!");
            }
        }





        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

    }
}
