using System;

namespace Org.Messaging.Tests
{
    public class LoginFailedEvent : IMessage
    {
        public string UserName { get; set; }

        public DateTime FailedAt { get; set; }
    }
}
