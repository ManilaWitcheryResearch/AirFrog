namespace Manila.AirFrog.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.Serialization;

    [DataContract]
    public class TgChatModel
    {
        [DataMember(Name = "displayname")]
        public string DisplayName;
        [DataMember(Name = "text")]
        public string Text;
    }
}
