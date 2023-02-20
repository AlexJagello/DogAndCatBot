using DogAndCatBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace DogAndCatBot
{
    public class BotSingleton
    {
        private ITelegramBotClient bot = new TelegramBotClient("BOTTOKEN");

        private static BotSingleton instance;

        public static BotSingleton getInstance()
        {
            if (instance == null)
                instance = new BotSingleton();
            return instance;
        }

        private BotSingleton()
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();

        }

     

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;

                switch(message.Text.ToLower())
                {
                    case "/start":
                        await botClient.SendTextMessageAsync(message.Chat.Id, "Click CAT for cat picture. Click DOG for dog picture", replyMarkup: CreateButtons("Cat", "Dog"));
                        break;
                    case "cat":
                        var apiDogAnswer = await GetWebResponse("https://api.thecatapi.com/v1/images/search").DeserializeApiAnswer<CatAPIAnswer>();
                        await botClient.SendPhotoAsync(message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(apiDogAnswer.Url));
                        break;
                    case "dog":
                        var apiCatAnswer = await GetWebResponse("https://dog.ceo/api/breeds/image/random").DeserializeApiAnswer<DogAPIAnswer>();
                        await botClient.SendPhotoAsync(message.Chat.Id, new Telegram.Bot.Types.InputFiles.InputOnlineFile(apiCatAnswer.Message));
                        break;
                    default:
                        await botClient.SendTextMessageAsync(message.Chat, "Click CAT for cat picture. Click DOG for dog picture");
                        break;
                }
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }




        private async Task<String> GetWebResponse(string api)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(string.Format(api));
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
         
            return responseBody;
        }


        private ReplyKeyboardMarkup CreateButtons(params string[] names)
        {
            var buttons = names.Select(name => new KeyboardButton(name)); 

            return new ReplyKeyboardMarkup(buttons);
        }   
    }
}


public static class HttpWebResponseExtension
{
    public async static Task<T> DeserializeApiAnswer<T>(this Task<String> httpWebResponse)
    {
        string jsonString = httpWebResponse.Result.Replace('[', ' ').Replace(']', ' ');
        T modelsApi = JsonConvert.DeserializeObject<T>(jsonString);

        return modelsApi;
    }
}