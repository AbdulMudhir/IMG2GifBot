using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IMG2GifBot.Modules
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
		private const string regExpression = @"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)";



        private  HttpClient client;


        [Command("gif")]
		public async Task GIFAsync(string url)
		{

            client = new HttpClient();

            if (urlIsValid(url))
            {

                
                var response = await client.GetAsync(url);
               

                if (response.StatusCode == HttpStatusCode.OK)
                {

                    var filename = Path.GetRandomFileName() + ".gif";
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), filename);

                    var imgByteArray = await response.Content.ReadAsByteArrayAsync();
                    File.WriteAllBytes(fullPath, imgByteArray);


                    if (isValidImage(fullPath))
                    {

                        await Context.Channel.SendFileAsync(fullPath);

                    }
                    else
                    {
                        ReplyAsync("I could not convert that shit.");
                    }
                    File.Delete(fullPath);

                    await Context.Message.DeleteAsync();



                }

            }


        }

        private bool isValidImage(string filePath)
        {
            try
            {
                var bmp = new Bitmap(filePath);
              
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool urlIsValid(string url)
        {
            Regex rx = new Regex(regExpression, RegexOptions.Compiled | RegexOptions.IgnoreCase);

            return rx.IsMatch(url);

        }

        // ReplyAsync is a method on ModuleBase 
    }
}
