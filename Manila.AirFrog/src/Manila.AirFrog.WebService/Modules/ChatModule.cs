namespace Manila.AirFrog.WebService.Modules
{
    using Nancy;
    using Newtonsoft.Json;
    using Manila.AirFrog.WebService.Models;
    public class ChatModule : NancyModule
    {
        private object successResponse = new { result = "success", errormsg = "" };
        public ChatModule()
        {
            Post["/api/mcs/chatmsg"] = parameters =>
            {
                var id = this.Request.Body;
                var length = this.Request.Body.Length;
                var data = new byte[length];
                id.Read(data, 0, (int)length);
                var body = System.Text.Encoding.Default.GetString(data);

                AirFrog.LoggerMan.Log(body);

                var request = JsonConvert.DeserializeObject<ChatModel>(body);

                AirFrog.LoggerMan.Log(JsonConvert.SerializeObject(request));

                return Response.AsJson(successResponse);
            };
        }
    }
}