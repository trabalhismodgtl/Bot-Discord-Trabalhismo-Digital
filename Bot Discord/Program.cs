using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bot_Discord
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            var token = System.IO.File.ReadAllLines(Environment.CurrentDirectory + "/SECRETS.TXT")[0];

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            var commands = new CommandHandler(_client, new Discord.Commands.CommandService());
            await commands.InstallCommandsAsync();
            // Block this task until the program is closed.

            Server.listener = new HttpListener();
            Server.listener.Prefixes.Add(Server.url);
            Server.listener.Start();
            Console.WriteLine("Listening for connections on {0}", Server.url);

            // Handle requests
            Task listenTask = Server.HandleIncomingConnections((ulong id)=>
            {
                getPic(id);
            });
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            Server.listener.Close();

            await Task.Delay(-1);
        }
        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
        public void getPic(ulong id)
        {
           _client.GetUser(id).SendMessageAsync("Obrigado por realizar o cadastro no nosso formulário! \u2764\uFE0F \uD83C\uDF39");
           Console.WriteLine(_client.GetUser(id).GetAvatarUrl());
        }
    }
}
