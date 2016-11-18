namespace Minila.AirFrog.WebService.Modules
{
    using Nancy;
    public class ChatModule : NancyModule
    {
        public ChatModule()
        {
            Get["/api/mcchatmsg"] = parameters =>
            {
                return ".";
            };
        }
    }
}