using System;
using System.Runtime.Serialization;

namespace APIPlayground.Models
{
    [DataContract]
    [Serializable]
    public class BusinessResponse
    {
        [DataMember(Name = "Value")]
        public string Value { get; set; }
    }
}
