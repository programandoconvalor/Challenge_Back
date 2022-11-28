using System.Runtime.Serialization;

namespace tekchoice.Core.DTO

{
    /// <summary>
    /// Response: return response of data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Response_DTO<T>
    {
        [DataMember(Name = "Success")]
        public bool Success { get; set; }

        [DataMember(Name = "Message")]
        public string Message { get; set; }

        [DataMember(Name = "Data")]
        public dynamic Data { get; set; }
    }
}
