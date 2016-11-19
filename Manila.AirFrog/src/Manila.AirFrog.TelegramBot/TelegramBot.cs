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
    using Manila.AirFrog.Common.Core;
    using Manila.AirFrog.Common.Event;
    using Manila.AirFrog.Common.Models;

    public class TelegramBot
    {
        private TelegramBotClient Bot;
        private string token;
        private List<long> trustedGroups = new List<long>();
        private List<long> trustedAdmins = new List<long>();
        private ILogger Logger;
        private IEventHub EventHub;

        public TelegramBot(string token, ILogger logger, IEventHub eventHub)
        {
            this.token = token;
            this.Logger = logger;
            this.EventHub = eventHub;

            trustedGroups.Add(-1001098762726);
            trustedAdmins.Add(184285932);
        }

        public void Start()
        {
            try
            {
                Bot = new TelegramBotClient(this.token);

                Bot.OnCallbackQuery += BotOnCallbackQueryReceived;
                Bot.OnMessage += BotOnMessageReceived;
                Bot.OnMessageEdited += BotOnMessageReceived;
                Bot.OnInlineResultChosen += BotOnChosenInlineResultReceived;
                Bot.OnReceiveError += BotOnReceiveError;

                var me = Bot.GetMeAsync().Result;

                Logger.Log(string.Format("Telegram successfully login with {0}", me.Username));

                EventHub.Register("Chat.Public.SendToTelegram.Group", 
                    new Action<object>((x) => {
                        var y = (TgChatModel)x;
                        Task.Run(async () => {
                            await Bot.SendTextMessageAsync(trustedGroups[0], string.Format("[McMsg][{0}]: {1}", y.DisplayName, y.Text));
                        }).Wait();
                    }));

                EventHub.Register("Chat.Public.BroadcastToTelegram.Group",
                    new Action<object>((x) => {
                        var y = (TgChatModel)x;
                        Task.Run(async () => {
                            await Bot.SendTextMessageAsync(trustedGroups[0], string.Format("[McMsg] {0}", y.Text));
                        }).Wait();
                    }));

                EventHub.Register("Chat.Public.SendToTelegram.Admin",
                    new Action<object>((x) => {
                        var y = (TgChatModel)x;
                        Task.Run(async () => {
                            await Bot.SendTextMessageAsync(trustedGroups[0], string.Format("[McMsg][{0}]: {1}", y.DisplayName, y.Text));
                        }).Wait();
                    }));

                Bot.StartReceiving();
            }
            catch (Exception e)
            {
                Logger.LogErr(string.Format("TelegramBot Error: {0}", e.ToString()));
            }
            
        }

        public void Stop()
        {
            if (Bot == null)
            {
                return;
            }
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

            Logger.Log(string.Format("TG Message From {0}: {1}", message.Chat.Id, message.Text));

            if (!trustedGroups.Contains(message.Chat.Id) && !trustedAdmins.Contains(message.Chat.Id))
            {
                return;
            }

            if (message.Text.StartsWith("/")) // send custom keyboard
            {
                ;
                // go into process commands.

                await Bot.SendTextMessageAsync(message.Chat.Id, string.Format("Hello {0}!", message.Chat.Id));
            }
            else
            {
                if (trustedGroups.Contains(message.Chat.Id))
                {
                    EventHub.Emit("Chat.Public.FromTelegram", new TgChatModel
                    {
                        DisplayName = string.Format("{0} {1}", message.From.FirstName, message.From.LastName),
                        Text = message.Text
                    });
                    // go into process group messages.
                }
            }
        }

        private async void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            await Bot.AnswerCallbackQueryAsync(callbackQueryEventArgs.CallbackQuery.Id,
                $"Received {callbackQueryEventArgs.CallbackQuery.Data}");
        }
    }
}

