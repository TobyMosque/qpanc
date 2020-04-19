using System;
using System.Runtime.Serialization;

namespace QPANC.Services.Abstract
{
    [DataContract]
    public class LoginResponse
    {
        [DataMember]
        public Guid SessionId { get; set; }
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public DateTimeOffset ExpiresAt { get; set; }
    }
}
