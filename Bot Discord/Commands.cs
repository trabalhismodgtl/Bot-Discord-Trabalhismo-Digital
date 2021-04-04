using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bot_Discord
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        static string url = "";
        // !say hello world -> hello world
        [Command("say")]
        [Summary("repitir o usuário")]
        public Task SayAsync([Summary("desc")][Remainder] string echo)
        {
            return ReplyAsync(echo);
        }
        [Command("url")] //todo remover esse debug, só ler o URL do SECRETS no Program.cs
        public Task Url([Summary("endereço com / no final")][Remainder] string echo)
        {
            return Task.Run(() =>
            {
                var user = Context.Message.Author as SocketGuildUser;
                var role = (user as IGuildUser).Guild.Roles.FirstOrDefault(x => x.Name == "Coordenação Digital");
                if(role != null)
                    url = echo;
            });

        }
        [Command("cargo")]
        public Task CargoAsync()
        {
            var user = Context.User as SocketGuildUser;
            var role = (user as IGuildUser).Guild.Roles.FirstOrDefault();
            return ReplyAsync(role.Name);
        }
        [Command("cadastro")]
        public Task CardAsync() 
        {
            var embed = new EmbedBuilder
            {
                // Embed property can be set within object initializer
                Title = "Formulário",
                Description = @"Clique em __**Formulário**__ logo acima para iniciar seu cadastro!"
            };
            // Or with methods
            embed.WithAuthor(Context.Client.GetUser(167389495182753792))
                .WithFooter(footer => footer.Text = "Trabalhismo Digital")
                .WithColor(Color.Red)
                .WithImageUrl("https://user-images.githubusercontent.com/81885777/113494306-6dfda000-94bd-11eb-882c-ec16ff3c4b18.png")
                .WithUrl($"{url}?id={Context.Message.Author.Id}&pic={System.Convert.ToBase64String(Encoding.UTF8.GetBytes(Context.Message.Author.GetAvatarUrl()))}&name={Context.Message.Author.Username}")
                .WithCurrentTimestamp();

            //Your embed needs to be built before it is able to be sent
            return Context.Message.Author.SendMessageAsync(embed: embed.Build());
        }
    }
}
