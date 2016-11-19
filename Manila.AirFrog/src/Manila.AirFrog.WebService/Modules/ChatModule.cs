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
                return Response.AsJson(successResponse);
            };
            Post["/api/mcs/archievemsg"] = parameters =>
            {
                return Response.AsJson(successResponse);
            };
            Post["/api/mcs/loginmsg"] = parameters =>
            {
                return Response.AsJson(successResponse);
            };
        }
    }
}