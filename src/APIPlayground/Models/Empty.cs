using System;
using System.Runtime.Serialization;

namespace APIPlayground.Models
{
    [DataContract]
    [Serializable]
    public class Empty
    {
        [DataMember(Name = "Value")]
        public string Value { get; set; }
    }
}
