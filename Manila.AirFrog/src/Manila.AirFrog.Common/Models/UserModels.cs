namespace Manila.AirFrog.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.Serialization;

    public enum BasicUserModelRoleType
    {
        PLAYER,
        OP,
        WEBADMIN,
    }

    [DataContract]
    public class BasicUserModel
    {
        [DataMember(Name = "guid")]
        public string Guid;

        [DataMember(Name = "mcid")]
        public string McId;

        [DataMember(Name = "displayname")]
        public string DisplayName;

        [DataMember(Name = "email")]
        public string Email;

        [DataMember(Name = "tgid")]
        public string TgId;

        [DataMember(Name = "password")]
        public string Password;

        [DataMember(Name = "head")]
        public string Head;

        [DataMember(Name = "description")]
        public string Description;

        [DataMember(Name = "role")]
        public BasicUserModelRoleType Role;
    }
}
