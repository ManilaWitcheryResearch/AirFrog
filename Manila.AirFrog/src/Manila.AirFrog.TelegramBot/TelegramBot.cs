namespace Manila.AirFrog.TelegramBot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Telegram.Bot;
    using Telegram.Bot.Args;
    using Telegram.Bot.Types;
    using Telegram.Bot.Types.Enums;
    using Telegram.Bot.Types.InlineQueryResults;
    using Telegram.Bot.Types.InputMessageContents;
    using Telegram.Bot.Types.ReplyMarkups;

    using Manila.AirFrog.Common;

    class TelegramBot
    {
        private TelegramBotClient Bot;
        private string token;

        public TelegramBot(string token)
        {
            this.token = token;
        }

        public void Start()
        {
            try
            {
                ;
            }
            catch (Exception)
            {
                ;
            }
            Bot = new TelegramBotClient(this.token);

            Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageReceived;
            Bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;
            Bot.OnReceiveError += BotOnReceiveError;

            var me = Bot.GetMeAsync().Result;

            Console.Title = me.Username;

            Bot.StartReceiving();
        }

        public void Stop()
        {
            Bot.StopReceiving();
        }

        private void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            ;
        }

        private void BotOnChosenInlineResultReceived(object sender, ChosenInlineResultEventArgs chosenInlineResultEventArgs)
        {
            Console.WriteLine($"Received choosen inline result: {chosenInlineResultEventArgs.ChosenInlineResult.ResultId}");
        }

        private async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            if (message.Text.StartsWith("/aisatsu")) // send custom keyboard
            {
                ;

                await Bot.SendTextMessageAsync(message.Chat.Id, "Hello!");
            }
            else if (message.Text.StartsWith("/drive")) // request location or contact
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, "Drive!");
            }
            else if (message.Text.StartsWith("/start") || message.Text.StartsWith("/usage"))
            {
                var usage =
@"Usage:
/aisatsu   - Get a greeting message.
/drive - Get a car by random pick from SuperTrainKing5000.
";

                await Bot.SendTextMessageAsync(message.Chat.Id, usage,
                    replyMarkup: new ReplyKeyboardHide());
            }
            else
            {
                ;
            }
        }

        private async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            await Bot.AnswerCallbackQueryAsync(callbackQueryEventArgs.CallbackQuery.Id,
                $"Received {callbackQueryEventArgs.CallbackQuery.Data}");
        }
    }
}

