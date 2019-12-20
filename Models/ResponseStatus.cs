using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace employeeservice.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseStatus<T>
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "responseStatus")]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]        
        public T Response { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Success")]
        public bool Success { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
