using NUnit.Framework;
using System;
using Moq;

namespace Org.Messaging.Tests
{
    public class LoginFailedEventTests
    {
        IMessageQueue _queue;

        [SetUp]
        public void Setup()
        {
            _queue = new Mock<IMessageQueue>().Object;
        }

        [Test]
        public void LoginFailed()
        {
            var loginFailed = new LoginFailedEvent
            {
                UserName = "sixeyed",
                FailedAt = DateTime.Now
            };
            var message = loginFailed.Wrap();
            _queue.Publish(message);
        }
    }
}