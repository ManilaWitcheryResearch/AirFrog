namespace Manila.AirFrog.WebService.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Nancy;
    using Newtonsoft.Json;
    using Manila.AirFrog.WebService.Models;
    using Manila.AirFrog.Common.Models;
    public class ChatModule : BaseApiModule
    {
        private ChatModel requestChatModel = null;
        private Response BeforeChatApiRequest(NancyContext ctx)
        {
            try
            {
                requestChatModel = JsonConvert.DeserializeObject<ChatModel>(RequestJson);
                AirFrog.LoggerMan.Log(JsonConvert.SerializeObject(requestChatModel));
            }
            catch (Exception e)
            {
                AirFrog.LoggerMan.LogErr(e.ToString());
                return Response.AsJson(badRequestResponse, Nancy.HttpStatusCode.BadRequest);
            }
            return null;
        }
        private void AfterChatApiResponse(NancyContext ctx)
        {
            ;
        }
        private Response OnChatApiRequestError(NancyContext ctx, Exception ex)
        {
            AirFrog.LoggerMan.LogErr(ex.ToString());
            return Response.AsJson(internalErrorResponse, Nancy.HttpStatusCode.InternalServerError);
        }
        public ChatModule()
        {
            Before += BeforeChatApiRequest;
            After += AfterChatApiResponse;
            OnError += OnChatApiRequestError;

            Post["/api/mcs/chatmsg"] = parameters =>
            {
                AirFrog.EventHub.Emit("Chat.Public.SendToTelegram.Group", new TgChatModel {
                    DisplayName = requestChatModel.PlayerName,
                    Text = requestChatModel.Text
                });

                return Response.AsJson(successResponse);
            };

            Post["/api/mcs/archievemsg"] = parameters =>
            {
                AirFrog.EventHub.Emit("Chat.Public.BroadcastToTelegram.Group", new TgChatModel
                {
                    Text = string.Format("Player {0} just achieved {1}, congrats!", requestChatModel.PlayerName, requestChatModel.Archieve),
                });

                return Response.AsJson(successResponse);
            };

            Post["/api/mcs/deathmsg"] = parameters =>
            {
                AirFrog.EventHub.Emit("Chat.Public.BroadcastToTelegram.Group", new TgChatModel
                {
                    Text = string.Format("Player {0} just died because {1}!", requestChatModel.PlayerName, requestChatModel.Action),
                });

                return Response.AsJson(successResponse);
            };

            Post["/api/mcs/loginmsg"] = parameters =>
            {
                AirFrog.EventHub.Emit("Chat.Public.BroadcastToTelegram.Group", new TgChatModel
                {
                    Text = string.Format("Player {0} just {1}!", requestChatModel.PlayerName, requestChatModel.Action),
                });

                return Response.AsJson(successResponse);
            };
        }
    }
}