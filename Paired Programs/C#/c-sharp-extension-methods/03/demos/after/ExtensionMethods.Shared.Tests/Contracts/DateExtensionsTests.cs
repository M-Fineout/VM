using ExtensionMethods.Contracts;
using NUnit.Framework;
using System;

namespace ExtensionMethods.Tests.Contracts
{
    public class DateExtensionsTests
    {
        [Test]
        public void ToUnixSeconds()
        {
            var dateTime = new DateTime(2019, 09, 03, 12, 03, 02);
            var timestamp = dateTime.ToUnixSeconds();
            Assert.AreEqual(1567508582L, timestamp);
        }
    }
}