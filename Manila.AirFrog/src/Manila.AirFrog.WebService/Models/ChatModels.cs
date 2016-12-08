namespace Manila.AirFrog.WebService.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.Serialization;

    [DataContract]
    class WebChatModel
    {
        [DataMember(Name = "serverid")]
        public string ServerId { get; set; }
        [DataMember(Name = "playername")]
        public string PlayerName { get; set; }
        [DataMember(Name = "text")]
        public string Text { get; set; }
        [DataMember(Name = "archieve")]
        public string Archieve { get; set; }
        [DataMember(Name = "action")]
        public string Action { get; set; }
    }
}
