namespace Manila.AirFrog.WebService.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.Serialization;

    [DataContract]
    class MonitoringModel
    {
        [DataMember(Name = "serverid")]
        public string ServerId { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "address")]
        public string Address { get; set; }
        [DataMember(Name = "mc_version")]
        public string McVersion { get; set; }
        [DataMember(Name = "mod_version")]
        public string ModVersion { get; set; }
        [DataMember(Name = "mod_port")]
        public int ModPort { get; set; }
        [DataMember(Name = "reason")]
        public string Reason { get; set; }
    }
}
