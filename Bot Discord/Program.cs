using Bot_Discord.Properties;
using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bot_Discord
{
    class Program
    {
        static MemoryStream Paris = new MemoryStream();
        public static void Main(string[] args)
        {
            Resources.paris.Save(Paris, System.Drawing.Imaging.ImageFormat.Bmp);
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        private DiscordSocketClient _client;
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;
            _client.MessageReceived += _client_MessageReceived;

            var token = System.IO.File.ReadAllLines(Environment.CurrentDirectory + "/SECRETS.TXT")[0];

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await _client.SetGameAsync("seu nome para fora do SPC!");
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
        //todo colocar isso numa classe de utils
        bool IsAllUpper(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]) && !Char.IsUpper(input[i]))
                    return false;
            }
            return true;
        }

        private Task _client_MessageReceived(SocketMessage message)
        {
            if(message.Content.ToLower().Contains("paris") && message.Content.ToLower().Contains("foi"))
            {
                message.Channel.SendMessageAsync("Je crois que non");
                return message.Channel.SendMessageAsync("https://www.ocafezinho.com/wp-content/uploads/2018/11/45323522-2149527855375634-2236660852830765056-n.jpg");
            }
            else if(message.Content != "" && IsAllUpper(message.Content))
            {
                if (Regex.Replace(message.Content.Replace(".", "").Replace(",", "").Replace("?", "").Replace("!", ""), @"[\d-]", string.Empty).All(Char.IsLetter))
                {
                    message.Channel.SendMessageAsync($"Calma dona Maria... Digo... {message.Author.Mention}");
                    return message.Channel.SendMessageAsync("https://i.imgur.com/eTZyqx1.gif");
                }
            }
            return Task.CompletedTask;
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
