namespace Manila.AirFrog.Common.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Runtime.Serialization;

    [DataContract]
    public class McsMetaModel
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
        [DataMember(Name = "endpoint")]
        public string Endpoint { get; set; }

        public void ApplyUpdate(McsMetaModel modifier, McsMetaModelMask mask)
        {
            if (mask.Name)
            {
                if (modifier.Name == null) throw new Exception("mask validation failed.");
                this.Name = modifier.Name;
            }
            if (mask.Address)
            {
                if (modifier.Address == null) throw new Exception("mask validation failed.");
                this.Address = modifier.Address;
            }
            if (mask.McVersion)
            {
                if (modifier.McVersion == null) throw new Exception("mask validation failed.");
                this.McVersion = modifier.McVersion;
            }
            if (mask.ModVersion)
            {
                if (modifier.ModVersion == null) throw new Exception("mask validation failed.");
                this.ModVersion = modifier.ModVersion;
            }
            if (mask.ModPort)
            {
                if (modifier.ModPort == 0) throw new Exception("mask validation failed.");
                this.ModPort = modifier.ModPort;
            }

            this.Endpoint = string.Format("http://{0}:{1}/", this.Address, this.ModPort);

            if (mask.Endpoint)
            {
                if (modifier.Endpoint == null) throw new Exception("mask validation failed.");
                this.Endpoint = modifier.Endpoint;
            }
        }
    }

    [DataContract]
    public class McsMonitoringModel
    {
        [DataMember(Name = "serverid")]
        public string ServerId { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "lastseen")]
        public DateTime LastSeen { get; set; }

        public void ApplyUpdate(McsMonitoringModel modifier, McsMonitoringModelMask mask)
        {
            if (mask.Status)
            {
                if (modifier.Status == null) throw new Exception("mask validation failed.");
                this.Status = modifier.Status;
            }
            if (mask.LastSeen)
            {
                if (modifier.LastSeen == null) throw new Exception("mask validation failed.");
                this.LastSeen = modifier.LastSeen;
            }
        }
    }

    [DataContract]
    public class McsMetaModelMask
    {
        [DataMember(Name = "serverid")]
        public bool ServerId { get; set; }
        [DataMember(Name = "name")]
        public bool Name { get; set; }
        [DataMember(Name = "address")]
        public bool Address { get; set; }
        [DataMember(Name = "mc_version")]
        public bool McVersion { get; set; }
        [DataMember(Name = "mod_version")]
        public bool ModVersion { get; set; }
        [DataMember(Name = "mod_port")]
        public bool ModPort { get; set; }
        [DataMember(Name = "endpoint")]
        public bool Endpoint { get; set; }
    }

    [DataContract]
    public class McsMonitoringModelMask
    {
        [DataMember(Name = "serverid")]
        public bool ServerId { get; set; }
        [DataMember(Name = "status")]
        public bool Status { get; set; }
        [DataMember(Name = "lastseen")]
        public bool LastSeen { get; set; }
    }

    [DataContract]
    public class McsResponseWithTextModel
    {
        [DataMember(Name = "result")]
        public string Result { get; set; }
        [DataMember(Name = "text")]
        public string Text { get; set; }
        [DataMember(Name = "errormsg")]
        public string ErrorMsg { get; set; }
    }
}
