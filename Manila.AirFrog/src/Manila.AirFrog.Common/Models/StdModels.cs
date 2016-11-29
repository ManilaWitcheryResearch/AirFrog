namespace Manila.AirFrog.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.Serialization;

    [DataContract]
    public class StdResponseWithTextModel
    {
        [DataMember(Name = "result")]
        public string Result { get; set; }
        [DataMember(Name = "text")]
        public string Text { get; set; }
        [DataMember(Name = "errormsg")]
        public string ErrorMsg { get; set; }
    }

    [DataContract]
    public class StdChatModel
    {
        [DataMember(Name = "source")]
        public string Source;
        [DataMember(Name = "displayname")]
        public string DisplayName;
        [DataMember(Name = "text")]
        public string Text;
    }
}
