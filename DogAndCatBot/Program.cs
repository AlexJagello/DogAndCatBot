using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using System.Net;
using Newtonsoft.Json;

namespace DogAndCatBot
{

    class Program
    {

        static void Main(string[] args)
        {
           BotSingleton botSingleton = BotSingleton.getInstance();
        }
    }
}