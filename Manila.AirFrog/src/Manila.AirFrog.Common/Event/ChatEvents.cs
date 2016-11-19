namespace Manila.AirFrog.Common.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Manila.AirFrog.Common.Core;
    using Manila.AirFrog.Common.Models;

    public class ChatEvents
    {
        public static void Register(IEventHub eventHub, DataAccess dataAccess)
        {
            eventHub.Register("Chat.Public.FromTelegram",
                    new Action<object>((x) => {
                        var y = (TgChatModel)x;
                        dataAccess.SendChatMsgToMcs(y);
                    }));
        }
    }
}
